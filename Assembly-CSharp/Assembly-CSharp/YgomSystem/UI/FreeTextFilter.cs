using System;
using TMPro;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000595 RID: 1429
	public class FreeTextFilter : MonoBehaviour
	{
		// Token: 0x06002D12 RID: 11538 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetText(string text)
		{
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFreeText(string freeText)
		{
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFilteredFreeText(string freeText, string filteredFreeText)
		{
		}

		// Token: 0x04002B44 RID: 11076
		private FreeTextFilterCache m_FreeTextFilterCache;

		// Token: 0x04002B45 RID: 11077
		private MDText m_TargetText;

		// Token: 0x04002B46 RID: 11078
		private TMP_Text m_TargetTMPText;

		// Token: 0x04002B47 RID: 11079
		private string m_FreeText;
	}
}
