using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000533 RID: 1331
	public class OnScreenSignature : MonoBehaviour
	{
		// Token: 0x06002A9F RID: 10911 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnabled(bool enabled)
		{
		}

		// Token: 0x040029C2 RID: 10690
		public static OnScreenSignature instance;

		// Token: 0x040029C3 RID: 10691
		private SpriteRenderer spriteRenderer;

		// Token: 0x040029C4 RID: 10692
		private bool m_isInitialized;
	}
}
