using System;
using MDPro3.UI;
using MDPro3.UI.PropertyOverride;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.Servant
{
	// Token: 0x020012D4 RID: 4820
	public class DeckBrowser : Servant
	{
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06008CD6 RID: 36054 RVA: 0x00128212 File Offset: 0x00126412
		public override int Depth
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06008CD7 RID: 36055 RVA: 0x0000763C File Offset: 0x0000583C
		protected override bool ShowLine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06008CD8 RID: 36056 RVA: 0x00128215 File Offset: 0x00126415
		protected override string Label_UI
		{
			get
			{
				if (!PropertyOverrider.NeedMobileLayout())
				{
					return "ServantUI/DeckBrowserUI.prefab";
				}
				return "ServantUI/DeckBrowserUIMobile.prefab";
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06008CD9 RID: 36057 RVA: 0x00128229 File Offset: 0x00126429
		// (set) Token: 0x06008CDA RID: 36058 RVA: 0x00128236 File Offset: 0x00126436
		public DeckBrowserUI.ResponseRegion ResponseRegion
		{
			get
			{
				return this.GetUI<DeckBrowserUI>()._ResponseRegion;
			}
			set
			{
				this.GetUI<DeckBrowserUI>()._ResponseRegion = value;
			}
		}

		// Token: 0x06008CDB RID: 36059 RVA: 0x00128244 File Offset: 0x00126444
		public void SwitchCondition(DeckBrowser.Condition condition)
		{
			this.condition = condition;
			if (condition == DeckBrowser.Condition.ChangePickup)
			{
				this.returnServant = Program.instance.appearance;
			}
		}

		// Token: 0x06008CDC RID: 36060 RVA: 0x00128260 File Offset: 0x00126460
		public override void Initialize()
		{
			SystemEvent.OnResolutionChange += this.ChangeCanvasMatch;
			this.returnServant = Program.instance.appearance;
			base.Initialize();
		}

		// Token: 0x06008CDD RID: 36061 RVA: 0x00128289 File Offset: 0x00126489
		protected override void FirstLoadEvent()
		{
			base.FirstLoadEvent();
			this.GetUI<DeckBrowserUI>().SetCondition(this.condition);
		}

		// Token: 0x06008CDE RID: 36062 RVA: 0x001282A2 File Offset: 0x001264A2
		private void ChangeCanvasMatch()
		{
			if (!this.showing)
			{
				return;
			}
			UIManager.SetCanvasMatch(Program.instance.deckEditor.GetCanvasMatch(), 0f);
		}

		// Token: 0x06008CDF RID: 36063 RVA: 0x001282C6 File Offset: 0x001264C6
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (this.lastSelectedCardInDeck != null)
			{
				this.lastSelectedCardInDeck.GetSelectable().Select();
				return;
			}
			this.GetUI<DeckBrowserUI>().DeckView.SelectDefaultItem();
		}

		// Token: 0x0400CAFD RID: 51965
		public DeckBrowser.Condition condition;

		// Token: 0x0400CAFE RID: 51966
		[HideInInspector]
		public SelectionButton_CardInDeck lastSelectedCardInDeck;

		// Token: 0x020012D5 RID: 4821
		public enum Condition
		{
			// Token: 0x0400CB00 RID: 51968
			ChangePickup
		}
	}
}
