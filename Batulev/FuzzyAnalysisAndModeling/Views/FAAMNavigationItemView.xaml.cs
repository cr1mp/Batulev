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

namespace FuzzyAnalysisAndModeling.Views
{
	/// <summary>
	/// Interaction logic for FAAMNavigationItemView.xaml
	/// </summary>
	[ViewSortHint("04")]
	public partial class FAAMNavigationItemView : NavigationItemUserControl
	{
		private static Uri FAAMMainViewUri = new Uri("/FAAMMainView", UriKind.Relative);

		public FAAMNavigationItemView(IRegionManager regionManager)
			: base(regionManager, RegionNames.MainContentRegion)
		{
			InitializeComponent();
		}

		private void NavigateToFAAMMainViewRadioButton_OnClick(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.MainContentRegion, FAAMMainViewUri);
		}

		protected override void UpdateNavigationButtonState(Uri uri)
		{
			NavigateToFAAMMainRadioButton.IsChecked = (uri == FAAMMainViewUri);
		}

		
	}
}
