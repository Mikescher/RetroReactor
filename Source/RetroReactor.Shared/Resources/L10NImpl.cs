using System;
using System.Collections.Generic;
using System.Text;
using MonoSAMFramework.Portable.Localization;

namespace RetroReactor.Shared.Resources
{
	class L10NImpl
	{
		public const int LANG_EN_US = 0;
		public const int LANG_DE_DE = 1;

		public static int LANG_COUNT = 5;

		private const int TEXT_COUNT = 0; // = next idx

		public static void Init(int lang)
		{
			L10N.Init(lang, TEXT_COUNT, LANG_COUNT);

#if DEBUG
			L10N.Verify();
#endif
		}
	}
}
