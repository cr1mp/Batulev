using FuzzyLogic.LinguisticVariables;
using FuzzyLogic.Terms;

namespace FuzzyLogic.FuzzySystem.Mamdani.Parser
{
	public class SingleCondition
	{
		LinguisticVariable _var = null;
		bool _not = false;
		Term _term = null;


		/// <summary>
		/// Default constructor
		/// </summary>
		internal SingleCondition()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="var">A linguistic variable to which the condition is related</param>
		/// <param name="term">A term in expression 'var is term'</param>
		internal SingleCondition(LinguisticVariable var, Term term)
		{
			_var = var;
			_term = term;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="var">A linguistic variable to which the condition is related</param>
		/// <param name="term">A term in expression 'var is term'</param>
		/// <param name="not">Does condition contain 'not'</param>
		internal SingleCondition(LinguisticVariable var, Term term, bool not)
			: this(var, term)
		{
			_not = not;
		}

		/// <summary>
		/// A linguistic variable to which the condition is related
		/// </summary>
		public LinguisticVariable Var
		{
			get { return _var; }
			set { _var = value; }
		}

		/// <summary>
		/// Is MF inverted
		/// </summary>
		public bool Not
		{
			get { return _not; }
			set { _not = value; }
		}

		/// <summary>
		/// A term in expression 'var is term'
		/// </summary>
		public Term Term
		{
			get { return _term; }
			set { _term = value; }
		}
	}
}