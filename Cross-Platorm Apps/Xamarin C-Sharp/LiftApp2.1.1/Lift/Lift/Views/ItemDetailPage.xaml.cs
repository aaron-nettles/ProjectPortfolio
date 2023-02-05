using System.ComponentModel;
using Xamarin.Forms;
using Lift.ViewModels;

namespace Lift.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
