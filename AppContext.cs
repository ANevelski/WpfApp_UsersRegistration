using System.Data.Entity;

namespace WpfApp_UsersRegistration
{
    internal class AppContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext(): base("DefaultConnection") { }
    }
}
