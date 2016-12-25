using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels
{
	public class Lab1ViewModel : BindableBase
	{
		private readonly IKernelLink _kernelLink;
		private readonly MathKernel _mathKernel;
		private string _job1;

		public Lab1ViewModel(IKernelLink kernelLink, MathKernel mathKernel)
		{
			_kernelLink = kernelLink;
			_mathKernel = mathKernel;
		}

		public string Job1
		{
			get { return _job1; }
			set
			{
				_job1 = value;
				OnPropertyChanged(nameof(Result1));
			}
		}

		public string Result1
		{
			get
			{
				if (!string.IsNullOrWhiteSpace( _job1 ))
				{
					if (_mathKernel.IsComputing)
					{
						_mathKernel.Abort();
					}
					else
					{
						_mathKernel.Compute($"Simplify[{_job1}]");

						return (string)_mathKernel.Result;
					}
				}
				return string.Empty;
			}
		}
	}
}
