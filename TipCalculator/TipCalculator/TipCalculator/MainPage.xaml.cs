﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TipCalculator
{
    public partial class MainPage : ContentPage
    {
        int tipPercentValue = 17;

        public MainPage()
        {
            InitializeComponent();
            CalcButton.Clicked += CalcButton_Clicked;
            PercentValue.Text = string.Format("$ {0}", tipPercentValue);
        }

        private void CalcButton_Clicked(object sender, EventArgs e)
        {
            string enteredAmount = AmountEntry.Text;
            if (string.IsNullOrEmpty(enteredAmount))
                this.DisplayAlert("Empty value error", "Input value must be filled", "Back");

            double amount = 0;
            if (double.TryParse(enteredAmount, out amount))
            {

            }
            else
                this.DisplayAlert("Number value error", "Number value has wrong format", "Back");
        }
    }
}
