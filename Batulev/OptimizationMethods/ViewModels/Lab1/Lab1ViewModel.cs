using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using OptimizationMethods.ViewModels.Lab1;
using Prism.Commands;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels
{
	public class Lab1ViewModel : BindableBase
	{


		public Lab1ViewModel(IKernelLink kernelLink, MathKernel mathKernel)
		{
			Job1 = new Job1(kernelLink,mathKernel);
			Job2 = new Job2(kernelLink,mathKernel);
			Job3 = new Job3(kernelLink,mathKernel);
			Job4 = new Job4(kernelLink,mathKernel);
		}

		public Job1 Job1 { get; }
		public Job2 Job2 { get; }
		public Job3 Job3 { get; }
		public Job4 Job4 { get; }
	}
}
