using System.Collections.Generic;
using System.Linq;
using FuzzyLogic.FuzzySystem.Mamdani.Parser;
using FuzzyLogic.FuzzySystem.Mamdani.Rules;
using FuzzyLogic.LinguisticVariables;

namespace FuzzyLogic.FuzzySystem.Mamdani
{
	public class MamdaniFuzzySystem: IFuzzySystem
	{
		private readonly InputLinguisticVariable[] _inputLinguisticVariables;
		private readonly OutputLinguisticVariable[] _outputLinguisticVariables;
		private readonly List<MamdaniFuzzyRule> _rules;

		public MamdaniFuzzySystem(InputLinguisticVariable[] inputLinguisticVariables, OutputLinguisticVariable[] outputVariables)
		{
			_inputLinguisticVariables = inputLinguisticVariables;
			_outputLinguisticVariables = outputVariables;
			_rules = new List<MamdaniFuzzyRule>();
		}

		public IOutputLinguisticVariable[] Evaluate(params double[] inputVariables)
		{
			for (int i = 0; i < inputVariables.Length; i++)
			{
				_inputLinguisticVariables[i].InputValue = inputVariables[i];
			}

			Fuzzify(_inputLinguisticVariables)
						.EvaluateConditions(_rules,AndMethod.Min, OrMethod.Max)
						.Implicate(ImplicationMethod.Min)
						.Aggregate(AggregationMethod.Max,_outputLinguisticVariables)
						.Defuzzify(DefuzzificationMethod.Centroid);

			return _outputLinguisticVariables;
		}

		/// <summary>
		/// Фаззификация входных переменных.
		/// </summary>
		/// <param name="inputVariables"></param>
		/// <returns></returns>
		private FuzzifyResult Fuzzify(params InputLinguisticVariable[] inputVariables)
		{
			return new FuzzifyResult(inputVariables);
		}

		public void AddRule(string rule)
		{
			_rules.Add(RuleParser.Parse(rule, _inputLinguisticVariables.ToList(), _outputLinguisticVariables.ToList()));
		}
	}
}