using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyLogic.LinguisticVariables;

namespace FuzzyLogic.FuzzySystem
{
	public interface IFuzzySystem
	{
		IOutputLinguisticVariable[] Evaluate(params double[] inputVariables);
	}
}
