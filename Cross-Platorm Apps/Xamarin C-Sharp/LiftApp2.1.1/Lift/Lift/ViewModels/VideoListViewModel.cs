using System.ComponentModel;
using Firebase.Database;
using System.Linq;
using System.Collections.ObjectModel;

namespace Lift.ViewModels
{
    public class VideoListViewModel : INotifyPropertyChanged
    {
        private FirebaseClient _firebase;
        private ObservableCollection<Video> _videos;

        public event PropertyChangedEventHandler PropertyChanged;

        public VideoListViewModel()
        {
            _firebase = new FirebaseClient("https://lift-3795b.appspot.com/");
            _videos = new ObservableCollection<Video>();
            LoadVideos();
        }

        public ObservableCollection<Video> Videos
        {
            get { return _videos; }
            set
            {
                _videos = value;
                OnPropertyChanged("Videos");
            }
        }

        private async void LoadVideos()
        {
            var videos = await _firebase
                .Child("videos")
                .OnceAsync<Video>();

            videos.ToList().ForEach(video =>
            {
                Videos.Add(video.Object);
            });
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Video : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private string _videoUrl;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string VideoUrl
        {
            get { return _videoUrl; }
            set
            {
                _videoUrl = value;
                OnPropertyChanged("VideoUrl");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
