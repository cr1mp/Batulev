using System.Collections.Generic;

namespace MathematicalMethodsAnalysisComplexSystems.ViewModel
{
	public class ResultViewModel
	{
		public ResultViewModel()
		{
			Functions = new List<string>();
			Eps = new List<string>();
		}

		public string Step { get; set; }
		public string Value { get; set; }

		
		public List<string> Functions { get; set; }
		public List<string> Eps { get; set; }
	}

	
}