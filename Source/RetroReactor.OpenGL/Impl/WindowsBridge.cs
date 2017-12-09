using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using MonoSAMFramework.Portable;
using MonoSAMFramework.Portable.DeviceBridge;
using MonoSAMFramework.Portable.GameMath.Geometry;
using MonoSAMFramework.Portable.Language;

namespace RetroReactor.OpenGL.Impl
{
	class WindowsBridge : ISAMOperatingSystemBridge
	{
		public FileHelper FileHelper { get; } = new WindowsFileHelper();
		public string EnvironmentStackTrace => Environment.StackTrace;

		public FSize DeviceResolution => new FSize(MonoSAMGame.CurrentInst.Window.ClientBounds.Width, MonoSAMGame.CurrentInst.Window.ClientBounds.Height);
		public string FullDeviceInfoString { get; } = "?? RetroReactor.Windows.WindowsImpl ??" + "\n" + Environment.MachineName + "/" + Environment.UserName;
		public string DeviceName { get; } = "PC";
		public string DeviceVersion { get; } = Environment.OSVersion.VersionString;

		public string DoSHA256(string input)
		{
			using (var sha256 = SHA256.Create()) return ByteUtils.ByteToHexBitFiddle(sha256.ComputeHash(Encoding.UTF8.GetBytes(input)));
		}

		public void Sleep(int milsec) => Thread.Sleep(milsec);

		public void ExitApp() { Environment.Exit(0); }
	}
}
