using Microsoft.Practices.Unity;
using Prism.Unity;
using Shell.Views;
using System.Windows;
using Prism.Modularity;

namespace Shell
{
	class Bootstrapper : UnityBootstrapper
	{
		protected override IModuleCatalog CreateModuleCatalog()
		{
			return new ConfigurationModuleCatalog();
		}

		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();
			Application.Current.MainWindow = (Window)this.Shell;
			Application.Current.MainWindow.Show();
		}
	}
}
