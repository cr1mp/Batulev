using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Practices.Unity;
using OptimizationMethods.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace OptimizationMethods
{
	//[Module(ModuleName = nameof(OptimizationMethodsModule), OnDemand = true)]
	public class OptimizationMethodsModule : IModule
	{
		private readonly IUnityContainer _unityContainer;
		private readonly IRegionManager _regionManager;

		public OptimizationMethodsModule(IUnityContainer unityContainer,IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_unityContainer.RegisterType<OptimizationMethodsNavigationItemView>();

			_unityContainer.RegisterType<OptimizationMethodsMainView>();
			_unityContainer.RegisterTypeForNavigation<OptimizationMethodsMainView>();

			_regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion,typeof(OptimizationMethodsNavigationItemView));
		}
	}
}
