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
using OptimizationMethods.ViewModels;

namespace OptimizationMethods.Views.Labs
{
	/// <summary>
	/// Interaction logic for Lab1View.xaml
	/// </summary>
	public partial class Lab1View : UserControl
	{
		public Lab1View(Lab1ViewModel model)
		{
			InitializeComponent();
			this.DataContext = model;
		}
	}
}
