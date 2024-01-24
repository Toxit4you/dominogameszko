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
		}
	}
}
