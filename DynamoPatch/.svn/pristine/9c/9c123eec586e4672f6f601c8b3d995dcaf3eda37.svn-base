using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Dynamo.Boekingssysteem.Controls
{
    public class IntegerTextBox : TextBox
    {
        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
            base.OnPreviewTextInput(e);
        }

        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex(@"^[0-9]?$"); //regex that matches allowed text
            return regex.IsMatch(text);
        }
    }
}
