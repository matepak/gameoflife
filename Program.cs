using System;

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
			while (true)
			{
				Console.Clear();
				Console.WriteLine($"Grid size: {World.RowsUpperBound} x {World.ColumnsUpperBound}");
				Console.WriteLine($"Generation Nr: {World.GenerationNumber}");
				Console.WriteLine(world.DisplayGrid());
				world.NextGeneration();
				System.Threading.Thread.Sleep(100);
			}
		}
	}
}
