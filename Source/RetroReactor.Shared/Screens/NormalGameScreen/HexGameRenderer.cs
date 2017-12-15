using Microsoft.Xna.Framework;
using MonoSAMFramework.Portable.BatchRenderer;
using MonoSAMFramework.Portable.Extensions;
using MonoSAMFramework.Portable.GameMath;
using MonoSAMFramework.Portable.GameMath.Geometry;
using RetroReactor.Shared.Game;
using RetroReactor.Shared.Resources;

namespace RetroReactor.Shared.Screens.NormalGameScreen
{
	// https://www.redblobgames.com/grids/hexagons/
	public class HexGameRenderer
	{
		private const float HEX_WIDTH     = (int)((RRConstants.VIEW_WIDTH - 64f) / RRGameMap.ARR_WIDTH);
		private const float HEX_HEIGHT    = HEX_WIDTH / (FloatMath.SQRT_THREE / 2f);
		private const float HEX_SIZE      = HEX_HEIGHT / 2f;
		private const float HEX_INNER_RAD = HEX_WIDTH / 2f;
		private const float HEX_DIST_VERT = (HEX_HEIGHT * 3f) / 4f;
		private const float HEX_DIST_HORZ = HEX_WIDTH;

		private static readonly FPoint[] COORDS_CORNER =
		{
			new FPoint(0,              -HEX_HEIGHT / 2).AsScaled(0.95f),
			new FPoint(-HEX_WIDTH / 2, -HEX_HEIGHT / 4).AsScaled(0.95f),
			new FPoint(-HEX_WIDTH / 2, +HEX_HEIGHT / 4).AsScaled(0.95f),
			new FPoint(0,              +HEX_HEIGHT / 2).AsScaled(0.95f),
			new FPoint(+HEX_WIDTH / 2, +HEX_HEIGHT / 4).AsScaled(0.95f),
			new FPoint(+HEX_WIDTH / 2, -HEX_HEIGHT / 4).AsScaled(0.95f),
		};

		private const float GRID_WIDTH  = RRGameMap.ARR_WIDTH * HEX_WIDTH + (HEX_WIDTH  / 2);
		private const float GRID_HEIGHT = HEX_HEIGHT + HEX_DIST_VERT * (RRGameMap.ARR_HEIGHT - 1);

		private readonly float _offsetX;
		private readonly float _offsetY;

		public HexGameRenderer()
		{
			_offsetX = (RRConstants.VIEW_WIDTH  - GRID_WIDTH)  / 2;
			_offsetY = (RRConstants.VIEW_HEIGHT - GRID_HEIGHT) / 2;
		}

		public void Draw(IBatchRenderer sbatch, RRGameMap map, float intermedRotation)
		{
			sbatch.FillRectangle(new FPoint(_offsetX, _offsetY), new FSize(GRID_WIDTH, GRID_HEIGHT), Color.Gray);

			for (int x = 0; x < RRGameMap.ARR_WIDTH; x++)
			{
				for (int y = 0; y < RRGameMap.ARR_HEIGHT; y++)
				{
					var elem = map.Get(x, y);
					var center = GetHexCenter(x, y);

					for (int i = 0; i < 6; i++)
					{
						sbatch.DrawLine(COORDS_CORNER[i].AsScaled(0.9f).WithOrigin(center), COORDS_CORNER[(i+1)%6].AsScaled(0.9f).WithOrigin(center), Color.Black, 2f);
					}

					sbatch.DrawCentered(Textures.TexCircle, center, HEX_INNER_RAD*1, HEX_INNER_RAD*1, Color.White);

					for (int i = 0; i < 6; i++)
					{
						if (!elem.Arms[i]) continue;

						var rot1 = (i + (int) elem.PreviousRotation) % 6;
						var rot2 = (i + (int) elem.NextRotation) % 6;

						if (rot1 == 5 && rot2 == 0) rot2 = 6;
						if (rot1 == 0 && rot2 == 5) rot1 = 6;

						var rotx = (rot1 + (rot2 - rot1) * intermedRotation) * FloatMath.RAD_POS_060 + FloatMath.RAD_POS_030;

						var point = center + new Vector2(0, -HEX_INNER_RAD).Rotate(rotx);

						sbatch.DrawLine(center, point, Color.Blue, 12);
						sbatch.DrawCentered(Textures.TexCircle, point, 16, 16, Color.DarkGray);
					}

				}
			}

		}

		private FPoint GetHexCenter(int x, int y)
		{
			var hx = (HEX_WIDTH  / 2f) + x * HEX_DIST_HORZ + ((y + 1) % 2) * (HEX_WIDTH / 2f);
			var hy = (HEX_HEIGHT / 2f) + y * HEX_DIST_VERT;

			return new FPoint(_offsetX + hx, _offsetY + hy);
		}

		public DPoint? GetHexagonUnderMouse(FPoint pointerPos)
		{
			//TODO optimize & better & w/e
			for (int x = 0; x < RRGameMap.ARR_WIDTH; x++)
			{
				for (int y = 0; y < RRGameMap.ARR_HEIGHT; y++)
				{
					var center = GetHexCenter(x, y);

					if ((center - pointerPos).LengthSquared() < (HEX_INNER_RAD * HEX_INNER_RAD))
					{
						return new DPoint(x, y);
					}

				}
			}

			return null;
		}
	}
}
