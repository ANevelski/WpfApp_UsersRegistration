using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_UsersRegistration
{
    internal class User
    {
        public int id { get; set; }
        private string login, email, password;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public User() { }

        public User(string login, string email, string password)
        {
            //this.id = id;
            this.login = login;
            this.email = email;
            this.password = password;
        }

        //public override string ToString()
        //{
        //    return string.Format("User login: {0}. Email: {1}", login, email);
        //}
    }
}
