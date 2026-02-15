using System;
using YgomGame.Colosseum;
using YgomGame.Menu;

namespace YgomGame.Solo
{
	// Token: 0x02000919 RID: 2329
	public class SoloStartProductionViewController : BaseMenuViewController
	{
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ProgressUpdate()
		{
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x0000216D File Offset: 0x0000036D
		private void EvalEachSteps()
		{
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x0000216D File Offset: 0x0000036D
		private void Init()
		{
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitInit()
		{
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectTurn()
		{
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitSelectTurn()
		{
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x0000216D File Offset: 0x0000036D
		private void Final()
		{
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitFinal()
		{
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartDuel()
		{
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x0000216D File Offset: 0x0000036D
		private void DispFirstorSecond(ColosseumUtil.Turn turn)
		{
		}

		// Token: 0x0400829F RID: 33439
		private readonly string ROOT_RESULT_COINTOSS_LABEL;

		// Token: 0x040082A0 RID: 33440
		private readonly string TXT_LABEL;

		// Token: 0x040082A1 RID: 33441
		private SoloStartProductionViewController.Step step;

		// Token: 0x040082A2 RID: 33442
		private int chapterID;

		// Token: 0x040082A3 RID: 33443
		private ColosseumUtil.Turn playerTurn;

		// Token: 0x040082A4 RID: 33444
		private SoloModeUtil.DeckType deckType;

		// Token: 0x0200091A RID: 2330
		public enum Step
		{
			// Token: 0x040082A6 RID: 33446
			None,
			// Token: 0x040082A7 RID: 33447
			Init,
			// Token: 0x040082A8 RID: 33448
			WaitInit,
			// Token: 0x040082A9 RID: 33449
			SelectTurn,
			// Token: 0x040082AA RID: 33450
			WaitSelectTurn,
			// Token: 0x040082AB RID: 33451
			Final,
			// Token: 0x040082AC RID: 33452
			WaitFinal,
			// Token: 0x040082AD RID: 33453
			StartDuel,
			// Token: 0x040082AE RID: 33454
			End
		}
	}
}
