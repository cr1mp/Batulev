using System.Collections;
using System.Collections.Generic;

namespace OptimizationMethods.ViewModels.Lab2
{
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
}