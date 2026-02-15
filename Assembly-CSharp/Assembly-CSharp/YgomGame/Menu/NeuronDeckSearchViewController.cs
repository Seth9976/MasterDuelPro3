using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Menu
{
	// Token: 0x02000AAB RID: 2731
	public class NeuronDeckSearchViewController : PublicDeckSearchController
	{
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateDecks()
		{
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClickPublicDeck(string cardgameId, int deckNo, int pickCardId)
		{
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OpenDeckBrowser(string cardgameId, int deckNo, int pickCardId)
		{
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x0000216A File Offset: 0x0000036A
		protected override IEnumerator Initialize()
		{
			return null;
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x0000216A File Offset: 0x0000036A
		protected override IEnumerator AdditionalDeckDataLoad()
		{
			return null;
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x0000216D File Offset: 0x0000036D
		private void GetNeuronToken(UnityAction getTokenEvent)
		{
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator DeckSearch_Search(int requestPageNo = 0, bool isFirst = false)
		{
			return null;
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x0000216D File Offset: 0x0000036D
		private void DeckSearch_Detail(string targetId, int deckNo, int pickCardId, bool isFirst = false)
		{
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenCautionMMA()
		{
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OpenMaintenanceDialog()
		{
		}

		// Token: 0x04008D76 RID: 36214
		private readonly string k_ELabelDeckNumValue;

		// Token: 0x04008D77 RID: 36215
		private readonly string k_ELabelTextDeckNumValue;

		// Token: 0x04008D78 RID: 36216
		private readonly string k_ELabelNeuronDecksHelpButton;

		// Token: 0x04008D79 RID: 36217
		private readonly string k_ELabelOpenNeuronDecksButton;

		// Token: 0x04008D7A RID: 36218
		private readonly string k_ELabelNeuronLogoBG;

		// Token: 0x04008D7B RID: 36219
		private readonly string k_ELabelNeuronLogoIconBG;

		// Token: 0x04008D7C RID: 36220
		private Transform m_DeckNumValue;

		// Token: 0x04008D7D RID: 36221
		private TextMeshProUGUI m_TextDeckNumValue;

		// Token: 0x04008D7E RID: 36222
		private SelectionButton m_NeuronDecksHelpButton;

		// Token: 0x04008D7F RID: 36223
		private SelectionButton m_OpenNeuronDecksButton;

		// Token: 0x04008D80 RID: 36224
		private Transform m_NeuronLogoBG;

		// Token: 0x04008D81 RID: 36225
		private Image m_NeuronLogoIconBG;

		// Token: 0x04008D82 RID: 36226
		[SerializeField]
		private SpriteContainer m_BackGroundContainer;

		// Token: 0x04008D83 RID: 36227
		private readonly string k_LabelJpLogo;

		// Token: 0x04008D84 RID: 36228
		private readonly string k_LabelUniversalLogo;

		// Token: 0x04008D85 RID: 36229
		private readonly string k_LabelKoLogo;

		// Token: 0x04008D86 RID: 36230
		private const string GROUP_LABEL = "NeuronMyDeck";
	}
}
