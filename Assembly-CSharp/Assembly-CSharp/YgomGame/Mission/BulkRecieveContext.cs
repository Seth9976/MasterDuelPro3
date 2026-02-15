using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomGame.Dialog.CommonDialog;

namespace YgomGame.Mission
{
	// Token: 0x02000A24 RID: 2596
	public class BulkRecieveContext
	{
		// Token: 0x06004B55 RID: 19285 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetRecievableLength()
		{
			return 0;
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(IReadOnlyList<TabContext> tabCtxts)
		{
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x000F4A04 File Offset: 0x000F2C04
		public ValueTuple<List<int>, List<int>, List<int>> ExportReq(TabContext tabContext)
		{
			return default(ValueTuple<List<int>, List<int>, List<int>>);
		}

		// Token: 0x04008957 RID: 35159
		private readonly Dictionary<TabContext, BulkRecieveContext.RecievableContext> m_RecievableContextMap;

		// Token: 0x04008958 RID: 35160
		public readonly BulkRecieveContext.ResultContext resultContext;

		// Token: 0x02000A25 RID: 2597
		public class RecievableContext
		{
			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x06004B5A RID: 19290 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004B5B RID: 19291 RVA: 0x0000216D File Offset: 0x0000036D
			public int length
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

			// Token: 0x06004B5C RID: 19292 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x04008959 RID: 35161
			public readonly Dictionary<int, Dictionary<int, List<int>>> recieveMap;
		}

		// Token: 0x02000A26 RID: 2598
		public class ResultContext
		{
			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x06004B5E RID: 19294 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004B5F RID: 19295 RVA: 0x0000216D File Offset: 0x0000036D
			public List<object> totalItems
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

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x06004B60 RID: 19296 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004B61 RID: 19297 RVA: 0x0000216D File Offset: 0x0000036D
			public bool isSendPresent
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

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x06004B62 RID: 19298 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isReceived
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004B63 RID: 19299 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x06004B64 RID: 19300 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(Dictionary<string, object> resultData)
			{
			}

			// Token: 0x06004B65 RID: 19301 RVA: 0x0000216A File Offset: 0x0000036A
			public EntryItemListData.Context SearchGoalItemContext(MissionContext missionCtx, int goalPos)
			{
				return null;
			}

			// Token: 0x0400895A RID: 35162
			public Dictionary<string, object> goalItems;
		}
	}
}
