using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathematicalMethodsAnalysisComplexSystems.ViewModel;

namespace MathematicalMethodsAnalysisComplexSystems.Views
{
	/// <summary>
	/// Interaction logic for MathematicalMethodsAnalysisComplexSystemsMainView.xaml
	/// </summary>
	public partial class MMACSMainView : UserControl
	{
		public MMACSMainView(MMACSMainViewModel model)
		{
			InitializeComponent();
			DataContext = model;
		}
	}
}
