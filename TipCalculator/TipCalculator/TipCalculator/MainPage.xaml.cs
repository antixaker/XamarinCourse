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
        int tipPercentValue = 17;
        bool wasChangedText; //for performance optimization

        public MainPage()
        {
            InitializeComponent();

            CalcButton.Clicked += CalcButton_Clicked;
            AmountEntry.TextChanged += AmountEntry_TextChanged;

            PercentValue.Text = string.Format("$ {0}", tipPercentValue);
        }

        private void AmountEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            wasChangedText = true;
        }

        private void CalcButton_Clicked(object sender, EventArgs e)
        {
            //cheking input string
            string enteredAmount = AmountEntry.Text;

            if (!wasChangedText)
                return;
            if (string.IsNullOrEmpty(enteredAmount))
            {
                ResetLabelsToDefault();
                wasChangedText = false;
                return;
            }

            //parsing and calculating
            double amount = 0;
            if (double.TryParse(enteredAmount, out amount))
            {
                double tipAmount = amount * tipPercentValue / 100;
                double totalAmount = amount + tipAmount;

                TipAmountValue.Text = string.Format("$ {0:0.00}", tipAmount);
                TotalAmountValue.Text = string.Format("$ {0:0.00}", totalAmount);
            }
            else
                this.DisplayAlert("Number value error", "Number value has wrong format", "Back");

            //performance optimization
            wasChangedText = false;
        }

        void ResetLabelsToDefault()
        {
            TipAmountValue.Text = "$ 0.0";
            TotalAmountValue.Text = "$ 0.0";
        }
    }
}
