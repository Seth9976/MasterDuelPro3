using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Team
{
	// Token: 0x020008CE RID: 2254
	public class TeamRegulationSetSelectViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported, IBokeSupported
	{
		// Token: 0x060041E5 RID: 16869 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTabSelected(int tabIndex, string[] values)
		{
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupTabShortcut(GameObject target, UnityAction action)
		{
		}

		// Token: 0x04008046 RID: 32838
		private const int SELECT_MAX = 3;

		// Token: 0x04008047 RID: 32839
		private const int REGULATIONSET_MAX = 5;

		// Token: 0x04008048 RID: 32840
		internal const string VC_PATH = "Team/TeamRegulationSetSelect";

		// Token: 0x04008049 RID: 32841
		private TeamRegulationSetSelectViewController.Param _param;

		// Token: 0x0400804A RID: 32842
		private int _prevSelectedIndex;

		// Token: 0x0400804B RID: 32843
		private TeamRegulationSetSelectViewController.TabButton[] _tabButtons;

		// Token: 0x0400804C RID: 32844
		private ExtendedTextMeshProUGUI _title;

		// Token: 0x0400804D RID: 32845
		private ExtendedTextMeshProUGUI[] _reguHeadTxts;

		// Token: 0x0400804E RID: 32846
		private ExtendedTextMeshProUGUI[] _reguValueTxts;

		// Token: 0x0400804F RID: 32847
		private SelectionButton _decideBtn;

		// Token: 0x020008CF RID: 2255
		public class Param
		{
			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x060041EA RID: 16874 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041EB RID: 16875 RVA: 0x0000216D File Offset: 0x0000036D
			public List<TeamUtil.RegulationSet> regulationSetList
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

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x060041EC RID: 16876 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060041ED RID: 16877 RVA: 0x0000216D File Offset: 0x0000036D
			public int selectedIndex
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

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x060041EE RID: 16878 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041EF RID: 16879 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<int> onResult
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

		// Token: 0x020008D0 RID: 2256
		private class TabButton
		{
			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x060041F1 RID: 16881 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041F2 RID: 16882 RVA: 0x0000216D File Offset: 0x0000036D
			internal ElementObjectManager tabTop
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

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x060041F3 RID: 16883 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041F4 RID: 16884 RVA: 0x0000216D File Offset: 0x0000036D
			internal SelectionButton button
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

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x060041F5 RID: 16885 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041F6 RID: 16886 RVA: 0x0000216D File Offset: 0x0000036D
			internal GameObject imageOn
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

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x060041F7 RID: 16887 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041F8 RID: 16888 RVA: 0x0000216D File Offset: 0x0000036D
			internal GameObject imageOff
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

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x060041F9 RID: 16889 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041FA RID: 16890 RVA: 0x0000216D File Offset: 0x0000036D
			internal string[] itemStrings
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

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x060041FB RID: 16891 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060041FC RID: 16892 RVA: 0x0000216D File Offset: 0x0000036D
			internal int index
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

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x060041FD RID: 16893 RVA: 0x000029CC File Offset: 0x00000BCC
			internal bool active
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x060041FE RID: 16894 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041FF RID: 16895 RVA: 0x0000216D File Offset: 0x0000036D
			internal string title
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x1400004B RID: 75
			// (add) Token: 0x06004200 RID: 16896 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004201 RID: 16897 RVA: 0x0000216D File Offset: 0x0000036D
			internal event Action<int, string[]> onSelected
			{
				[CompilerGenerated]
				add
				{
				}
				[CompilerGenerated]
				remove
				{
				}
			}

			// Token: 0x1400004C RID: 76
			// (add) Token: 0x06004202 RID: 16898 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004203 RID: 16899 RVA: 0x0000216D File Offset: 0x0000036D
			internal event Action onOkGoing
			{
				[CompilerGenerated]
				add
				{
				}
				[CompilerGenerated]
				remove
				{
				}
			}

			// Token: 0x06004204 RID: 16900 RVA: 0x00002739 File Offset: 0x00000939
			internal TabButton(ElementObjectManager tabTop, int index, string buttonTitle, string[] values)
			{
			}

			// Token: 0x06004205 RID: 16901 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetSelected(bool active)
			{
			}

			// Token: 0x06004206 RID: 16902 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Focus()
			{
			}

			// Token: 0x04008050 RID: 32848
			private string _title;
		}
	}
}
