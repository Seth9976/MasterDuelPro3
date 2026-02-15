using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000563 RID: 1379
	public abstract class AbstractInputBlocker : MonoBehaviour
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06002BF8 RID: 11256
		protected abstract int blockPriority { get; }

		// Token: 0x06002BF9 RID: 11257 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x04002A77 RID: 10871
		protected bool blocking;
	}
}
