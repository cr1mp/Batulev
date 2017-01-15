using System;
using System.Collections.Generic;
using FuzzyLogic.FuzzySystem.Mamdani.Enums;
using FuzzyLogic.FuzzySystem.Mamdani.Rules;

namespace FuzzyLogic.FuzzySystem.Mamdani
{
	internal class EvaluateConditionsResult
	{
		private Dictionary<MamdaniFuzzyRule, double> result;
		private ImplicationMethod _implMethod;

		public EvaluateConditionsResult(Dictionary<MamdaniFuzzyRule, double> result)
		{
			this.result = result;
		}

		/// <summary>
		/// Активизация подзаключений.
		/// </summary>
		/// <returns></returns>
		public ImplicateResult Implicate(ImplicationMethod implicationMethod)
		{
			_implMethod = implicationMethod;
			return new ImplicateResult(Implicate(result));
		}

		private Dictionary<MamdaniFuzzyRule, Func<double, double>> Implicate(Dictionary<MamdaniFuzzyRule, double> conditions)
		{
			var conclusions = new Dictionary<MamdaniFuzzyRule, Func<double, double>>();

			foreach (var rule in conditions.Keys)
			{
				MfCompositionType compType;
				switch (_implMethod)
				{
					case ImplicationMethod.Min:
						compType = MfCompositionType.Min;
						break;

					case ImplicationMethod.Production:
						compType = MfCompositionType.Prod;
						break;

					default:
						throw new Exception("Internal error.");
				}

				var resultMf = new CompositeMembershipFunction(
					compType,
					new ConstantMembershipFunction(conditions[rule]).GetValue,
					rule.Conclusion.Term.GetValue);

				conclusions.Add(rule, resultMf.GetValue);
			}

			return conclusions;
		}
	}

	public class ConstantMembershipFunction
	{
		private double _constValue;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="constValue">Constant value</param>
		public ConstantMembershipFunction(double constValue)
		{
			if (constValue < 0.0 || constValue > 1.0)
			{
				throw new ArgumentException();
			}

			_constValue = constValue;
		}

		/// <summary>
		/// Evaluate value of the membership function
		/// </summary>
		/// <param name="x">Argument (x axis value)</param>
		/// <returns></returns>
		public double GetValue(double x)
		{
			return _constValue;
		}
	}
}