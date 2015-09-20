using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Calculator
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
        }

        void buttonClick(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            if (tmp == null)
                return;

            Display.Text += tmp.Text;
        }

        void RemoveLastSymbol(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Display.Text))
                return;

            Display.Text = Display.Text.Remove(Display.Text.Length - 1, 1);
        }

        void ClearAll(object sender, EventArgs e)
        {
            Display.Text = "";
        }
    }
}
