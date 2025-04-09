using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WpfApp_UsersRegistration.Views
{
    public static class Helper
    {
        public static void ClearField(TextBox textBox, string toolTip)
        {
            textBox.ToolTip = toolTip;
            textBox.Background = Brushes.OrangeRed;
        }

        public static void ClearField(PasswordBox passwordBox, string toolTip)
        {
            passwordBox.ToolTip = toolTip;
            passwordBox.Background = Brushes.OrangeRed;
        }

        public static void RemoveToolTip(object sender)
        {
            if (sender is TextBox)
            {
                var field = (TextBox)sender;
                field.ToolTip = string.Empty;
                field.Background = Brushes.Transparent;
            }
            else if (sender is PasswordBox)
            {
                var field = (PasswordBox)sender;
                field.ToolTip = string.Empty;
                field.Background = Brushes.Transparent;
            }
            else
            {
                MessageBox.Show("A field should be TextBox or PasswordBox.");
            }
        }
    }
}
