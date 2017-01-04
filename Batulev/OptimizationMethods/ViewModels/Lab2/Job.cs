
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Commands;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab2
{
	public class Job : BaseJob
	{
		private List<ResultType> results = new List<ResultType>();
		private string _function;
		private string _result;
		private bool _isMax;
		private string _allK;

		public Job(MathKernel mathKernel)
			: base(mathKernel)
		{
			Range = new ObservableCollection<Range>();
			Restrictions = new ObservableCollection<Restriction>();
			Restrictions.CollectionChanged += (o, e) => OnPropertyChanged(nameof(Result));
			AddCommand = new DelegateCommand(addRestriction);
		}

		private void addRestriction()
		{
			Restriction r = null;
			Action a = () => Restrictions.Remove(r);
			r = new Restriction(a);
			r.PropertyChanged += (o, e) => OnPropertyChanged(nameof(Result));
			Restrictions.Add(r);
		}

		public DelegateCommand AddCommand { get; set; }

		public bool MinMax
		{
			get { return _isMax; }
			set { _isMax = value; OnPropertyChanged(nameof(Result)); }
		}

		public string Function
		{
			get { return _function; }
			set
			{
				_function = value;
				Range.Clear();

				GetUnknownVariables(value).Where(x => x.ToLower().Contains("k")).ForEach(c =>
				{
					var r = new Range { Coefficient = c };
					r.PropertyChanged += (o, e) =>
					{
						if (e.PropertyName == "Min" || e.PropertyName == "Max")
						{
							OnPropertyChanged(nameof(Result));
						}
					};
					Range.Add(r);
				});
			}
		}

		public ObservableCollection<Range> Range { get; set; }

		public ObservableCollection<Restriction> Restrictions { get; set; }

		public string Result
		{
			get
			{
				_result = _isMax ? Compute($"Max[{{ {GetResults()} }}]") : Compute($"Min[{{ {GetResults()} }}]");
				OnPropertyChanged(nameof(ResultF));
				OnPropertyChanged(nameof(Ks));
				return _result;
			}
		}

		public string AllK => _allK;

		public string ResultF => string.Join(", ", _isMax ? results.Where(x => x.Max == _result).Select(x => x.FMax) : results.Where(x => x.Min == _result).Select(x => x.FMin));
		public string Ks => string.Join(", ", _isMax ? results.Where(x => x.Max == _result).Select(x => x.ks) : results.Where(x => x.Min == _result).Select(x => x.ks));

		string GetResults()
		{
			results.Clear();

			var sb = new StringBuilder();
			var sbK = new StringBuilder();

			var manager = new RangeManager(Range.ToArray());

			foreach (RangeItem[] rangeItems in manager)
			{

				var f = _function;

				var ks = string.Empty;

				foreach (var rangeItem in rangeItems)
				{
					var __ks = $"{rangeItem.Coefficient}={rangeItem.Item} ";

					ks += __ks;

					sbK.Append(__ks);

					f = f.Replace(rangeItem.Coefficient, rangeItem.Item.ToString());
				}
				sbK.Append("\r\n");

				var restrictions = string.Join(" && ", Restrictions.Select(x => x.Value));

				var xs = RemoveLastChar(string.Join(",", GetUnknownVariables(f)), ',');

				if (!string.IsNullOrWhiteSpace(restrictions))
				{
					f = $"{f},{restrictions}";
				}

				var r = new ResultType();

				r.ks = ks;

				if (_isMax)
				{
					r.Max = Compute($"Maximize[{{ {f} }},{{ {xs} }}][[1]]");
					r.FMax = Compute($"Maximize[{{ {f} }},{{ {xs} }}]");

					sb.Append(r.Max + ",");
				}
				else
				{
					r.Min = Compute($"Minimize[{{ {f} }},{{ {xs} }}][[1]]");
					r.FMin = Compute($"Minimize[{{ {f} }},{{ {xs} }}]");

					sb.Append(r.Min + ",");
				}

				results.Add(r);

			}

			var result = sb.ToString();

			_allK = sbK.ToString();
			OnPropertyChanged(nameof(AllK));

			result = RemoveLastChar(result, ',');

			return result;
		}

		string RemoveLastChar(string s, char c)
		{
			if (s.Any() && s.Last() == c)
			{
				s = s.Remove(s.Length - 1);
			}
			return s;
		}
	}
}