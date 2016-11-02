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
	public partial class MathematicalMethodsAnalysisComplexSystemsNavigationItemView : NavigationItemUserControl
	{
		private static Uri MathematicalMethodsAnalysisComplexSystemsMainViewUri = new Uri("/MathematicalMethodsAnalysisComplexSystemsMainView", UriKind.Relative);

		public MathematicalMethodsAnalysisComplexSystemsNavigationItemView(IRegionManager regionManager) 
			: base(regionManager, RegionNames.MainContentRegion)
		{
			InitializeComponent();
		}

		protected override void UpdateNavigationButtonState(Uri uri)
		{
			NavigateToMathematicalMethodsAnalysisComplexSystemsMainRadioButton.IsChecked = (uri == MathematicalMethodsAnalysisComplexSystemsMainViewUri);
		}

		private void NavigateToRadioButton_OnClick(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.MainContentRegion, MathematicalMethodsAnalysisComplexSystemsMainViewUri);
		}

		
	}
}
