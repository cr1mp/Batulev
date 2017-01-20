using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace MathematicalMethodsAnalysisComplexSystems.ViewModel
{
	public partial class MMACSMainViewModel
	{
		public MMACSMainViewModel()
		{
			SelectFileCommand = new DelegateCommand(SelectFile);
			Selection = new List<ResultViewModel>();
		}

		public DataView OriginalMatrix { get; set; }

		public DataView TrainingData { get; set; }
		public DataView CheckingData { get; set; }

		public ICommand SelectFileCommand { get; set; }

		public List<ResultViewModel> Selection { get; set; }

	}
}
