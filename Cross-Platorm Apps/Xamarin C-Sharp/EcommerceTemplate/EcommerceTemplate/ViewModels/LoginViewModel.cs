using EcommerceTemplate.Views;
using Xamarin.Forms;

namespace EcommerceTemplate.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool rememberMe;
        public bool RememberMe
        {
            get => rememberMe;
            set => SetProperty(ref rememberMe, value);
        }

        public Command LoginCommand { get; }
        public Command SignUpCommand { get; }
        public Command RememberMeCommand { get; }
        public Command ResetPasswordCommand { get; }

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () =>
                await Shell.Current.GoToAsync(".."));

            SignUpCommand = new Command(async () =>
                await Shell.Current.GoToAsync($"{nameof(SignUpPage)}"));

            RememberMeCommand = new Command(() => { RememberMe = !RememberMe; });

            ResetPasswordCommand = new Command(async () =>
                await Shell.Current.GoToAsync($"{nameof(ResetPasswordPage)}"));
        }

    }
}
