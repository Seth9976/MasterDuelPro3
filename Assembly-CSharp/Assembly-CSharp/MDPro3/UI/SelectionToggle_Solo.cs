using System;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013D5 RID: 5077
	public class SelectionToggle_Solo : SelectionToggle_ScrollRectItem
	{
		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06009319 RID: 37657 RVA: 0x0014B468 File Offset: 0x00149668
		private TextMeshProUGUI Title
		{
			get
			{
				return this.m_Title = ((this.m_Title != null) ? this.m_Title : base.Manager.GetElement<TextMeshProUGUI>("Title"));
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x0600931A RID: 37658 RVA: 0x0014B4A4 File Offset: 0x001496A4
		private ArtRawImageHandler Art
		{
			get
			{
				return this.m_Art = ((this.m_Art != null) ? this.m_Art : base.Manager.GetElement<ArtRawImageHandler>("Image"));
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x0600931B RID: 37659 RVA: 0x0014B4E0 File Offset: 0x001496E0
		private GameObject NumBadge
		{
			get
			{
				return this.m_NumBadge = ((this.m_NumBadge != null) ? this.m_NumBadge : base.Manager.GetElement("NumBadge"));
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x0600931C RID: 37660 RVA: 0x0014B51C File Offset: 0x0014971C
		private GameObject TextClear
		{
			get
			{
				return this.m_TextClear = ((this.m_TextClear != null) ? this.m_TextClear : base.Manager.GetElement("TextClear"));
			}
		}

		// Token: 0x0600931D RID: 37661 RVA: 0x0014B558 File Offset: 0x00149758
		public override void Refresh()
		{
			base.Refresh();
			this.Title.text = this.botInfo.name;
			this.isDiyDeck = this.botInfo.command.Contains("Lucky");
			this.Art.SetArt(this.botInfo.main0);
			this.NumBadge.SetActive(false);
			this.TextClear.SetActive(false);
		}

		// Token: 0x0600931E RID: 37662 RVA: 0x0014B5CC File Offset: 0x001497CC
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.solo.GetUI<SoloSelectorUI>().superScrollView.selected = this.index;
			Program.instance.solo.GetUI<SoloSelectorUI>().SetOverview(this.botInfo.desc, this.isDiyDeck);
			Program.instance.solo.lastSoloItem = this;
		}

		// Token: 0x0600931F RID: 37663 RVA: 0x0014B634 File Offset: 0x00149834
		protected override void CallSubmitEvent()
		{
			base.CallSubmitEvent();
			if (SoloSelector.condition == SoloSelector.Condition.ForSolo)
			{
				Program.instance.solo.StartAIForSolo(this.index, this.isDiyDeck);
				return;
			}
			Program.instance.solo.StartAIForRoom(this.index, this.isDiyDeck);
		}

		// Token: 0x06009320 RID: 37664 RVA: 0x0014B685 File Offset: 0x00149885
		public void PublicSubmit()
		{
			this.CallSubmitEvent();
		}

		// Token: 0x06009321 RID: 37665 RVA: 0x0014B68D File Offset: 0x0014988D
		protected override void OnNavigation(AxisEventData eventData)
		{
			base.OnNavigation(eventData);
			if (eventData.moveDir == MoveDirection.Right)
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.solo.GetUI<SoloSelectorUI>().SelectOnRight();
			}
		}

		// Token: 0x0400D16B RID: 53611
		private const string LABEL_TXT_TITLE = "Title";

		// Token: 0x0400D16C RID: 53612
		private TextMeshProUGUI m_Title;

		// Token: 0x0400D16D RID: 53613
		private const string LABEL_ART = "Image";

		// Token: 0x0400D16E RID: 53614
		private ArtRawImageHandler m_Art;

		// Token: 0x0400D16F RID: 53615
		private const string LABEL_GO_NUMBADGE = "NumBadge";

		// Token: 0x0400D170 RID: 53616
		private GameObject m_NumBadge;

		// Token: 0x0400D171 RID: 53617
		private const string LABEL_GO_TEXTCLEAR = "TextClear";

		// Token: 0x0400D172 RID: 53618
		private GameObject m_TextClear;

		// Token: 0x0400D173 RID: 53619
		public SoloSelector.BotInfo botInfo;

		// Token: 0x0400D174 RID: 53620
		private bool isDiyDeck;
	}
}
