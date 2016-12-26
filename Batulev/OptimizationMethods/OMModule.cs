using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Practices.Unity;
using OptimizationMethods.Views;
using OptimizationMethods.Views.Labs;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Wolfram.NETLink;

namespace OptimizationMethods
{
	public class OMModule : IModule
	{
		private readonly IUnityContainer _unityContainer;
		private readonly IRegionManager _regionManager;

		public OMModule(IUnityContainer unityContainer,IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			var kernel = MathLinkFactory.CreateKernelLink();
			_unityContainer.RegisterInstance<IKernelLink>(kernel);

			var mathKernel = new MathKernel(kernel);
			mathKernel.CaptureGraphics = true;
			_unityContainer.RegisterInstance(mathKernel);

			_unityContainer.RegisterType<OptimizationMethodsNavigationItemView>();

			_unityContainer.RegisterType<OptimizationMethodsMainView>();
			_unityContainer.RegisterTypeForNavigation<OptimizationMethodsMainView>();

			_unityContainer.RegisterType<Lab1View>();
			_unityContainer.RegisterTypeForNavigation<Lab1View>();

			_unityContainer.RegisterType<Lab2View>();
			_unityContainer.RegisterTypeForNavigation<Lab2View>();

			_regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion,typeof(OptimizationMethodsNavigationItemView));
		}
	}
}
