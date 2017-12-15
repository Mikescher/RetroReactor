using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using MonoSAMFramework.Portable;
using MonoSAMFramework.Portable.DeviceBridge;
using MonoSAMFramework.Portable.GameMath.Geometry;
using MonoSAMFramework.Portable.Language;
using RetroReactor.Generic.Impl;

namespace RetroReactor.OpenGL.Impl
{
	class WindowsBridge : ISAMOperatingSystemBridge
	{
		public FileHelper FileHelper { get; } = new WindowsFileHelper();
		public string EnvironmentStackTrace => Environment.StackTrace;

		public FSize DeviceResolution => new FSize(MonoSAMGame.CurrentInst.Window.ClientBounds.Width, MonoSAMGame.CurrentInst.Window.ClientBounds.Height);
		public string FullDeviceInfoString { get; } = "?? RetroReactor.Windows.WindowsImpl ??" + "\n" + Environment.MachineName + "/" + Environment.UserName;
		public string DeviceName { get; } = "PC_OGL";
		public string DeviceVersion { get; } = Environment.OSVersion.VersionString;
		public SAMSystemType SystemType { get; } = SAMSystemType.MONOGAME_DESKTOP;

		public void OnNativeInitialize(MonoSAMGame game)
		{
			//const double ZOOM = 0.925;
			//const double ZOOM = 0.525;
			const double ZOOM = 0.425;

			game.IsMouseVisible = true;
			game.Graphics.IsFullScreen = false;

			game.Graphics.PreferredBackBufferWidth = (int)(1080 * ZOOM);
			game.Graphics.PreferredBackBufferHeight = (int)(1920 * ZOOM);
			game.Window.AllowUserResizing = true;

#if DEBUG
			game.Graphics.SynchronizeWithVerticalRetrace = false;
			game.IsFixedTimeStep = false;
			game.TargetElapsedTime = TimeSpan.FromMilliseconds(1);
#endif

			game.Graphics.ApplyChanges();
			game.Window.Position = new Point((1920 - game.Graphics.PreferredBackBufferWidth) / 2, (1080 - game.Graphics.PreferredBackBufferHeight) / 2);
		}

		public string DoSHA256(string input)
		{
			using (var sha256 = SHA256.Create()) return ByteUtils.ByteToHexBitFiddle(sha256.ComputeHash(Encoding.UTF8.GetBytes(input)));
		}

		public void Sleep(int milsec) => Thread.Sleep(milsec);

		public void ExitApp() { Environment.Exit(0); }
	}
}
