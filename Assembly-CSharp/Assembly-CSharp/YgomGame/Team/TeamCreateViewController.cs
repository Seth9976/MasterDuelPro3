using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomGame.Menu;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.YGomTMPro;

namespace YgomGame.Team
{
	// Token: 0x020008B7 RID: 2231
	public class TeamCreateViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004138 RID: 16696 RVA: 0x0000216D File Offset: 0x0000036D
		private TeamCreateViewController.Mode mode
		{
			[CompilerGenerated]
			get
			{
				return TeamCreateViewController.Mode.CREATE;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06004139 RID: 16697 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600413A RID: 16698 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600413B RID: 16699 RVA: 0x0000216D File Offset: 0x0000036D
		private SortedDictionary<int, List<TeamUtil.RegulationSet>> regulationSetDic
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager)
		{
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x0000216A File Offset: 0x0000036A
		private static SortedDictionary<int, List<TeamUtil.RegulationSet>> LoadRegulationSetData(ref int minimumTeamNum)
		{
			return null;
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitScrollAsync(Action onEnd)
		{
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupView()
		{
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadData()
		{
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMemberNum()
		{
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRegulationSet()
		{
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTeamNameCard()
		{
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRegulationToJoin()
		{
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateUsingDeck(Dictionary<string, object> deckInfo = null)
		{
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTeamMemberNumChanging(int num)
		{
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnRegulationSetSelecting(int index)
		{
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnRegulationToJoinSelecting(int regulationID, bool deckCheck = true)
		{
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCardIdSelected(int cardId)
		{
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPI_TeamCreate(Action onSuccess)
		{
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ShowFatalError()
		{
		}

		// Token: 0x04007F93 RID: 32659
		private TeamCreateViewController.ConfigData _config;

		// Token: 0x04007F94 RID: 32660
		private const string VC_PATH = "Team/TeamCreate";

		// Token: 0x04007F95 RID: 32661
		public static readonly string ARG_MODE;

		// Token: 0x04007F96 RID: 32662
		private const string ARG_REGSET_DATA = "REGSET_DATA";

		// Token: 0x04007F97 RID: 32663
		private const string ARG_CONFIG_DATA = "CONFIG_DATA";

		// Token: 0x04007F98 RID: 32664
		private static bool c_TextLoadInOpen;

		// Token: 0x04007F99 RID: 32665
		private ElementObjectManager _searchInputField;

		// Token: 0x04007F9A RID: 32666
		private ExtendedTextMeshProUGUI _titleText;

		// Token: 0x04007F9B RID: 32667
		private InfinityScrollView _scrollView;

		// Token: 0x04007F9C RID: 32668
		private ElementObjectManager _memberNumItem;

		// Token: 0x04007F9D RID: 32669
		private ElementObjectManager _regulationSetItem;

		// Token: 0x04007F9E RID: 32670
		private ElementObjectManager _teamCardNameItem;

		// Token: 0x04007F9F RID: 32671
		private ElementObjectManager _regulationToJoinItem;

		// Token: 0x04007FA0 RID: 32672
		private ElementObjectManager _usingDeckItem;

		// Token: 0x04007FA1 RID: 32673
		private SelectionButton _memberNumBtn;

		// Token: 0x04007FA2 RID: 32674
		private SelectionButton _regulationSetBtn;

		// Token: 0x04007FA3 RID: 32675
		private SelectionButton _teamCardNameBtn;

		// Token: 0x04007FA4 RID: 32676
		private SelectionButton _regulationToJoinBtn;

		// Token: 0x04007FA5 RID: 32677
		private SelectionButton _usingDeckBtn;

		// Token: 0x04007FA6 RID: 32678
		private DeckCaseWidget _deckCaseWidget;

		// Token: 0x04007FA7 RID: 32679
		private SelectionButton _decideBtn;

		// Token: 0x020008B8 RID: 2232
		private class ConfigData
		{
			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06004151 RID: 16721 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004152 RID: 16722 RVA: 0x0000216D File Offset: 0x0000036D
			internal int memberNum
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x06004153 RID: 16723 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004154 RID: 16724 RVA: 0x0000216D File Offset: 0x0000036D
			internal int regulationSetIdx
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x06004155 RID: 16725 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004156 RID: 16726 RVA: 0x0000216D File Offset: 0x0000036D
			internal int mrkForTeamName
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x06004157 RID: 16727 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004158 RID: 16728 RVA: 0x0000216D File Offset: 0x0000036D
			internal int regulationIDToJoin
			{
				[CompilerGenerated]
				get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x06004159 RID: 16729 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600415A RID: 16730 RVA: 0x0000216D File Offset: 0x0000036D
			internal TeamUtil.RegulationSet selectedRegulationSet
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}
		}

		// Token: 0x020008B9 RID: 2233
		public enum Mode
		{
			// Token: 0x04007FA9 RID: 32681
			CREATE,
			// Token: 0x04007FAA RID: 32682
			SEARCH,
			// Token: 0x04007FAB RID: 32683
			UNKNOWN
		}
	}
}
