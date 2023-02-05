using System;
using System.Collections.Generic;
using Firebase.Storage;
using Lift.ViewModels;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Firebase.Database;
using Firebase.Database.Query;
using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using System.Runtime.CompilerServices;
using Octane.Xamarin.Forms.VideoPlayer;
using Lift.Models;
using static Lift.Views.LessonPage;

namespace Lift.Views
{
    public partial class AddItemPage : ContentPage, INotifyPropertyChanged
    {
        //view lesson after save button models
        private bool _isLessonSaved = false;
        public bool IsLessonSaved
        {
            get { return _isLessonSaved; }
            set
            {
                _isLessonSaved = value;
                OnPropertyChanged();
            }
        }


        public AddItemPage()
        {
            InitializeComponent();
            //add to database
            Button2.Clicked += OnClick_Button2;
            BindingContext = this;

        }
        //upload progess model
        private double _uploadProgress;
        public double UploadProgress
        {
            get { return _uploadProgress; }
            set
            {
                _uploadProgress = value;
                OnPropertyChanged();
            }
        }

        private string _uploadStatus;
        public string UploadStatus
        {
            get { return _uploadStatus; }
            set
            {
                _uploadStatus = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //select video 
        private async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var photo = await Xamarin.Essentials.MediaPicker.PickVideoAsync();

            if (photo == null)
                return;

            var task = new FirebaseStorage("lift-3795b.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child("lessons")
                .Child("videos")
                .Child(photo.FileName)
                .PutAsync(await photo.OpenReadAsync());

            task.Progress.ProgressChanged += (s, args) =>
            {
                UploadProgress = args.Percentage;
                if (UploadProgress != 100)
                    UploadStatus = $"Uploading... {UploadProgress}%";
            };
            //this code may be redundant
            await task;
            UploadStatus = "Upload Completed!";

            //wait for the task to finish so it can display the upload completed
            await task;

            var downloadlink = await task;
            //this code below displays the url link
            //downloadLink.Text = downloadlink;

            UploadStatus = "Upload Complete!";

            //the _ symbol is for assigning the anonymous method to the Clicked event of the ViewLessonButton without the event args parameter.
            //usually the symbol would be an 'e'
            ViewLessonButton.Clicked += (s, _) => ViewLesson_Clicked(downloadlink, TitleEntry.Text, DescriptionEditor.Text);


        }

        // ...

        private async Task AddItemToDatabase(string id, string title, string description, string link)
        {
            var firebase = new FirebaseClient("https://lift-3795b-default-rtdb.firebaseio.com/");

            var item = new Item
            {
                Id = id,
                Title = title,
                Description = description,
                Link = link
                
                
            };

            await firebase
                .Child("videos")
                .Child(id)
                .PutAsync(item);
        }

        public class Item
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Link { get; set; }
        }

        //onbuttonclicked, add to database
        private async void OnClick_Button2(object sender, EventArgs e)
        {
            string id = Guid.NewGuid().ToString();
            string title = TitleEntry.Text;
            string description = DescriptionEditor.Text;
            string link = downloadLink.Text;

           await AddItemToDatabase(id, title, description, link);

            //view lesson button visable
            IsLessonSaved = true;
        }
        //view lesson button clicked
        private void ViewLesson_Clicked(string downloadlink, string title, string description)
        {
            var lesson = new Lesson { VideoLink = downloadlink, Title = title, Description = description };
            var lessonPage = new LessonPage(lesson);
            Navigation.PushAsync(lessonPage);
        }


    }
}
