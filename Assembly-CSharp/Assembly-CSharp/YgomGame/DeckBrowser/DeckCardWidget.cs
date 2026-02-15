using System;
using TMPro;
using UnityEngine.UI;
using YgomGame.Deck;
using YgomSystem.ElementSystem;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F90 RID: 3984
	public class DeckCardWidget : DeckCard
	{
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600752B RID: 29995 RVA: 0x0000216A File Offset: 0x0000036A
		public ElementObjectManager eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600752C RID: 29996 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600752D RID: 29997 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isMonochrome
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600752E RID: 29998 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600752F RID: 29999 RVA: 0x0000216D File Offset: 0x0000036D
		public bool regulationVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06007530 RID: 30000 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007531 RID: 30001 RVA: 0x0000216D File Offset: 0x0000036D
		public bool rarityVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06007532 RID: 30002 RVA: 0x0000216A File Offset: 0x0000036A
		public static DeckCardWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007533 RID: 30003 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(CardBaseData baseData)
		{
		}

		// Token: 0x06007534 RID: 30004 RVA: 0x0000216D File Offset: 0x0000036D
		public void DispCardNum(bool b)
		{
		}

		// Token: 0x06007535 RID: 30005 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardNum(int num)
		{
		}

		// Token: 0x06007536 RID: 30006 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinishSetCardPicture()
		{
		}

		// Token: 0x0400AE64 RID: 44644
		private ElementObjectManager m_EomCache;

		// Token: 0x0400AE65 RID: 44645
		private bool m_Ready;

		// Token: 0x0400AE66 RID: 44646
		private bool m_IsMonochrome;

		// Token: 0x0400AE67 RID: 44647
		private bool m_RagulationVisible;

		// Token: 0x0400AE68 RID: 44648
		private bool m_RarityVisible;

		// Token: 0x0400AE69 RID: 44649
		private const string k_ELabelGroupCardNum = "GroupCardNum";

		// Token: 0x0400AE6A RID: 44650
		private const string k_ELabelTextCardNum = "TextCardNum";

		// Token: 0x0400AE6B RID: 44651
		private Image m_CardNumBase;

		// Token: 0x0400AE6C RID: 44652
		private TextMeshProUGUI m_CardNumText;
	}
}
