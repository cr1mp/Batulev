using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job7 : ImageBaseJob
	{
		private string _step;

		public Job7(MathKernel mathKernel) 
			: base(mathKernel)
		{
			PropertyChanged += (o, e) => {
				if (e.PropertyName == nameof(Img) )
				{
					UpdateView();
				} };
		}

		private void UpdateView()
		{
			OnPropertyChanged(nameof(MaxResult));
			OnPropertyChanged(nameof(MinResult));
		}

		public string Step
		{
			get { return _step; }
			set
			{
				_step = value;
				UpdateView();
			}
		}

		public string MaxResult => Compute($"Max[{{ {GetResults()} }}]");

		public string MinResult => Compute($"Min[{{ {GetResults()} }}]");

		protected override string GetGraphicFunc()
		{
			return "y=3*x^3-2*x^2+7";
		}

		string GetResults()
		{
			var sb = new StringBuilder();

			double min, max, step;
			double.TryParse(_xMin, out min);
			double.TryParse(_xMax, out max);
			double.TryParse(_step, out step);

			if (min < max && step > 0)
			{
				for (double x = min; x <= max; x = x + step)
				{
					sb.Append(Compute(GetEquation(x)) + ",");
				}
			}

			var result = sb.ToString();

			if (result.Any() && result.Last() == ',')
			{
				result = result.Remove(result.Length - 1);
			}

			return result;
		}

		protected virtual string GetEquation(double x)
		{
			return $"3*({x})^3-2*({x})^2+7";
		}

	}
}
