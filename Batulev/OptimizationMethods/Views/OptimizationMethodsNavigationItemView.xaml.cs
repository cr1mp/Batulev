using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infrastructure;
using Prism.Regions;

namespace OptimizationMethods.Views
{
	/// <summary>
	/// Interaction logic for OptimizationMethodsNavigationItemView.xaml
	/// </summary>
	public partial class OptimizationMethodsNavigationItemView : UserControl
	{
		private static readonly Uri OptimizationMethodsMainViewUri = new Uri("/OptimizationMethodsMainView", UriKind.Relative);

		private readonly IRegionManager _regionManager;

		public OptimizationMethodsNavigationItemView(IRegionManager regionManager)
		{
			_regionManager = regionManager;

			InitializeComponent();
		}

		private void RadioButton_Click(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.MainContentRegion, OptimizationMethodsMainViewUri);
		}
	}
}
