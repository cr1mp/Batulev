using System;
using System.Collections.Generic;
using FuzzyLogic.FuzzySystem.Mamdani.Enums;

namespace FuzzyLogic.FuzzySystem.Mamdani
{
	internal class CompositeMembershipFunction
	{
		List<Func<double, double>> _mfs = new List<Func<double, double>>();
		MfCompositionType _composType;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="composType">Membership functions composition type</param>
		public CompositeMembershipFunction(MfCompositionType composType)
		{
			_composType = composType;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="composType">Membership functions composition type</param>
		/// <param name="mf1">Membership function 1</param>
		/// <param name="mf2">Membership function 2</param>
		public CompositeMembershipFunction(
			MfCompositionType composType,
			Func<double, double> mf1,
			Func<double, double> mf2) : this(composType)
		{
			_mfs.Add(mf1);
			_mfs.Add(mf2);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="composType">Membership functions composition type</param>
		/// <param name="mfs">Membership functions</param>
		public CompositeMembershipFunction(
			MfCompositionType composType,
			List<Func<double, double>> mfs)
			: this(composType)
		{
			_mfs = mfs;
		}

		/// <summary>
		/// List of membership functions
		/// </summary>
		public List<Func<double, double>> MembershipFunctions
		{
			get { return _mfs; }
		}

		/// <summary>
		/// Evaluate value of the membership function
		/// </summary>
		/// <param name="x">Argument (x axis value)</param>
		/// <returns></returns>
		public double GetValue(double x)
		{
			if (_mfs.Count == 0)
			{
				return 0.0;
			}
			else if (_mfs.Count == 1)
			{
				return _mfs[0](x);
			}
			else
			{
				double result = _mfs[0](x);
				for (int i = 1; i < _mfs.Count; i++)
				{
					result = Compose(result, _mfs[i](x));
				}
				return result;
			}
		}

		double Compose(double val1, double val2)
		{
			switch (_composType)
			{
				case MfCompositionType.Max:
					return Math.Max(val1, val2);
				case MfCompositionType.Min:
					return Math.Min(val1, val2);
				case MfCompositionType.Prod:
					return val1 * val2;
				case MfCompositionType.Sum:
					return val1 + val2;
				default:
					throw new Exception("Internal exception.");
			}
		}
	}
}