﻿using System;
using Xamarin.Forms;

namespace EcommerceTemplate.CustomViews
{
    public partial class CartLineItemView : ContentView
    {
        public CartLineItemView()
        {
            InitializeComponent();
        }

        //For animation triggered on quantity change.
        async void QuantityChanged(Object sender, EventArgs e)
        {
            await pvQuantity.ScaleTo(1.25, 150);
            await pvQuantity.ScaleTo(1.0, 150);
        }
    }
}
