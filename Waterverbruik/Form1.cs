using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transportbedrijf;

namespace Waterverbruik
{
    public partial class Form1 : Form
    {
        private const string amountOfWaterString = "amount of water in liters";
        private const string calculateCostString = "calculate cost";

        private const int widthMargin = 10;
        private const int heightMargin = 10;
        private const int rowHeight = 30;

        private TextBoxWithLabelFormElement amountOfWater;
        private RadioButtonsFormElement payOption;
        private ButtonFormElement calculateCost;

        public Form1()
        {
            InitializeComponent();
            InitializeElements();
            ResetPosition();
        }
        private void InitializeElements()
        {
            amountOfWater = new TextBoxWithLabelFormElement(this, amountOfWaterString);
            payOption = new RadioButtonsFormElement(this, WaterUseCost.GetPayOptions(), rowHeight);
            calculateCost = new ButtonFormElement(this, calculateCostString, calculateCostButtonFunction);
        }
        private void ResetPosition()
        {
            int row = 0;
            amountOfWater.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            payOption.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row += payOption.GetAmountOfRows();
            calculateCost.ChangePosition(widthMargin, heightMargin + row * rowHeight);
        }

        private void PresentPrice()
        {
            int amountOfWater = this.amountOfWater.GetValueAsInt();
            string payOption = this.payOption.GetValue();
            string presentString = WaterUseCost.GetPriceString(amountOfWater, payOption);
            MessageBox.Show(presentString);
        }

        private void calculateCostButtonFunction(object sender, EventArgs e)
        {
            PresentPrice();
        }
    }
}
