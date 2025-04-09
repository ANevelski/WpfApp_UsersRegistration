using System.Data.Entity;
using WpfApp_UsersRegistration.DAL.UserModel;


namespace WpfApp_UsersRegistration.DAL.DBconnect_AppContext
{
    internal class App_Context : DbContext    {
           
        public DbSet<User> Users { get; set; }

        public App_Context(): base("DefaultConnection") { }
    }
}
