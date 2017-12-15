using System;
using MonoSAMFramework.Portable.GameMath;
using MonoSAMFramework.Portable.GameMath.Geometry;
using RetroReactor.Shared.Resources;

namespace RetroReactor.Shared.Game
{
	public class RRGameMap
	{
		public const int ARR_WIDTH = RRConstants.HEXGRID_WIDTH;
		public const int ARR_HEIGHT = RRConstants.HEXGRID_HEIGHT;

		public class GridElem
		{
			public bool WasActive = false;
			public bool Active = false;

			public CircularDirection RotationDirection;

			public HexRotation PreviousRotation;
			public HexRotation NextRotation;

			public readonly bool[] Arms = new bool[6];
		}

		private static readonly DPoint[,] DIRECTIONS =
		{
			{ new DPoint(+1, -1), new DPoint(0, -1),  new DPoint(-1, 0), new DPoint(0, +1),  new DPoint(+1, +1), new DPoint(+1, 0), },
			{ new DPoint(0, -1),  new DPoint(-1, -1), new DPoint(-1, 0), new DPoint(-1, +1), new DPoint(0, +1),  new DPoint(+1, 0), },
		};

		private readonly Random _rand;
		private readonly GridElem[,] _offsetGrid; // "even-r" horizontal layout

		public RRGameMap(int seed)
		{
			_rand = new Random(seed);
			_offsetGrid = new GridElem[ARR_WIDTH, ARR_HEIGHT];
		}

		public void Init()
		{
			for (int x = 0; x < ARR_WIDTH; x++)
			{
				for (int y = 0; y < ARR_HEIGHT; y++)
				{
					_offsetGrid[x, y] = new GridElem
					{
						NextRotation = (HexRotation) _rand.Next(0, 6),
						Arms =
						{
							[(int) HexRotation.DEG_000] = true,
							[(int) HexRotation.DEG_060] = false,
							[(int) HexRotation.DEG_120] = _rand.Next(0, 8)>=1,
							[(int) HexRotation.DEG_180] = false,
							[(int) HexRotation.DEG_240] = _rand.Next(0, 2)==1,
							[(int) HexRotation.DEG_300] = _rand.Next(0, 5)==1
						},
						Active = false,
					};
					_offsetGrid[x, y].PreviousRotation = _offsetGrid[x, y].NextRotation;
				}
			}
		}

		public GridElem Get(int xx, int yy)
		{
			return _offsetGrid[xx, yy];
		}

		public void Rotate(int xx, int yy, CircularDirection dir)
		{
			_offsetGrid[xx, yy].Active = true;

			_offsetGrid[xx, yy].PreviousRotation = _offsetGrid[xx, yy].NextRotation;

			if (dir == CircularDirection.CW)  _offsetGrid[xx, yy].NextRotation = (HexRotation) (((int)_offsetGrid[xx, yy].NextRotation - 1 + 6) % 6);
			if (dir == CircularDirection.CCW) _offsetGrid[xx, yy].NextRotation = (HexRotation) (((int)_offsetGrid[xx, yy].NextRotation + 1 + 6) % 6);

			_offsetGrid[xx, yy].RotationDirection = dir;
		}

		public bool Step()
		{
			for (int x = 0; x < ARR_WIDTH; x++)
			{
				for (int y = 0; y < ARR_HEIGHT; y++)
				{
					_offsetGrid[x, y].WasActive = _offsetGrid[x, y].Active;
					_offsetGrid[x, y].Active = false;
					_offsetGrid[x, y].PreviousRotation = _offsetGrid[x, y].NextRotation;
				}
			}

			bool anyActive = false;

			for (int x = 0; x < ARR_WIDTH; x++)
			{
				for (int y = 0; y < ARR_HEIGHT; y++)
				{
					if (!_offsetGrid[x, y].WasActive) continue;

					for (int rdd = 0; rdd < 6; rdd++)
					{
						if (!_offsetGrid[x, y].Arms[rdd]) continue;

						var dd = (rdd + (int)_offsetGrid[x, y].NextRotation) % 6;

						var nb = GetNeighbor(x, y, (HexRotation)dd);

						if (nb.X < 0) continue;
						if (nb.Y < 0) continue;
						if (nb.X >= ARR_WIDTH) continue;
						if (nb.Y >= ARR_HEIGHT) continue;

						if (!_offsetGrid[nb.X, nb.Y].Arms[(dd + 3 + 6 + (int) _offsetGrid[nb.X, nb.Y].PreviousRotation) % 6]) continue;

						_offsetGrid[nb.X, nb.Y].Active = true;

						if (_offsetGrid[x, y].RotationDirection == CircularDirection.CCW)
						{
							_offsetGrid[nb.X, nb.Y].Active = true;
							_offsetGrid[nb.X, nb.Y].RotationDirection = CircularDirection.CW;
							_offsetGrid[nb.X, nb.Y].NextRotation = (HexRotation)(((int)_offsetGrid[nb.X, nb.Y].PreviousRotation - 1 + 6) % 6);
						}
						else if (_offsetGrid[x, y].RotationDirection == CircularDirection.CW)
						{
							_offsetGrid[nb.X, nb.Y].Active = true;
							_offsetGrid[nb.X, nb.Y].RotationDirection = CircularDirection.CCW;
							_offsetGrid[nb.X, nb.Y].NextRotation = (HexRotation)(((int)_offsetGrid[nb.X, nb.Y].PreviousRotation + 1 + 6) % 6);
						}

						anyActive = true;
					}

				}
			}

			return anyActive;
		}

		private DPoint GetNeighbor(int xx, int yy, HexRotation dd)
		{
			var dir = DIRECTIONS[yy % 2, (int) dd];

			return new DPoint(xx + dir.X, yy + dir.Y);
		}
	}
}
