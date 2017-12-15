using System;

namespace RetroReactor.Shared.Game
{
	public class RRGameMap
	{
		public const int ARR_WIDTH = 12;
		public const int ARR_HEIGHT = 20;

		private readonly Random _rand;
		private readonly HexRotation[,] _offsetGrid; // "even-r" horizontal layout

		public RRGameMap(int seed)
		{
			_rand = new Random(seed);
			_offsetGrid = new HexRotation[ARR_WIDTH, ARR_HEIGHT];
		}

		public void Init()
		{
			for (int x = 0; x < ARR_WIDTH; x++)
			{
				for (int y = 0; y < ARR_HEIGHT; y++)
				{
					_offsetGrid[x, y] = (HexRotation) _rand.Next(0, 6);
				}
			}
		}

		public HexRotation GetRotation(int x, int y)
		{
			return _offsetGrid[x, y];
		}
	}
}
