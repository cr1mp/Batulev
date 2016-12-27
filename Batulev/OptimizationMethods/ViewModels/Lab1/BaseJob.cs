using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public abstract class BaseJob : BindableBase
	{
		protected readonly MathKernel _mathKernel;

		public BaseJob(MathKernel mathKernel)
		{
			_mathKernel = mathKernel;
		}

		public string Compute(string input)
		{
			if (!string.IsNullOrWhiteSpace(input))
			{
				if (_mathKernel.IsComputing)
				{
					_mathKernel.Abort();
				}
				else
				{
					try
					{
						_mathKernel.Compute(input);
					}
					catch (Exception ex)
					{
						return string.Empty;
					}

					return (string)_mathKernel.Result;
				}
			}
			return string.Empty;
		}
	}
}
