using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000089 RID: 137
	public struct QuaternionOptions : IPlugOptions
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		public void Reset()
		{
			this.rotateMode = RotateMode.Fast;
			this.axisConstraint = AxisConstraint.None;
			this.up = Vector3.zero;
			this.dynamicLookAt = false;
			this.dynamicLookAtWorldPosition = Vector3.zero;
		}

		// Token: 0x0400017F RID: 383
		public RotateMode rotateMode;

		// Token: 0x04000180 RID: 384
		public AxisConstraint axisConstraint;

		// Token: 0x04000181 RID: 385
		public Vector3 up;

		// Token: 0x04000182 RID: 386
		public bool dynamicLookAt;

		// Token: 0x04000183 RID: 387
		public Vector3 dynamicLookAtWorldPosition;
	}
}
