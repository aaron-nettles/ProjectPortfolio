using EcommerceTemplate.Models;

namespace EcommerceTemplate.ViewModels
{
    public class AddressViewModel : BaseViewModel
    {
        public Address Address { get; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);
            }
        }

        public AddressViewModel(Address address)
        {
            Address = address;
        }
    }
}
