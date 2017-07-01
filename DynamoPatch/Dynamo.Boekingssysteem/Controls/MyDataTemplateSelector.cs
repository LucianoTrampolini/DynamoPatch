using System.Windows;
using System.Windows.Controls;

namespace Dynamo.Boekingssysteem.Controls
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(
            object item,
            DependencyObject container)
        {
            Window wnd = Application.Current.MainWindow;
            if (item is string)
                return wnd.FindResource("WaitTemplate") as DataTemplate;
            return wnd.FindResource("TheItemTemplate") as DataTemplate;
        }
    }
}