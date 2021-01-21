using System.Collections.Generic;

namespace dotnetcore_gameoflife
{
	partial class World
	{
		const char ALIVE = '*';
		const char DEAD = ' ';
		public World(string pathToSeedFile)
		{
			string[] lines = System.IO.File.ReadAllLines(pathToSeedFile);

			int rows = lines.Length;
			int cols = lines[0].Length;

			RowsUpperBound = rows - 2;
			ColumnsUpperBound = cols - 2;

			Grid = new bool[rows, cols];

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < cols; col++)
				{
					if (lines[row][col] == ALIVE)
					{
						Grid[row, col] = true;
						Cells.Add(new Cell(row, col, isAlive: true));
					}
					else if (lines[row][col] == DEAD)
					{
						Cells.Add(new Cell(row, col, isAlive: false));
					}
				}
			}
		}
		public static int RowsUpperBound {get; private set;}
		public static int ColumnsUpperBound {get; private set;}
		public static int GenerationNumber {get; private set;} = 0;
		private static bool[,] Grid { set; get; }
		private List<Cell> Cells { set; get; } = new List<Cell>();

		public string GetCells()
		{
			string cells = "";
			foreach (Cell cell in Cells)
			{
				cells += $"Row: {cell.Row},  Column: {cell.Column}, IsAlive: {cell.IsAlive}";
				cells += "\n";
				cells += $" no of neighbours: {cell.NumberOfNeighbours()}";
				cells += "\n";
			}

			return cells;
		}

		public void NextGeneration()
		{
			bool[,] tempGrid = new bool[Grid.GetUpperBound(0) + 1, Grid.GetUpperBound(1) + 1];
			foreach (Cell cell in Cells)
			{
				tempGrid[cell.Row, cell.Column] = cell.Tick();
			}
			Grid = tempGrid;
			GenerationNumber++;
		}

		public string DisplayGrid()
		{
			string grid = "";
			for (int row = 0; row < Grid.GetUpperBound(0); row++)
			{
				for (int col = 0; col < Grid.GetUpperBound(1); col++)
				{
					if (Grid[row, col] == true) grid += ALIVE;
					else grid += DEAD;
				}
				grid += "\n";

			}
			return grid;
		}
	}

}