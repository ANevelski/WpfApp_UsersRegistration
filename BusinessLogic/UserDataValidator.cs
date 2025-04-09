using System.Text.RegularExpressions;


namespace WpfApp_UsersRegistration.BusinessLogic
{
    internal static class UserDataValidator
    {
        private const string passwordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[a-zA-Z])[A-Za-z\d]{5,}$";
        private const string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public static bool IsLoginValid(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
               return false;
            }
            if (login.Length < 4)
            {
                return false;
            }
            return true;
        }

        public static bool IsPasswordValid(string password)
        {
            return Regex.IsMatch(password, passwordPattern);
        }

        public static bool ArePasswordsmatched(string password, string password2)
        {          
            if (password.Equals(password2)) 
                return true;            
            return false;
        }

        public static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool AreAllUserDataCorrect(string login, string password)
        {
            return IsLoginValid(login) && IsPasswordValid(password);
        }

        public static bool AreAllUserDataCorrect(string login, string password, string password2, string email)
        {
            return IsLoginValid(login) && IsPasswordValid(password) && IsPasswordValid(password2) && IsEmailValid(email);
        }
    }
}
