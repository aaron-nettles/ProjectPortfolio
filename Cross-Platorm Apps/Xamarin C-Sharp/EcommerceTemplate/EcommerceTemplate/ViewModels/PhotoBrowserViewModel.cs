using Xamarin.Forms;

namespace EcommerceTemplate.ViewModels
{
    [QueryProperty(nameof(ParamImages), nameof(ParamImages))]
    [QueryProperty(nameof(Image), nameof(Image))]
    public class PhotoBrowserViewModel : BaseViewModel
    {
        public Command CloseCommand { get; }
        public Command<string> ItemCommand { get; }

        private string paramImages;
        public string ParamImages
        {
            get => paramImages;
            set
            {
                paramImages = value;
                Images = paramImages.Split(',');
            }
        }

        private string[] images;
        public string[] Images
        {
            get => images;
            set
            {
                SetProperty(ref images, value);
            }
        }

        private string image;
        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public PhotoBrowserViewModel()
        {
            CloseCommand = new Command(OnCloseTapped);
            ItemCommand = new Command<string>(OnItemTapped);
        }

        async void OnCloseTapped()
        {
            await Shell.Current.GoToAsync("..");
        }

        void OnItemTapped(string item)
        {
            if (item == null) return;

            Image = item;
        }
    }
}
