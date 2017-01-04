using System;
using System.Collections.Generic;
using Prism.Mvvm;

namespace OptimizationMethods.ViewModels.Lab2
{
	public class Range : BindableBase
	{
		private int _min;
		private int _max;
		private bool _isStart = true;
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

			if (nextFunc != null)
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

		public bool IsValid => _max >= _min;

		public void SetNextIndexFunc(Action<Range> nextAction)
		{
			nextFunc = nextAction;
		}
	}
}