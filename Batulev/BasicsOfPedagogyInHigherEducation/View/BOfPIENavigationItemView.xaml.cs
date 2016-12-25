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

namespace BasicsOfPedagogyInHigherEducation.View
{
	/// <summary>
	/// Interaction logic for BOfPIENavigationItemView.xaml
	/// </summary>
	[ViewSortHint("05")]
	public partial class BOfPIENavigationItemView : NavigationItemUserControl
	{
		private static Uri BOfPIEMainViewUri = new Uri("/BOfPIEMainView", UriKind.Relative);

		public BOfPIENavigationItemView(IRegionManager regionManager)
			: base(regionManager, RegionNames.MainContentRegion)
		{
			InitializeComponent();
		}

		private void NavigateToBOfPIEMainViewRadioButton_OnClick(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.MainContentRegion, BOfPIEMainViewUri);
		}

		protected override void UpdateNavigationButtonState(Uri uri)
		{
			NavigateToBOfPIEMainRadioButton.IsChecked = (uri == BOfPIEMainViewUri);
		}

		
	}
}
