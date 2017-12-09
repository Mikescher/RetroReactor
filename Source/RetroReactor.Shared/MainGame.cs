using System;
using Microsoft.Xna.Framework;
using MonoSAMFramework.Portable;
using MonoSAMFramework.Portable.LogProtocol;
using MonoSAMFramework.Portable.Screens;
using MonoSAMFramework.Portable.Sound;
using RetroReactor.Shared.Resources;
using RetroReactor.Shared.Screens.NormalGameScreen;

namespace GridDominance.Shared
{
	public class MainGame : MonoSAMGame
	{
		public static MainGame Inst;

		public readonly RRSounds RRSound = new RRSounds();
		public override SAMSoundPlayer Sound => RRSound;

		public MainGame()
		{
			SAMLog.AdditionalLogInfo.Add(GetLogInfo);
			
			L10NImpl.Init(L10NImpl.LANG_EN_US);
			
			Inst = this;
		}

		private string GetLogInfo()
		{
			return string.Empty;
		}

		protected override void OnInitialize()
		{
#if __DESKTOP__

//			const double ZOOM = 0.925;
//			const double ZOOM = 0.525;
			const double ZOOM = 0.425;

			IsMouseVisible = true;
			Graphics.IsFullScreen = false;

			Graphics.PreferredBackBufferWidth  = (int)(1080 * ZOOM);
			Graphics.PreferredBackBufferHeight = (int)(1920 * ZOOM);
			Window.AllowUserResizing = true;

#if DEBUG
			Graphics.SynchronizeWithVerticalRetrace = false;
			IsFixedTimeStep = false;
			TargetElapsedTime = TimeSpan.FromMilliseconds(1);
#endif

			Graphics.ApplyChanges();
			Window.Position = new Point((1920 - Graphics.PreferredBackBufferWidth) / 2, (1080 - Graphics.PreferredBackBufferHeight) / 2);

#else
			Graphics.IsFullScreen = true;
			Graphics.SupportedOrientations = DisplayOrientation.Portrait;
			Graphics.ApplyChanges();
#endif
		}

		protected override void OnAfterInitialize()
		{
			SetCurrentScreen(new RRGameScreen(this, Graphics));
		}

		protected override void OnUpdate(SAMTime gameTime)
		{
			//
		}
		
		protected override void LoadContent()
		{
			Textures.Initialize(Content, GraphicsDevice);
			try
			{
				Sound.Initialize(Content);
			}
			catch (Exception e)
			{
				SAMLog.Error("MG::LC", "Initializing sound failed", e.ToString());
				Sound.InitErrorState = true;
			}
		}

		protected override void UnloadContent()
		{
			// NOP
		}
	}
}

