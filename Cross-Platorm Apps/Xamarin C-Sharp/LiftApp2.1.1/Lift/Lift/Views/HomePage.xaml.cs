using System;
using System.Collections.Generic;
using Firebase.Database;
using Xamarin.Forms;
using Lift.Models;

namespace Lift.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            var firebase = new FirebaseClient("https://lift-3795b-default-rtdb.firebaseio.com/");
            var items = firebase.Child("videos").OnceAsync<Item>().Result;
            var lessonList = new List<Item>();
            foreach (var item in items)
            {
                lessonList.Add(item.Object);
            }
            LessonListView.ItemsSource = lessonList;


        }
        private void OnLessonTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (Item)e.Item;
            var lesson = new Lesson
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                VideoLink = item.Link
            };
            Navigation.PushAsync(new LessonPage(lesson));
        }

    }
}

