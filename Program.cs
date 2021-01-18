using System;
using System.Collections.Generic;

namespace dotnetcore_gameoflife
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Missing seed file..");
				Console.WriteLine("pass path to seed file as an argument");
				Environment.Exit(-1);
			}

			World world = new World(args[0]);
            while(true)
            {
                Console.Clear();
                Console.WriteLine($"Grid size: {World.Grid.GetUpperBound(0).ToString()} x {World.Grid.GetUpperBound(1)}");
                Console.WriteLine($"Generation Nr: {World.GenerationNumber}");
                Console.WriteLine(world.DisplayGrid());
                world.NextGeneration();
                System.Threading.Thread.Sleep(500);
            }
		}
	}

	class World
	{
		const char ALIVE = '*';
		const char DEAD = ' ';
		public World(string pathToSeedFile)
		{
			string[] lines = System.IO.File.ReadAllLines(pathToSeedFile);

			int rows = lines.Length;
			int cols = lines[0].Length;

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
        public static int GenerationNumber = 0;
		public static bool[,] Grid { set; get; }
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
            bool[,] tempGrid = new bool[Grid.GetUpperBound(0)+1, Grid.GetUpperBound(1)+1];
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

		protected class Cell
		{
			public Cell(int row, int column, bool isAlive)
			{
				Column = column;
				Row = row;
				IsAlive = isAlive;
			}
			public int Row { private set; get; }
			public int Column { private set; get; }
			public bool IsAlive { private set; get; }

			private bool[] Neighbours { set; get; }
			public int NumberOfNeighbours()
			{
				Neighbours = new bool[8] { UpperLeft(), Upper(), UpperRight(), Left(), Right(), LowerRight(), Lower(), LowerLeft() };
				int numberOfNeighbours = 0;
				foreach (bool neighbours in Neighbours)
				{
					if (neighbours) numberOfNeighbours++;
				}
				return numberOfNeighbours;
			}

			bool UpperLeft() => World.Grid[Row - 1, Column - 1];
			bool Upper() => World.Grid[Row - 1, Column];
			bool UpperRight() => World.Grid[Row - 1, Column + 1];
			bool Left()
            {
                if (Column == 1) {return World.Grid[Row, World.Grid.GetUpperBound(1)-1];}
                return World.Grid[Row, Column - 1];
            }
			bool Right()
            {
                if (Column == World.Grid.GetUpperBound(1)-1) {return World.Grid[Row, 1];}
                return World.Grid[Row, Column + 1];
            }
			bool LowerRight() => World.Grid[Row + 1, Column + 1];
			bool Lower() => World.Grid[Row + 1, Column];
			bool LowerLeft() => World.Grid[Row + 1, Column - 1];

            public bool Tick()
            {
                if(!IsAlive && NumberOfNeighbours() == 3)
                { 
                    IsAlive = true;
                    return true;
                }

                if((IsAlive && NumberOfNeighbours() == 2) || (IsAlive && NumberOfNeighbours() == 3))
                {
                    return true;
                }
                return false;
            }
		}
	}
}
