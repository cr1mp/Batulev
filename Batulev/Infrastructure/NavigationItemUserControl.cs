using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Regions;

namespace Infrastructure
{
	public abstract class NavigationItemUserControl:UserControl
	{
		protected readonly IRegionManager _regionManager;

		public NavigationItemUserControl(IRegionManager regionManager,string regionName)
		{
			_regionManager = regionManager;

			IRegion mainContentRegion = _regionManager.Regions[regionName];
			if (mainContentRegion != null && mainContentRegion.NavigationService != null)
			{
				mainContentRegion.NavigationService.Navigated += this.MainContentRegion_Navigated;
			}
		}

		protected virtual void MainContentRegion_Navigated(object sender, RegionNavigationEventArgs e)
		{
			UpdateNavigationButtonState(e.Uri);
		}

		protected abstract void UpdateNavigationButtonState(Uri uri);
	}
}
