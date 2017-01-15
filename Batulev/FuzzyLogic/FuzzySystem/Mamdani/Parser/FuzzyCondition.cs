using FuzzyLogic.LinguisticVariables;
using FuzzyLogic.Terms;

namespace FuzzyLogic.FuzzySystem.Mamdani.Parser
{
	public class FuzzyCondition : SingleCondition, ICondition
	{
		HedgeType _hedge = HedgeType.None;

		/// <summary>
		/// Hedge modifier
		/// </summary>
		public HedgeType Hedge
		{
			get { return _hedge; }
			set { _hedge = value; }
		}

		internal FuzzyCondition(LinguisticVariable variable, Term term)
			: this(variable, term, false)
		{
		}

		internal FuzzyCondition(LinguisticVariable variable, Term term, bool not)
			: this(variable, term, not, HedgeType.None)
		{
		}

		internal FuzzyCondition(LinguisticVariable variable, Term term, bool not, HedgeType hedge)
			: base(variable, term, not)
		{
			_hedge = hedge;
		}
	}
}
