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
    }
}
