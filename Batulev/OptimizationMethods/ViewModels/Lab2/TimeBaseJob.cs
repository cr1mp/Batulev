using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab2
{
	public abstract class TimeBaseJob: BaseJob
	{
		protected string _time;

		public string Time => _time;

		public TimeBaseJob(MathKernel mathKernel)
			: base(mathKernel)
		{
		}

		protected void EvaluateTime(Action a)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			a();
			stopWatch.Stop();
			var ts = stopWatch.Elapsed;
			_time = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";

			OnPropertyChanged(nameof(Time));
		}

		
	}
}
