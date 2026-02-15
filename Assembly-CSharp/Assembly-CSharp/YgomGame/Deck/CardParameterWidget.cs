using System;
using UnityEngine.UI;
using YgomGame.Card;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FBD RID: 4029
	public abstract class CardParameterWidget : CardBase
	{
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x0600786D RID: 30829 RVA: 0x0000216A File Offset: 0x0000036A
		protected static Content m_cci
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x0600786E RID: 30830
		protected abstract Image m_AttrIcon { get; }

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x0600786F RID: 30831
		protected abstract Image m_TunerIcon { get; }

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06007870 RID: 30832
		protected abstract Image m_PendScaleIcon { get; }

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06007871 RID: 30833
		protected abstract ExtendedTextMeshProUGUI m_PendScaleText { get; }

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06007872 RID: 30834
		protected abstract Image m_LvlIcon { get; }

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06007873 RID: 30835
		protected abstract ExtendedTextMeshProUGUI m_LvlText { get; }

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06007874 RID: 30836
		protected abstract Image m_RankIcon { get; }

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06007875 RID: 30837
		protected abstract ExtendedTextMeshProUGUI m_RankText { get; }

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06007876 RID: 30838
		protected abstract Image m_LinkIcon { get; }

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06007877 RID: 30839
		protected abstract ExtendedTextMeshProUGUI m_LinkText { get; }

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06007878 RID: 30840
		protected abstract Image m_TypeIcon { get; }

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06007879 RID: 30841
		protected abstract Image m_SpellTrapTypeIcon { get; }

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600787A RID: 30842
		protected abstract Image m_RegulationIcon { get; }

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x0600787B RID: 30843
		protected abstract Image m_RarityIcon { get; }

		// Token: 0x0600787C RID: 30844 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setRegulationIcon(int id)
		{
		}

		// Token: 0x0600787D RID: 30845 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setRegulationVisible(bool b)
		{
		}

		// Token: 0x0600787E RID: 30846 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setAttribute(bool b = true)
		{
		}

		// Token: 0x0600787F RID: 30847 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setTuner(bool b = true)
		{
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setPendulumScale(bool b = true)
		{
		}

		// Token: 0x06007881 RID: 30849 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setLevel(bool b = true)
		{
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setRank(bool b = true)
		{
		}

		// Token: 0x06007883 RID: 30851 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setLinkRating(bool b = true)
		{
		}

		// Token: 0x06007884 RID: 30852 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setType(bool b = true)
		{
		}

		// Token: 0x06007885 RID: 30853 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setSpellTrapType(bool b = true)
		{
		}

		// Token: 0x06007886 RID: 30854 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setRarity(bool b = true)
		{
		}

		// Token: 0x0400B04C RID: 45132
		public SelectionButton m_BodyButton;

		// Token: 0x0400B04D RID: 45133
		protected CardIconSprites m_CardIconSprites;
	}
}
