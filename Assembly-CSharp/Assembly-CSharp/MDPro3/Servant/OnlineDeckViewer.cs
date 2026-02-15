using System;
using System.Collections;
using System.Threading.Tasks;
using MDPro3.Net;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.Servant
{
	// Token: 0x020012F1 RID: 4849
	public class OnlineDeckViewer : Servant
	{
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06008DCD RID: 36301 RVA: 0x0012E2BA File Offset: 0x0012C4BA
		public override int Depth
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06008DCE RID: 36302 RVA: 0x0000763C File Offset: 0x0000583C
		protected override bool ShowLine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008DCF RID: 36303 RVA: 0x0012E2BD File Offset: 0x0012C4BD
		public override void Initialize()
		{
			this.returnServant = Program.instance.deckSelector;
			base.Initialize();
		}

		// Token: 0x06008DD0 RID: 36304 RVA: 0x0012E2D5 File Offset: 0x0012C4D5
		protected override void ApplyShowArrangement(int preDepth)
		{
			base.ApplyShowArrangement(preDepth);
			this.RefreshList();
		}

		// Token: 0x06008DD1 RID: 36305 RVA: 0x0012E2E4 File Offset: 0x0012C4E4
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			this.lastSelectedDeckItem.GetSelectable().Select();
		}

		// Token: 0x06008DD2 RID: 36306 RVA: 0x0012E304 File Offset: 0x0012C504
		public override void PerFrameFunction()
		{
			if (this.NeedResponseInput())
			{
				if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
				{
					this.OnReturn();
				}
				if (UserInput.WasGamepadButtonWestPressed)
				{
					AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
					this.GetUI<OnlineDeckViewerUI>().InputDeckName.ActivateInputField();
				}
				if (UserInput.WasGamepadButtonNorthPressed)
				{
					AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
					this.GetUI<OnlineDeckViewerUI>().InputDeckAuthor.ActivateInputField();
				}
			}
		}

		// Token: 0x06008DD3 RID: 36307 RVA: 0x0012E379 File Offset: 0x0012C579
		public void RefreshList()
		{
			OnlineDeckViewer.decks = null;
			base.StartCoroutine(this.RefreshAsync());
		}

		// Token: 0x06008DD4 RID: 36308 RVA: 0x0012E38E File Offset: 0x0012C58E
		private IEnumerator RefreshAsync()
		{
			Task<OnlineDeck.OnlineDeckData[]> task = OnlineDeck.FetchSimpleDeckList(10000, this.GetUI<OnlineDeckViewerUI>().InputDeckName.text, this.GetUI<OnlineDeckViewerUI>().InputDeckAuthor.text, true);
			yield return new WaitUntil(() => task.IsCompleted);
			if (task.Status == TaskStatus.RanToCompletion)
			{
				OnlineDeckViewer.decks = task.Result;
				if (OnlineDeckViewer.decks == null)
				{
					MessageManager.Cast(InterString.Get("网络异常，获取在线卡组列表失败。", 0));
					yield break;
				}
				this.GetUI<OnlineDeckViewerUI>().Print();
			}
			else
			{
				MessageManager.Cast(InterString.Get("网络异常，获取在线卡组列表失败。", 0));
			}
			yield break;
		}

		// Token: 0x0400CBFA RID: 52218
		public static OnlineDeck.OnlineDeckData[] decks;

		// Token: 0x0400CBFB RID: 52219
		[HideInInspector]
		public SelectionToggle_DeckOnline lastSelectedDeckItem;
	}
}
