using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000640 RID: 1600
	public class TweenStepRotation : Tween
	{
		// Token: 0x06003217 RID: 12823 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002EB8 RID: 11960
		public Vector3 from;

		// Token: 0x04002EB9 RID: 11961
		public Vector3 to;

		// Token: 0x04002EBA RID: 11962
		public Vector3 origin;

		// Token: 0x04002EBB RID: 11963
		public Vector3 step;
	}
}
