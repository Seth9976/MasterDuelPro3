using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000E9 RID: 233
	internal class GaussianWindow1D_Quaternion : GaussianWindow1d<Quaternion>
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0002203A File Offset: 0x0002023A
		public GaussianWindow1D_Quaternion(float sigma, int maxKernelRadius = 10)
			: base(sigma, maxKernelRadius)
		{
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00022044 File Offset: 0x00020244
		protected override Quaternion Compute(int windowPos)
		{
			Quaternion sum = new Quaternion(0f, 0f, 0f, 0f);
			Quaternion q = this.mData[this.mCurrentPos];
			Quaternion qInverse = Quaternion.Inverse(q);
			for (int i = 0; i < base.KernelSize; i++)
			{
				float scale = this.mKernel[i];
				Quaternion q2 = qInverse * this.mData[windowPos];
				if (Quaternion.Dot(Quaternion.identity, q2) < 0f)
				{
					scale = -scale;
				}
				sum.x += q2.x * scale;
				sum.y += q2.y * scale;
				sum.z += q2.z * scale;
				sum.w += q2.w * scale;
				if (++windowPos == base.KernelSize)
				{
					windowPos = 0;
				}
			}
			return q * Quaternion.Normalize(sum);
		}
	}
}
