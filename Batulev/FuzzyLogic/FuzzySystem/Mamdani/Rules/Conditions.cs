using System;
using System.Collections.Generic;
using FuzzyLogic.FuzzySystem.Mamdani.Parser;

namespace FuzzyLogic.FuzzySystem.Mamdani.Rules
{
	public class Conditions: ICondition
	{

		bool _not = false;
		OperatorType _op = OperatorType.And;
		List<ICondition> _Conditions = new List<ICondition>();

		/// <summary>
		/// Is MF inverted
		/// </summary>
		public bool Not
		{
			get { return _not; }
			set { _not = value; }
		}

		/// <summary>
		/// Operator that links expressions (and/or)
		/// </summary>
		public OperatorType Op
		{
			get { return _op; }
			set { _op = value; }
		}

		/// <summary>
		/// A list of conditions (single or multiples)
		/// </summary>
		public List<ICondition> ConditionsList
		{
			get { return _Conditions; }
		}
	}
}