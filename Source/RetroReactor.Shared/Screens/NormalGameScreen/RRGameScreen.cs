using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoSAMFramework.Portable;
using MonoSAMFramework.Portable.BatchRenderer;
using MonoSAMFramework.Portable.DebugTools;
using MonoSAMFramework.Portable.GameMath.Geometry;
using MonoSAMFramework.Portable.Input;
using MonoSAMFramework.Portable.Screens;
using MonoSAMFramework.Portable.Screens.Background;
using MonoSAMFramework.Portable.Screens.Entities;
using MonoSAMFramework.Portable.Screens.HUD;
using MonoSAMFramework.Portable.Screens.ViewportAdapters;
using RetroReactor.Shared.Game;
using RetroReactor.Shared.Resources;
using RetroReactor.Shared.Screens.Common;
using RetroReactor.Shared.Screens.NormalGameScreen.Background;
using RetroReactor.Shared.Screens.NormalGameScreen.HUD;

namespace RetroReactor.Shared.Screens.NormalGameScreen
{
	public class RRGameScreen : GameScreen
	{
		public const int VIEW_WIDTH  = RRConstants.VIEW_WIDTH;
		public const int VIEW_HEIGHT = RRConstants.VIEW_HEIGHT;

		protected override EntityManager CreateEntityManager() => new RREntityManager(this);
		protected override GameHUD CreateHUD() => new RRGameHUD(this);
		protected override GameBackground CreateBackground() => new RRGameBackground(this);
		protected override SAMViewportAdapter CreateViewport() => new TolerantBoxingViewportAdapter(Game.Window, Graphics, VIEW_WIDTH, VIEW_HEIGHT);
		protected override DebugMinimap CreateDebugMinimap() => new StandardDebugMinimapImplementation(this, 192, 32);
		protected override FRectangle CreateMapFullBounds() => new FRectangle(0, 0, 1, 1);
		protected override float GetBaseTextureScale() => Textures.DEFAULT_TEXTURE_SCALE_F;

		private readonly RRGameMap _gameMap;
		private readonly HexGameRenderer _gameRenderer;

		public RRGameScreen(MonoSAMGame game, GraphicsDeviceManager gdm) : base(game, gdm)
		{
			_gameMap = new RRGameMap(0);
			_gameRenderer = new HexGameRenderer();

			Initialize();
		}

		private void Initialize()
		{
#if DEBUG
			DebugUtils.CreateShortcuts(this);
			DebugDisp = DebugUtils.CreateDisplay(this);
#endif

			_gameMap.Init();
		}

		protected override void OnUpdate(SAMTime gameTime, InputState istate)
		{
#if DEBUG
			DebugDisp.IsEnabled = DebugSettings.Get("DebugTextDisplay");
			DebugDisp.Scale = 0.65f;
#endif

			//
		}

		protected override void OnDrawGame(IBatchRenderer sbatch)
		{
			_gameRenderer.Draw(sbatch, _gameMap);
		}

		protected override void OnDrawHUD(IBatchRenderer sbatch)
		{
			//
		}
	}
}
