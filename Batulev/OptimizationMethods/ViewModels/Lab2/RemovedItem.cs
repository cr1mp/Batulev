using System;
using Prism.Commands;
using Prism.Mvvm;

namespace OptimizationMethods.ViewModels.Lab2
{
	public class RemovedItem : BindableBase
	{
		public RemovedItem(Action removeThis)
		{
			Remove = new DelegateCommand(removeThis);
		}

		public DelegateCommand Remove { get; set; }
	}
}