using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000E8 RID: 232
	internal class GaussianWindow1D_Vector3 : GaussianWindow1d<Vector3>
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x00021FD9 File Offset: 0x000201D9
		public GaussianWindow1D_Vector3(float sigma, int maxKernelRadius = 10)
			: base(sigma, maxKernelRadius)
		{
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00021FE4 File Offset: 0x000201E4
		protected override Vector3 Compute(int windowPos)
		{
			Vector3 sum = Vector3.zero;
			for (int i = 0; i < base.KernelSize; i++)
			{
				sum += this.mData[windowPos] * this.mKernel[i];
				if (++windowPos == base.KernelSize)
				{
					windowPos = 0;
				}
			}
			return sum;
		}
	}
}
