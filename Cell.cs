namespace dotnetcore_gameoflife
{
	partial class World

	{
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

			bool UpperLeft()
			{
				if (Row == 1 && Column == 1)
				{
					return World.Grid[World.RowsUpperBound, World.ColumnsUpperBound];
				}
				else if (Row == 1)
				{
					return World.Grid[World.RowsUpperBound, Column - 1];
				}
				else if (Column == 1)
				{
					return World.Grid[Row - 1, World.ColumnsUpperBound];
				}

				return World.Grid[Row - 1, Column - 1];
			}
			bool Upper()
			{
				if (Row == 1)
				{
					return World.Grid[World.RowsUpperBound, Column];
				}

				return World.Grid[Row - 1, Column];
			}
			bool UpperRight()
			{
				if (Row == 1 && Column == World.ColumnsUpperBound)
				{
					return World.Grid[World.RowsUpperBound, 1];
				}
				else if (Row == 1)
				{
					return World.Grid[World.RowsUpperBound, Column + 1];
				}
				else if (Column == World.ColumnsUpperBound)
				{
					return World.Grid[Row - 1, 1];
				}

				return World.Grid[Row - 1, Column + 1];
			}
			bool Left()
			{
				if (Column == 1) 
				{
					return World.Grid[Row, World.ColumnsUpperBound];
				}

				return World.Grid[Row, Column - 1];
			}
			bool Right()
			{
				if (Column == World.ColumnsUpperBound)
				{
					return World.Grid[Row, 1];
				}
				
				return World.Grid[Row, Column + 1];
			}
			bool LowerRight()
			{
				if (Row == World.RowsUpperBound && Column == World.ColumnsUpperBound)
				{
					return World.Grid[1, 1];
				}
				else if (Row == World.RowsUpperBound)
				{
					return World.Grid[1, Column + 1];
				}
				else if (Column == World.ColumnsUpperBound)
				{
					return World.Grid[Row + 1, 1];
				}

				return World.Grid[Row + 1, Column + 1];
			}
			bool Lower()
			{
				if (Row == World.RowsUpperBound)
				{
					return World.Grid[1, Column];
				}
				
				return World.Grid[Row + 1, Column];
			}
			bool LowerLeft()
			{
				if (Row == World.RowsUpperBound && Column == 1)
				{
					return World.Grid[1, World.ColumnsUpperBound];
				}
				else if (Column == 1)
				{
					return World.Grid[Row + 1, World.ColumnsUpperBound];
				}
				else if (Row == World.RowsUpperBound)
				{
					return World.Grid[1, Column - 1];
				}

				return World.Grid[Row + 1, Column - 1];
			}

			public bool Tick()
			{
				if (!IsAlive && NumberOfNeighbours() == 3)
				{
					IsAlive = true;
					return true;
				}

				if ((IsAlive && NumberOfNeighbours() == 2) || (IsAlive && NumberOfNeighbours() == 3))
				{
					return true;
				}
				IsAlive = false;
				return false;
			}
		}

	}
}