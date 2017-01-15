using System;
using System.Collections.Generic;
using FuzzyLogic.FuzzySystem.Mamdani.Enums;
using FuzzyLogic.FuzzySystem.Mamdani.Rules;
using FuzzyLogic.LinguisticVariables;

namespace FuzzyLogic.FuzzySystem.Mamdani
{
	internal class ImplicateResult
	{
		private Dictionary<MamdaniFuzzyRule, Func<double, double>> _dictionary;
		private OutputLinguisticVariable[] _output;
		private AggregationMethod _aggregationMethod;

		public ImplicateResult(Dictionary<MamdaniFuzzyRule, Func<double, double>> dictionary)
		{
			this._dictionary = dictionary;
		}

		/// <summary>
		/// Аккумулирование заключений
		/// </summary>
		/// <returns></returns>
		public AggregateResult Aggregate(AggregationMethod aggregationMethod,OutputLinguisticVariable[] output)
		{
			_aggregationMethod = aggregationMethod;
			_output = output;
			return new AggregateResult(Aggregate(_dictionary));
		}

		public Dictionary<OutputLinguisticVariable, Func<double,double>> Aggregate(Dictionary<MamdaniFuzzyRule, Func<double, double>> conclusions)
		{
			var fuzzyResult = new Dictionary<OutputLinguisticVariable, Func<double, double>>();
			foreach (var variable in _output)
			{
				var mfList = new List<Func<double, double>>();
				foreach (MamdaniFuzzyRule rule in conclusions.Keys)
				{
					if (rule.Conclusion.Var == variable)
					{
						mfList.Add(conclusions[rule]);
					}
				}

				MfCompositionType composType;
				switch (_aggregationMethod)
				{
					case AggregationMethod.Max:
						composType = MfCompositionType.Max;
						break;
					case AggregationMethod.Sum:
						composType = MfCompositionType.Sum;
						break;
					default:
						throw new Exception("Internal exception.");
				}
				fuzzyResult.Add(variable, new CompositeMembershipFunction(composType, mfList).GetValue);
			}

			return fuzzyResult;
		}
	}
}