using System;
using System.Windows.Forms;
using Transportbedrijf;

namespace Waterverbruik
{
    internal class ButtonFormElement : FormElement
    {
        private Button button;

        public ButtonFormElement(Form form, string buttonText, Action<object, EventArgs> buttonFunction) : base(form)
        {
            button = new Button();
            button.Text = buttonText;
            button.Click += new EventHandler(buttonFunction);
            form.Controls.Add(button);
        }
        protected override void AlterPosition(int widthOfset, int heightOfset)
        {
            button.Location = new System.Drawing.Point(widthOfset, heightOfset);
        }
    }
}