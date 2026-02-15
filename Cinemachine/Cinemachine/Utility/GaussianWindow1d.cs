using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000E7 RID: 231
	internal abstract class GaussianWindow1d<T>
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00021DEF File Offset: 0x0001FFEF
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00021DF7 File Offset: 0x0001FFF7
		public float Sigma { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00021E00 File Offset: 0x00020000
		public int KernelSize
		{
			get
			{
				return this.mKernel.Length;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00021E0C File Offset: 0x0002000C
		private void GenerateKernel(float sigma, int maxKernelRadius)
		{
			int kernelRadius = Math.Min(maxKernelRadius, Mathf.FloorToInt(Mathf.Abs(sigma) * 2.5f));
			this.mKernel = new float[2 * kernelRadius + 1];
			if (kernelRadius == 0)
			{
				this.mKernel[0] = 1f;
			}
			else
			{
				float sum = 0f;
				for (int i = -kernelRadius; i <= kernelRadius; i++)
				{
					this.mKernel[i + kernelRadius] = (float)(Math.Exp((double)((float)(-(float)(i * i)) / (2f * sigma * sigma))) / (6.283185307179586 * (double)sigma * (double)sigma));
					sum += this.mKernel[i + kernelRadius];
				}
				for (int j = -kernelRadius; j <= kernelRadius; j++)
				{
					this.mKernel[j + kernelRadius] /= sum;
				}
			}
			this.Sigma = sigma;
		}

		// Token: 0x0600053D RID: 1341
		protected abstract T Compute(int windowPos);

		// Token: 0x0600053E RID: 1342 RVA: 0x00021ECA File Offset: 0x000200CA
		public GaussianWindow1d(float sigma, int maxKernelRadius = 10)
		{
			this.GenerateKernel(sigma, maxKernelRadius);
			this.mData = new T[this.KernelSize];
			this.mCurrentPos = -1;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00021EF9 File Offset: 0x000200F9
		public void Reset()
		{
			this.mCurrentPos = -1;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00021F02 File Offset: 0x00020102
		public bool IsEmpty()
		{
			return this.mCurrentPos < 0;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00021F10 File Offset: 0x00020110
		public void AddValue(T v)
		{
			if (this.mCurrentPos < 0)
			{
				for (int i = 0; i < this.KernelSize; i++)
				{
					this.mData[i] = v;
				}
				this.mCurrentPos = Mathf.Min(1, this.KernelSize - 1);
			}
			this.mData[this.mCurrentPos] = v;
			int num = this.mCurrentPos + 1;
			this.mCurrentPos = num;
			if (num == this.KernelSize)
			{
				this.mCurrentPos = 0;
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00021F8A File Offset: 0x0002018A
		public T Filter(T v)
		{
			if (this.KernelSize < 3)
			{
				return v;
			}
			this.AddValue(v);
			return this.Value();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00021FA4 File Offset: 0x000201A4
		public T Value()
		{
			return this.Compute(this.mCurrentPos);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00021FB2 File Offset: 0x000201B2
		public int BufferLength
		{
			get
			{
				return this.mData.Length;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00021FBC File Offset: 0x000201BC
		public void SetBufferValue(int index, T value)
		{
			this.mData[index] = value;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00021FCB File Offset: 0x000201CB
		public T GetBufferValue(int index)
		{
			return this.mData[index];
		}

		// Token: 0x040004A8 RID: 1192
		protected T[] mData;

		// Token: 0x040004A9 RID: 1193
		protected float[] mKernel;

		// Token: 0x040004AA RID: 1194
		protected int mCurrentPos = -1;
	}
}
