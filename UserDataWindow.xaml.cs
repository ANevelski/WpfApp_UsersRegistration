using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace WpfApp_UsersRegistration
{
    /// <summary>
    /// Interaction logic for UserDataWindow.xaml
    /// </summary>
    public partial class UserDataWindow : Window
    {
        public UserDataWindow()
        {
            InitializeComponent();

            AppContext db = new AppContext();
            List<User> users = db.Users.ToList();

            listOfUsers.ItemsSource = users;
        }
    }
}
