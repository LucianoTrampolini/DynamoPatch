using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dynamo.Boekingssysteem.Controls
{
    public class IntegerTextBox : TextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
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