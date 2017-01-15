namespace FuzzyLogic.LinguisticVariables
{
	public class InputLinguisticVariable : LinguisticVariable
	{

		public InputLinguisticVariable(string name, double min,double max)
			: base(name,min,max)
		{
		}

		public double InputValue { get; set; }
	}
}