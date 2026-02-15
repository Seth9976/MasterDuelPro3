using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Mission
{
	// Token: 0x02000A43 RID: 2627
	public class TabContext : IComparable<TabContext>
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<MissionContext> missions
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyDictionary<int, MissionContext> missionMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004C8A RID: 19594 RVA: 0x0000216D File Offset: 0x0000036D
		public string tabNameText
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

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004C8C RID: 19596 RVA: 0x0000216D File Offset: 0x0000036D
		public string tabShortNameText
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

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool inRewardUnlocked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06004C8E RID: 19598 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool completed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x000F4A48 File Offset: 0x000F2C48
		public ValueTuple<bool, int> SearchBadgeInfos()
		{
			return default(ValueTuple<bool, int>);
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x00002739 File Offset: 0x00000939
		public TabContext(MissionTabType tabType, int campaignPoolId = 0, long campaignBeginTs = 0L, string tabNameTextId = null, string tabShortNameTextId = null)
		{
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0000216D File Offset: 0x0000036D
		public void SortMissions()
		{
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadTexts()
		{
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Compare(TabContext a, TabContext b)
		{
			return 0;
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CompareTo(TabContext other)
		{
			return 0;
		}

		// Token: 0x04008A55 RID: 35413
		public readonly MissionTabType tabType;

		// Token: 0x04008A56 RID: 35414
		public readonly int campaignPoolId;

		// Token: 0x04008A57 RID: 35415
		public readonly long campaignBeginTs;

		// Token: 0x04008A58 RID: 35416
		public readonly string tabNameTextId;

		// Token: 0x04008A59 RID: 35417
		public readonly string tabShortNameTextId;

		// Token: 0x04008A5A RID: 35418
		protected readonly List<MissionContext> m_Missions;

		// Token: 0x04008A5B RID: 35419
		protected readonly Dictionary<int, MissionContext> m_MissionMap;
	}
}
