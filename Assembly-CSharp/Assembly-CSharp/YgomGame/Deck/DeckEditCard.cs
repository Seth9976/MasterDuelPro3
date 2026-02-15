using System;
using UnityEngine.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FC8 RID: 4040
	public class DeckEditCard : CardParameterWidget
	{
		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x060078E5 RID: 30949 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_AttrIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x060078E6 RID: 30950 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_TunerIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x060078E7 RID: 30951 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_TypeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x060078E8 RID: 30952 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_SpellTrapTypeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x060078E9 RID: 30953 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_PendScaleIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x060078EA RID: 30954 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_PendScaleText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x060078EB RID: 30955 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_LvlIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x060078EC RID: 30956 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_LvlText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x060078ED RID: 30957 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_RankIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x060078EE RID: 30958 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_RankText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x060078EF RID: 30959 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_LinkIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x060078F0 RID: 30960 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_LinkText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x060078F1 RID: 30961 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_RegulationIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x060078F2 RID: 30962 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_RarityIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060078F3 RID: 30963 RVA: 0x0000216D File Offset: 0x0000036D
		protected new void InitializeElemnts()
		{
		}

		// Token: 0x060078F4 RID: 30964 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetData(CardBaseData baseData, int regulationID, DeckEditViewController2.DisplayMode mode = DeckEditViewController2.DisplayMode.Simple)
		{
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x0000216D File Offset: 0x0000036D
		public void ScalingIcons(float scale = 1.5f)
		{
		}

		// Token: 0x0400B0A4 RID: 45220
		protected const string LABEL_SBN_BODY = "ImageCard";

		// Token: 0x0400B0A5 RID: 45221
		private const string LABEL_IMG_ATTRIBUTEICON = "IconAttribute";

		// Token: 0x0400B0A6 RID: 45222
		private const string LABEL_IMG_LEVELICON = "IconLevel";

		// Token: 0x0400B0A7 RID: 45223
		private const string LABEL_TXT_LEVEL = "TextLevel";

		// Token: 0x0400B0A8 RID: 45224
		protected const string LABEL_IMG_REGULATIONICON = "IconLimit";

		// Token: 0x0400B0A9 RID: 45225
		private const string LABEL_IMG_LINKICON = "IconLink";

		// Token: 0x0400B0AA RID: 45226
		private const string LABEL_TXT_LINK = "TextLink";

		// Token: 0x0400B0AB RID: 45227
		private const string LABEL_IMG_PENDULUMICON = "IconPendulumScale";

		// Token: 0x0400B0AC RID: 45228
		private const string LABEL_TXT_PENDULUM_SCALE = "TextPendulumScale";

		// Token: 0x0400B0AD RID: 45229
		private const string LABEL_IMG_RANKICON = "IconRank";

		// Token: 0x0400B0AE RID: 45230
		private const string LABEL_TXT_RANK = "TextRank";

		// Token: 0x0400B0AF RID: 45231
		private const string LABEL_IMG_TUNERICON = "IconTuner";

		// Token: 0x0400B0B0 RID: 45232
		private const string LABEL_IMG_TYPEICON = "IconType";

		// Token: 0x0400B0B1 RID: 45233
		private const string LABEL_IMG_SPELLTRAPTYPEICON = "IconSpellTrapType";

		// Token: 0x0400B0B2 RID: 45234
		private const string LABEL_IMG_RARITYICON = "IconRarity";

		// Token: 0x0400B0B3 RID: 45235
		private Image AttrIcon;

		// Token: 0x0400B0B4 RID: 45236
		private Image TunerIcon;

		// Token: 0x0400B0B5 RID: 45237
		private Image PendScaleIcon;

		// Token: 0x0400B0B6 RID: 45238
		private ExtendedTextMeshProUGUI PendScaleText;

		// Token: 0x0400B0B7 RID: 45239
		private Image LvlIcon;

		// Token: 0x0400B0B8 RID: 45240
		private ExtendedTextMeshProUGUI LvlText;

		// Token: 0x0400B0B9 RID: 45241
		private Image RankIcon;

		// Token: 0x0400B0BA RID: 45242
		private ExtendedTextMeshProUGUI RankText;

		// Token: 0x0400B0BB RID: 45243
		private Image LinkIcon;

		// Token: 0x0400B0BC RID: 45244
		private ExtendedTextMeshProUGUI LinkText;

		// Token: 0x0400B0BD RID: 45245
		private Image TypeIcon;

		// Token: 0x0400B0BE RID: 45246
		private Image SpellTrapTypeIcon;

		// Token: 0x0400B0BF RID: 45247
		private Image RegulationIcon;

		// Token: 0x0400B0C0 RID: 45248
		private Image RarityIcon;

		// Token: 0x0400B0C1 RID: 45249
		private bool isIni;
	}
}
