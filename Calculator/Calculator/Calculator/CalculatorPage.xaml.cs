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

        IInputManager displayManager;
        ICaclProcessor mathProcessor;

        public CalculatorPage()
        {
            InitializeComponent();

            displayManager = new InputManager();
            mathProcessor = new CaclProcessor();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            if (tmp == null)
                return;

            Display.Text = displayManager.AddSymbol(Display.Text, tmp.Text);
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
                case "Reset":
                    resultString = displayManager.ClearAll();
                    break;
                case "Remove":
                    resultString = displayManager.RemoveLastSymbol(Display.Text);
                    break;
                case "Result":
                    var res = mathProcessor.Calculate(Display.Text);
                    resultString = res?.ToString();
                    break;
                default:
                    break;
            }

            if (resultString != null)
                Display.Text = resultString;
        }

    }

    class InputManager : IInputManager
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

            //check point enter
            if (symbolToAdd == "." && !CanAddPoint(valueToChange, lastSymbol))
                return valueToChange;

            //check re-enter mathoperator
            else if (!Char.IsDigit(symbolToAdd[0]) && !CanAddMath(valueToChange, lastSymbol))
                return ReplaceLastSymbol(valueToChange, symbolToAdd);

            return valueToChange += symbolToAdd;
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

        string ReplaceLastSymbol(string valueToChange, string symbolToReplace)
        {
            return valueToChange.Substring(0, valueToChange.Length - 1) + symbolToReplace;
        }

        bool CommandIsFull(string valueToTest)
        {
            if (valueToTest.Split(actionSymbols, StringSplitOptions.RemoveEmptyEntries).Length == 2)
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

    class CaclProcessor : ICaclProcessor
    {
        public double? Calculate(string command)
        {
            if (string.IsNullOrEmpty(command))
                return null;

            char[] operators = new[] { '+', '-', '/', '*' };
            double firstNumber = 0;
            double secondNumber = 0;
            char mathOperator = ' ';
            bool isNegative = false;


            //if first number is negative after previous operation
            if (command[0] == '-')
            {
                command = command.Substring(1, command.Length - 1);
                isNegative = true;
            }


            string[] array = command.Split(operators, StringSplitOptions.RemoveEmptyEntries);

            //if some errors is return null
            if (array.Length != 2 || !double.TryParse(array[0], out firstNumber) || !double.TryParse(array[1], out secondNumber))
                return null;

            //get mathoperator symbol from command
            mathOperator = command[command.IndexOfAny(operators)];

            if (isNegative)
                firstNumber *= -1;

            return DoMathOperation(firstNumber, secondNumber, mathOperator);
        }

        double DoMathOperation(double var1, double var2, char mathOperator)
        {
            double result = double.NaN;
            switch (mathOperator)
            {
                case '+':
                    result = var1 + var2;
                    break;
                case '-':
                    result = var1 - var2;
                    break;
                case '*':
                    result = var1 * var2;
                    break;
                case '/':
                    result = var1 / var2;
                    break;
            }
            return result;
        }

    }

}
