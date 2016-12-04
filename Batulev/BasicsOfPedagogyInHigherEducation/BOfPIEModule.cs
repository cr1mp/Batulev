using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicsOfPedagogyInHigherEducation.View;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace BasicsOfPedagogyInHigherEducation
{
    public class BOfPIEModule:IModule
	{
		private readonly IUnityContainer _unityContainer;
		private readonly IRegionManager _regionManager;

		public BOfPIEModule(IUnityContainer unityContainer, IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_unityContainer.RegisterType<BOfPIENavigationItemView>();

			_unityContainer.RegisterType<BOfPIEMainView>();
			_unityContainer.RegisterTypeForNavigation<BOfPIEMainView>();

			_regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(BOfPIENavigationItemView));
		}
	}
}
