using System;
using System.Collections.Generic;
using FuzzyLogic.LinguisticVariables;

namespace FuzzyLogic.FuzzySystem.Mamdani
{
	internal class AggregateResult
	{
		private Dictionary<OutputLinguisticVariable, Func<double, double>> dictionary;
		private DefuzzificationMethod _defuzzMethod;

		public AggregateResult(Dictionary<OutputLinguisticVariable, Func<double, double>> dictionary)
		{
			this.dictionary = dictionary;
		}

		/// <summary>
		/// Дефаззификация выходных переменных.
		/// </summary>
		public void Defuzzify(DefuzzificationMethod defuzzMethod)
		{
			_defuzzMethod = defuzzMethod;
			Defuzzify(dictionary);
		}

		public Dictionary<OutputLinguisticVariable, double> Defuzzify(Dictionary<OutputLinguisticVariable, Func<double, double>> fuzzyResult)
		{
			var crispResult = new Dictionary<OutputLinguisticVariable, double>();
			foreach (var variable in fuzzyResult.Keys)
			{
				var result = Defuzzify(fuzzyResult[variable], variable.Range.Min, variable.Range.Max);
				crispResult.Add(variable, result);
				variable.Result = result;
			}

			return crispResult;
		}

		private double Defuzzify(Func<double, double> mf, double min, double max)
		{
			if (_defuzzMethod == DefuzzificationMethod.Centroid)
			{
				int k = 50;
				double step = (max - min) / k;

				double ptLeft = 0.0;
				double ptCenter = 0.0;
				double ptRight = 0.0;

				double valLeft = 0.0;
				double valCenter = 0.0;
				double valRight = 0.0;

				double val2Left = 0.0;
				double val2Center = 0.0;
				double val2Right = 0.0;

				double numerator = 0.0;
				double denominator = 0.0;
				for (int i = 0; i < k; i++)
				{
					if (i == 0)
					{
						ptRight = min;
						valRight = mf(ptRight);
						val2Right = ptRight * valRight;
					}

					ptLeft = ptRight;
					ptCenter = min + step * ((double)i + 0.5);
					ptRight = min + step * (i + 1);

					valLeft = valRight;
					valCenter = mf(ptCenter);
					valRight = mf(ptRight);

					val2Left = val2Right;
					val2Center = ptCenter * valCenter;
					val2Right = ptRight * valRight;

					numerator += step * (val2Left + 4 * val2Center + val2Right) / 3.0;
					denominator += step * (valLeft + 4 * valCenter + valRight) / 3.0;
				}

				if (denominator == 0)
				{
					return 0.5;
				}
				return numerator / denominator;
			}
			else if (_defuzzMethod == DefuzzificationMethod.Bisector)
			{
				// TODO:
				throw new NotSupportedException();
			}
			else if (_defuzzMethod == DefuzzificationMethod.AverageMaximum)
			{
				// TODO:
				throw new NotSupportedException();
			}
			else
			{
				throw new Exception("Internal exception.");
			}
		}
	}
}