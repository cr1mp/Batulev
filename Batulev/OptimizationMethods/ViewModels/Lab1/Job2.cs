using System;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public class Job2 : BindableBase
	{
		private IKernelLink _kernelLink;
		private readonly MathKernel _mathKernel;
		protected string _xMin;
		protected string _xMax;
		private BitmapImage _img;
		private System.Drawing.Image imgToSave;
		private string _x1;

		public Job2(IKernelLink kernelLink, MathKernel mathKernel)
		{
			_kernelLink = kernelLink;
			_mathKernel = mathKernel;

			SaveCommand = new DelegateCommand(Save, () => _img != null);

		}

		private void Save()
		{
			var saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Image Files (*.bmp)|*.bmp;";
			if (saveFileDialog.ShowDialog() == true)
			{
				imgToSave.Save(saveFileDialog.FileName);
			}
		}

		public string xMin
		{
			get { return _xMin; }
			set
			{
				_xMin = value;
				OnPropertyChanged(nameof(Img));
			}
		}

		public string xMax
		{
			get { return _xMax; }
			set
			{
				_xMax = value;
				OnPropertyChanged(nameof(Img));
			}
		}

		public DelegateCommand SaveCommand { get; }

		public string X1
		{
			get { return _x1; }
			set
			{
				_x1 = value;
				OnPropertyChanged(nameof(Result));
			}
		}

		public string Result
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(_x1))
				{
					if (_mathKernel.IsComputing)
					{
						_mathKernel.Abort();
					}
					else
					{
						_mathKernel.Compute($"Erf[{_x1}]");

						return (string)_mathKernel.Result;
					}
				}
				return string.Empty;
			}
		}

		public BitmapImage Img
		{
			get
			{
				if (_mathKernel.IsComputing)
				{
					_mathKernel.Abort();
				}
				if (!string.IsNullOrWhiteSpace(_xMin) && !string.IsNullOrWhiteSpace(_xMax))
				{
					_mathKernel.GraphicsHeight = 350; //и ядро для работы с ним
					_mathKernel.GraphicsWidth = 350;
					_mathKernel.Compute(GetFunc());

					if (_mathKernel.Graphics.Length > 0)
					{
						_img = GetImg(_mathKernel.Graphics[1]);
					}
				}
				else
				{
					_img = null;
				}

				SaveCommand.RaiseCanExecuteChanged();
				return _img;
			}
		}

		protected virtual string GetFunc()
		{
			return $"Plot[Erf[x], {{x, {_xMin}, {_xMax}}}, PlotRange -> Full]";
		}

		private BitmapImage GetImg(System.Drawing.Image img)
		{
			imgToSave = img;

			if (img != null)
			{
				BitmapImage bi = new BitmapImage();

				bi.BeginInit();

				MemoryStream ms = new MemoryStream();

				img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

				ms.Seek(0, SeekOrigin.Begin);

				bi.StreamSource = ms;

				bi.EndInit();
				return bi;
			}
			return null;
		}
	}
}