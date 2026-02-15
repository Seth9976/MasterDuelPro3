using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x020004F5 RID: 1269
	public class AdoptiveParent : MonoBehaviour
	{
		// Token: 0x06002818 RID: 10264 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddAdoptedChild(GameObject adoptedChild)
		{
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveAdoptedChild(GameObject adoptedChild)
		{
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x040028DF RID: 10463
		public List<GameObject> adoptedChildren;

		// Token: 0x040028E0 RID: 10464
		public bool syncActive;

		// Token: 0x040028E1 RID: 10465
		public bool syncDestroy;
	}
}
