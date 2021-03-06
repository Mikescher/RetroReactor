﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoSAMFramework.Portable;
using MonoSAMFramework.Portable.BatchRenderer.TextureAtlases;
using MonoSAMFramework.Portable.DeviceBridge;
using MonoSAMFramework.Portable.LogProtocol;
using MonoSAMFramework.Portable.RenderHelper;

namespace RetroReactor.Shared.Resources
{
	enum TextureQuality
	{
		UNSPECIFIED,

		HD, // x2.000
		MD, // x1.000
		LD, // x0.500
		BD, // x0.250
		FD, // x0.125
	}

	static class Textures
	{
		#region Scaling

		public static TextureQuality TEXTURE_QUALITY = TextureQuality.UNSPECIFIED;

		public static Vector2 TEXTURE_SCALE_HD = new Vector2(0.5f);
		public static Vector2 TEXTURE_SCALE_MD = new Vector2(1.0f);
		public static Vector2 TEXTURE_SCALE_LD = new Vector2(2.0f);
		public static Vector2 TEXTURE_SCALE_BD = new Vector2(4.0f);
		public static Vector2 TEXTURE_SCALE_FD = new Vector2(8.0f);

		public const string TEXTURE_ASSETNAME_HD = "textures/spritesheet_default-sheet_hd";
		public const string TEXTURE_ASSETNAME_MD = "textures/spritesheet_default-sheet_md";
		public const string TEXTURE_ASSETNAME_LD = "textures/spritesheet_default-sheet_ld";
		public const string TEXTURE_ASSETNAME_BD = "textures/spritesheet_default-sheet_bd";
		public const string TEXTURE_ASSETNAME_FD = "textures/spritesheet_default-sheet_fd";

		public const string TEXTURE_ASSETNAME_2_HD = "textures/spritesheet_extra-sheet_hd";
		public const string TEXTURE_ASSETNAME_2_MD = "textures/spritesheet_extra-sheet_md";
		public const string TEXTURE_ASSETNAME_2_LD = "textures/spritesheet_extra-sheet_ld";
		public const string TEXTURE_ASSETNAME_2_BD = "textures/spritesheet_extra-sheet_bd";
		public const string TEXTURE_ASSETNAME_2_FD = "textures/spritesheet_extra-sheet_fd";

		public static float DEFAULT_TEXTURE_SCALE_F => DEFAULT_TEXTURE_SCALE.X;

		public static Vector2 DEFAULT_TEXTURE_SCALE
		{
			get
			{
				switch (TEXTURE_QUALITY)
				{
					case TextureQuality.HD:
						return TEXTURE_SCALE_HD;
					case TextureQuality.MD:
						return TEXTURE_SCALE_MD;
					case TextureQuality.LD:
						return TEXTURE_SCALE_LD;
					case TextureQuality.BD:
						return TEXTURE_SCALE_BD;
					case TextureQuality.FD:
						return TEXTURE_SCALE_FD;
					case TextureQuality.UNSPECIFIED:
					default:
						SAMLog.Error("TEX::EnumSwitch_DTS", "value = " + TEXTURE_QUALITY);
						return TEXTURE_SCALE_MD;
				}
			}
		}

		public static string TEXTURE_ASSETNAME
		{
			get
			{
				switch (TEXTURE_QUALITY)
				{
					case TextureQuality.HD:
						return TEXTURE_ASSETNAME_HD;
					case TextureQuality.MD:
						return TEXTURE_ASSETNAME_MD;
					case TextureQuality.LD:
						return TEXTURE_ASSETNAME_LD;
					case TextureQuality.BD:
						return TEXTURE_ASSETNAME_BD;
					case TextureQuality.FD:
						return TEXTURE_ASSETNAME_FD;
					case TextureQuality.UNSPECIFIED:
					default:
						SAMLog.Error("TEX::EnumSwitch_TA", "value = " + TEXTURE_QUALITY);
						return TEXTURE_ASSETNAME_MD;
				}
			}
		}

		public static string TEXTURE_ASSETNAME_2
		{
			get
			{
				switch (TEXTURE_QUALITY)
				{
					case TextureQuality.HD:
						return TEXTURE_ASSETNAME_2_HD;
					case TextureQuality.MD:
						return TEXTURE_ASSETNAME_2_MD;
					case TextureQuality.LD:
						return TEXTURE_ASSETNAME_2_LD;
					case TextureQuality.BD:
						return TEXTURE_ASSETNAME_2_BD;
					case TextureQuality.FD:
						return TEXTURE_ASSETNAME_2_FD;
					case TextureQuality.UNSPECIFIED:
					default:
						SAMLog.Error("TEX::EnumSwitch_TA2", "value = " + TEXTURE_QUALITY);
						return TEXTURE_ASSETNAME_2_MD;
				}
			}
		}

		#endregion

		public static TextureAtlas AtlasTextures;

		#region Textures

		public static TextureRegion2D TexPixel;
		public static TextureRegion2D TexCircle;
		public static TextureRegion2D TexPanelBlurEdge;
		public static TextureRegion2D TexPanelBlurCorner;
		public static TextureRegion2D TexScissorBlurEdge;
		public static TextureRegion2D TexScissorBlurCorner;
		public static TextureRegion2D TexPanelCorner;
		public static TextureRegion2D TexHUDIconKeyboardCaps;
		public static TextureRegion2D TexHUDIconKeyboardEnter;
		public static TextureRegion2D TexHUDIconKeyboardBackspace;

		public static SpriteFont HUDFontRegular;

#if DEBUG
		public static SpriteFont DebugFont;
		public static SpriteFont DebugFontSmall;
#endif

		#endregion

		public static void Initialize(ContentManager content, GraphicsDevice device)
		{
			if (MonoSAMGame.IsDesktop())
				TEXTURE_QUALITY = TextureQuality.HD;
			else
				TEXTURE_QUALITY = GetPreferredQuality(device);

			LoadContent(content);
		}

		private static void LoadContent(ContentManager content)
		{
			AtlasTextures = content.Load<TextureAtlas>(TEXTURE_ASSETNAME);

			TexCircle                   = AtlasTextures["simple_circle"];
			TexPixel                    = AtlasTextures["simple_pixel"];
			TexPixel                    = new TextureRegion2D(TexPixel.Texture, TexPixel.X + TexPixel.Width / 2, TexPixel.Y + TexPixel.Height / 2, 1, 1); // Anti-Antialising
			TexHUDIconKeyboardCaps      = AtlasTextures["caps"];
			TexHUDIconKeyboardEnter     = AtlasTextures["enter"];
			TexHUDIconKeyboardBackspace = AtlasTextures["backspace"];
			TexScissorBlurEdge          = AtlasTextures["scissor_blur_edge"];
			TexScissorBlurCorner        = AtlasTextures["scissor_blur_corner"];
			TexPanelBlurEdge            = AtlasTextures["panel_blur_edge"];
			TexPanelBlurCorner          = AtlasTextures["panel_blur_corner"];
			TexScissorBlurEdge          = AtlasTextures["scissor_blur_edge"];
			TexScissorBlurCorner        = AtlasTextures["scissor_blur_corner"];
			TexPanelCorner              = AtlasTextures["panel_corner"];

			HUDFontRegular = content.Load<SpriteFont>("fonts/hudFontRegular");

#if DEBUG
			DebugFont = content.Load<SpriteFont>("fonts/debugFont");
			DebugFontSmall = content.Load<SpriteFont>("fonts/debugFontSmall");
#endif

			StaticTextures.SinglePixel           = TexPixel;
			StaticTextures.MonoCircle            = TexCircle;
			StaticTextures.PanelBlurCorner       = TexPanelBlurCorner;
			StaticTextures.PanelBlurEdge         = TexPanelBlurEdge;
			StaticTextures.PanelCorner           = TexPanelCorner;
			StaticTextures.PanelBlurCornerPrecut = TexScissorBlurCorner;
			StaticTextures.PanelBlurEdgePrecut   = TexScissorBlurEdge;
			StaticTextures.KeyboardBackspace     = TexHUDIconKeyboardBackspace;
			StaticTextures.KeyboardEnter         = TexHUDIconKeyboardEnter;
			StaticTextures.KeyboardCaps          = TexHUDIconKeyboardCaps;
			StaticTextures.KeyboardCircle        = TexCircle;
		}

		public static TextureQuality GetPreferredQuality(GraphicsDevice device)
		{
			float scale = GetDeviceTextureScaling(device);

			if (scale > 1.00f) return TextureQuality.HD; // 2.0x
			if (scale > 0.50f) return TextureQuality.MD; // 1.0x
			if (scale > 0.25f) return TextureQuality.LD; // 0.5x

			return TextureQuality.BD;
		}

		public static float GetDeviceTextureScaling(GraphicsDevice device)
		{
			var screenWidth = device.Viewport.Width;
			var screenHeight = device.Viewport.Height;
			var screenRatio = screenWidth * 1f / screenHeight;

			var worldWidth = RRConstants.VIEW_WIDTH;
			var worldHeight = RRConstants.VIEW_HEIGHT;
			var worldRatio = worldWidth * 1f / worldHeight;

			if (screenRatio < worldRatio)
				return screenWidth * 1f / worldWidth;
			else
				return screenHeight * 1f / worldHeight;
		}
	}
}
