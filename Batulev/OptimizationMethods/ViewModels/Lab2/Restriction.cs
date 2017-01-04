using System;

namespace OptimizationMethods.ViewModels.Lab2
{
	public class Restriction : RemovedItem
	{
		private string _value;

		public string Value
		{
			get { return _value; }
			set { SetProperty(ref _value, value); }
		}

		public Restriction(Action removeThis)
			: base(removeThis)
		{
		}
	}
}