using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job4 : BaseJob
	{
		private string _a;
		private string _b;

		public Job4(MathKernel mathKernel)
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

		public string Result => Compute($"Solve[{_a} * x + y == 7 && {_b} * x - y == 1, {{x,y}}]");
	}
}