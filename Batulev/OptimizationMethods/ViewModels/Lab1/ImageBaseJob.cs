using System;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Prism.Commands;
using Wolfram.NETLink;

namespace OptimizationMethods.ViewModels.Lab1
{
	public abstract class ImageBaseJob:BaseJob
	{
		protected string _xMin;
		protected string _xMax;
		protected System.Drawing.Image imgToSave;

		public ImageBaseJob(MathKernel mathKernel) 
			: base(mathKernel)
		{
			SaveCommand = new DelegateCommand(Save, () => imgToSave != null);
		}

		public DelegateCommand SaveCommand { get; }

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

		public BitmapImage Img
		{
			get
			{
				BitmapImage _img = null;

				if (_mathKernel.IsComputing)
				{
					_mathKernel.Abort();
				}
				if (!string.IsNullOrWhiteSpace(_xMin) && !string.IsNullOrWhiteSpace(_xMax))
				{
					_mathKernel.GraphicsHeight = 350; //и ядро для работы с ним
					_mathKernel.GraphicsWidth = 350;
					_mathKernel.Compute($"Plot[{GetGraphicFunc()}, {{x, {_xMin}, {_xMax}}}, PlotRange -> Full]");

					if (_mathKernel.Graphics.Length > 0)
					{
						_img = GetImg(_mathKernel.Graphics[1]);
					}
				}

				SaveCommand.RaiseCanExecuteChanged();
				return _img;
			}
		}

		protected abstract string GetGraphicFunc();

		private void Save()
		{
			var saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Image Files (*.bmp)|*.bmp;";
			if (saveFileDialog.ShowDialog() == true)
			{
				imgToSave.Save(saveFileDialog.FileName);
			}
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