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
            #region Create_datagrid
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
				Table.RowDefinitions.Add(new RowDefinition() {Height = height});
				Table.ColumnDefinitions.Add(new ColumnDefinition() {Width = width });
			}

			
			for (int i = 0; i < 50; i++)
			{
				for (int j = 0; j < 50; j++)
				{
					TextBlock textBlock = new TextBlock
					{
						Text = matrix[i, j].ToString(),
						Height = 20,
						Width = 20,
						HorizontalAlignment = HorizontalAlignment.Center,
						VerticalAlignment = VerticalAlignment.Center,
					};
					textBlock.Foreground = new SolidColorBrush(Colors.White);
					int locali = i;
					int localj = j;
					textBlock.MouseDown += (sender, e) =>
					{
						PlaceDomino(locali, localj, Table);
					};
					Grid.SetRow(textBlock, i);
					Grid.SetColumn(textBlock, j);

					Table.Children.Add(textBlock);
				}
			}


			/*Style hoverStyle = (Style)this.FindResource("DataGridHover");
			Table.CellStyle = hoverStyle;
			Canvas.SetLeft(Table, 480);*/
			#endregion

			asd[0, 0] = 1;
			asd[1, 0] = 2;
			asd[0, 1] = 0;
			asd[1, 1] = 0;

			
			dsa[0, 0] = 1;
			dsa[1, 0] = 2;
			dsa[0, 1] = 0;
			dsa[1, 1] = 0;
		}

        private void PlaceDomino(int row,int column,Grid table)
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
