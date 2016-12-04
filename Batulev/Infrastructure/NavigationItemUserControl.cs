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

		protected NavigationItemUserControl(IRegionManager regionManager,string regionName)
		{
			_regionManager = regionManager;

			InitializeView();

			if (regionManager.Regions.ContainsRegionWithName(regionName))
			{
				IRegion contentRegion = _regionManager.Regions[regionName];
				if (contentRegion != null && contentRegion.NavigationService != null)
				{
					contentRegion.NavigationService.Navigated += this.MainContentRegion_Navigated;
				}
			}
		}

		protected abstract void InitializeView();

		protected virtual void MainContentRegion_Navigated(object sender, RegionNavigationEventArgs e)
		{
			UpdateNavigationButtonState(e.Uri);
		}

		protected abstract void UpdateNavigationButtonState(Uri uri);
	}
}
