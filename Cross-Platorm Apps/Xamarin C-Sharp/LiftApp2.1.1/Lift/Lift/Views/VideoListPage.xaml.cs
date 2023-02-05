using System;
using System.Collections.Generic;
using Lift.ViewModels;
using Xamarin.Forms;

namespace Lift.Views
{	
	public partial class VideoListPage : ContentPage
	{
        private VideoListViewModel _viewModel;
        public VideoListPage ()
		{
			InitializeComponent ();
            _viewModel = new VideoListViewModel();
            BindingContext = _viewModel;
        }
	}
}

