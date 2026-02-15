using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200062F RID: 1583
	public class TweenRotationTo : Tween
	{
		// Token: 0x060031E8 RID: 12776 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E6D RID: 11885
		private Vector3 from;

		// Token: 0x04002E6E RID: 11886
		public Vector3 to;

		// Token: 0x04002E6F RID: 11887
		public bool quaternionLerp;
	}
}
