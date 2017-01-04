using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job1 : BaseJob
	{

		private string _in1;
		private string _job2;

		public Job1( MathKernel mathKernel)
			:base(mathKernel)
		{
			Unknown = new ObservableCollection<Point>();

		}

		public ObservableCollection<Point> Unknown { get; set; }

		private void Evaluate(object obj, PropertyChangedEventArgs e)
		{
			_job2 = _in1;

			foreach (var point in Unknown)
			{
				if (!string.IsNullOrWhiteSpace(point.Value))
				{
					_job2 = _job2.Replace(point.Name, point.Value);
				}
			}
			OnPropertyChanged(nameof(Result2));
		}

		public string In1
		{
			get { return _in1; }
			set
			{
				_in1 = value;
				_job2 = string.Empty;

				Unknown.Clear();
				GetUnknownVariables(_in1).ForEach(p =>
				{
					var point = new Point { Name = p };
					point.PropertyChanged += Evaluate;
					Unknown.Add(point);
				});

				OnPropertyChanged(nameof(Unknown));
				OnPropertyChanged(nameof(Result1));
				OnPropertyChanged(nameof(Result2));
			}
		}

		public string Result1 => Simplify(_in1);

		public string Result2 => Simplify(_job2);

		private string Simplify(string input)
		{
			return Compute($"Simplify[{input}]");
		}

		
	}

	public class Point : BindableBase
	{
		private string _value;
		public string Name { get; set; }

		public string Value
		{
			get { return _value; }
			set { SetProperty(ref _value, value); }
		}
	}
}