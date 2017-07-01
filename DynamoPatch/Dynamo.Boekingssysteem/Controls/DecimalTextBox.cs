using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dynamo.Boekingssysteem.Controls
{
    public class DecimalTextBox : TextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);

            base.OnPreviewTextInput(e);
        }

        private bool AreAllValidNumericChars(string str)
        {
            bool ret = true;
            if (str == NumberFormatInfo.CurrentInfo.NegativeSign |
                str == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator |
                str == NumberFormatInfo.CurrentInfo.PositiveSign)
                return ret;

            int l = str.Length;
            for (int i = 0;
                i < l;
                i++)
            {
                char ch = str[i];
                ret &= Char.IsDigit(ch);
            }

            return ret;
        }

        //{
        //protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        //    var c = CultureInfo.GetCultureInfo("NL-nl");

        //    e.Handled = e.Text.Contains(c.NumberFormat.CurrencyGroupSeparator);
        //    e.Handled = e.Handled || (!IsTextAllowed(e.Text) && e.Text == c.NumberFormat.CurrencyDecimalSeparator);
        //    //e.Handled = e.Handled || !IsTextAllowed(Text) && Text.Length;
        //    e.Handled = e.Handled || (Text.Contains(c.NumberFormat.CurrencyDecimalSeparator) && e.Text.Contains(c.NumberFormat.CurrencyDecimalSeparator));
        //    base.OnPreviewTextInput(e);
        //}

        //private bool IsTextAllowed(string text)
        //{
        //    Regex regex = new Regex(@"^[0-9]+(\" + CultureInfo.GetCultureInfo("NL-nl").NumberFormat.CurrencyDecimalSeparator + "[0-9]{1,2})?$"); //regex that matches allowed text
        //    return regex.IsMatch(text);
        //}
    }
}