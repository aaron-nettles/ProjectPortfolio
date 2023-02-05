using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Joins;
using Octane.Xamarin.Forms.VideoPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Lift.Models;

using static Lift.Views.LessonPage;

namespace Lift.Views
{
    public partial class LessonPage : ContentPage
    {

        public VideoPlayer VideoPlayer { get; set; }


       


        public LessonPage(Models.Lesson lesson)
        {
            InitializeComponent();
            VideoPlayer = new VideoPlayer();
            lesson.PlayCommand = new Command(Play);
            lesson.PauseCommand = new Command(Pause);
            lesson.RestartVideoCommand = new Command(RestartVideo);


            TitleLabel = this.FindByName<Label>("TitleLabel");
            DescriptionLabel = this.FindByName<Label>("DescriptionLabel");


            BindingContext = lesson;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var lesson = (Lesson)BindingContext;
            VideoPlayer.Source = lesson.VideoLink;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            mediaPlayer.Stop();

        }

        private void Play()
        {
            mediaPlayer.Play();
        }

        private void Pause()
        {
            mediaPlayer.Pause();
        }

        public void RestartVideo()
        {
            // stop the current video
            mediaPlayer.Stop();

            // get the lesson from the binding context
            var lesson = (Lesson)BindingContext;

            // set the video source
            VideoPlayer.Source = lesson.VideoLink;

            // start playing the video
            VideoPlayer.Play();
        }



    }

}

