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
            currentDisplayValue += value;
        }

        public void RemoveLastSymbol(string value = null)
        {
            currentDisplayValue = currentDisplayValue.Remove(currentDisplayValue.Length - 1, 1);
        }

        public void ClearAll(string value = null)
        {
            currentDisplayValue = String.Empty;
        }
    }

}
