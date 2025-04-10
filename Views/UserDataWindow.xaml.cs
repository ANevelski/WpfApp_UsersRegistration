using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Unity;
using WpfApp_UsersRegistration.DAL;
using WpfApp_UsersRegistration.DAL.UserModel;


namespace WpfApp_UsersRegistration
{
    /// <summary>
    /// Interaction logic for UserDataWindow.xaml
    /// </summary>
    public partial class UserDataWindow : Window
    {
        private readonly DALService<User> _dalService;

        public UserDataWindow()
        {
            InitializeComponent();
            _dalService = App.Container.Resolve<DALService<User>>();

            this.Closed += UserDataWindow_Closed;
            var users = _dalService.GetAllAsync();
            listOfUsers.ItemsSource = users.Result;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {                
                if (button.Tag is int userId)
                {
                    await _dalService.DeleteFromStorageAsync(userId);

                    var users = listOfUsers.ItemsSource.Cast<User>().ToList();                    
                    var itemToRemove = users.FirstOrDefault(u => u.id == userId);

                    if (itemToRemove != null)
                    {
                        users.Remove(itemToRemove);
                        listOfUsers.ItemsSource = users;
                    }
                }
            }
        }

        private void UserDataWindow_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void SaveToFile_Button_Click(object sender, RoutedEventArgs e)
        {
            _dalService.SaveToFileAllUsers();
            MessageBox.Show("Data is saved to 'users.json' file");
        }
    }
}
