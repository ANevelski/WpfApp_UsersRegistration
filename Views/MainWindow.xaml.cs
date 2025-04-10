using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Unity;
using WpfApp_UsersRegistration.BusinessLogic;
using WpfApp_UsersRegistration.DAL;
using WpfApp_UsersRegistration.DAL.UserModel;
using WpfApp_UsersRegistration.Views;


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

            // Animation
            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 450;
            btnAnimation.Duration = TimeSpan.FromSeconds(3);
            regBtn.BeginAnimation(Button.WidthProperty, btnAnimation);
        }

        private async void Button_Reg_ClickAsync(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = passwordBox.Password.Trim();
            string password2 = passwordBox.Password.Trim();
            string email = textBoxEmail.Text.Trim();

            if (UserDataValidator.AreAllUserDataCorrect(login, password, password2, email))
            {
                User user = new User(login, email, password);
                                
                var dalService = App.Container.Resolve<DALService<User>>();

                if (await dalService.ExistsAsync(login, email: email))
                {
                    MessageBox.Show("User with this Login and Email are already registered. Please Modify your data.");
                }
                else
                {
                    await dalService.SaveToStorageAsync(user);

                    MessageBox.Show("Congratulations. You are registed!");

                    UserDataWindow userDataWindow = new UserDataWindow();
                    userDataWindow.Show();
                    Hide();
                }
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


