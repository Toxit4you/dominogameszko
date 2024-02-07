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
using System.Windows.Shapes;
using System.Dynamic;

namespace dominogameszko
{
	/// <summary>
	/// Interaction logic for GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window
	{
		public GameWindow()
		{
			InitializeComponent();

			int[,] matrix = new int[50, 50];
			for (int i = 0; i < 50; i++)
			{
				for (int j = 0; j < 50; j++)
				{
					matrix[i, j] = i;
				}
			}

			var rows = new List<dynamic>();

			for (int i = 0; i < 50; i++)
			{
				dynamic row = new ExpandoObject();
				var rowDict = (IDictionary<string, object>)row;

				for (int j = 0; j < 50; j++)
				{
					rowDict[$"Column{j}"] = matrix[i, j];
				}

				rows.Add(row);
			}

			for (int i = 0; i < 50; i++)
			{
				Table.Columns.Add(new DataGridTextColumn
				{
					Header = $"Column {i}",
					Binding = new Binding($"Column{i}")
				});
			}

			Table.ItemsSource = rows;

			Style hoverStyle = (Style)this.FindResource("DataGridHover");
			Table.CellStyle = hoverStyle;
			Canvas.SetLeft(Table, 480);
		}

		private void Table_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			System.Windows.Controls.Image imageBox = new System.Windows.Controls.Image();
			imageBox.Width = 30;
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.UriSource = new Uri("pack://application:,,,/dominogameszko;component/Resources/domino_1.png", UriKind.RelativeOrAbsolute);
			bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
			bitmapImage.EndInit();
			bitmapImage.Freeze();

			imageBox.Source = bitmapImage;
			System.Windows.Point position = e.GetPosition(this);
			double xPosition = position.X - 480;
			xPosition = Math.Floor(xPosition / 20);
			xPosition *= 20;
			Canvas.SetLeft(imageBox, xPosition + 480);

			double yPosition = position.Y;
			yPosition = Math.Floor(yPosition / 19);
			yPosition *= 19;
			Canvas.SetTop(imageBox, yPosition);
			Canvas.SetZIndex(imageBox, 10);
			imageBox.Width = 20.5;
			imageBox.Height = 20.5;
			GameWindowCanvas.Children.Add(imageBox);
		}

	}
}
