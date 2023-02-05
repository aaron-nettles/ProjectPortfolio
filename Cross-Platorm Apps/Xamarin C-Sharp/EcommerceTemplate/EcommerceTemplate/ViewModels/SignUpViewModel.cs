using EcommerceTemplate.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EcommerceTemplate.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public Command SignUpCommand { get; }
        public Command LoginCommand { get; }
        public Command TermsCommand { get; }

        private string fullName;
        public string FulltName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        public SignUpViewModel()
        {
            Title = AppResources.SignUp;

            SignUpCommand = new Command(async () => await Shell.Current.GoToAsync($"../.."));
            LoginCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            TermsCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

    }
}
