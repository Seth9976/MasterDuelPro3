using System;
using UnityEngine;
using YgomGame.Card;

namespace YgomGame.Duel
{
	// Token: 0x02000F16 RID: 3862
	public class SlateSetting : ScriptableObject
	{
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060071C8 RID: 29128 RVA: 0x0000216A File Offset: 0x0000036A
		protected static SlateSetting Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060071C9 RID: 29129 RVA: 0x000F6370 File Offset: 0x000F4570
		public static Color LevelBaseColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060071CA RID: 29130 RVA: 0x000F6388 File Offset: 0x000F4588
		public static Color RankBaseColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060071CB RID: 29131 RVA: 0x000F63A0 File Offset: 0x000F45A0
		public static Color LinkBaseColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060071CC RID: 29132 RVA: 0x000F63B8 File Offset: 0x000F45B8
		public static Color FontColorNormal
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060071CD RID: 29133 RVA: 0x000F63D0 File Offset: 0x000F45D0
		public static Color FontColorChanged
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x000F63E8 File Offset: 0x000F45E8
		public static ValueTuple<Color, Color, Texture, Texture> GetFrameColor(Content.Frame frame)
		{
			return default(ValueTuple<Color, Color, Texture, Texture>);
		}

		// Token: 0x060071CF RID: 29135 RVA: 0x000F6400 File Offset: 0x000F4600
		public static ValueTuple<Color, Texture, string> GetAttributeBaseColor(Content.Attribute attr)
		{
			return default(ValueTuple<Color, Texture, string>);
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture GetIconTexture(Content.Icon icon)
		{
			return null;
		}

		// Token: 0x0400AB9C RID: 43932
		private static SlateSetting m_Instance;

		// Token: 0x0400AB9D RID: 43933
		private const string PATH = "Duel/ScriptableObject/SlateSetting";

		// Token: 0x0400AB9E RID: 43934
		[SerializeField]
		private SlateSetting.FrameColorPalette[] FieldCardframeColor;

		// Token: 0x0400AB9F RID: 43935
		[SerializeField]
		private SlateSetting.AttributeColorPalette[] attributeBaseColorPalettes;

		// Token: 0x0400ABA0 RID: 43936
		[SerializeField]
		private SlateSetting.MagicIcon[] magicIcons;

		// Token: 0x0400ABA1 RID: 43937
		[SerializeField]
		private Color levelBaseColor;

		// Token: 0x0400ABA2 RID: 43938
		[SerializeField]
		private Color rankBaseColor;

		// Token: 0x0400ABA3 RID: 43939
		[SerializeField]
		private Color linkBaseColor;

		// Token: 0x0400ABA4 RID: 43940
		[SerializeField]
		private Color fontColorNormal;

		// Token: 0x0400ABA5 RID: 43941
		[SerializeField]
		private Color fontColorChanged;

		// Token: 0x02000F17 RID: 3863
		[Serializable]
		public struct FrameColorPalette
		{
			// Token: 0x0400ABA6 RID: 43942
			public Content.Frame frame;

			// Token: 0x0400ABA7 RID: 43943
			public Color topCol;

			// Token: 0x0400ABA8 RID: 43944
			public Color bottomCol;

			// Token: 0x0400ABA9 RID: 43945
			public Texture topTex;

			// Token: 0x0400ABAA RID: 43946
			public Texture bottomTex;
		}

		// Token: 0x02000F18 RID: 3864
		[Serializable]
		public struct AttributeColorPalette
		{
			// Token: 0x0400ABAB RID: 43947
			public Content.Attribute attr;

			// Token: 0x0400ABAC RID: 43948
			public Color color;

			// Token: 0x0400ABAD RID: 43949
			public Texture texture;

			// Token: 0x0400ABAE RID: 43950
			public string text;
		}

		// Token: 0x02000F19 RID: 3865
		[Serializable]
		public struct MagicIcon
		{
			// Token: 0x0400ABAF RID: 43951
			public Content.Icon icon;

			// Token: 0x0400ABB0 RID: 43952
			public Texture texture;
		}
	}
}
