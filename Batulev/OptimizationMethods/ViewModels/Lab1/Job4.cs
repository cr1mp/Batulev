using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job4: BindableBase
	{
		private readonly IKernelLink _kernelLink;
		private readonly MathKernel _mathKernel;
		private string _a;
		private string _b;

		public Job4(IKernelLink kernelLink, MathKernel mathKernel)
		{
			_kernelLink = kernelLink;
			_mathKernel = mathKernel;
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

		public string Result
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(_a) && !string.IsNullOrWhiteSpace(_b))
				{
					if (_mathKernel.IsComputing)
					{
						_mathKernel.Abort();
					}
					else
					{
						_mathKernel.Compute($"Solve[{_a} x + y == 7 && {_b} x - y == 1, {{x,y}}]");

						return (string)_mathKernel.Result;
					}
				}
				return string.Empty;
			}
		}
	}
}
