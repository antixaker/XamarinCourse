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

        CalculatorManager manager;

        public CalculatorPage()
        {
            InitializeComponent();

            manager = new CalculatorManager();
        }

        void buttonClick(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            if (tmp == null)
                return;

            string buttonClassId = tmp.ClassId;

            string resultString = string.Empty;

            switch (buttonClassId)
            {
                case "Digit":
                case "Point":
                case "Math":
                    resultString = manager.AddSymbol(Display.Text, tmp.Text);
                    break;
                case "Reset":
                    resultString = manager.ClearAll();
                    break;
                default:
                    break;
            }

            Display.Text = resultString;
        }

    }

    class CalculatorManager
    {
        public string AddSymbol(string displayValue, string symbolToAdd)
        {
            char lastSymbol = GetLastSymbol(displayValue);

            if (symbolToAdd == "." && !CanAddPoint(displayValue, lastSymbol))
                return string.Empty;
            else if (!Char.IsDigit(symbolToAdd[0]) && !CanAddMath(displayValue, lastSymbol))
                return string.Empty;

            return displayValue += symbolToAdd;
        }

        public string RemoveLastSymbol(string value)
        {
            return value.Remove(value.Length - 1, 1);
        }

        public string ClearAll()
        {
            return String.Empty;
        }

        bool CanAddPoint(string currentDisplayValue, char lastSymbol)
        {
            if (string.IsNullOrEmpty(lastSymbol.ToString()) || !Char.IsDigit(lastSymbol))
                return false;

            string[] arr = currentDisplayValue.Split(new[] { '+', '-', '*', '/' });

            string last = arr[arr.Length - 1];
            if (last.Contains("."))
                return false;

            return true;
        }

        bool CanAddMath(string currentDisplayValue, char lastSymbol)
        {
            return (!string.IsNullOrEmpty(lastSymbol.ToString()) && Char.IsDigit(lastSymbol) && currentDisplayValue[currentDisplayValue.Length - 1] != '.');
        }

        char GetLastSymbol(string value)
        {
            return value[value.Length - 1];
        }

    }

}
