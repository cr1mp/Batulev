using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Lab1ViewModel : BindableBase
	{
		public Lab1ViewModel( MathKernel mathKernel)
		{
			Job1 = new Job1(mathKernel);
			Job2 = new Job2(mathKernel);
			Job3 = new Job3(mathKernel);
			Job4 = new Job4(mathKernel);
			Job5 = new Job5(mathKernel);
			Job6 = new Job6(mathKernel);
			Job7 = new Job7(mathKernel);
			Job8 = new Job8(mathKernel);
			Job9 = new Job9(mathKernel);
			Job10 = new Job10(mathKernel);
			Job11 = new Job11(mathKernel);
		}

		public Job1 Job1 { get; }
		public Job2 Job2 { get; }
		public Job3 Job3 { get; }
		public Job4 Job4 { get; }
		public Job5 Job5 { get; }
		public Job6 Job6 { get; }
		public Job7 Job7 { get; }
		public Job8 Job8 { get; }
		public Job9 Job9 { get; }
		public Job10 Job10 { get; }
		public Job11 Job11 { get; }
	}
}
