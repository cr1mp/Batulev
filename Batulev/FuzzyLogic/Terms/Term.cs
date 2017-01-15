namespace FuzzyLogic.Terms
{
	public abstract class Term
	{
		private readonly string _name;

		protected Term(string name)
		{
			_name = name;
		}

		public string Name => _name;

		public abstract double GetValue(double x);
	}
}