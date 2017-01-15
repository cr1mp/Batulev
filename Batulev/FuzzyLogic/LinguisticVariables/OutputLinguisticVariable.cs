using System;
using FuzzyLogic.FuzzySystem;

namespace FuzzyLogic.LinguisticVariables
{
	public class OutputLinguisticVariable : LinguisticVariable, IOutputLinguisticVariable
	{
		private double _result;

		public OutputLinguisticVariable(string name, double min, double max)
			: base(name, min, max)
		{
		}

		public double Result
		{
			get
			{
				return Math.Round(_result, 2);
			}
			set { _result = value; }
		}
	}
}