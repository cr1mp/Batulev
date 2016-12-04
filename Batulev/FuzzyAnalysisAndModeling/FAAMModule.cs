using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyAnalysisAndModeling.Views;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace FuzzyAnalysisAndModeling
{
    public class FAAMModule:IModule
	{
		private readonly IUnityContainer _unityContainer;
		private readonly IRegionManager _regionManager;

		public FAAMModule(IUnityContainer unityContainer, IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_unityContainer.RegisterType<FAAMNavigationItemView>();

			_unityContainer.RegisterType<FAAMMainView>();
			_unityContainer.RegisterTypeForNavigation<FAAMMainView>();

			_regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(FAAMNavigationItemView));
		}
	}
}
