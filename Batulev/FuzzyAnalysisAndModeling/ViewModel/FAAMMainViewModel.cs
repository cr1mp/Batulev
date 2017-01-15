using System;
using System.Linq;
using FuzzyLogic.FuzzySystem;
using FuzzyLogic.FuzzySystem.Mamdani;
using FuzzyLogic.LinguisticVariables;
using FuzzyLogic.Terms;
using Prism.Mvvm;

namespace FuzzyAnalysisAndModeling.ViewModel
{
	public class FAAMMainViewModel : BindableBase
	{
		private IFuzzySystem _fuzzySystem;
		private string _in1;
		private string _in2;
		private string _in3;
		private string _step;

		public FAAMMainViewModel()
		{
			var input1 = new InputLinguisticVariable("in1", 0, 1);
			var input2 = new InputLinguisticVariable("in2", 0, 1);
			var input3 = new InputLinguisticVariable("in3", 0, 1);

			input1.Terms.Add(new PiTerm("T11", -0.4, -0.05, 0.05, 0.4));
			input1.Terms.Add(new PiTerm("T12", 0.1, 0.45, 0.55, 0.9));
			input1.Terms.Add(new PiTerm("T13", 0.6, 0.95, 1.05, 1.4));

			input2.Terms.Add(new TrianTerm("T21", -0.4, 0, 0.4));
			input2.Terms.Add(new TrianTerm("T22", 0.1, 0.5, 0.9));
			input2.Terms.Add(new TrianTerm("T23", 0.6, 1, 1.4));

			input3.Terms.Add(new TrianTerm("T31", -0.4, 0, 0.4));
			input3.Terms.Add(new TrianTerm("T32", 0.1, 0.5, 0.9));
			input3.Terms.Add(new TrianTerm("T33", 0.6, 1, 1.4));

			var output1 = new OutputLinguisticVariable("out1", 0, 1);

			output1.Terms.Add(new TrianTerm("X1", -0.4, 0, 0.4));
			output1.Terms.Add(new TrianTerm("X2", 0.1, 0.5, 0.9));
			output1.Terms.Add(new TrianTerm("X3", 0.6, 1, 1.4));

			var mamdani = new MamdaniFuzzySystem(new[] { input1, input2, input3 }, new[] { output1 });

			mamdani.AddRule("if (in1 is T13)  and (in2 is T21) and (in3 is T32) then (out1 is X1)");
			mamdani.AddRule("if (in1 is T12)  or  (in2 is T23) or  (in3 is T31) then (out1 is X2)");
			mamdani.AddRule("if (in1 is T11)  and (in2 is T22)                  then (out1 is X3)");

			_fuzzySystem = mamdani;
		}

		public string In1
		{
			get { return _in1; }
			set
			{
				_in1 = value.ReplacePoint();
				OnPropertyChanged(nameof(Out1));
			}
		}

		public string In2
		{
			get { return _in2; }
			set
			{
				_in2 = value.ReplacePoint();
				OnPropertyChanged(nameof(Out1));
			}
		}

		public string In3
		{
			get { return _in3; }
			set
			{
				_in3 = value.ReplacePoint();
				OnPropertyChanged(nameof(Out1));
			}
		}

		public string Step
		{
			get { return _step; }
			set
			{
				_step = value.ReplacePoint();
				OnPropertyChanged(nameof(Results));
			}
		}

		public string Results
		{
			get
			{
				double step;

				if (!double.TryParse(_step, out step))
				{
					step = 0.25;
				}

				if (step <= 0)
				{
					return string.Empty;
				}

				string result = String.Empty;
				int iterator = 0;
				for (double i = 0; i <= 1; i = i + step)
				{
					for (double j = 0; j <= 1; j = j + step)
					{
						for (double k = 0; k <= 1; k = k + step)
						{
							iterator++;
							var r = String.Join(",", _fuzzySystem.Evaluate(i, j, k).Select(x => x.Result));
							result += $"{iterator}\t{i}\t{j}\t{k}\t{r}\r\n";
						}
					}
				}
				return result;
			}
		}

		public string Out1 => Evaluate();

		private string Evaluate()
		{
			double in1;
			double in2;
			double in3;

			if (string.IsNullOrWhiteSpace(_in1) || string.IsNullOrWhiteSpace(_in2) || string.IsNullOrWhiteSpace(_in3) ||
				!double.TryParse(_in1, out in1) || !double.TryParse(_in2, out in2) || !double.TryParse(_in3, out in3) ||
				_fuzzySystem == null)
			{
				return String.Empty;
			}

			return String.Join(",", _fuzzySystem.Evaluate(in1, in2, in3).Select(x => x.Result));
		}
	}
}