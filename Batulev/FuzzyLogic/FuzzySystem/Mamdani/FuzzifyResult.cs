using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyLogic.FuzzySystem.Mamdani.Parser;
using FuzzyLogic.FuzzySystem.Mamdani.Rules;
using FuzzyLogic.LinguisticVariables;
using FuzzyLogic.Terms;

namespace FuzzyLogic.FuzzySystem.Mamdani
{
	internal class FuzzifyResult
	{
		private readonly InputLinguisticVariable[] _inputVariables;
		private AndMethod _andMethod;
		private OrMethod _orMethod;

		public FuzzifyResult(params InputLinguisticVariable[] inputVariables)
		{
			_inputVariables = inputVariables;
		}

		/// <summary>
		/// Агрегирование подусловий.
		/// </summary>
		/// <param name="fuzzyRules"></param>
		/// <param name="andMethod"></param>
		/// <returns></returns>
		public EvaluateConditionsResult EvaluateConditions(List<MamdaniFuzzyRule> fuzzyRules, AndMethod andMethod, OrMethod orMethod)
		{
			_andMethod = andMethod;
			_orMethod = orMethod;

			var result = new Dictionary<MamdaniFuzzyRule, double>();

			var fuzzifiedInput = new Dictionary<LinguisticVariable, Dictionary<Term, double>>();

			foreach (var inputLinguisticVariable in _inputVariables)
			{
				fuzzifiedInput[inputLinguisticVariable] = inputLinguisticVariable.Terms.ToDictionary(t => t, term => term.GetValue(inputLinguisticVariable.InputValue));
			}

			foreach (MamdaniFuzzyRule rule in fuzzyRules)
			{
				result.Add(rule, EvaluateCondition(rule.Condition, fuzzifiedInput));
			}

			return new EvaluateConditionsResult(result);
		}

		protected double EvaluateCondition(ICondition condition, Dictionary<LinguisticVariable, Dictionary<Term, double>> fuzzifiedInput)
		{
			if (condition is Conditions)
			{
				double result = 0.0;
				Conditions conds = (Conditions)condition;

				if (conds.ConditionsList.Count == 0)
				{
					throw new Exception("Inner exception.");
				}
				else if (conds.ConditionsList.Count == 1)
				{
					result = EvaluateCondition(conds.ConditionsList[0], fuzzifiedInput);
				}
				else
				{
					result = EvaluateCondition(conds.ConditionsList[0], fuzzifiedInput);
					for (int i = 1; i < conds.ConditionsList.Count; i++)
					{
						result = EvaluateConditionPair(result, EvaluateCondition(conds.ConditionsList[i], fuzzifiedInput), conds.Op);
					}
				}

				if (conds.Not)
				{
					result = 1.0 - result;
				}

				return result;
			}
			else if (condition is FuzzyCondition)
			{
				FuzzyCondition cond = (FuzzyCondition)condition;
				double result = fuzzifiedInput[(LinguisticVariable)cond.Var][(Term)cond.Term];

				switch (cond.Hedge)
				{
					case HedgeType.Slightly:
						result = Math.Pow(result, 1.0 / 3.0); //Cube root
						break;

					case HedgeType.Somewhat:
						result = Math.Sqrt(result);
						break;

					case HedgeType.Very:
						result = result * result;
						break;

					case HedgeType.Extremely:
						result = result * result * result;
						break;

					default:
						break;
				}

				if (cond.Not)
				{
					result = 1.0 - result;
				}
				return result;
			}
			else
			{
				throw new Exception("Internal exception.");
			}
		}

		private double EvaluateConditionPair(double cond1, double cond2, OperatorType op)
		{
			if (op == OperatorType.And)
			{
				if (_andMethod == AndMethod.Min)
				{
					return Math.Min(cond1, cond2);
				}
				else if (_andMethod == AndMethod.Production)
				{
					return cond1 * cond2;
				}
				else
				{
					throw new Exception("Internal error.");
				}
			}
			else if (op == OperatorType.Or)
			{
				if (_orMethod == OrMethod.Max)
				{
					return Math.Max(cond1, cond2);
				}
				else if (_orMethod == OrMethod.Probabilistic)
				{
					return cond1 + cond2 - cond1 * cond2;
				}
				else
				{
					throw new Exception("Internal error.");
				}
			}
			else
			{
				throw new Exception("Internal error.");
			}
		}
	}
}