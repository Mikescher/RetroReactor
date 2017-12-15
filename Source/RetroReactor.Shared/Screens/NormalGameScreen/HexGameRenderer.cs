﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoSAMFramework.Portable.BatchRenderer;
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
		private const float HEX_DIST_VERT = (HEX_HEIGHT * 3f) / 4f;
		private const float HEX_DIST_HORZ = HEX_WIDTH;

		private static readonly FPoint[] COORDS =
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

		public void Draw(IBatchRenderer sbatch, RRGameMap map)
		{
			sbatch.FillRectangle(new FPoint(_offsetX, _offsetY), new FSize(GRID_WIDTH, GRID_HEIGHT), Color.Gray);

			for (int x = 0; x < RRGameMap.ARR_WIDTH; x++)
			{
				for (int y = 0; y < RRGameMap.ARR_HEIGHT; y++)
				{
					var rot = map.GetRotation(x, y);
					var center = GetHexCenter(x, y);

					for (int i = 0; i < 6; i++)
					{
						sbatch.DrawLine(COORDS[i].AsScaled(0.9f).WithOrigin(center), COORDS[(i+1)%6].AsScaled(0.9f).WithOrigin(center), Color.Black, 2f);
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
	}
}
