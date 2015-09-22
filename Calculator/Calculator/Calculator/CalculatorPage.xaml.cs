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

        InputManager displayManager;

        public CalculatorPage()
        {
            InitializeComponent();

            displayManager = new InputManager();
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
                    resultString = displayManager.AddSymbol(Display.Text, tmp.Text);
                    break;
                case "Reset":
                    resultString = displayManager.ClearAll();
                    break;
                case "Remove":
                    resultString = displayManager.RemoveLastSymbol(Display.Text);
                    break;
                default:
                    break;
            }

            Display.Text = resultString;
        }

    }

    class InputManager
    {
        readonly char[] actionSymbols = { '+', '-', '/', '*' };

        public string AddSymbol(string valueToChange, string symbolToAdd)
        {
            //for first button press
            if (string.IsNullOrEmpty(valueToChange))
            {
                if (char.IsDigit(symbolToAdd[0]))
                    return symbolToAdd;
                else
                    return string.Empty;
            }

            //constraint for input only two numbers and action symbol
            if (!char.IsDigit(symbolToAdd[0]) && symbolToAdd != "." && CommandIsFull(valueToChange))
                return valueToChange;

            char lastSymbol = GetLastSymbol(valueToChange);

            if (symbolToAdd == "." && !CanAddPoint(valueToChange, lastSymbol))
                return valueToChange;
            else if (!Char.IsDigit(symbolToAdd[0]) && !CanAddMath(valueToChange, lastSymbol))
                return ReplaceLastSymbol(valueToChange, symbolToAdd);

            return valueToChange += symbolToAdd;
        }

        string ReplaceLastSymbol(string valueToChange, string symbolToReplace)
        {
            return valueToChange.Substring(0, valueToChange.Length - 1) + symbolToReplace;
        }

        public string RemoveLastSymbol(string value)
        {
            return string.IsNullOrEmpty(value) ? value : value.Remove(value.Length - 1, 1);
        }

        public string ClearAll()
        {
            return String.Empty;
        }

        #region Servise methods

        bool CommandIsFull(string valueToTest)
        {
            if (valueToTest.Split(actionSymbols,StringSplitOptions.RemoveEmptyEntries).Length == 2)
                return true;
            else
                return false;
        }

        bool CanAddPoint(string currentDisplayValue, char lastSymbol)
        {
            if (string.IsNullOrEmpty(lastSymbol.ToString()) || !Char.IsDigit(lastSymbol))
                return false;

            string[] arr = StringSplit(currentDisplayValue, actionSymbols);

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

        string[] StringSplit(string stringToSplit, char[] separatorArray)
        {
            return stringToSplit.Split(separatorArray);
        }

        #endregion

    }

}
