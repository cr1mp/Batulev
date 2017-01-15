namespace FuzzyAnalysisAndModeling.ViewModel
{
	public static class ExtensionString
	{
		public static string ReplacePoint(this string s)
		{
			return s.Replace(".", ",");
		}
	}
}