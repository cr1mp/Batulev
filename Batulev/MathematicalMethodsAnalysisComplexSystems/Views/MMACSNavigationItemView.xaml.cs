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

namespace MathematicalMethodsAnalysisComplexSystems.Views
{
	/// <summary>
	/// Interaction logic for MathematicalMethodsAnalysisComplexSystemsNavigationItemView.xaml
	/// </summary>
	[ViewSortHint("02")]
	public partial class MMACSNavigationItemView : NavigationItemUserControl
	{
		private static Uri MMACSMainViewUri = new Uri("/MMACSMainView", UriKind.Relative);

		public MMACSNavigationItemView(IRegionManager regionManager) 
			: base(regionManager, RegionNames.MainContentRegion)
		{
		}

		protected override void InitializeView()
		{
			InitializeComponent();
		}

		protected override void UpdateNavigationButtonState(Uri uri)
		{
			NavigateToMMACSMainRadioButton.IsChecked = (uri == MMACSMainViewUri);
		}

		private void NavigateToMMACSMainRadioButton_OnClick(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.MainContentRegion, MMACSMainViewUri);
		}
	}
}
