﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using IntelligentSystems.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace IntelligentSystems
{
	public class ISModule : IModule
	{
		private readonly IUnityContainer _unityContainer;
		private readonly IRegionManager _regionManager;

		public ISModule(IUnityContainer unityContainer, IRegionManager regionManager)
		{
			_unityContainer = unityContainer;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_unityContainer.RegisterType<ISNavigationItemView>();

			_unityContainer.RegisterType<ISMainView>();
			_unityContainer.RegisterTypeForNavigation<ISMainView>();

			_regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ISNavigationItemView));
		}
	}
}
