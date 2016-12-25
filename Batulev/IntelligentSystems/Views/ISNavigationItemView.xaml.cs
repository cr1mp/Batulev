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

namespace IntelligentSystems.Views
{
	/// <summary>
	/// Interaction logic for ISNavigationItemView.xaml
	/// </summary>
	[ViewSortHint("01")]
	public partial class ISNavigationItemView : NavigationItemUserControl
	{
		private static Uri ISMainViewUri = new Uri("/ISMainView", UriKind.Relative);

		public ISNavigationItemView(IRegionManager regionManager) 
			: base(regionManager, RegionNames.MainContentRegion)
		{
			InitializeComponent();
		}

		protected override void UpdateNavigationButtonState(Uri uri)
		{
			NavigateToISMainRadioButton.IsChecked = (uri == ISMainViewUri);
		}


		private void NavigateToISMainViewRadioButton_OnClick(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.MainContentRegion, ISMainViewUri);
		}
	}
}
