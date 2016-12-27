using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job6:BaseJob
	{
		private string _a;
		private string _b;

		public Job6(MathKernel mathKernel) 
			: base(mathKernel)
		{
		}

		public string A
		{
			get { return _a; }
			set
			{
				_a = value;
				OnPropertyChanged(nameof(Result));
			}
		}

		public string B
		{
			get { return _b; }
			set
			{
				_b = value;
				OnPropertyChanged(nameof(Result));
			}
		}

		public string Result => Compute($"Solve[ x^2+{_a}*x+{_b}==0 ,x]");
	}
}
