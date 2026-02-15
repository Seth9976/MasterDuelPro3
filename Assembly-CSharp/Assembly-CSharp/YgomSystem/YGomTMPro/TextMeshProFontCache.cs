using System;
using TMPro;
using UnityEngine;

namespace YgomSystem.YGomTMPro
{
	// Token: 0x020004EF RID: 1263
	[Obsolete]
	public class TextMeshProFontCache : MonoBehaviour
	{
		// Token: 0x060027FE RID: 10238 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x040028C0 RID: 10432
		private TMP_FontAsset m_CachedFontAsset;
	}
}
