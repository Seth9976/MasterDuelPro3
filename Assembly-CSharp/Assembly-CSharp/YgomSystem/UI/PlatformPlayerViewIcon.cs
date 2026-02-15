using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005AE RID: 1454
	public class PlatformPlayerViewIcon : MonoBehaviour
	{
		// Token: 0x06002DFC RID: 11772 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Awake()
		{
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x04002BAF RID: 11183
		public PlatformPlayerViewIcon.DispType dispTarget;

		// Token: 0x04002BB0 RID: 11184
		public bool isReverse;

		// Token: 0x020005AF RID: 1455
		public enum DispType
		{
			// Token: 0x04002BB2 RID: 11186
			Graphic,
			// Token: 0x04002BB3 RID: 11187
			GameObject
		}
	}
}
