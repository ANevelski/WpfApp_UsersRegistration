using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace WpfApp_UsersRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext appContext;

        public MainWindow()
        {
            InitializeComponent();

            appContext = new AppContext();

            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 450;
            btnAnimation.Duration = TimeSpan.FromSeconds(3);
            regBtn.BeginAnimation(Button.WidthProperty, btnAnimation);
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login =textBoxLogin.Text.Trim();
            string password = passwordBox.Password.Trim();
            string password2 = passwordBox2.Password.Trim();
            string email = textBoxEmail.Text.Trim().ToLower();

            if(login.Length<5)
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

               
                User user = new User(login, email, password);

                appContext.Users.Add(user);
                appContext.SaveChanges();

                MessageBox.Show("Data is correct!");
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
    }
}
