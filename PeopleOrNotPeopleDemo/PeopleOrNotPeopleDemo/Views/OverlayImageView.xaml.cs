﻿using PeopleOrNotPeopleDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeopleOrNotPeopleDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverlayImageView : ContentPage
    {
        public OverlayImageView(OverlayImageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        public void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            displayLabel.Text = String.Format($"The Slider value is {value}");
        }
    }
}