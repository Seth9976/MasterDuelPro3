using System;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013CD RID: 5069
	public class SelectionToggle_Puzzle : SelectionToggle_ScrollRectItem
	{
		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x060092E3 RID: 37603 RVA: 0x0014A59C File Offset: 0x0014879C
		private TextMeshProUGUI Title
		{
			get
			{
				return this.m_Title = ((this.m_Title != null) ? this.m_Title : base.Manager.GetElement<TextMeshProUGUI>("Title"));
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x060092E4 RID: 37604 RVA: 0x0014A5D8 File Offset: 0x001487D8
		private ArtRawImageHandler Art
		{
			get
			{
				return this.m_Art = ((this.m_Art != null) ? this.m_Art : base.Manager.GetElement<ArtRawImageHandler>("Image"));
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x060092E5 RID: 37605 RVA: 0x0014A614 File Offset: 0x00148814
		private GameObject NumBadge
		{
			get
			{
				return this.m_NumBadge = ((this.m_NumBadge != null) ? this.m_NumBadge : base.Manager.GetElement("NumBadge"));
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x060092E6 RID: 37606 RVA: 0x0014A650 File Offset: 0x00148850
		private GameObject TextClear
		{
			get
			{
				return this.m_TextClear = ((this.m_TextClear != null) ? this.m_TextClear : base.Manager.GetElement("TextClear"));
			}
		}

		// Token: 0x060092E7 RID: 37607 RVA: 0x0014A68C File Offset: 0x0014888C
		public override void Refresh()
		{
			base.Refresh();
			this.Title.text = this.puzzle.name;
			this.Art.SetArt(int.Parse(this.puzzle.firstCard));
			this.NumBadge.SetActive(!Config.GetBool("Puzzle/" + this.puzzle.name + "_Enter", false));
			this.TextClear.SetActive(Config.GetBool("Puzzle/" + this.puzzle.name + "_Clear", false));
		}

		// Token: 0x060092E8 RID: 37608 RVA: 0x0014A72C File Offset: 0x0014892C
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.puzzle.GetUI<PuzzleSelectorUI>().superScrollView.selected = this.index;
			Program.instance.puzzle.GetUI<PuzzleSelectorUI>().SetOverview(this.puzzle.description + "\r\n" + this.puzzle.solution);
			Program.instance.puzzle.GetUI<PuzzleSelectorUI>().Art.SetArt(int.Parse(this.puzzle.firstCard));
			Program.instance.puzzle.currentPuzzle = "Puzzle/" + this.puzzle.name;
			Program.instance.puzzle.lastPuzzleItem = this;
		}

		// Token: 0x060092E9 RID: 37609 RVA: 0x0014A7EF File Offset: 0x001489EF
		protected override void CallSubmitEvent()
		{
			base.CallSubmitEvent();
			Program.instance.puzzle.StartCurrentPuzzle();
		}

		// Token: 0x060092EA RID: 37610 RVA: 0x0014A806 File Offset: 0x00148A06
		protected override void OnNavigation(AxisEventData eventData)
		{
			base.OnNavigation(eventData);
			if (eventData.moveDir == MoveDirection.Right)
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.puzzle.GetUI<PuzzleSelectorUI>().ButtonPlay.GetSelectable().Select();
			}
		}

		// Token: 0x0400D149 RID: 53577
		private const string LABEL_TXT_TITLE = "Title";

		// Token: 0x0400D14A RID: 53578
		private TextMeshProUGUI m_Title;

		// Token: 0x0400D14B RID: 53579
		private const string LABEL_ART = "Image";

		// Token: 0x0400D14C RID: 53580
		private ArtRawImageHandler m_Art;

		// Token: 0x0400D14D RID: 53581
		private const string LABEL_GO_NUMBADGE = "NumBadge";

		// Token: 0x0400D14E RID: 53582
		private GameObject m_NumBadge;

		// Token: 0x0400D14F RID: 53583
		private const string LABEL_GO_TEXTCLEAR = "TextClear";

		// Token: 0x0400D150 RID: 53584
		private GameObject m_TextClear;

		// Token: 0x0400D151 RID: 53585
		public PuzzleSelectorUI.Puzzle puzzle;
	}
}
