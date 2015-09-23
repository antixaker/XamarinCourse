﻿using System;
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

        void OnResetClicked(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            if (tmp == null)
                return;
            Display.Text = displayManager.ClearAll();
        }

        void OnResultClicked(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            if (tmp == null)
                return;
            var res = mathProcessor.Calculate(Display.Text);
            if (res != null)
                Display.Text = res.ToString();
        }

        void OnRemoveLastSymbolClicked(object sender, EventArgs e)
        {
            Button tmp = sender as Button;
            if (tmp == null)
                return;
            Display.Text = displayManager.RemoveLastSymbol(Display.Text);
        }
    }

    class CaclProcessor : ICaclProcessor
    {
        public double ? Calculate(string command)
        {
            if (string.IsNullOrEmpty(command))
                return null;
            char[] operators = new[]{'+', '-', '/', '*'};
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