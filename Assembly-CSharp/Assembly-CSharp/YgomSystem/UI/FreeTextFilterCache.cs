using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000596 RID: 1430
	public class FreeTextFilterCache : MonoBehaviour
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<FreeTextFilter> referencedFilteres
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<string> cachedFreeTexts
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyDictionary<string, string> filteredTextMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x0000216A File Offset: 0x0000036A
		private static FreeTextFilterCache GetInstance()
		{
			return null;
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x0000216D File Offset: 0x0000036D
		private static void DestroyInstance()
		{
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x0000216A File Offset: 0x0000036A
		public static FreeTextFilterCache AssignReference(FreeTextFilter freeTextFilter)
		{
			return null;
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveReference(FreeTextFilter freeTextFilter)
		{
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x0000216D File Offset: 0x0000036D
		public void RequestFilter(FreeTextFilter owner, string freeText)
		{
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExecuteFilterAsync(string freeText)
		{
		}

		// Token: 0x04002B48 RID: 11080
		private static FreeTextFilterCache s_Instance;

		// Token: 0x04002B49 RID: 11081
		private List<FreeTextFilter> m_ReferencedFilteres;

		// Token: 0x04002B4A RID: 11082
		private List<string> m_CachedFreeTexts;

		// Token: 0x04002B4B RID: 11083
		private Dictionary<string, string> m_FilteredTextMap;

		// Token: 0x04002B4C RID: 11084
		private Dictionary<string, List<FreeTextFilter>> m_FilterRequests;

		// Token: 0x04002B4D RID: 11085
		private readonly int k_CacheReleaseLine;

		// Token: 0x04002B4E RID: 11086
		private readonly int k_CacheReleaseCount;
	}
}
