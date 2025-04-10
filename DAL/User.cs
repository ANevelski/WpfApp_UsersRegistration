
namespace WpfApp_UsersRegistration.DAL.UserModel
{
    /// <summary>
    /// User data model class
    /// </summary>
    public class User
    {
        private int _id;
        private string _login, _email, _password;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public User() { }

        public User(int id, string login, string email, string password)
        {
            _id = id;
            _login = login;
            _email = email;
            _password = password;
        }

        public User(string login, string email, string password)
        {
            _login = login;
            _email = email;
            _password = password;
        }
    }
}
