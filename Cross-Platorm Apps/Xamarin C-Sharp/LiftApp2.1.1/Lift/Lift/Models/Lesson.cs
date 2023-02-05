using System;
using Xamarin.Forms;

namespace Lift.Models
{
    public class Lesson
    {
        public string Id { get; set; }
        public string VideoLink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // create a new command property
        public Command RestartVideoCommand { get; set; }
        public Command PlayCommand { get; set; }
        public Command PauseCommand { get; set; }
    }

}

