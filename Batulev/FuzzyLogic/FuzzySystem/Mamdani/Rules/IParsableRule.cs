using System;
using FuzzyLogic.FuzzySystem.Mamdani.Parser;
using FuzzyLogic.LinguisticVariables;
using FuzzyLogic.Terms;

namespace FuzzyLogic.FuzzySystem.Mamdani.Rules
{
	interface IParsableRule
	{
		/// <summary>
		/// Condition (IF) part of the rule
		/// </summary>
		Conditions Condition { get; set; }

		/// <summary>
		/// Conclusion (THEN) part of the rule
		/// </summary>
		SingleCondition Conclusion { get; set; }
	}
}