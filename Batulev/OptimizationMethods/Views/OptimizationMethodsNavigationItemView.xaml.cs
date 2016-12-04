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
	[ViewSortHint("03")]
	public partial class OptimizationMethodsNavigationItemView : NavigationItemUserControl
	{
		private static readonly Uri OptimizationMethodsMainViewUri = new Uri("/OptimizationMethodsMainView", UriKind.Relative);

		

		public OptimizationMethodsNavigationItemView(IRegionManager regionManager)
			:base(regionManager, RegionNames.MainContentRegion)
		{
		}

		protected override void InitializeView()
		{
			InitializeComponent();
		}

		protected override void UpdateNavigationButtonState(Uri uri)
		{
			NavigateToOptimizationMethodsMainRadioButton.IsChecked = (uri == OptimizationMethodsMainViewUri);
		}

		private void NavigateToOptimizationMethodsMainRadioButton_OnClick(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.MainContentRegion, OptimizationMethodsMainViewUri);
		}
	}
}
