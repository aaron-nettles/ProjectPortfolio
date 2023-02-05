using Xamarin.Essentials;
using Xamarin.Forms;

namespace EcommerceTemplate.ViewModels
{
    public class StringViewModel : BaseViewModel
    {
        public string Name { get; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);
                OnPropertyChanged(nameof(BackgroundColor));
                OnPropertyChanged(nameof(TextColor));
            }
        }

        public Color BackgroundColor
        {
            get {
                string keyCardColor = "CardColorLight";
                string keyPrimaryColor = "PrimaryColorLight";

                if (AppInfo.RequestedTheme == AppTheme.Dark)
                {
                    keyCardColor = "CardColorDark";
                    keyPrimaryColor = "PrimaryColorDark";
                }

                return IsSelected ? (Color)Application.Current.Resources[keyPrimaryColor]
                                     : (Color)Application.Current.Resources[keyCardColor];
            }
        }

        public Color TextColor
        {
            get
            {
                string keyPrimaryTextColor = "PrimaryTextColorLight";
                string keyTextColorOnPrimary = "TextColorOnPrimaryLight";

                if (AppInfo.RequestedTheme == AppTheme.Dark)
                {
                    keyPrimaryTextColor = "PrimaryTextColorDark";
                    keyTextColorOnPrimary = "TextColorOnPrimaryDark";
                }

                return IsSelected ? (Color)Application.Current.Resources[keyTextColorOnPrimary]
                                     : (Color)Application.Current.Resources[keyPrimaryTextColor];
            }
        }

        public StringViewModel(string name)
        {
            Name = name;
        }
    }
}
