using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job3 : Job2
	{
		private string _graphicFunc;

		public Job3(MathKernel mathKernel)
			: base(mathKernel)
		{
		}

		public string GraphicFunc
		{
			get { return _graphicFunc; }
			set
			{
				_graphicFunc = value;
				OnPropertyChanged(nameof(Img));
			}
		}

		protected override string GetGraphicFunc()
		{
			return GraphicFunc;
		}
	}
}