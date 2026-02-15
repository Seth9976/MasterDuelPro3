using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Card;
using YgomSystem;

namespace YgomGame
{
	// Token: 0x020007AF RID: 1967
	public class CardPictureSetting : ScriptableObject
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x0000216A File Offset: 0x0000036A
		protected static CardPictureSetting Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture FrameMask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture FrameMaskForPendulum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture FrameMaskForLink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture KiraMask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture KiraMaskForPendulum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture KiraMaskForLink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture NormalMask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture NormalMaskForPendulum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture NormalMaskForLink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x0000216A File Offset: 0x0000036A
		public static Material GetCardMaterial(CardFinishSetting.FinishType finishType)
		{
			return null;
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sprite GetCardFrame(Content.Frame frame)
		{
			return null;
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sprite GetLoadingTex(Content.Frame frame)
		{
			return null;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sprite GetLinkNumTex(int linknum)
		{
			return null;
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000F4648 File Offset: 0x000F2848
		public static Color GetCardNameColorForShineFinish(Content.Frame frame)
		{
			return default(Color);
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x0000216A File Offset: 0x0000036A
		public static TMP_FontAsset GetFontAsset(RubyRoot.Lang lang)
		{
			return null;
		}

		// Token: 0x040035A1 RID: 13729
		private static CardPictureSetting m_Instance;

		// Token: 0x040035A2 RID: 13730
		private const string PATH = "Card/ScriptableObjects/CardPictureSetting";

		// Token: 0x040035A3 RID: 13731
		[SerializeField]
		private Material m_MatNromalStyle;

		// Token: 0x040035A4 RID: 13732
		[SerializeField]
		private Material m_MatShineStyle;

		// Token: 0x040035A5 RID: 13733
		[SerializeField]
		private Material m_MatRoyalStyle;

		// Token: 0x040035A6 RID: 13734
		[SerializeField]
		private Texture m_FrameMask;

		// Token: 0x040035A7 RID: 13735
		[SerializeField]
		private Texture m_FrameMaskForPendulum;

		// Token: 0x040035A8 RID: 13736
		[SerializeField]
		private Texture m_FrameMaskForLink;

		// Token: 0x040035A9 RID: 13737
		[SerializeField]
		private Texture m_KiraMask;

		// Token: 0x040035AA RID: 13738
		[SerializeField]
		private Texture m_KiraMaskForPendulum;

		// Token: 0x040035AB RID: 13739
		[SerializeField]
		private Texture m_KiraMaskForLink;

		// Token: 0x040035AC RID: 13740
		[SerializeField]
		private Texture m_NormalMask;

		// Token: 0x040035AD RID: 13741
		[SerializeField]
		private Texture m_NormalMaskForPendulum;

		// Token: 0x040035AE RID: 13742
		[SerializeField]
		private Texture m_NormalMaskForLink;

		// Token: 0x040035AF RID: 13743
		[SerializeField]
		private CardPictureSetting.FrameSprite[] m_FrameSpriteTable;

		// Token: 0x040035B0 RID: 13744
		[SerializeField]
		private CardPictureSetting.FrameSprite[] m_LoadingSpriteTable;

		// Token: 0x040035B1 RID: 13745
		[SerializeField]
		private CardPictureSetting.FrameCardNameColor[] m_FrameCardNameColorTableForShineFinish;

		// Token: 0x040035B2 RID: 13746
		[SerializeField]
		private Sprite[] m_LinkNumTexSet;

		// Token: 0x040035B3 RID: 13747
		private Dictionary<RubyRoot.Lang, TMP_FontAsset> m_FontAssetTable;

		// Token: 0x020007B0 RID: 1968
		[Serializable]
		public struct FrameSprite
		{
			// Token: 0x040035B4 RID: 13748
			public Content.Frame frame;

			// Token: 0x040035B5 RID: 13749
			public Sprite sprite;
		}

		// Token: 0x020007B1 RID: 1969
		[Serializable]
		public struct FrameCardNameColor
		{
			// Token: 0x040035B6 RID: 13750
			public Content.Frame frame;

			// Token: 0x040035B7 RID: 13751
			[SerializeField]
			public Color color;
		}
	}
}
