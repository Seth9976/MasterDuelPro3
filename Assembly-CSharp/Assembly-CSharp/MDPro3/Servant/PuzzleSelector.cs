using System;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.Servant
{
	// Token: 0x020012FC RID: 4860
	public class PuzzleSelector : Servant
	{
		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06008E26 RID: 36390 RVA: 0x0000763C File Offset: 0x0000583C
		public override int Depth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06008E27 RID: 36391 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008E28 RID: 36392 RVA: 0x0012F988 File Offset: 0x0012DB88
		public override void Initialize()
		{
			this.returnServant = Program.instance.menu;
			base.Initialize();
		}

		// Token: 0x06008E29 RID: 36393 RVA: 0x0012F9A0 File Offset: 0x0012DBA0
		protected override void FirstLoadEvent()
		{
			base.FirstLoadEvent();
			Program.instance.texture_.SetCommonShopButtonMaterial(this.GetUI<PuzzleSelectorUI>().ImageOut, false);
			Program.instance.texture_.SetCommonShopButtonMaterial(this.GetUI<PuzzleSelectorUI>().ImageHover, true);
		}

		// Token: 0x06008E2A RID: 36394 RVA: 0x0012F9E0 File Offset: 0x0012DBE0
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			this.lastPuzzleItem.GetSelectable().Select();
		}

		// Token: 0x06008E2B RID: 36395 RVA: 0x001289B0 File Offset: 0x00126BB0
		public override void OnExit()
		{
			if (Program.exitOnReturn)
			{
				Program.GameQuit();
				return;
			}
			Program.instance.ShiftToServant(this.returnServant);
		}

		// Token: 0x06008E2C RID: 36396 RVA: 0x0012F9FD File Offset: 0x0012DBFD
		public void PrintPuzzles()
		{
			if (this.servantUI == null)
			{
				return;
			}
			this.GetUI<PuzzleSelectorUI>().Print();
		}

		// Token: 0x06008E2D RID: 36397 RVA: 0x0012FA19 File Offset: 0x0012DC19
		public void StartCurrentPuzzle()
		{
			if (Program.instance.currentServant != Program.instance.puzzle)
			{
				return;
			}
			this.StartPuzzle(this.currentPuzzle);
		}

		// Token: 0x06008E2E RID: 36398 RVA: 0x0012FA43 File Offset: 0x0012DC43
		public void StartPuzzle(string puzzle)
		{
			PercyOCG percyOCG = this.percy;
			if (percyOCG != null)
			{
				percyOCG.Dispose();
			}
			this.percy = new PercyOCG();
			this.percy.StartPuzzle(puzzle + ".lua");
		}

		// Token: 0x06008E2F RID: 36399 RVA: 0x00127555 File Offset: 0x00125755
		public void SelectLastPuzzleItem()
		{
			UserInput.NextSelectionIsAxis = true;
			this.Select(false);
		}

		// Token: 0x0400CC2D RID: 52269
		[HideInInspector]
		public string currentPuzzle;

		// Token: 0x0400CC2E RID: 52270
		[HideInInspector]
		public SelectionToggle_Puzzle lastPuzzleItem;

		// Token: 0x0400CC2F RID: 52271
		private PercyOCG percy;
	}
}
