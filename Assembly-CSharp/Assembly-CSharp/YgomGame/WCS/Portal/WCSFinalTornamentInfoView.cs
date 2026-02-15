using System;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.WCS.Portal
{
	// Token: 0x02000810 RID: 2064
	public class WCSFinalTornamentInfoView : WCSBattleInfoBaseViewController.View
	{
		// Token: 0x06003FDC RID: 16348 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetDefaultSlotName(WCSFinalTornamentInfoView.SlotPos pos)
		{
			return null;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x000F46A4 File Offset: 0x000F28A4
		public WCSFinalTornamentInfoView(ElementObjectManager topEom, ElementObjectManager scrollEom, ViewControllerManager manager)
			: base(null, null)
		{
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Terminate()
		{
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetChampionCrownIcon(bool disp)
		{
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTeamInfo(WCSFinalTornamentInfoView.SlotPos pos, string areaName, string teamName, string teamDesc)
		{
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTeamIcon(WCSFinalTornamentInfoView.SlotPos pos, string iconSpritePath)
		{
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTeamWinner(WCSFinalTornamentInfoView.SlotPos pos, bool on)
		{
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetWinningLineStatus(WCSFinalTornamentInfoView.WinningLinePos pos, WCSFinalTornamentInfoView.WinningLineStatus status)
		{
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBattleScoreText(WCSFinalTornamentInfoView.WinningLinePos pos, string score)
		{
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetWinningLineStatusFromWinner(WCSFinalTornamentInfoView.SlotPos winnerPos)
		{
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x0000216D File Offset: 0x0000036D
		private void RegisterTeamSituationBtnAction(WCSFinalTornamentInfoView.SlotPos pos, UnityAction callback)
		{
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ApplyFromCW(object baseData)
		{
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenExplanationPage()
		{
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenDuelRoom(int roomId, string roomUniqueId)
		{
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x0000216D File Offset: 0x0000036D
		private void TestCode()
		{
		}

		// Token: 0x04003903 RID: 14595
		private ElementObjectManager _topEom;

		// Token: 0x04003904 RID: 14596
		private ElementObjectManager[] _slotEoms;

		// Token: 0x04003905 RID: 14597
		private ElementObjectManager[] _winningLineEoms;

		// Token: 0x04003906 RID: 14598
		private SelectionButton[] _slotButtons;

		// Token: 0x04003907 RID: 14599
		private SelectionButton _ruleButton;

		// Token: 0x02000811 RID: 2065
		private enum SlotPos
		{
			// Token: 0x04003909 RID: 14601
			SEMI_FINAL_POS1,
			// Token: 0x0400390A RID: 14602
			SEMI_FINAL_POS2,
			// Token: 0x0400390B RID: 14603
			SEMI_FINAL_POS3,
			// Token: 0x0400390C RID: 14604
			SEMI_FINAL_POS4,
			// Token: 0x0400390D RID: 14605
			FINAL_POS1,
			// Token: 0x0400390E RID: 14606
			FINAL_POS2,
			// Token: 0x0400390F RID: 14607
			CHAMPION_POS
		}

		// Token: 0x02000812 RID: 2066
		private enum WinningLinePos
		{
			// Token: 0x04003911 RID: 14609
			SEMI_FINAL_LEFT,
			// Token: 0x04003912 RID: 14610
			SEMI_FINAL_RIGHT,
			// Token: 0x04003913 RID: 14611
			FINAL
		}

		// Token: 0x02000813 RID: 2067
		private enum WinningLineStatus
		{
			// Token: 0x04003915 RID: 14613
			NO_WINNER,
			// Token: 0x04003916 RID: 14614
			LEFT_WINNER,
			// Token: 0x04003917 RID: 14615
			RIGHT_WINNER
		}
	}
}
