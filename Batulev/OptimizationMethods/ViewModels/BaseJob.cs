using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels
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

		protected string[] GetUnknownVariables(string out1)
		{
			List<string> result = new List<string>();
			var arr = out1.Split(' ', '+', '-', '*', '/', '(', ')', '^', '=','<','>');
			foreach (var item in arr)
			{
				if (!string.IsNullOrWhiteSpace(item))
				{
					double tmp;
					if (!double.TryParse(item, out tmp))
						result.Add(item);
				}
			}
			return result.Distinct().ToArray();
		}
	}
}
