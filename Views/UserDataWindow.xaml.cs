using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp_UsersRegistration.DAL.DBconnect_AppContext;
using WpfApp_UsersRegistration.DAL.UserModel;


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

            App_Context db = new App_Context();
            List<User> users = db.Users.ToList();

            listOfUsers.ItemsSource = users;
        }
    }
}
