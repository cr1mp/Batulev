using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using OfficeOpenXml;
using Prism.Mvvm;


namespace MathematicalMethodsAnalysisComplexSystems.ViewModel
{
	/// <summary>
	/// http://iasa.org.ua/lections/tpr/mgua/polinom.htm описание алгоритма.
	/// http://www.machinelearning.ru/wiki/images/6/65/DM_L3-2_part1.pdf подробное описание.
	/// Мы будем использовать Алгоритмы МГУА - COMBI
	/// </summary>
	public partial class MMACSMainViewModel : BindableBase
	{
		/// <summary>
		/// a0,ai,aj,aij,aii,ajj
		/// </summary>
		private const int _coefficientArrayCount = 6;

		private int _rowCount;
		private int _columnCount;

		private int _trainingRowsCount;
		private int _checkingRowsCount;

		private int _numberOfInputVariables;
		private int _columnIndexOutputVariable;

		private double[][] _originalTable;
		private double[][] _dataTraining;
		private double[][] _dataChecking;

		private void SelectFile()
		{
			var openFileDialog = new OpenFileDialog();
			var showDialog = openFileDialog.ShowDialog();
			if (showDialog != null && showDialog.Value)
			{
				var fileName = openFileDialog.FileName;

				if (fileName.Last() != 'x')
				{
					fileName = Convert(fileName);
				}


				var newFile = new FileInfo(fileName);

				var pck = new ExcelPackage(newFile);

				var firstWorksheet = pck.Workbook.Worksheets.First();

				_rowCount = firstWorksheet.Dimension.End.Row;
				_columnCount = firstWorksheet.Dimension.End.Column;

				_numberOfInputVariables = _columnIndexOutputVariable = _columnCount - 1;

				_originalTable = new double[_rowCount][];

				for (var rowNum = 0; rowNum < _rowCount; rowNum++)
				{
					_originalTable[rowNum] = new double[_columnCount];


					var wsRow = firstWorksheet.Cells[rowNum + 1, 1, rowNum + 1, _columnCount];

					foreach (var cell in wsRow)
					{
						double r;
						double.TryParse(cell.Text, out r);

						_originalTable[rowNum][cell.Start.Column - 1] = r;
					}
				}

				OriginalMatrix = Fill(_originalTable);
				OnPropertyChanged(nameof(OriginalMatrix));

				ConfigTrainingData();

				ConfigCheckingData();

				Selection.Clear();
				OnPropertyChanged(nameof(Selection));

				MGUA();
			}
		}

		public string Convert(string fileName)
		{
			var app = new Microsoft.Office.Interop.Excel.Application();
			var wb = app.Workbooks.Open(fileName);
			var newName = fileName + "x";
			if (File.Exists(newName))
			{
				return newName;
			}
			wb.SaveAs(Filename: newName, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
			wb.Close();
			app.Quit();
			return newName;
		}

		private void ConfigTrainingData()
		{
			_trainingRowsCount = (int)Math.Ceiling((double)_rowCount / 2);

			_dataTraining = new double[_trainingRowsCount][];

			for (int i = 0; i < _trainingRowsCount; ++i)
			{
				_dataTraining[i] = new double[_columnCount];

				for (int j = 0; j < _columnCount; j++)
				{
					_dataTraining[i][j] = _originalTable[i][j];
				}
			}

			TrainingData = Fill(_dataTraining);
			OnPropertyChanged(nameof(TrainingData));
		}

		private void ConfigCheckingData()
		{
			_checkingRowsCount = _rowCount - _trainingRowsCount;

			_dataChecking = new double[_checkingRowsCount][];

			for (int i = _trainingRowsCount; i < _rowCount; ++i)
			{
				var currentCheckingRow = i - _trainingRowsCount;

				_dataChecking[currentCheckingRow] = new double[_columnCount];

				for (int j = 0; j < _columnCount; j++)
				{
					_dataChecking[currentCheckingRow][j] = _originalTable[i][j];
				}
			}

			CheckingData = Fill(_dataChecking);
			OnPropertyChanged(nameof(CheckingData));
		}

		private DataView Fill(double[][] source)
		{
			var rowCount = source.Length;
			var columnCount = (rowCount > 0 ? source[0].Length : 0);

			var tbl = new DataTable();

			for (int c = 0; c < columnCount; c++)
			{
				tbl.Columns.Add(new DataColumn(c.ToString()));
			}

			for (int r = 0; r < rowCount; r++)
			{
				var row = tbl.NewRow();
				for (int c = 0; c < columnCount; c++)
				{
					row[c] = source[r][c];
				}
				tbl.Rows.Add(row);
			}

			return tbl.DefaultView;
		}


		private void MGUA()
		{
			// Критерий минимума на текущем шаге.
			double epsilonSquaredCurrent = 0;
			// Критерий минимума на предыдущем шаге.
			double epsilonSquaredPrev = double.MaxValue;
			// Текущая итерация.
			int iteration = 1;

			int[] sortingFuncsNumbers = new int[6];
			double[][] coefArr = new double[6][];

			// Проверяются величины критерия точности для наилучших моделей
			// текущего и предыдущего ряда селекции.
			while (true)
			{
				if (iteration != 1)
				{
					double[][] buf = new double[_trainingRowsCount][];

					for (int i = 0; i < _trainingRowsCount; ++i)
					{
						buf[i] = new double[_columnCount];

						for (int j = 0; j < 4; j++)
						{
							buf[i][j] = MathixHelper.getNewValue(sortingFuncsNumbers[j], coefArr, _dataTraining[i]);
						}

						buf[i][4] = _dataTraining[i][4];
						_dataTraining[i] = buf[i];
					}
				}

				#region Создаем и инициализируем матрицы
				double[][] matr1 = new double[_trainingRowsCount][];
				double[][] matr2 = new double[_trainingRowsCount][];
				double[][] matr3 = new double[_trainingRowsCount][];
				double[][] matr4 = new double[_trainingRowsCount][];
				double[][] matr5 = new double[_trainingRowsCount][];
				double[][] matr6 = new double[_trainingRowsCount][];
				for (int i = 0; i < _trainingRowsCount; ++i)
				{
					matr1[i] = new double[_coefficientArrayCount];
					matr2[i] = new double[_coefficientArrayCount];
					matr3[i] = new double[_coefficientArrayCount];
					matr4[i] = new double[_coefficientArrayCount];
					matr5[i] = new double[_coefficientArrayCount];
					matr6[i] = new double[_coefficientArrayCount];
				}
				#endregion

				for (int i = 0; i < _trainingRowsCount; ++i)
				{
					var x1 = _dataTraining[i][0];
					var x2 = _dataTraining[i][1];
					var x3 = _dataTraining[i][2];
					var x4 = _dataTraining[i][3];
					var y = _dataTraining[i][4];

					//x1 x2 x1^2 x2^2 x1*x2 y
					matr1[i][0] = x1;
					matr1[i][1] = x2;
					matr1[i][2] = x1 * x1;
					matr1[i][3] = x2 * x2;
					matr1[i][4] = x1 * x2;
					matr1[i][5] = y;

					//x1 x3 x1^2 x3^2 x1*x3 y
					matr2[i][0] = x1;
					matr2[i][1] = x3;
					matr2[i][2] = x1 * x1;
					matr2[i][3] = x3 * x3;
					matr2[i][4] = x1 * x3;
					matr2[i][5] = y;

					//x1 x4 x1^2 x4^2 x1*x4 y
					matr3[i][0] = x1;
					matr3[i][1] = x4;
					matr3[i][2] = x1 * x1;
					matr3[i][3] = x4 * x4;
					matr3[i][4] = x1 * x4;
					matr3[i][5] = y;

					//x2 x3 x2^2 x3^2 x2*x3 y
					matr4[i][0] = x2;
					matr4[i][1] = x3;
					matr4[i][2] = x2 * x2;
					matr4[i][3] = x3 * x3;
					matr4[i][4] = x2 * x3;
					matr4[i][5] = y;

					//x2 x4 x2^2 x4^2 x2*x4 y
					matr5[i][0] = x2;
					matr5[i][1] = x4;
					matr5[i][2] = x2 * x2;
					matr5[i][3] = x4 * x4;
					matr5[i][4] = x2 * x4;
					matr5[i][5] = y;

					//x3 x4 x3^2 x4^2 x3*x4 y
					matr6[i][0] = x3;
					matr6[i][1] = x4;
					matr6[i][2] = x3 * x3;
					matr6[i][3] = x4 * x4;
					matr6[i][4] = x3 * x4;
					matr6[i][5] = y;
				}

				double[][] design1 = MathixHelper.Design(matr1);
				double[][] design2 = MathixHelper.Design(matr2);
				double[][] design3 = MathixHelper.Design(matr3);
				double[][] design4 = MathixHelper.Design(matr4);
				double[][] design5 = MathixHelper.Design(matr5);
				double[][] design6 = MathixHelper.Design(matr6);

				double[] coef1 = MathixHelper.Solve(design1);
				double[] coef2 = MathixHelper.Solve(design2);
				double[] coef3 = MathixHelper.Solve(design3);
				double[] coef4 = MathixHelper.Solve(design4);
				double[] coef5 = MathixHelper.Solve(design5);
				double[] coef6 = MathixHelper.Solve(design6);

				coefArr[0] = coef1;
				coefArr[1] = coef2;
				coefArr[2] = coef3;
				coefArr[3] = coef4;
				coefArr[4] = coef5;
				coefArr[5] = coef6;

				if (iteration != 1)
				{
					double[][] buf = new double[_checkingRowsCount][];
					for (int i = 0; i < _checkingRowsCount; ++i)
					{
						buf[i] = new double[_columnCount];

						for (int j = 0; j < _numberOfInputVariables; j++)
						{
							buf[i][j] = MathixHelper.getNewValue(sortingFuncsNumbers[j], coefArr, _dataChecking[i]);
						}
						buf[i][_columnIndexOutputVariable] = _dataChecking[i][_columnIndexOutputVariable];

						_dataChecking[i] = buf[i];
					}
				}

				double epsSquare1 = 0;
				double epsSquare2 = 0;
				double epsSquare3 = 0;
				double epsSquare4 = 0;
				double epsSquare5 = 0;
				double epsSquare6 = 0;

				for (int i = 0; i < _checkingRowsCount; i++)
				{
					var x1 = _dataChecking[i][0];
					var x2 = _dataChecking[i][1];
					var x3 = _dataChecking[i][2];
					var x4 = _dataChecking[i][3];
					var y = _dataChecking[i][_columnIndexOutputVariable];

					epsSquare1 += Math.Pow(y - MathixHelper.solveFunction(coef1, x1, x2), 2);
					epsSquare2 += Math.Pow(y - MathixHelper.solveFunction(coef2, x1, x3), 2);
					epsSquare3 += Math.Pow(y - MathixHelper.solveFunction(coef3, x1, x4), 2);
					epsSquare4 += Math.Pow(y - MathixHelper.solveFunction(coef4, x2, x3), 2);
					epsSquare5 += Math.Pow(y - MathixHelper.solveFunction(coef5, x2, x4), 2);
					epsSquare6 += Math.Pow(y - MathixHelper.solveFunction(coef6, x3, x4), 2);
				}


				double[] epsArray = new double[_coefficientArrayCount];

				epsArray[0] = (double)epsSquare1 / _checkingRowsCount;
				epsArray[1] = (double)epsSquare2 / _checkingRowsCount;
				epsArray[2] = (double)epsSquare3 / _checkingRowsCount;
				epsArray[3] = (double)epsSquare4 / _checkingRowsCount;
				epsArray[4] = (double)epsSquare5 / _checkingRowsCount;
				epsArray[5] = (double)epsSquare6 / _checkingRowsCount;


				for (int i = 0; i < _coefficientArrayCount; i++)
				{
					sortingFuncsNumbers[i] = i;
				}

				SortArray(epsArray, sortingFuncsNumbers);

				epsilonSquaredPrev = epsilonSquaredCurrent;
				epsilonSquaredCurrent = epsArray[0];

				FillResult(iteration, epsilonSquaredCurrent, coefArr, epsArray);

				if (epsilonSquaredCurrent > epsilonSquaredPrev && iteration > 1)
				{
					break;
				}

				iteration++;
			}
		}

		private void FillResult(int iteration, double epsilonSquaredCurrent, double[][] coefArr, double[] epsArray)
		{
			var sel = new ResultViewModel
			{
				Step = iteration.ToString(),
				Value = epsilonSquaredCurrent.ToString(),
			};

			sel.Functions.Add($"f1(x1,x2) = {coefArr[0][0]:#.##} + {coefArr[0][1]:#.##} * x1 + {coefArr[0][2]:#.##} * x2 + {coefArr[0][3]:#.##} * x1^2 + {coefArr[0][4]:#.##} * x2^2 + {coefArr[0][5]:#.##} * x1 * x2");
			sel.Functions.Add($"f2(x1,x3) = {coefArr[1][0]:#.##} + {coefArr[1][1]:#.##} * x1 + {coefArr[1][2]:#.##} * x3 + {coefArr[1][3]:#.##} * x1^2 + {coefArr[1][4]:#.##} * x3^2 + {coefArr[1][5]:#.##} * x1 * x3");
			sel.Functions.Add($"f3(x1,x4) = {coefArr[2][0]:#.##} + {coefArr[2][1]:#.##} * x1 + {coefArr[2][2]:#.##} * x4 + {coefArr[2][3]:#.##} * x1^2 + {coefArr[2][4]:#.##} * x4^2 + {coefArr[2][5]:#.##} * x1 * x4");
			sel.Functions.Add($"f4(x2,x3) = {coefArr[3][0]:#.##} + {coefArr[3][1]:#.##} * x2 + {coefArr[3][2]:#.##} * x3 + {coefArr[3][3]:#.##} * x2^2 + {coefArr[3][4]:#.##} * x3^2 + {coefArr[3][5]:#.##} * x2 * x3");
			sel.Functions.Add($"f5(x2,x4) = {coefArr[4][0]:#.##} + {coefArr[4][1]:#.##} * x2 + {coefArr[4][2]:#.##} * x4 + {coefArr[4][3]:#.##} * x2^2 + {coefArr[4][4]:#.##} * x4^2 + {coefArr[4][5]:#.##} * x2 * x4");
			sel.Functions.Add($"f6(x3,x4) = {coefArr[5][0]:#.##} + {coefArr[5][1]:#.##} * x3 + {coefArr[5][2]:#.##} * x4 + {coefArr[5][3]:#.##} * x3^2 + {coefArr[5][4]:#.##} * x4^2 + {coefArr[5][5]:#.##} * x3 * x4");

			sel.Eps.Add($"Eps^2 = {epsArray[0]:#.##}");
			sel.Eps.Add($"Eps^2 = {epsArray[1]:#.##}");
			sel.Eps.Add($"Eps^2 = {epsArray[2]:#.##}");
			sel.Eps.Add($"Eps^2 = {epsArray[3]:#.##}");
			sel.Eps.Add($"Eps^2 = {epsArray[4]:#.##}");
			sel.Eps.Add($"Eps^2 = {epsArray[5]:#.##}");

			Selection.Add(sel);
		}

		private static void SortArray(double[] epsArray, int[] sortingFuncsNumbers)
		{
			for (int i = 0; i < _coefficientArrayCount; i++)
			{
				for (int j = 0; j < _coefficientArrayCount - 1; j++)
				{
					if (epsArray[j] >= epsArray[j + 1])
					{
						double buf1 = epsArray[j];
						epsArray[j] = epsArray[j + 1];
						epsArray[j + 1] = buf1;

						int buf2 = sortingFuncsNumbers[j];
						sortingFuncsNumbers[j] = sortingFuncsNumbers[j + 1];
						sortingFuncsNumbers[j + 1] = buf2;
					}
				}
			}
		}
	}


}
