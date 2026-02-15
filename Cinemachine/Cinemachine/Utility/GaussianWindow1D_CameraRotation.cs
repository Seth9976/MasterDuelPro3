using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000EA RID: 234
	internal class GaussianWindow1D_CameraRotation : GaussianWindow1d<Vector2>
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x00022143 File Offset: 0x00020343
		public GaussianWindow1D_CameraRotation(float sigma, int maxKernelRadius = 10)
			: base(sigma, maxKernelRadius)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00022150 File Offset: 0x00020350
		protected override Vector2 Compute(int windowPos)
		{
			Vector2 sum = Vector2.zero;
			Vector2 v = this.mData[this.mCurrentPos];
			for (int i = 0; i < base.KernelSize; i++)
			{
				Vector2 v2 = this.mData[windowPos] - v;
				if (v2.y > 180f)
				{
					v2.y -= 360f;
				}
				if (v2.y < -180f)
				{
					v2.y += 360f;
				}
				sum += v2 * this.mKernel[i];
				if (++windowPos == base.KernelSize)
				{
					windowPos = 0;
				}
			}
			return v + sum;
		}
	}
}
