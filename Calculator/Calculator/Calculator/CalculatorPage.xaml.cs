using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Calculator
{

    delegate void DisplayProcess(string value);

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

            switch (buttonClassId)
            {
                case "Digit":
                case "Point":
                case "Math":
                    ProcessingAction(manager.AddSymbol, tmp.Text);
                    break;
                default:
                    break;
            }
        }

        void ProcessingAction(DisplayProcess process, string value = null)
        {
            CreateDisplaySnapshot();
            process?.Invoke(value);
            SynchronizeDisplay();
        }

        #region Synchronization methods

        void CreateDisplaySnapshot()
        {
            manager?.CopyDisplayValue(Display.Text);
        }

        void SynchronizeDisplay()
        {
            Display.Text = manager?.CurrentDisplayValue;
        }

        #endregion

    }

    class CalculatorManager
    {
        string currentDisplayValue;
        string lastSymbol;

        public string CurrentDisplayValue
        {
            get { return currentDisplayValue; }
        }

        public void CopyDisplayValue(string value)
        {
            currentDisplayValue = value;
        }

        public void AddSymbol(string value)
        {
            if (value == "." && !CanAddPoint())
                return;
            else if (!Char.IsDigit(value[0]) && !CanAddMath())
                return;

            currentDisplayValue += value;

            lastSymbol = value;
        }

        public void RemoveLastSymbol(string value = null)
        {
            currentDisplayValue = currentDisplayValue.Remove(currentDisplayValue.Length - 1, 1);
        }

        public void ClearAll(string value = null)
        {
            currentDisplayValue = String.Empty;
        }

        bool CanAddPoint()
        {
            string[] arr = currentDisplayValue.Split(new[] { '+', '-', '*', '/' });

            string last = arr[arr.Length - 1];
            if (last.Contains("."))
                return false;

            return true;
        }

        bool CanAddMath()
        {
            return currentDisplayValue[currentDisplayValue.Length - 1] != '.';
        }

    }

}
