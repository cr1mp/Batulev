using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job10 : Job5
	{
		public Job10(MathKernel mathKernel)
			: base(mathKernel)
		{
		}

		protected override string GetEquation(double a)
		{
			return $"Sqrt[{a}*x^2+11]=={a}";
		}
	}
}