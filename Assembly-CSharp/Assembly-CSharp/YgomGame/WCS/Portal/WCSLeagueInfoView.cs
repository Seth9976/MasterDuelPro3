using System;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.WCS.Portal
{
	// Token: 0x02000814 RID: 2068
	public class WCSLeagueInfoView : WCSBattleInfoBaseViewController.View
	{
		// Token: 0x06003FEB RID: 16363 RVA: 0x000F46A4 File Offset: 0x000F28A4
		public WCSLeagueInfoView(ElementObjectManager topEom, ElementObjectManager scrollEom, ViewControllerManager manager)
			: base(null, null)
		{
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Terminate()
		{
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ApplyFromCW(object baseData)
		{
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenExplanationPage()
		{
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenDuelRoom(int roomId, string roomUniqueId)
		{
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x0000216D File Offset: 0x0000036D
		private void TestCode(int tableIndex)
		{
		}

		// Token: 0x04003918 RID: 14616
		private WCSLeagueInfoView.Table[] _tableList;

		// Token: 0x04003919 RID: 14617
		private ElementObjectManager _topEom;

		// Token: 0x0400391A RID: 14618
		private SelectionButton _ruleButton;

		// Token: 0x02000815 RID: 2069
		private enum BattleResult
		{
			// Token: 0x0400391C RID: 14620
			UNKOWN,
			// Token: 0x0400391D RID: 14621
			WIN,
			// Token: 0x0400391E RID: 14622
			LOSE,
			// Token: 0x0400391F RID: 14623
			DRAW
		}

		// Token: 0x02000816 RID: 2070
		private class Table
		{
			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x000029CC File Offset: 0x00000BCC
			internal int rowNum
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x000029CC File Offset: 0x00000BCC
			internal int colNum
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06003FF3 RID: 16371 RVA: 0x00002739 File Offset: 0x00000939
			internal Table(ElementObjectManager eom)
			{
			}

			// Token: 0x06003FF4 RID: 16372 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Terminate()
			{
			}

			// Token: 0x06003FF5 RID: 16373 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetHeaderGroupName(string name)
			{
			}

			// Token: 0x06003FF6 RID: 16374 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetTeamIconOnHeader(int colIndex, string spritePath)
			{
			}

			// Token: 0x06003FF7 RID: 16375 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetTeamInfo(int rowIndex, string areaName, string name, string iconSpritePath)
			{
			}

			// Token: 0x06003FF8 RID: 16376 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetTeamWinCount(int rowIndex, int winCount)
			{
			}

			// Token: 0x06003FF9 RID: 16377 RVA: 0x0000216D File Offset: 0x0000036D
			internal void HideTeamWinCount(int rowIndex)
			{
			}

			// Token: 0x06003FFA RID: 16378 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetIndivisualWinDiff(int rowIndex, int deltaNum)
			{
			}

			// Token: 0x06003FFB RID: 16379 RVA: 0x0000216D File Offset: 0x0000036D
			internal void HideIndivisualWinDiff(int rowIndex)
			{
			}

			// Token: 0x06003FFC RID: 16380 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetRank(int rowIndex, int rank, bool top2Fixed)
			{
			}

			// Token: 0x06003FFD RID: 16381 RVA: 0x0000216D File Offset: 0x0000036D
			internal void HideRank(int rowIndex)
			{
			}

			// Token: 0x06003FFE RID: 16382 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetBattleRecord(int rowIndex, int columnIndex, WCSLeagueInfoView.BattleResult? result, int myWinCount, int oppWinCount)
			{
			}

			// Token: 0x06003FFF RID: 16383 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetStatusBattleRecord(int rowIndex, int columnIndex, bool inDuel)
			{
			}

			// Token: 0x06004000 RID: 16384 RVA: 0x000F46AE File Offset: 0x000F28AE
			internal void RegisterTeamSituationBtnAction(int rowIndex, int columnIndex, UnityAction callback, out bool cursorMoving)
			{
				cursorMoving = false;
			}

			// Token: 0x06004001 RID: 16385 RVA: 0x000029CC File Offset: 0x00000BCC
			internal bool FocusButtonCursor()
			{
				return false;
			}

			// Token: 0x04003920 RID: 14624
			internal const int TEAM_MAX = 4;

			// Token: 0x04003921 RID: 14625
			private const string LABEL_DUELICON = "DuelIcon";

			// Token: 0x04003922 RID: 14626
			private ElementObjectManager _eom;

			// Token: 0x04003923 RID: 14627
			private ElementObjectManager _tableHeaderEom;

			// Token: 0x04003924 RID: 14628
			private ElementObjectManager[] _teamRowEom;

			// Token: 0x04003925 RID: 14629
			private ElementObjectManager[,] _eachBattleRecordsEom;

			// Token: 0x04003926 RID: 14630
			private SelectionButton[] _teamSituationButtons;
		}
	}
}
