using System;
using MDPro3.UI.ServantUI;
using Percy;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013CF RID: 5071
	public class SelectionToggle_Replay : SelectionToggle_ScrollRectItem
	{
		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x060092F1 RID: 37617 RVA: 0x0014A8A0 File Offset: 0x00148AA0
		private TextMeshProUGUI Title
		{
			get
			{
				return this.m_Title = ((this.m_Title != null) ? this.m_Title : base.Manager.GetElement<TextMeshProUGUI>("Title"));
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x060092F2 RID: 37618 RVA: 0x0014A8DC File Offset: 0x00148ADC
		private ArtRawImageHandler Art
		{
			get
			{
				return this.m_Art = ((this.m_Art != null) ? this.m_Art : base.Manager.GetElement<ArtRawImageHandler>("Image"));
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x060092F3 RID: 37619 RVA: 0x0014A918 File Offset: 0x00148B18
		private GameObject NumBadge
		{
			get
			{
				return this.m_NumBadge = ((this.m_NumBadge != null) ? this.m_NumBadge : base.Manager.GetElement("NumBadge"));
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x060092F4 RID: 37620 RVA: 0x0014A954 File Offset: 0x00148B54
		private GameObject TextClear
		{
			get
			{
				return this.m_TextClear = ((this.m_TextClear != null) ? this.m_TextClear : base.Manager.GetElement("TextClear"));
			}
		}

		// Token: 0x060092F5 RID: 37621 RVA: 0x0014A990 File Offset: 0x00148B90
		public override void Refresh()
		{
			base.Refresh();
			this.Title.text = this.replayName;
			this.yrp = Program.instance.replay.GetUI<ReplaySelectorUI>().CacheYRP(this.replayName);
			this.NumBadge.SetActive(false);
			this.TextClear.SetActive(false);
			this.Art.SetArt((this.yrp == null) ? 0 : this.yrp.playerData[0].main[0]);
		}

		// Token: 0x060092F6 RID: 37622 RVA: 0x0014AA20 File Offset: 0x00148C20
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.replay.lastSelectedReplayItem = this;
			Program.instance.replay.GetUI<ReplaySelectorUI>().superScrollView.selected = this.index;
			ReplaySelectorUI ui = Program.instance.replay.GetUI<ReplaySelectorUI>();
			if (this.yrp == null)
			{
				ui.TextOverview.text = string.Empty;
				ui.ButtonPlayer0.gameObject.SetActive(false);
				ui.ButtonPlayer1.gameObject.SetActive(false);
				ui.ButtonPlayer2.gameObject.SetActive(false);
				ui.ButtonPlayer3.gameObject.SetActive(false);
				return;
			}
			ui.ButtonPlayer0.gameObject.SetActive(true);
			ui.ButtonPlayer1.gameObject.SetActive(true);
			ui.ButtonPlayer2.gameObject.SetActive(true);
			ui.ButtonPlayer3.gameObject.SetActive(true);
			string description = "";
			bool tag = false;
			if ((this.yrp.opt & 32U) > 0U)
			{
				description = description + StringHelper.GetUnsafe(1246, 0) + "\r\n";
				tag = true;
			}
			description = description + StringHelper.GetUnsafe((int)(1259U + (this.yrp.opt >> 16)), 0) + "\r\n";
			description = description + StringHelper.GetUnsafe(1231, 0) + this.yrp.StartLp.ToString() + "\r\n";
			description = description + StringHelper.GetUnsafe(1232, 0) + this.yrp.StartHand.ToString() + "\r\n";
			description = description + StringHelper.GetUnsafe(1233, 0) + this.yrp.DrawCount.ToString() + "\r\n";
			if ((this.yrp.opt & 16U) > 0U)
			{
				description = description + StringHelper.GetUnsafe(1230, 0) + "\r\n";
			}
			ui.ButtonPlayer0.SetButtonText(this.yrp.playerData[0].name);
			ui.ButtonPlayer1.SetButtonText(this.yrp.playerData[1].name);
			if (tag)
			{
				ui.ButtonPlayer2.SetButtonText(this.yrp.playerData[2].name);
				ui.ButtonPlayer3.SetButtonText(this.yrp.playerData[3].name);
			}
			else
			{
				ui.ButtonPlayer2.gameObject.SetActive(false);
				ui.ButtonPlayer3.gameObject.SetActive(false);
			}
			ui.TextOverview.text = description;
		}

		// Token: 0x060092F7 RID: 37623 RVA: 0x0014ACBF File Offset: 0x00148EBF
		protected override void CallSubmitEvent()
		{
			Program.instance.replay.GetUI<ReplaySelectorUI>().KF_Replay(this.replayName, false);
		}

		// Token: 0x060092F8 RID: 37624 RVA: 0x0014ACDC File Offset: 0x00148EDC
		protected override void OnNavigation(AxisEventData eventData)
		{
			base.OnNavigation(eventData);
			if (eventData.moveDir == MoveDirection.Right)
			{
				UserInput.NextSelectionIsAxis = true;
				SelectionButton deckButton = Program.instance.replay.GetUI<ReplaySelectorUI>().ButtonPlayer0;
				if (deckButton.gameObject.activeSelf)
				{
					deckButton.GetSelectable().Select();
					return;
				}
				Program.instance.replay.GetUI<ReplaySelectorUI>().ButtonGodView.GetSelectable().Select();
			}
		}

		// Token: 0x0400D153 RID: 53587
		private const string LABEL_TXT_TITLE = "Title";

		// Token: 0x0400D154 RID: 53588
		private TextMeshProUGUI m_Title;

		// Token: 0x0400D155 RID: 53589
		private const string LABEL_ART = "Image";

		// Token: 0x0400D156 RID: 53590
		private ArtRawImageHandler m_Art;

		// Token: 0x0400D157 RID: 53591
		private const string LABEL_GO_NUMBADGE = "NumBadge";

		// Token: 0x0400D158 RID: 53592
		private GameObject m_NumBadge;

		// Token: 0x0400D159 RID: 53593
		private const string LABEL_GO_TEXTCLEAR = "TextClear";

		// Token: 0x0400D15A RID: 53594
		private GameObject m_TextClear;

		// Token: 0x0400D15B RID: 53595
		public string replayName;

		// Token: 0x0400D15C RID: 53596
		private YRP yrp;
	}
}
