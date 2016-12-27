using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job5 : BaseJob
	{

		private string _step;
		private string _aMin;
		private string _aMax;

		public Job5(MathKernel mathKernel)
			: base(mathKernel)
		{
		}

		public string aMin
		{
			get { return _aMin; }
			set
			{
				_aMin=value;
				OnPropertyChanged(nameof(MaxResult));
				OnPropertyChanged(nameof(MinResult));
			}
		}

		public string aMax
		{
			get { return _aMax; }
			set
			{
				_aMax = value;
				OnPropertyChanged(nameof(MaxResult));
				OnPropertyChanged(nameof(MinResult));
			}
		}

		public string Step
		{
			get
			{
				return _step;
			}
			set
			{
				_step = value;
				OnPropertyChanged(nameof(MaxResult));
				OnPropertyChanged(nameof(MinResult));
			}
		}

		public string MaxResult =>Compute($"Max[{{ {GetResults()} }}]" );

		public string MinResult => Compute($"Min[{{ {GetResults()} }}]");


		string GetResults()
		{
			var sb = new StringBuilder();

			double min, max, step;
			double.TryParse(_aMin, out min);
			double.TryParse(_aMax, out max);
			double.TryParse(_step, out step);

			if (min < max && step > 0)
			{
				for (double i = min; i <= max; i = i + step)
				{
					sb.Append(Compute($"N[x /.Solve[{GetEquation(i)}, x, Reals]]")+",");
				}
			}

			var result = sb.ToString();

			if (result.Any() && result.Last() == ',')
			{
				result = result.Remove(result.Length - 1);
			}

			return result;
		}

		protected virtual string GetEquation(double a)
		{
			return $"3*x*x-2*x-({a})==0";
		}
	}
}
