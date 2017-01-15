using FuzzyLogic.FuzzySystem.Mamdani.Parser;
using FuzzyLogic.LinguisticVariables;
using FuzzyLogic.Terms;

namespace FuzzyLogic.FuzzySystem.Mamdani.Rules
{
	public class MamdaniFuzzyRule:ParsableRule, IParsableRule
	{
		public MamdaniFuzzyRule()
		{
			Conclusion = new SingleCondition();
			Weight = 1;
		}

		public SingleCondition Conclusion { get; set; }

		public double Weight { get; set; }
	}

	public abstract class ParsableRule
	{
		Conditions _condition = new Conditions();

		/// <summary>
		/// Condition (IF) part of the rule
		/// </summary>
		public Conditions Condition
		{
			get { return _condition; }
			set { _condition = value; }
		}

		/// <summary>
		/// Create a single condition
		/// </summary>
		/// <param name="var">A linguistic variable to which the condition is related</param>
		/// <param name="term">A term in expression 'var is term'</param>
		/// <returns>Generated condition</returns>
		public FuzzyCondition CreateCondition(LinguisticVariable var, Term term)
		{
			return new FuzzyCondition(var, term);
		}

		/// <summary>
		/// Create a single condition
		/// </summary>
		/// <param name="var">A linguistic variable to which the condition is related</param>
		/// <param name="term">A term in expression 'var is term'</param>
		/// <param name="not">Does condition contain 'not'</param>
		/// <returns>Generated condition</returns>
		public FuzzyCondition CreateCondition(LinguisticVariable var, Term term, bool not)
		{
			return new FuzzyCondition(var, term, not);
		}

		/// <summary>
		/// Create a single condition
		/// </summary>
		/// <param name="var">A linguistic variable to which the condition is related</param>
		/// <param name="term">A term in expression 'var is term'</param>
		/// <param name="not">Does condition contain 'not'</param>
		/// <param name="hedge">Hedge modifier</param>
		/// <returns>Generated condition</returns>
		public FuzzyCondition CreateCondition(LinguisticVariable var, Term term, bool not, HedgeType hedge)
		{
			return new FuzzyCondition(var, term, not, hedge);
		}
	}
}
