namespace FuzzyLogic.FuzzySystem.Mamdani
{
	/// <summary>
	/// Or evaluating method
	/// </summary>
	public enum OrMethod
	{
		/// <summary>
		/// Maximum: max(a, b)
		/// </summary>
		Max,
		/// <summary>
		/// Probabilistic OR: a + b - a * b
		/// </summary>
		Probabilistic
	}
}