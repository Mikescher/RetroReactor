using System;
using Microsoft.Xna.Framework;
using MonoSAMFramework.Portable;
using MonoSAMFramework.Portable.DeviceBridge;
using MonoSAMFramework.Portable.LogProtocol;
using MonoSAMFramework.Portable.Screens;
using MonoSAMFramework.Portable.Sound;
using RetroReactor.Shared.Resources;
using RetroReactor.Shared.Screens.NormalGameScreen;

namespace RetroReactor.Shared
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
			// nothing
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

