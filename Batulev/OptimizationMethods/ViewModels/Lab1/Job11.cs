using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job11 : Job7 {
		public Job11(MathKernel mathKernel) 
			: base(mathKernel)
		{
		}

		protected override string GetGraphicFunc()
		{
			return "y=Sqrt[5*x^2-x]";
		}

		protected override string GetEquation(double x)
		{
			return $"Sqrt[5*{x}^2-{x}]";
		}
	}
}