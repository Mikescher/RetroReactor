using GridDominance.Shared;
using MonoSAMFramework.Portable;
using System;
using RetroReactor.OpenGL.Impl;

namespace RetroReactor.DirectX
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			MonoSAMGame.StaticBridge = new WindowsBridge();

			using (var game = new MainGame()) game.Run();
		}
	}
}

