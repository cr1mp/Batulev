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
    public class MathematicalMethodsAnalysisComplexSystemsModule:IModule
	{
		private readonly IUnityContainer _unityContainer;
		private readonly IRegionManager _regionManager;

		public MathematicalMethodsAnalysisComplexSystemsModule(IUnityContainer unityContainer,IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_unityContainer.RegisterType<MathematicalMethodsAnalysisComplexSystemsNavigationItemView>();

			_unityContainer.RegisterType<MathematicalMethodsAnalysisComplexSystemsMainView>();
			_unityContainer.RegisterTypeForNavigation<MathematicalMethodsAnalysisComplexSystemsMainView>();

			_regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(MathematicalMethodsAnalysisComplexSystemsNavigationItemView));
		}
	}
}
