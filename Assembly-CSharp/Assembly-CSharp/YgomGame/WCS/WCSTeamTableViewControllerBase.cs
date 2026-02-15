using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.YGomTMPro;

namespace YgomGame.WCS
{
	// Token: 0x020007ED RID: 2029
	public class WCSTeamTableViewControllerBase : BaseMenuViewController
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06003EFD RID: 16125 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x0000216D File Offset: 0x0000036D
		private protected bool dryrun
		{
			[CompilerGenerated]
			protected get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06003F00 RID: 16128 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003F01 RID: 16129 RVA: 0x0000216D File Offset: 0x0000036D
		private protected bool initialized
		{
			[CompilerGenerated]
			protected get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06003F02 RID: 16130 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int teamCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int tableCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0000216D File Offset: 0x0000036D
		private void Terminate()
		{
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0000216D File Offset: 0x0000036D
		protected sealed override void OnCreatedView()
		{
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void InitializeView()
		{
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTeamInfo()
		{
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTableData(GameObject gob, int index)
		{
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTitle(string title)
		{
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLeftTeamInfo(string areaName, string name, int? score, string iconPath)
		{
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetRightTeamInfo(string areaName, string name, int? score, string iconPath)
		{
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x0000216D File Offset: 0x0000036D
		protected void EnableLeftTeamInfoBtn(bool on)
		{
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x0000216D File Offset: 0x0000036D
		protected void EnableRightTeamInfoBtn(bool on)
		{
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTeamWin(WCSTeamTableViewControllerBase.Side which)
		{
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ResetTeamResult()
		{
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTableStatusText(int index, string text)
		{
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLeftPlayerInfo(int index, string name, ValueTuple<int, int>? profileIconIDs)
		{
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetRightPlayerInfo(int index, string name, ValueTuple<int, int>? profileIconIDs)
		{
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetPlayerWin(int tableIndex, WCSTeamTableViewControllerBase.Side which)
		{
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetResultDraw(int tableIndex)
		{
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ResetBothPlayerResult(int tableIndex)
		{
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLeftTeamInfoButtonListener(UnityAction callback)
		{
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetRightTeamInfoButtonListener(UnityAction callback)
		{
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTableStatusButton(int index, bool pushable)
		{
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTableStatusButtonListener(int index, UnityAction callback)
		{
		}

		// Token: 0x040037F8 RID: 14328
		private const int DEFAULT_TABLE_NUM = 3;

		// Token: 0x040037F9 RID: 14329
		internal const string ARG_KEY_TABLE_NUM = "table_num";

		// Token: 0x040037FA RID: 14330
		internal const string ARG_KEY_DRYRUN = "dryrun";

		// Token: 0x040037FB RID: 14331
		private WCSTeamTableViewControllerBase.TeamInfo[] _teamList;

		// Token: 0x040037FC RID: 14332
		private WCSTeamTableViewControllerBase.Table[] _tableDataList;

		// Token: 0x040037FD RID: 14333
		protected InfinityScrollView _scrollView;

		// Token: 0x020007EE RID: 2030
		public enum Side
		{
			// Token: 0x040037FF RID: 14335
			LEFT,
			// Token: 0x04003800 RID: 14336
			RIGHT
		}

		// Token: 0x020007EF RID: 2031
		private enum Result
		{
			// Token: 0x04003802 RID: 14338
			NONE,
			// Token: 0x04003803 RID: 14339
			WIN,
			// Token: 0x04003804 RID: 14340
			LOSE,
			// Token: 0x04003805 RID: 14341
			DRAW
		}

		// Token: 0x020007F0 RID: 2032
		private class TeamInfo
		{
			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x06003F1F RID: 16159 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003F20 RID: 16160 RVA: 0x0000216D File Offset: 0x0000036D
			internal bool win
			{
				[CompilerGenerated]
				get
				{
					return false;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x06003F21 RID: 16161 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003F22 RID: 16162 RVA: 0x0000216D File Offset: 0x0000036D
			internal string areaName
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x06003F23 RID: 16163 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003F24 RID: 16164 RVA: 0x0000216D File Offset: 0x0000036D
			internal string name
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x06003F25 RID: 16165 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003F26 RID: 16166 RVA: 0x0000216D File Offset: 0x0000036D
			internal int score
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x170004DF RID: 1247
			// (get) Token: 0x06003F27 RID: 16167 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003F28 RID: 16168 RVA: 0x0000216D File Offset: 0x0000036D
			internal string teamIconPath
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06003F29 RID: 16169 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Terminate()
			{
			}

			// Token: 0x06003F2A RID: 16170 RVA: 0x0000216D File Offset: 0x0000036D
			internal void UpdateUI()
			{
			}

			// Token: 0x06003F2B RID: 16171 RVA: 0x0000216D File Offset: 0x0000036D
			internal void EnableInfoButton(bool on)
			{
			}

			// Token: 0x06003F2C RID: 16172 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetInfoButtonListener(UnityAction callback)
			{
			}

			// Token: 0x06003F2D RID: 16173 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetTeamInfo(string areaName, string name, int? score, string iconPath)
			{
			}

			// Token: 0x06003F2E RID: 16174 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetResult(bool win)
			{
			}

			// Token: 0x04003806 RID: 14342
			private bool _update;

			// Token: 0x04003807 RID: 14343
			internal ExtendedTextMeshProUGUI uiGroupText;

			// Token: 0x04003808 RID: 14344
			internal ExtendedTextMeshProUGUI uiNameText;

			// Token: 0x04003809 RID: 14345
			internal ExtendedTextMeshProUGUI uiScoreText;

			// Token: 0x0400380A RID: 14346
			internal GameObject uiWinnerIcon;

			// Token: 0x0400380B RID: 14347
			internal SelectionButton uiInfoBtn;

			// Token: 0x0400380C RID: 14348
			internal Image uiTeamIcon;
		}

		// Token: 0x020007F1 RID: 2033
		private class Table
		{
			// Token: 0x170004E0 RID: 1248
			// (get) Token: 0x06003F30 RID: 16176 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003F31 RID: 16177 RVA: 0x0000216D File Offset: 0x0000036D
			internal string statusText
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06003F32 RID: 16178 RVA: 0x00002739 File Offset: 0x00000939
			private Table()
			{
			}

			// Token: 0x06003F33 RID: 16179 RVA: 0x0000216A File Offset: 0x0000036A
			internal static WCSTeamTableViewControllerBase.Table[] CreateTables(uint count)
			{
				return null;
			}

			// Token: 0x06003F34 RID: 16180 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Terminate()
			{
			}

			// Token: 0x06003F35 RID: 16181 RVA: 0x0000216D File Offset: 0x0000036D
			internal void UpdateUI()
			{
			}

			// Token: 0x06003F36 RID: 16182 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetStatusButton(bool pushable)
			{
			}

			// Token: 0x06003F37 RID: 16183 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetStatusButtonText(string labelText)
			{
			}

			// Token: 0x06003F38 RID: 16184 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetStatusButtonListener(UnityAction callback)
			{
			}

			// Token: 0x06003F39 RID: 16185 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetWin(WCSTeamTableViewControllerBase.Side which)
			{
			}

			// Token: 0x06003F3A RID: 16186 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetDraw()
			{
			}

			// Token: 0x06003F3B RID: 16187 RVA: 0x0000216D File Offset: 0x0000036D
			internal void ResetBothResult()
			{
			}

			// Token: 0x0400380D RID: 14349
			internal WCSTeamTableViewControllerBase.Player[] players;

			// Token: 0x0400380E RID: 14350
			private bool _update;

			// Token: 0x0400380F RID: 14351
			internal SelectionButton uiStatusButton;

			// Token: 0x04003810 RID: 14352
			internal ExtendedTextMeshProUGUI uiStatusButtonText;
		}

		// Token: 0x020007F2 RID: 2034
		private class Player
		{
			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x06003F3C RID: 16188 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003F3D RID: 16189 RVA: 0x0000216D File Offset: 0x0000036D
			internal WCSTeamTableViewControllerBase.Result result
			{
				[CompilerGenerated]
				get
				{
					return WCSTeamTableViewControllerBase.Result.NONE;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x06003F3E RID: 16190 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003F3F RID: 16191 RVA: 0x0000216D File Offset: 0x0000036D
			internal string name
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x06003F40 RID: 16192 RVA: 0x000F4680 File Offset: 0x000F2880
			// (set) Token: 0x06003F41 RID: 16193 RVA: 0x0000216D File Offset: 0x0000036D
			internal ValueTuple<int, int> iconIDs
			{
				[CompilerGenerated]
				get
				{
					return default(ValueTuple<int, int>);
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06003F42 RID: 16194 RVA: 0x0000216D File Offset: 0x0000036D
			internal void UpdateUI()
			{
			}

			// Token: 0x06003F43 RID: 16195 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetPlayerInfo(string name, ValueTuple<int, int>? profileIconIDs)
			{
			}

			// Token: 0x06003F44 RID: 16196 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetResult(WCSTeamTableViewControllerBase.Result result)
			{
			}

			// Token: 0x04003811 RID: 14353
			private bool _update;

			// Token: 0x04003812 RID: 14354
			internal ExtendedTextMeshProUGUI uiPlayerName;

			// Token: 0x04003813 RID: 14355
			internal GameObject uiPlayerIcon;

			// Token: 0x04003814 RID: 14356
			internal ExtendedTextMeshProUGUI uiResultText;
		}
	}
}
