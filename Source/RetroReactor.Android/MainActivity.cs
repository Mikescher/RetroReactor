﻿using System;
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using GridDominance.Shared;
using Microsoft.Xna.Framework;
using Android.Content;
using MonoSAMFramework.Portable.LogProtocol;
using MonoSAMFramework.Portable;
using RetroReactor.Android.Impl;

namespace RetroReactor.Android
{
	[Activity(Label = "Retro Reactor",
		MainLauncher = true,
		Icon = "@drawable/icon",
		Theme = "@style/Theme.Splash",
		LaunchMode = LaunchMode.SingleInstance,
		ScreenOrientation = ScreenOrientation.SensorLandscape,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.Keyboard | ConfigChanges.ScreenSize)]

	// ReSharper disable once ClassNeverInstantiated.Global
	public class MainActivity : AndroidGameActivity
	{
		private AndroidBridge _impl;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_impl = new AndroidBridge(this);
            MonoSAMGame.StaticBridge = _impl;
            var g = new MainGame();
			SetContentView(g.Services.GetService<View>());
			g.Run();
		}

		protected override void OnDestroy()
		{
			try
			{
				base.OnDestroy();
			}
			catch (Exception e)
			{
				SAMLog.Error("AMA_IAB::OnDestroy", e);
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			//
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			//
		}
	}
}


