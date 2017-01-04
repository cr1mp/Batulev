using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace OptimizationMethods.ViewModels.Lab2
{
	public class RangeEnumerator : IEnumerator<RangeItem[]>
	{
		private readonly Range[] _elements;
		private readonly int _count;


		public RangeEnumerator(Range[] elements)
		{
			_elements = elements;
			_count = _elements.Length;

			_elements.ForEach(x => x.Reset());
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

			if (_elements.Any(x => !x.IsValid))
			{
				return false;
			}

			for (int i = 0; i < _count; i++)
			{
				var currentRang = _elements[i];
				currentRang.SetNextIndex();
				if (i > 0)
				{
					var prevRangAll = new List<Range>();

					for (int j = 0; j <= i - 1; j++)
					{
						prevRangAll.Add(_elements[j]);
					}

					if (prevRangAll.Any() && prevRangAll.All(x => x.IsMax))
					{
						currentRang.SetNextIndexFunc(x => x.UpdateIndex());
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
}