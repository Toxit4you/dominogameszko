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
		Pack Dominopacks = new Pack();
//<<<<<<< HEAD

		Board board = new Board();
		Domino domino = new Domino();
		
//=======
		
		
//>>>>>>> 9e5783f675511b4fdf9e941359a3c42fbd6bac6f
		public GameWindow()
		{
			InitializeComponent();
			InitializeMainGrid();
			dsa[0, 0] = 6;
			dsa[1, 0] = 2;
			dsa[0, 1] = 0;
			dsa[1, 1] = 0;
			domino.sides = dsa;
			domino.vertical = false;
			player.selected_D = domino;
			asd[0, 0] = 6;
			asd[1, 0] = 2;
			asd[0, 1] = 0;
			asd[1, 1] = 0;
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
						Background = new SolidColorBrush(Colors.White),
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
						//PlaceDomino(locali, localj, Table);

						player.place(locali, localj, board);
					};

					border.Child = textBlock;
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
				border.Background = new SolidColorBrush(Colors.LightGray);
			}
		}
		private void Border_MouseLeave(object sender, MouseEventArgs e)
		{
			if (sender is Border border)
			{
				border.Background = new SolidColorBrush(Colors.White);
			}
		}
        public void Generate_Domino()
        {
            int generation_exception = 0;
            Dominopacks.all_Dominoes = new List<Domino>();
            for (int i = 1; i < 7; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    if (i > generation_exception && j > generation_exception)
                    {
                        int[,] side = new int[2, 2];
                        side[0, 0] = i;
                        side[1, 0] = j;
                        Domino seged = new Domino();
                        seged.sides = side;
                        seged.vertical = true;

                        Dominopacks.all_Dominoes.Add(seged);

                    }


                }
                generation_exception += 1;

            }
        }
		private string sourcebyvalue(int value)
        {
			//0= empty, 1-6=domino value,7=placable,8=ASZ,9=BAS,10=BSZ,11=BFS,12=FSZ,13=JFS,14=JSZ,15=JAS
			switch (value)
            {
				case 0:
					return "Asztal_K.png";
					break;
				case 1:
					return "domino_1.png";
					break;
				case 2:
					return "domino_2.png";
					break;
				case 3:
					return "domino_3.png";
					break;
				case 4:
					return "domino_4.png";
					break;
				case 5:
					return "domino_5.png";
					break;
				case 6:
					return "domino_6.png";
					break;
				case 8:
					return "Asztal_ASZ.png";
					break;
				case 9:
					return "Asztal_BAS.png";
					break;
				case 10:
					return "Asztal_BSZ.png";
					break;
				case 11:
					return "Asztal_BFS.png";
					break;
				case 12:
					return "Asztal_FSZ.png";
					break;
				case 13:
					return "Asztal_JFS.png";
					break;
				case 14:
					return "Asztal_JSZ.png";
					break;
				case 15:
					return "Asztal_JAS.png";
					break;
				default:
					return "toothless.jpg";
			}
        }
		private void refreshGrid(Board board, Grid table)
        {
            for (int i = 0; i < board.values.GetLength(0); i++)
            {
                for (int j = 0; j < board.values.GetLength(1); j++)
                {
					if (board.values[j, i] != 0 && board.values[j, i] < 7)
                    {
						PlaceDomino(i, j, table);
                    }
                }
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
			
			player.turn(domino);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine(domino.sides[i,j]);
                }
            }
			for (int i = 0; i < 2; i++)
			{
                for (int j = 0; j < 2; j++)
                {
					if (domino.sides[i,j]==asd[0,0])
                    {
                        Console.WriteLine("belepett");
						SetImageSource(domino1, asd[0, 0]);
						Grid.SetColumn(domino1, j);
						Grid.SetRow(domino1, i);
						turningMachine.Children.Add(domino1);
					}
					else if(domino.sides[i, j] == asd[1, 0])
                    {
						Console.WriteLine("belepett2");
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
