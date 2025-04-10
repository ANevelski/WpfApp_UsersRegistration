using System.Windows;
using Unity;
using WpfApp_UsersRegistration.DAL;

namespace WpfApp_UsersRegistration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUnityContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Container = new UnityContainer();
            Container.RegisterType(typeof(IStorageProvider<>), typeof(EntityProvider<>));
            Container.RegisterType(typeof(DALService<>), typeof(DALService<>));
            Container.RegisterType<MainWindow>();

            var mainWindow = Container.Resolve<MainWindow>();          
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Освобождение ресурсов контейнера
            Container.Dispose();
            base.OnExit(e);
        }
    }
}
