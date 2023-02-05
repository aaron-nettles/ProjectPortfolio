using System;
using Xamarin.Forms;
using EcommerceTemplate.Models;
using EcommerceTemplate.Services;
using EcommerceTemplate.Resources;

namespace EcommerceTemplate.ViewModels
{
    [QueryProperty(nameof(AddressId), nameof(AddressId))]
    public class AddressDetailViewModel : BaseViewModel
    {
        IService service => DependencyService.Get<IService>();

        public Command DeleteCommand { get; }
        public Command OkCommand { get; }
        public Command CancelCommand { get; }

        private string addressId;
        public string AddressId
        {
            get => addressId;
            set
            {
                addressId = value;
                LoadAddress(value);
            }
        }

        private string addressTitle;
        public string AddressTitle
        {
            get => addressTitle;
            set => SetProperty(ref addressTitle, value);
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private string company;
        public string Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }

        private string address1;
        public string Address1
        {
            get => address1;
            set => SetProperty(ref address1, value);
        }

        private string address2;
        public string Address2
        {
            get => address2;
            set => SetProperty(ref address2, value);
        }

        private string city;
        public string City
        {
            get => city;
            set => SetProperty(ref city, value);
        }

        private string state;
        public string State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        private string postCode;
        public string PostCode
        {
            get => postCode;
            set => SetProperty(ref postCode, value);
        }

        private string country;
        public string Country
        {
            get => country;
            set => SetProperty(ref country, value);
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        public AddressDetailViewModel()
        {
            DeleteCommand = new Command(OnDeleteTapped);
            OkCommand = new Command(OnOkTapped);
            CancelCommand = new Command(OnCancelTapped);
        }

        private async void LoadAddress(string id)
        {
            var item = await service.GetAddressAsync(id);
            AddressTitle = item.Title;
            FirstName = item.FirstName;
            LastName = item.LastName;
            Company = item.Company;
            Address1 = item.Address1;
            Address2 = item.Address2;
            City = item.City;
            State = item.State;
            PostCode = item.PostCode;
            Country = item.Country;
            Phone = item.Phone;
        }

        private async void OnDeleteTapped()
        {
            var result = await Shell.Current.DisplayAlert(AppResources.Question,
                            AppResources.DoYouWantDeleteAddress, AppResources.Yes, AppResources.No);

            if (result == true)
            {
                await service.DeleteAddressAsync(addressId);
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void OnCancelTapped()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnOkTapped()
        {
            var address = new Address
            {
                Id = addressId != null ? addressId : Guid.NewGuid().ToString(),
                CustomerId = Globals.LoggedCustomerId,
                Title = addressTitle,
                FirstName = firstName,
                LastName = lastName,
                Company = company,
                Address1 = address1,
                Address2 = address2,
                City = city,
                State = state,
                PostCode = postCode,
                Country = country,
                Phone = phone
            };

            if (addressId != null)
                await service.UpdateAddressAsync(address);
            else
                await service.AddAddressAsync(address);

            await Shell.Current.GoToAsync("..");
        }
    }
}
