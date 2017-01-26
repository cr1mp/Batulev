using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab4
{
	public class Lab4ViewModel : BindableBase
	{
		public Lab4ViewModel(MathKernel mathKernel)
		{
			NeuralNetwork = new NeuralNetwork(mathKernel);
			GeneticAlgorithm = new GeneticAlgorithm(NeuralNetwork);
		}

		public NeuralNetwork NeuralNetwork { get; set; }
		public GeneticAlgorithm GeneticAlgorithm { get; set; }
	}

}
