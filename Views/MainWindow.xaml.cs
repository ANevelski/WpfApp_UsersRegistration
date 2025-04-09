using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfApp_UsersRegistration.BusinessLogic;
using WpfApp_UsersRegistration.DAL.DBconnect_AppContext;
using WpfApp_UsersRegistration.DAL.UserModel;
using WpfApp_UsersRegistration.Views;


namespace WpfApp_UsersRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        App_Context appContext;

        public MainWindow()
        {
            InitializeComponent();

            appContext = new App_Context();

            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 450;
            btnAnimation.Duration = TimeSpan.FromSeconds(3);
            regBtn.BeginAnimation(Button.WidthProperty, btnAnimation);
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = passwordBox.Password.Trim();
            string password2 = passwordBox.Password.Trim();
            string email = textBoxEmail.Text.Trim();

            if (UserDataValidator.AreAllUserDataCorrect(login, password, password2, email))
            {
                User user = new User(login, email, password);

                appContext.Users.Add(user);
                appContext.SaveChanges();
                

                MessageBox.Show("Congratulations. You are registed!");

                UserDataWindow userDataWindow = new UserDataWindow();
                userDataWindow.Show();
                Hide();
            }
        }

        private void Button_LoginWindow_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
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
        
        private void passwordBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = passwordBox.Password;
            string password2 = passwordBox2.Password;
            if (!UserDataValidator.IsPasswordValid(password2) || !UserDataValidator.ArePasswordsmatched(password, password2)) 
                Helper.ClearField(passwordBox2, "The passwords are not the same.");
            else
                Helper.RemoveToolTip(sender);

        }

        private void textBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = textBoxEmail.Text;
            if (!UserDataValidator.IsEmailValid(email))
                Helper.ClearField(textBoxEmail, "Incorrect email format.");
            else
                Helper.RemoveToolTip(sender);
        }        
    }
}


