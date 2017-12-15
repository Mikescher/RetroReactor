using System;
using System.Collections.Generic;
using System.Text;

namespace RetroReactor.Shared.Resources
{
	public static class RRConstants
	{
		public static readonly Version Version = new Version(0, 0, 0, 1);
		public static ulong IntVersion { get; } = (ulong)((((((Version.Major << 12) | Version.Minor) << 12) | Version.Build) << 12) | Version.Revision);

		public const int VIEW_WIDTH  = 700;
		public const int VIEW_HEIGHT = 1120;

		public const int HEXGRID_WIDTH  = 12;
		public const int HEXGRID_HEIGHT = 20;
	}
}
