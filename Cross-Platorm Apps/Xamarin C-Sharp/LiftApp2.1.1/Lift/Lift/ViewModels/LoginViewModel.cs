using Android.Content.Res;
using Lift.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Lift.ViewModels
{


class LoginViewModel : BaseViewModel
    {



        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await OnLoginClick());
            // setting the default async username and password
            Username = "username";
            Password = "password";
        }

        private async Task OnLoginClick()
        {
            IsBusy = true;

            try
            {

                if (Username == "Admin" && Password == "Password")
                {
                    await Shell.Current.GoToAsync($"//{nameof(AddItemPage)}");
                }
                else
                {
                    
                    await App.Current.MainPage.DisplayAlert("Alert", "Incorrect Username or Password", "OK");
                }
                // Add code to authenticate the user and log them in
                // ...

                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }


}

