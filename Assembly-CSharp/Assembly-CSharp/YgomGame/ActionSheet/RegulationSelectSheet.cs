using System;
using System.Collections.Generic;
using YgomGame.Menu;

namespace YgomGame.ActionSheet
{
	// Token: 0x02001152 RID: 4434
	public class RegulationSelectSheet
	{
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x060083DB RID: 33755 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> regulationIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x060083DC RID: 33756 RVA: 0x0000216A File Offset: 0x0000036A
		public List<ActionSheetViewController.EntryData> regulationNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060083DD RID: 33757 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInPeriodTournaments(bool checkBox = false, int defaultReg = 0, Dictionary<string, object> ruleInfo = null)
		{
		}

		// Token: 0x060083DE RID: 33758 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(Action<int, string> callback)
		{
		}

		// Token: 0x060083DF RID: 33759 RVA: 0x000F7454 File Offset: 0x000F5654
		public ValueTuple<int, string> GetFirstRegulationInfo()
		{
			return default(ValueTuple<int, string>);
		}

		// Token: 0x060083E0 RID: 33760 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetRegLabel(int regId)
		{
			return null;
		}

		// Token: 0x060083E1 RID: 33761 RVA: 0x000F1C76 File Offset: 0x000EFE76
		private bool GetTextGroup(string fullTextId, out string groupId)
		{
			groupId = null;
			return false;
		}

		// Token: 0x060083E2 RID: 33762 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenInPeriodTournament(Action<int, string> callback, bool checkBox = false, int defaultReg = 0)
		{
		}

		// Token: 0x0400BF40 RID: 48960
		private List<int> m_RegulationIds;

		// Token: 0x0400BF41 RID: 48961
		private List<ActionSheetViewController.EntryData> m_Entrys;
	}
}
