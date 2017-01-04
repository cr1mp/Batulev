using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab2
{
	public class Lab2ViewModel : BaseJob
	{
		List<ResultType> results = new List<ResultType>();
		string _result;

		public Lab2ViewModel(MathKernel mathKernel)
			: base(mathKernel)
		{
			Job = new Job(mathKernel);
		}

		public Job Job { get; }

		public string Function { get; set; }

		public string Result
		{
			get
			{
				/*_result = Compute($"Max[{{ {GetResults()} }}]");
				OnPropertyChanged(nameof(ResultF));
				OnPropertyChanged(nameof(K1));
				OnPropertyChanged(nameof(K2));
				return _result;*/
				return null;
			}
		}

		public string ResultF => string.Join(",", results.Where(x => x.Max == _result).Select(x => x.FMax));

		public string K1 => string.Join(",", results.Where(x => x.Max == _result).Select(x => x.k1));

		public string K2 => string.Join(",", results.Where(x => x.Max == _result).Select(x => x.k2));



		private string GetResults()
		{

			results.Clear();

			var sb = new StringBuilder();

			for (int i = 120; i <= 330; i++)
			{
				for (int j = 100; j <= 200; j++)
				{
					var r = new ResultType
					{
						Max = Compute($"Maximize[{{ {i} * a + {j} * b, a + b <= 20 && 4 * a + 2 * b <= 80 && a + 5 * b <= 140 && a >= 0 && b >= 2 }},{{a,b}}][[1]]"),
						//Min =Compute($"Minimize[{{ {i} * a + {j} * b, a + b <= 20 && 4 * a + 2 * b <= 80 && a + 5 * b <= 140 && a >= 0 && b >= 2 }},{{a,b}}][[1]]"),
						FMax = Compute($"Maximize[{{ {i} * a + {j} * b, a + b <= 20 && 4 * a + 2 * b <= 80 && a + 5 * b <= 140 && a >= 0 && b >= 2 }},{{a,b}}]"),
						//FMin =Compute($"Minimize[{{ {i} * a + {j} * b, a + b <= 20 && 4 * a + 2 * b <= 80 && a + 5 * b <= 140 && a >= 0 && b >= 2 }},{{a,b}}]"),
						k1 = i,
						k2 = j
					};

					results.Add(r);

					sb.Append(r.Max + ",");
				}
			}

			var result = sb.ToString();

			if (result.Any() && result.Last() == ',')
			{
				result = result.Remove(result.Length - 1);
			}

			return result;

		}
	}
}
