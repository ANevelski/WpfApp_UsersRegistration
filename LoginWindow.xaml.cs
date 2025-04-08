using System.Linq;
using System.Windows;
using System.Windows.Media;


namespace WpfApp_UsersRegistration
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = passwordBox.Password.Trim();
         
            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Incorrect login.";
                textBoxLogin.Background = Brushes.OrangeRed;
            }
            else if (password.Length < 5)
            {
                passwordBox.ToolTip = "Incorrect password.";
                passwordBox.Background = Brushes.OrangeRed;
            } else
            {
                textBoxLogin.ToolTip = string.Empty;
                textBoxLogin.Background = Brushes.Transparent;

                passwordBox.ToolTip = string.Empty;
                passwordBox.Background = Brushes.Transparent;

                User loginUser = null;
                using (AppContext db = new AppContext())
                {
                    loginUser = db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
                }

                if (loginUser != null)
                {
                    MessageBox.Show("User was found!");
                    UserDataWindow userDataWindow = new UserDataWindow();
                    userDataWindow.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("User was not found.");
                }
                
            }
        }

        private void Button_RegWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
    }
}
