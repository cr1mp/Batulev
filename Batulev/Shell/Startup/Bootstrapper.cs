using Microsoft.Practices.Unity;
using Prism.Unity;
using Shell.Views;
using System.Windows;

namespace Shell
{
	class Bootstrapper : UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void InitializeShell()
		{
			Application.Current.MainWindow.Show();
		}
	}
}
