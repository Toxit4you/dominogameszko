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
		Player player = new Player();
		Image domino1 = new Image();
		Image domino2 = new Image();
		int[,] asd = new int[2, 2];
		int[,] dsa = new int[2, 2];
		public GameWindow()
		{
			InitializeComponent();
			InitializeMainGrid();

			asd[0, 0] = 1;
			asd[1, 0] = 2;
			asd[0, 1] = 0;
			asd[1, 1] = 0;

			
			dsa[0, 0] = 1;
			dsa[1, 0] = 2;
			dsa[0, 1] = 0;
			dsa[1, 1] = 0;
		}
        private void InitializeMainGrid()
        {
			int[,] matrix = new int[50, 50];
			for (int i = 0; i < 50; i++)
			{
				for (int j = 0; j < 50; j++)
				{
					matrix[i, j] = i;
				}
			}
			var height = new GridLength(20);
			var width = new GridLength(20);
			for (int i = 0; i < 50; i++)
			{
				Table.RowDefinitions.Add(new RowDefinition() { Height = height });
				Table.ColumnDefinitions.Add(new ColumnDefinition() { Width = width });
			}



			for (int i = 0; i < 50; i++)
			{
				for (int j = 0; j < 50; j++)
				{
					Border border = new Border
					{
						Background = new SolidColorBrush(Colors.White), // Default background
						Height = 20,
						Width = 20,
						HorizontalAlignment = HorizontalAlignment.Center,
						VerticalAlignment = VerticalAlignment.Center
					};
					border.MouseEnter += Border_MouseEnter;
					border.MouseLeave += Border_MouseLeave;

					TextBlock textBlock = new TextBlock
					{
						Text = matrix[i, j].ToString(),
						HorizontalAlignment = HorizontalAlignment.Center,
						VerticalAlignment = VerticalAlignment.Center,
						Foreground = new SolidColorBrush(Colors.White)
					};
					textBlock.Visibility = Visibility.Hidden;

					int locali = i, localj = j;
					border.MouseDown += (sender, e) =>
					{
						PlaceDomino(locali, localj, Table);
					};

					border.Child = textBlock; // Add TextBlock to Border
					Grid.SetRow(border, i);
					Grid.SetColumn(border, j);
					Table.Children.Add(border);
				}
			}
		}
		private void Border_MouseEnter(object sender, MouseEventArgs e)
		{
			if (sender is Border border)
			{
				border.Background = new SolidColorBrush(Colors.LightGray); // Change on hover
			}
		}
		private void Border_MouseLeave(object sender, MouseEventArgs e)
		{
			if (sender is Border border)
			{
				border.Background = new SolidColorBrush(Colors.White); // Revert on mouse leave
			}
		}

		private void PlaceDomino(int row,int column,Grid table)
		{
			Image imageBox = new Image();
			imageBox.Width = 30;
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.UriSource = new Uri("pack://application:,,,/dominogameszko;component/Resources/domino_1.png", UriKind.RelativeOrAbsolute);
			bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
			bitmapImage.EndInit();
			bitmapImage.Freeze();
			imageBox.Source = bitmapImage;

			Grid.SetRow(imageBox, row);
			Grid.SetColumn(imageBox, column);
			table.Children.Add(imageBox);
		}

        private void turnButton_Click(object sender, RoutedEventArgs e)
        {
			turningMachine.Children.Clear();
			
			player.turn(dsa);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine(dsa[i,j]);
                }
            }
			for (int i = 0; i < 2; i++)
			{
                for (int j = 0; j < 2; j++)
                {
                    if (dsa[i,j]==asd[0,0])
                    {
						SetImageSource(domino1, asd[0, 0]);
						Grid.SetColumn(domino1, j);
						Grid.SetRow(domino1, i);
						turningMachine.Children.Add(domino1);
					}
					else if(dsa[i, j] == asd[1, 0])
                    {
						SetImageSource(domino2, asd[1, 0]);
						Grid.SetColumn(domino2, j);
						Grid.SetRow(domino2, i);
						turningMachine.Children.Add(domino2);
					}
                }
			}
		}
		private void SetImageSource(Image domino,int number)
        {
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.UriSource = new Uri("pack://application:,,,/dominogameszko;component/Resources/domino_" + number + ".png", UriKind.RelativeOrAbsolute);
			bitmapImage.EndInit();
			domino.Source = bitmapImage;
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			SetImageSource(domino1, 1);
			Grid.SetColumn(domino1, 0);
			Grid.SetRow(domino1, 0);
			turningMachine.Children.Add(domino1);


			SetImageSource(domino2, 2);
			Grid.SetColumn(domino2, 0);
			Grid.SetRow(domino2, 1);
			turningMachine.Children.Add(domino2);
		}
    }
}
