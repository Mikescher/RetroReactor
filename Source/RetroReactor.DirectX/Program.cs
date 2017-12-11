using MonoSAMFramework.Portable;
using System;
using RetroReactor.DirectX.Impl;
using RetroReactor.Shared;

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

