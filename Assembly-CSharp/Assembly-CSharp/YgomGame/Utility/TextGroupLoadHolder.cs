using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Utility
{
	// Token: 0x02000835 RID: 2101
	public class TextGroupLoadHolder : MonoBehaviour
	{
		// Token: 0x060040B4 RID: 16564 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadCheck<T>()
		{
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadCheck(Type textGroupType)
		{
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadCheck(string groupId)
		{
		}

		// Token: 0x040039B3 RID: 14771
		private List<string> m_LoadedTextGroups;
	}
}
