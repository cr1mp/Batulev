using Prism.Commands;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job8 : BaseJob {
		public Job8(MathKernel mathKernel) 
			: base(mathKernel)
		{
			EvaluateCommand = new DelegateCommand(Evaluate);
		}

		public DelegateCommand EvaluateCommand { get; set; }

		private void Evaluate()
		{
			OnPropertyChanged(nameof(Result));
		}

		public string Result => Compute( "Maximize[ { 10*a + 14*b + 12*c , 2*a + 4*b + 5*c <= 120 && a + 8*b + 6*c <= 280 && 7*a + 4*b + 5*c <= 240 && 4*a + 6*b + 7*c <= 360 && a >= 0 && b >= 0 && c >= 0 }, { a, b, c }]");
	}
}