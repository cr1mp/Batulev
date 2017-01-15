using System;

namespace FuzzyLogic.Terms
{
	public class BellTerm : Term
	{
		public BellTerm(string name)
			: base(name)
		{
		}

		public BellTerm(string name, double a, double b, double c)
			: this(name)
		{
			this.a = a;
			this.b = b;
			this.c = c;
		}

		public double a { get; set; }
		public double b { get; set; }
		public double c { get; set; }

		public override double GetValue(double x) => 1 / (1 + (Math.Pow((Math.Abs((x - c) / a)), 2 * b)));
	}
}