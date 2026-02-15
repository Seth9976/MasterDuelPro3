using System;
using MDPro3.Net;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MDPro3.UI
{
	// Token: 0x020013A7 RID: 5031
	public class SelectionButton_CardInfoType : SelectionButton
	{
		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x060091BC RID: 37308 RVA: 0x00143F78 File Offset: 0x00142178
		private GameObject Info0
		{
			get
			{
				return this.m_Info0 = ((this.m_Info0 != null) ? this.m_Info0 : base.Manager.GetElement("IconInfoSwitching0"));
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x060091BD RID: 37309 RVA: 0x00143FB4 File Offset: 0x001421B4
		private GameObject Info1
		{
			get
			{
				return this.m_Info1 = ((this.m_Info1 != null) ? this.m_Info1 : base.Manager.GetElement("IconInfoSwitching1"));
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x060091BE RID: 37310 RVA: 0x00143FF0 File Offset: 0x001421F0
		private GameObject Info2
		{
			get
			{
				return this.m_Info2 = ((this.m_Info2 != null) ? this.m_Info2 : base.Manager.GetElement("IconInfoSwitching2"));
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x060091BF RID: 37311 RVA: 0x0014402C File Offset: 0x0014222C
		private GameObject Info3
		{
			get
			{
				return this.m_Info3 = ((this.m_Info3 != null) ? this.m_Info3 : base.Manager.GetElement("IconInfoSwitching3"));
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x060091C0 RID: 37312 RVA: 0x00144068 File Offset: 0x00142268
		private TextMeshProUGUI TextGP
		{
			get
			{
				return this.m_TextGP = ((this.m_TextGP != null) ? this.m_TextGP : base.Manager.GetElement<TextMeshProUGUI>("TextGenesysPoint"));
			}
		}

		// Token: 0x060091C1 RID: 37313 RVA: 0x001440A4 File Offset: 0x001422A4
		protected override void Awake()
		{
			base.Awake();
			SelectionButton_CardInfoType.instance = this;
			this.SetCardInfoTypeIcon(DeckEditorUI.cardInfoType);
			this.SetClickEvent(new UnityAction(this.ClickEvent));
		}

		// Token: 0x060091C2 RID: 37314 RVA: 0x001440D0 File Offset: 0x001422D0
		private void ClickEvent()
		{
			DeckEditorUI.CardInfoType type = (DeckEditorUI.cardInfoType + 1) % (DeckEditorUI.CardInfoType)4;
			Program.instance.deckEditor.GetUI<DeckEditorUI>().SetCardInfoType(type);
			this.SetCardInfoTypeIcon(type);
		}

		// Token: 0x060091C3 RID: 37315 RVA: 0x00144104 File Offset: 0x00142304
		public void SetCardInfoTypeIcon(DeckEditorUI.CardInfoType type)
		{
			this.Info0.SetActive(type == DeckEditorUI.CardInfoType.None);
			this.Info1.SetActive(type == DeckEditorUI.CardInfoType.Detail);
			this.Info2.SetActive(type == DeckEditorUI.CardInfoType.Pool);
			this.Info3.SetActive(type == DeckEditorUI.CardInfoType.Genesys);
			this.TextGP.gameObject.SetActive(type == DeckEditorUI.CardInfoType.Genesys);
		}

		// Token: 0x060091C4 RID: 37316 RVA: 0x00144161 File Offset: 0x00142361
		public static void SetGenesysPoints(int gp)
		{
			if (SelectionButton_CardInfoType.instance == null)
			{
				return;
			}
			SelectionButton_CardInfoType.instance.TextGP.text = gp.ToString();
			SelectionButton_CardInfoType.instance.TextGP.color = OnlineService.GetGenesysPointsColor(gp);
		}

		// Token: 0x0400D05E RID: 53342
		private const string LABEL_GO_INFO0 = "IconInfoSwitching0";

		// Token: 0x0400D05F RID: 53343
		private GameObject m_Info0;

		// Token: 0x0400D060 RID: 53344
		private const string LABEL_GO_INFO1 = "IconInfoSwitching1";

		// Token: 0x0400D061 RID: 53345
		private GameObject m_Info1;

		// Token: 0x0400D062 RID: 53346
		private const string LABEL_GO_INFO2 = "IconInfoSwitching2";

		// Token: 0x0400D063 RID: 53347
		private GameObject m_Info2;

		// Token: 0x0400D064 RID: 53348
		private const string LABEL_GO_INFO3 = "IconInfoSwitching3";

		// Token: 0x0400D065 RID: 53349
		private GameObject m_Info3;

		// Token: 0x0400D066 RID: 53350
		private const string LABEL_TXT_GP = "TextGenesysPoint";

		// Token: 0x0400D067 RID: 53351
		private TextMeshProUGUI m_TextGP;

		// Token: 0x0400D068 RID: 53352
		public static SelectionButton_CardInfoType instance;
	}
}
