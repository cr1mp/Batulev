using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using MathematicalMethodsAnalysisComplexSystems.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace MathematicalMethodsAnalysisComplexSystems
{
    public class MMACSModule:IModule
	{
		private readonly IUnityContainer _unityContainer;
		private readonly IRegionManager _regionManager;

		public MMACSModule(IUnityContainer unityContainer,IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_unityContainer.RegisterType<MMACSNavigationItemView>();

			_unityContainer.RegisterType<MMACSMainView>();
			_unityContainer.RegisterTypeForNavigation<MMACSMainView>();

			_regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(MMACSNavigationItemView));
		}
	}
}
