
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Commands;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab2
{
	public class Job : BaseJob
	{
		private List<ResultType> results = new List<ResultType>();
		private string _function;
		private string _result;
		private bool _minMax;

		public Job(MathKernel mathKernel)
			: base(mathKernel)
		{
			Range = new ObservableCollection<Range>();
			Restrictions = new ObservableCollection<Restriction>();
		}

		public bool MinMax
		{
			get { return _minMax; }
			set { _minMax = value; OnPropertyChanged(nameof(Result)); }
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
					var r = new Range {Coefficient = c};
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
				_result = MinMax ? Compute($"Min[{{ {GetResults()} }}]") : Compute($"Max[{{ {GetResults()} }}]");
				//OnPropertyChanged(nameof(ResultF));
				//OnPropertyChanged(nameof(K1));
				//OnPropertyChanged(nameof(K2));
				return _result;
			}
		}

		public string ResultF { get; }

		string GetResults()
		{
			results.Clear();

			var sb = new StringBuilder();

			var manager = new RangeManager(Range.ToArray());

			foreach (RangeItem[] rangeItems in manager)
			{
				/*
			for (int i = 120; i <= 330; i++)
			{
				for (int j = 100; j <= 200; j++)
				{
					var r = new ResultType
					{
						Max = Compute($"Maximize[{{ {i} * a + {j} * b, a + b <= 20 && 4 * a + 2 * b <= 80 && a + 5 * b <= 140 && a >= 0 && b >= 2 }},{{a,b}}][[1]]"),
						Min =Compute($"Minimize[{{ {i} * a + {j} * b, a + b <= 20 && 4 * a + 2 * b <= 80 && a + 5 * b <= 140 && a >= 0 && b >= 2 }},{{a,b}}][[1]]"),
						FMax = Compute($"Maximize[{{ {i} * a + {j} * b, a + b <= 20 && 4 * a + 2 * b <= 80 && a + 5 * b <= 140 && a >= 0 && b >= 2 }},{{a,b}}]"),
						FMin =Compute($"Minimize[{{ {i} * a + {j} * b, a + b <= 20 && 4 * a + 2 * b <= 80 && a + 5 * b <= 140 && a >= 0 && b >= 2 }},{{a,b}}]"),
						k1 = i,
						k2 = j
					};

					results.Add(r);

					sb.Append(r.Max + ",");
				}
			}
			*/

				foreach (var rangeItem in rangeItems)
				{
					sb.Append($"{rangeItem.Coefficient}={rangeItem.Item} ");
				}
				sb.Append("\r\n");
			}



			var result = sb.ToString();

			if (result.Any() && result.Last() == ',')
			{
				result = result.Remove(result.Length - 1);
			}

			return result;
		}
	}

	public class Restriction : RemovedItem
	{
		public string Value { get; set; }
	}

	public class Range : RemovedItem
	{
		private int _min;
		private int _max;
		private bool _isStart=true;
		private Action<Range> nextFunc;

		public Range()
		{
			PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == nameof(Min) || e.PropertyName == nameof(Max))
				{
					OnPropertyChanged(nameof(Arr));
					_isStart = true;
				}
			};
		}

		public void Reset()
		{
			_isStart = true;
			CurrentIndex = 0;
		}

		public string Coefficient { get; set; }

		public int Min
		{
			get { return _min; }
			set { SetProperty(ref _min, value); }
		}

		public int Max
		{
			get { return _max; }
			set { SetProperty(ref _max, value); }
		}

		public int[] Arr
		{
			get
			{
				List<int> res = new List<int>();
				for (int i = Min; i <= Max; i++)
				{
					res.Add(i);
				}
				return res.ToArray();
			}
		}

		public int CurrentIndex { get; private set; }

		public void SetNextIndex()
		{
			if (_isStart)
			{
				_isStart = false;
				return;
			}

			if (nextFunc!=null)
			{
				nextFunc(this);
				return;
			}

			UpdateIndex();
		}

		public void UpdateIndex()
		{
			if (CurrentIndex < (_max - _min))
			{
				CurrentIndex++;
			}
			else
			{
				CurrentIndex = 0;
			}
		}

		public bool IsMax => CurrentIndex == (_max - _min);

		public void SetNextIndexFunc(Action<Range> nextAction)
		{
			nextFunc = nextAction;
		}
	}

	public class RemovedItem : BindableBase
	{
		public RemovedItem()
		{
			Remove = new DelegateCommand(() => { });
		}

		public DelegateCommand Remove { get; set; }
	}

	public class RangeManager : IEnumerable<RangeItem[]>
	{
		private readonly Range[] _elements;

		public RangeManager(Range[] elements)
		{
			_elements = elements;
		}

		public IEnumerator<RangeItem[]> GetEnumerator()
		{
			return new RangeEnumerator(_elements);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class RangeEnumerator : IEnumerator<RangeItem[]>
	{
		private readonly Range[] _elements;
		private readonly int _count;


		public RangeEnumerator(Range[] elements)
		{
			_elements = elements;
			_count = _elements.Length;

			_elements.ForEach(x=>x.Reset());
		}

		public void Dispose()
		{

		}

		public bool MoveNext()
		{
			if (_elements.All(x => x.IsMax))
			{
				return false;
			}

			for (int i = 0; i < _count; i++)
			{
				var currentRang = _elements[i];
				currentRang.SetNextIndex();
				if (i > 0)
				{
					var prevRangAll = new List< Range>();

					for (int j = 0; j <= i-1; j++)
					{
						prevRangAll.Add(_elements[j]);
					}

					if (prevRangAll.Any() && prevRangAll.All(x=>x.IsMax))
					{
						currentRang.SetNextIndexFunc(x=>x.UpdateIndex());
					}
					else
					{
						currentRang.SetNextIndexFunc(x => { });
					}
				}
			}

			return true;
		}

		public void Reset()
		{
			throw new System.NotSupportedException();
		}

		public RangeItem[] Current
		{
			get
			{
				return _elements.Select(x => new RangeItem
				{
					Coefficient = x.Coefficient,
					Item = x.Arr[x.CurrentIndex]
				}).ToArray();
			}
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}
	}

	public class RangeItem
	{
		public string Coefficient { get; set; }

		public int Item { get; set; }
	}
}