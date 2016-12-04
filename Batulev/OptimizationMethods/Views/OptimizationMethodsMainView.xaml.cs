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
	/// Interaction logic for OptimizationMethodsMainView.xaml
	/// </summary>
	public partial class OptimizationMethodsMainView : NavigationItemUserControl
	{
		private static Uri Lab1ViewUri = new Uri("/Labs/Lab1View", UriKind.Relative);
		private static Uri Lab2ViewUri = new Uri("/Labs/Lab2View", UriKind.Relative);

		public OptimizationMethodsMainView(IRegionManager regionManager)
			:base(regionManager, OptimizationMethodsRegionNames.LaboratoryWorkContentRegion)
		{
		}

		protected override void InitializeView()
		{
			InitializeComponent();
		}

		protected override void UpdateNavigationButtonState(Uri uri)
		{
			Lab1.IsChecked=(uri== Lab1ViewUri);
			Lab2.IsChecked=(uri== Lab2ViewUri);
		}

		private void Lab1_OnClick(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(OptimizationMethodsRegionNames.LaboratoryWorkContentRegion, Lab1ViewUri);
		}

		private void Lab2_OnClick(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(OptimizationMethodsRegionNames.LaboratoryWorkContentRegion, Lab2ViewUri);

		}


		
	}
}
