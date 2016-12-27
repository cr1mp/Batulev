using System;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job2 : ImageBaseJob
	{

		private string _x1;

		public Job2( MathKernel mathKernel)
			:base(mathKernel)
		{
		}

		public string X1
		{
			get { return _x1; }
			set
			{
				_x1 = value;
				OnPropertyChanged(nameof(Result));
			}
		}

		public string Result => Compute($"Erf[{_x1}]");

		protected override string GetGraphicFunc()
		{
			return "Erf[x]";
		}

	}
}