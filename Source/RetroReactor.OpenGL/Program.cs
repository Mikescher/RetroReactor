using System;
using MonoSAMFramework.Portable;
using RetroReactor.OpenGL.Impl;
using RetroReactor.Shared;

namespace RetroReactor.OpenGL
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			MonoSAMGame.StaticBridge = new WindowsBridge();

			using (var game = new MainGame()) game.Run();
		}
	}
}
