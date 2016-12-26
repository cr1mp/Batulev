using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job3:Job2
	{
		private string _graphicFunc;

		public Job3(IKernelLink kernelLink, MathKernel mathKernel) 
			: base(kernelLink, mathKernel)
		{
		}

		public string GraphicFunc
		{
			get { return _graphicFunc; }
			set
			{
				_graphicFunc = value;
				OnPropertyChanged(nameof(Img));
			}
		}

		protected override string GetFunc()
		{
			return $"Plot[{GraphicFunc}, {{x, {_xMin}, {_xMax}}}, PlotRange -> Full]";
		}
	}
}
