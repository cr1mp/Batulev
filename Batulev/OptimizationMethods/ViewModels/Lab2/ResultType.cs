namespace OptimizationMethods.ViewModels.Lab2
{
	class ResultType
	{
		public string Id => $"{k1}{k2}";

		public string FMax { get; set; }
		public string FMin { get; set; }

		public string Max { get; set; }
		public string Min { get; set; }

		public int k1 { get; set; }
		public int k2 { get; set; }
	}
}