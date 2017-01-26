using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace OptimizationMethods.ViewModels.Lab4
{
	public class GeneticAlgorithm : BindableBase
	{
		private readonly NeuralNetwork _neuralNetwork;
		/* Массивы генетического алгоритма */
		double[,] Parents = new double[300, 4];
		double[,] Children = new double[300, 3];
		double[,] MutatedChildren = new double[300, 4];

		/* Вспомогательные переменные*/
		double max, min;
		Random randObj = new Random();
		int CountMutations;
		private int _numberOfPeriods;

		public GeneticAlgorithm(NeuralNetwork neuralNetwork)
		{
			_neuralNetwork = neuralNetwork;
			NumberOfPeriods = 700;
			StartGeneticAlgorithmCommand = new DelegateCommand(Start);
		}

		public ICommand StartGeneticAlgorithmCommand { get; set; }
		public int NumberOfPeriods
		{
			get { return _numberOfPeriods; }
			set { SetProperty(ref _numberOfPeriods, value); }
		}
		public string Time { get; set; }
		public string F { get; set; }
		public string X3 { get; set; }
		public string X2 { get; set; }
		public string X1 { get; set; }

		public void Start()
		{
			var myStopWatch = new Stopwatch();

			myStopWatch.Start();
			int m = NumberOfPeriods;

			StartPopulation();
			for (int i = 1; i <= m; i++)
			{
				Crossingover();
				Mutation();
				Sorting();
				Selection();
			}
			myStopWatch.Stop();
			TimeSpan ts = myStopWatch.Elapsed;

			Time = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

			X1 = Convert.ToString(Parents[0, 0]);
			X2 = Convert.ToString(Parents[0, 1]);
			X3 = Convert.ToString(Parents[0, 2]);
			F = Convert.ToString(Math.Round(
				NeuralNetworkHelper.NeuralNetworkEvaluate(Parents[0, 0], Parents[0, 1], Parents[0, 2], _neuralNetwork) * 2 * (max - min) + min, 3));

			OnPropertyChanged(nameof(Time));
			OnPropertyChanged(nameof(X1));
			OnPropertyChanged(nameof(X2));
			OnPropertyChanged(nameof(X3));
			OnPropertyChanged(nameof(F));
		}

		void StartPopulation()
		{
			double x1, x2, x3;
			for (int i = 0; i < 300; i++)
			{
				x1 = (double)randObj.Next(600) / (double)100;
				x2 = (double)randObj.Next(600) / (double)100;
				x3 = (double)randObj.Next(600) / (double)100;
				if ((x1 + x2 + x3 < 17) && (2 * x1 + x2 + 3 * x3 < 51))
				{
					Parents[i, 0] = x1;
					Parents[i, 1] = x2;
					Parents[i, 2] = x3;
				}
			}
		}

		void Crossingover()
		{
			int nParent1, nParent2;
			double[] Parents1 = new double[3];
			double[] Parents2 = new double[3];
			for (int i = 0; i < 300; i++)
			{
				nParent1 = randObj.Next(299) + 1;
				nParent2 = randObj.Next(299) + 1;
				while (nParent1 == nParent2)
					nParent2 = randObj.Next(299) + 1;
				for (int j = 0; j < 3; j++)
				{
					Parents1[j] = Parents[nParent1, j];
					Parents2[j] = Parents[nParent2, j];
				}
				Children[i, 0] = Parents1[0];
				Children[i, 1] = Parents1[1];
				Children[i, 2] = Parents2[2];
			}
		}

		void Mutation()
		{
			int k = 0;
			for (int i = 0; i < 300; i++)
			{
				Children[i, randObj.Next(1) + 1] = Parents[randObj.Next(299) + 1, randObj.Next(1) + 1];
				if ((Children[k, 0] + Children[k, 1] + Children[k, 2]) <= 40 && 2 * Children[k, 0] + Children[k, 1] + 3 * Children[k, 2] <= 40)
				{
					MutatedChildren[k, 0] = Children[i, 0];
					MutatedChildren[k, 1] = Children[i, 1];
					MutatedChildren[k, 2] = Children[i, 2];
					k++;
				}
			}
			CountMutations = k;
			for (int i = 0; i < k; i++)
			{
				MutatedChildren[i, 3] =
					NeuralNetworkHelper.NeuralNetworkEvaluate(MutatedChildren[i, 0], MutatedChildren[i, 1], MutatedChildren[i, 2], _neuralNetwork);
			}
		}

		void Selection()
		{
			int k, proc70;
			double x1, x2, x3, buf;

			for (int i = 0; i < 300; i++)
			{
				Parents[i, 3] = NeuralNetworkHelper.NeuralNetworkEvaluate(Parents[i, 0], Parents[i, 1], Parents[i, 2], _neuralNetwork);
			}
			for (int i = 0; i < 300; i++)
				for (int j = 0; j < 299; j++)
				{
					if (Parents[j, 3] < Parents[j + 1, 3])
					{
						buf = Parents[j, 3];
						Parents[j, 3] = Parents[j + 1, 3];
						Parents[j + 1, 3] = buf;

						buf = Parents[j, 0];
						Parents[j, 0] = Parents[j + 1, 0];
						Parents[j + 1, 0] = buf;

						buf = Parents[j, 1];
						Parents[j, 1] = Parents[j + 1, 1];
						Parents[j + 1, 1] = buf;

						buf = Parents[j, 2];
						Parents[j, 2] = Parents[j + 1, 2];
						Parents[j + 1, 2] = buf;
					}
				}
			proc70 = (int)Math.Round(CountMutations * 0.75, 0);
			for (int i = 0; i < proc70; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					Parents[i, j] = MutatedChildren[i, j];
				}
			}
			//Дополняем популяцию до 300
			k = proc70 + 1;
			while (k < 300)
			{
				x1 = (double)randObj.Next(600) / (double)100;
				x2 = (double)randObj.Next(600) / (double)100;
				x3 = (double)randObj.Next(600) / (double)100;
				if ((x1 + x2 + x3 < 41) && (2 * x1 + x2 + 3 * x3 < 41))
				{
					Parents[k, 0] = x1;
					Parents[k, 1] = x2;
					Parents[k, 2] = x3;
					k++;
				}
			}
		}

		void Sorting()
		{
			double buf;
			for (int i = 0; i < CountMutations; i++)
				for (int j = 0; j < CountMutations - 1; j++)
				{
					if (MutatedChildren[j, 3] < MutatedChildren[j + 1, 3])
					{
						buf = MutatedChildren[j, 3];
						MutatedChildren[j, 3] = MutatedChildren[j + 1, 3];
						MutatedChildren[j + 1, 3] = buf;

						buf = MutatedChildren[j, 0];
						MutatedChildren[j, 0] = MutatedChildren[j + 1, 0];
						MutatedChildren[j + 1, 0] = buf;

						buf = MutatedChildren[j, 1];
						MutatedChildren[j, 1] = MutatedChildren[j + 1, 1];
						MutatedChildren[j + 1, 1] = buf;

						buf = MutatedChildren[j, 2];
						MutatedChildren[j, 2] = MutatedChildren[j + 1, 2];
						MutatedChildren[j + 1, 2] = buf;
					}
				}
		}
	}
}