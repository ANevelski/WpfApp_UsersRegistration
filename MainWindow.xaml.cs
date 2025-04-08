using System.Windows;
using System.Windows.Media;


namespace WpfApp_UsersRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string lorin =textBoxLogin.Text.Trim();
            string password = passwordBox.Password.Trim();
            string password2 = passwordBox2.Password.Trim();
            string email = textBoxEmail.Text.Trim().ToLower();

            if(lorin.Length<5)
            {
                textBoxLogin.ToolTip = "Incorrect login.";
                textBoxLogin.Background = Brushes.OrangeRed;
            } else if(password.Length<5)
            {
                passwordBox.ToolTip = "Incorrect password.";
                passwordBox.Background = Brushes.OrangeRed;
            }
            else if (password != password2)
            {
                passwordBox2.ToolTip = "Passwords are not the same.";
                passwordBox2.Background = Brushes.OrangeRed;
            } else if (!email.Contains("@") || !email.Contains("."))
            {
                textBoxEmail.ToolTip = "Incorrect email format.";
                textBoxEmail.Background = Brushes.OrangeRed;
            }
            else
            {
                textBoxLogin.ToolTip = string.Empty;
                textBoxLogin.Background = Brushes.Transparent;

                passwordBox.ToolTip = string.Empty;
                passwordBox.Background = Brushes.Transparent;

                passwordBox2.ToolTip = string.Empty;
                passwordBox2.Background = Brushes.Transparent;

                textBoxEmail.ToolTip = string.Empty;
                textBoxEmail.Background = Brushes.Transparent;

                MessageBox.Show("Data ia correct!");
            }

        }
    }
}
