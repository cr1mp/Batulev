using System;

namespace FuzzyLogic.Terms
{
	public class TrianTerm : Term
	{
		public TrianTerm(string name)
			: base(name)
		{
		}

		public TrianTerm(string name, double a, double b, double c)
			: this(name)
		{
			this.a = a;
			this.b = b;
			this.c = c;
		}

		public double a { get; set; }
		public double b { get; set; }
		public double c { get; set; }

		public override double GetValue(double x)
		{
			if (x <= a)
			{
				return 0;
			}

			if (a <= x && x <= b)
			{
				return (x - a) / (b - a);
			}

			if (b <= x && x <= c)
			{
				return (c - x) / (c - b);
			}

			if (c <= x)
			{
				return 0;
			}

			throw new InvalidOperationException("Ни одно из условий не выполнилось.");
		}
	}
}