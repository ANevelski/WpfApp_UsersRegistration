using System.Collections.Generic;
using System.Windows;
using WpfApp_UsersRegistration.BusinessLogic;
using WpfApp_UsersRegistration.DAL;
using WpfApp_UsersRegistration.DAL.UserModel;
using WpfApp_UsersRegistration.Views;


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

            if (UserDataValidator.AreAllUserDataCorrect(login, password))
            {

                EntityProvider<User> provideDB = DALService<User>.GetStorageProvider();

                List<User> loginUser = new List<User>();

                loginUser = provideDB.Find(u => u.Login == login);

                if(loginUser.Count != 0)
                {
                    UserDataWindow userDataWindow = new UserDataWindow();
                    userDataWindow.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Any Users were not found.");
                }               
            }
        }

        private void Button_RegWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }       

        private void textBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            if (!UserDataValidator.IsLoginValid(login))
                Helper.ClearField(textBoxLogin, "Incorrect login. Should be at least 4 characters.");
            else
                Helper.RemoveToolTip(sender);
        }
        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = passwordBox.Password;
            if (!UserDataValidator.IsPasswordValid(password))
                Helper.ClearField(passwordBox, "Incorrect password. It should be at least 5 Latin characters of which one is a capital letter and has at least one number.");
            else
                Helper.RemoveToolTip(sender);
        }
    }
}
