using System;

namespace FuzzyLogic.Terms
{
	public class PiTerm : Term
	{
		public PiTerm(string name) 
			: base(name)
		{
		}

		public PiTerm(string name,double a, double b, double c, double d)
			: this(name)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}

		public double a { get; set; }

		public double b { get; set; }

		public double c { get; set; }

		public double d { get; set; }

		public override double GetValue(double x)
		{
			if (x <= a)
			{
				return 0;
			}

			if (a <= x && x <= (a + b) / 2)
			{
				return 2 * Math.Pow( (x - a) / (b - a), 2);
			}

			if ((a + b) / 2 <= x && x <= b)
			{
				return 1 - 2 * Math.Pow((x - b) / (b - a), 2);
			}

			if (b <= x && x <= c)
			{
				return 1;
			}

			if (c <= x && x <= (c + d)/2)
			{
				return 1 - 2*Math.Pow( (x-c)/(d-c) , 2);
			}

			if ((c + d)/2 <= x && x <= d)
			{
				return 2*Math.Pow((x-d)/(d-c), 2);
			}

			if (x >= d)
			{
				return 0;
			}

			throw new InvalidOperationException("Ни одно из условий не выполнилось.");
		}

		
	}
}