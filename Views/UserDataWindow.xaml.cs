using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp_UsersRegistration.DAL;
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
            listOfUsers.ItemsSource = DALService<User>.GetAll(); 
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
           if (sender is Button button)
            {
                // Получаем ID пользователя из Tag
                if (button.Tag is int userId)
                {
                    // Вызываем метод удаления
                    DALService<User>.DeleteFromStorage(userId);                    
                    listOfUsers.ItemsSource = DALService<User>.GetAll(); ;
                }
            }

        }
    }
}
