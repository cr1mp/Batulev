using System;
using System.IO;

namespace OptimizationMethods.ViewModels.Lab4
{
	public static class NeuralNetworkHelper
	{
		#region РАБОТА С ФАЙЛАМИ

		//Записать нейронную сеть на диск
		public static void WriteArray(int param, NeuralNetwork neuralNetwork)
		{
			string path = "";
			switch (param)
			{
				case 1: path = @"VhTroyki.txt"; break;
				case 2: path = @"Fsr.txt"; break;
				case 3: path = @"W01.txt"; break;
				case 4: path = @"W1.txt"; break;
				case 5: path = @"W2.txt"; break;
				case 6: path = @"W02.txt"; break;
				case 7: path = @"min.txt"; break;
				case 8: path = @"max.txt"; break;
			}
			using (StreamWriter sw = new StreamWriter(path))
			{
				switch (param)
				{
					case 1:
						for (int i = 0; i < 100; i++)
							for (int j = 0; j < 3; j++)
								sw.WriteLine(String.Format("{0}|{1}|{2}|", i, j, neuralNetwork.InputVariables[i, j]));
						break;
					case 2:
						for (int i = 0; i < 100; i++)
							sw.WriteLine(String.Format("{0}|{1}|", i, neuralNetwork.Fsr[i]));
						break;
					case 3:
						for (int i = 0; i < 7; i++)
							sw.WriteLine(String.Format("{0}|{1}|", i, neuralNetwork.W01[i]));
						break;
					case 4:
						for (int i = 0; i < 7; i++)
							for (int j = 0; j < 3; j++)
								sw.WriteLine(String.Format("{0}|{1}|{2}|", i, j, neuralNetwork.W1[i, j]));
						break;
					case 5:
						for (int i = 0; i < 7; i++)
							sw.WriteLine(String.Format("{0}|{1}|", i, neuralNetwork.W2[i]));
						break;
					case 6:
						sw.WriteLine(neuralNetwork.W02);
						break;
					case 7:
						sw.WriteLine(neuralNetwork.min);
						break;
					case 8:
						sw.WriteLine(neuralNetwork.max);
						break;
				}
				sw.Close();
			}
		}

		//Загрузить нейронную сеть
		public static void ReadArray(int param, NeuralNetwork neuralNetwork)
		{
			string path = "";
			switch (param)
			{
				case 1: path = @"VhTroyki.txt"; break;
				case 2: path = @"Fsr.txt"; break;
				case 3: path = @"W01.txt"; break;
				case 4: path = @"W1.txt"; break;
				case 5: path = @"W2.txt"; break;
				case 6: path = @"W02.txt"; break;
				case 7: path = @"min.txt"; break;
				case 8: path = @"max.txt"; break;
			}
			if (File.Exists(path))
			{
				int x, y;

				StreamReader sr = File.OpenText(path);

				while (!sr.EndOfStream)
				{
					// считываем строку с файла
					string line = sr.ReadLine();
					// разделяем на массив из считанной строки до символа
					string[] fields = line.Split('|');

					switch (param)
					{
						case 1:
							x = Convert.ToInt32(fields[0]);
							y = Convert.ToInt32(fields[1]);
							neuralNetwork.InputVariables[x, y] = Convert.ToDouble(fields[2]);
							break;
						case 2:
							x = Convert.ToInt32(fields[0]);
							neuralNetwork.Fsr[x] = Convert.ToDouble(fields[1]);
							break;
						case 3:
							x = Convert.ToInt32(fields[0]);
							neuralNetwork.W01[x] = Convert.ToDouble(fields[1]);
							break;
						case 4:
							x = Convert.ToInt32(fields[0]);
							y = Convert.ToInt32(fields[1]);
							neuralNetwork.W1[x, y] = Convert.ToDouble(fields[2]);
							break;
						case 5:
							x = Convert.ToInt32(fields[0]);
							neuralNetwork.W2[x] = Convert.ToDouble(fields[1]);
							break;
						case 6:
							neuralNetwork.W02 = Convert.ToDouble(fields[0]);
							break;
						case 7:
							neuralNetwork.min = Convert.ToDouble(fields[0]);
							break;
						case 8:
							neuralNetwork.max = Convert.ToDouble(fields[0]);
							break;
					}
				}
				sr.Close();
			}
			else
			{
				throw new Exception("Нет файла");
			}
		}

		#endregion

		public static void DisplayTrainingNeuralNetwork(System.Windows.Forms.DataGridView dataGridView1,
			double[,] VhTroyki,
			double[] Fsr,
			NeuralNetwork neuralNetwork)
		{
			dataGridView1.RowCount = 100;

			for (int i = 0; i < 100; i++)
			{
				dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(i + 1);
				dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(VhTroyki[i, 0]);
				dataGridView1.Rows[i].Cells[2].Value = Convert.ToString(VhTroyki[i, 1]);
				dataGridView1.Rows[i].Cells[3].Value = Convert.ToString(VhTroyki[i, 2]);
				dataGridView1.Rows[i].Cells[4].Value = Convert.ToString(Math.Round(Fsr[i], 3));
				dataGridView1.Rows[i].Cells[5].Value = Convert.ToString(
					Math.Round(NeuralNetworkEvaluate(
						VhTroyki[i, 0], VhTroyki[i, 1], VhTroyki[i, 2], neuralNetwork) * 2 * (neuralNetwork.max - neuralNetwork.min) + neuralNetwork.min, 3));
			}
		}

		/* Вычисление целевой функции генетического алгоритма с помощью нейронной сети*/
		public static double NeuralNetworkEvaluate(double x1, double x2, double x3, NeuralNetwork neuralNetwork)
		{
			double[] layer1 = new double[50];
			double NetworkOutput;

			NetworkOutput = 0;
			for (int i = 1; i < 50; i++)
			{
				layer1[i] = ActivationFunction(neuralNetwork.W01[i] + neuralNetwork.W1[i, 0] * x1 + neuralNetwork.W1[i, 1] * x2 + neuralNetwork.W1[i, 2] * x3);
				NetworkOutput += layer1[i] * neuralNetwork.W2[i];
			}
			return ActivationFunction(NetworkOutput + neuralNetwork.W02);
			//Получили прямой проход нейросети
		}

		//Сигмоидальная (логистическая) функция активации нейронной сети
		public static double ActivationFunction(double x)
		{
			return (1 / (1 + Math.Exp(-x)));
		}
	}
}