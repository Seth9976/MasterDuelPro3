using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Mission
{
	// Token: 0x02000A37 RID: 2615
	public class MissionRootContext
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06004BEB RID: 19435 RVA: 0x0000216A File Offset: 0x0000036A
		public List<TabContext> tabs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06004BEC RID: 19436 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> tabTemplates
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06004BED RID: 19437 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004BEE RID: 19438 RVA: 0x0000216D File Offset: 0x0000036D
		public int tabIdx
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06004BEF RID: 19439 RVA: 0x0000216A File Offset: 0x0000036A
		public TabContext currentTab
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06004BF0 RID: 19440 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004BF1 RID: 19441 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int> onPrevChangeTabIdxEvent
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

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06004BF2 RID: 19442 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004BF3 RID: 19443 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int> onChangeTabIdxEvent
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

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06004BF4 RID: 19444 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004BF5 RID: 19445 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onUpdatedAllEvent
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

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06004BF6 RID: 19446 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004BF7 RID: 19447 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onUpdatedContainMissionsEvent
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

		// Token: 0x06004BF8 RID: 19448 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitResidentTabs()
		{
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0000216D File Offset: 0x0000036D
		public void SearchInOutMissions(Dictionary<string, object> datas, Dictionary<string, object> completeDatas, Dictionary<string, object> hideDatas, List<int> resEntries, List<int> resRemoves, List<int> resHides)
		{
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportMissions(Dictionary<string, object> master, Dictionary<string, object> data, int initTabPoolId = 0, MissionTabType initTabType = MissionTabType.All)
		{
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportContainMissionsUpdate(Func<int, int, Dictionary<string, object>> missionDataGetterFunc)
		{
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportMissionsOverride(Dictionary<string, object> master, Dictionary<string, object> data, int initTabPoolId = 0, MissionTabType initTabType = MissionTabType.All)
		{
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0000216D File Offset: 0x0000036D
		public void TurnOffNew(int tabIdx)
		{
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdatedContainMissions()
		{
		}

		// Token: 0x040089FC RID: 35324
		private readonly List<TabContext> m_Tabs;

		// Token: 0x040089FD RID: 35325
		private readonly List<int> m_TabTemplates;

		// Token: 0x040089FE RID: 35326
		private readonly List<TabContextImportable> m_ImportableTabs;

		// Token: 0x040089FF RID: 35327
		private readonly TabAllContext m_TabAll;

		// Token: 0x04008A00 RID: 35328
		private readonly Dictionary<int, TabContextImportable> m_CampaignTabMap;

		// Token: 0x04008A01 RID: 35329
		private bool m_IsInitialized;

		// Token: 0x04008A02 RID: 35330
		private int m_TabIdx;

		// Token: 0x04008A03 RID: 35331
		public readonly BulkRecieveContext bulkRecieve;
	}
}
