using System.Collections.Generic;
using FuzzyLogic.Terms;

namespace FuzzyLogic.LinguisticVariables
{
	public abstract class LinguisticVariable
	{
		private readonly List<Term> _terms;
		private readonly string _name;

		protected LinguisticVariable(string name,double min,double max)
		{
			_terms = new List<Term>();
			_name = name;

			Range = new Range
			{
				Min=min,
				Max = max
			};
		}

		public string Name => _name;

		public Range Range { get; set; }

		public List<Term> Terms => _terms;
	}
}
