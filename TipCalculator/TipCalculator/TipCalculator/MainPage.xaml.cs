using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TipCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CalcButton.Clicked += CalcButton_Clicked;
        }

        private void CalcButton_Clicked(object sender, EventArgs e)
        {
            string enteredAmount = AmountEntry.Text;
            if (string.IsNullOrEmpty(enteredAmount))
                this.DisplayAlert("Input error!", "Wrong input string format", "Cancel");
        }
    }
}
