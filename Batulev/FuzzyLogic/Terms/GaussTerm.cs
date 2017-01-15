using System;

namespace FuzzyLogic.Terms
{
	public class GaussTerm : Term
	{
		public GaussTerm(string name)
			: base(name)
		{
		}

		public GaussTerm(string name, double sigm, double c)
			: this(name)
		{
			this.sigm = sigm;
			this.c = c;
		}

		public double sigm { get; set; }

		public double c { get; set; }

		public override double GetValue(double x)=> Math.Exp((-1 * Math.Pow(x - c, 2)) / (2 * Math.Pow(sigm, 2)));

	}
}