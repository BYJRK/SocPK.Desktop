using Autofac;
using RestSharp;
using SocPK.Desktop.Helpers;
using SocPK.Desktop.ViewModels;
using SocPK.Desktop.Views;
using System.Windows;

namespace SocPK.Desktop
{
    public partial class App : Application
    {
        public static new App Current => new App();

        public IContainer Container;

        public App()
        {
            Container = BuildContainer();

            MainWindow = Container.Resolve<MainWindow>();
            MainWindow.Show();
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterInstance(new RestClient()).As<RestClient>().SingleInstance();
            builder.RegisterType<WebHelper>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
