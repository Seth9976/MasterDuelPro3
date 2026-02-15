using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000554 RID: 1364
	internal class ShaderInfoStorage<T> : BaseShaderInfoStorage where T : struct
	{
		// Token: 0x060025A5 RID: 9637 RVA: 0x00095698 File Offset: 0x00093898
		public ShaderInfoStorage(TextureFormat format, Func<Color, T> convert, int initialSize = 64, int maxSize = 4096)
		{
			Debug.Assert(maxSize <= SystemInfo.maxTextureSize);
			Debug.Assert(initialSize <= maxSize);
			Debug.Assert(Mathf.IsPowerOfTwo(initialSize));
			Debug.Assert(Mathf.IsPowerOfTwo(maxSize));
			Debug.Assert(convert != null);
			this.m_InitialSize = initialSize;
			this.m_MaxSize = maxSize;
			this.m_Format = format;
			this.m_Convert = convert;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00095710 File Offset: 0x00093910
		protected override void Dispose(bool disposing)
		{
			bool flag = !base.disposed && disposing;
			if (flag)
			{
				UIRUtility.Destroy(this.m_Texture);
				this.m_Texture = null;
				this.m_Texels = default(NativeArray<T>);
				UIRAtlasAllocator allocator = this.m_Allocator;
				if (allocator != null)
				{
					allocator.Dispose();
				}
				this.m_Allocator = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x0009576F File Offset: 0x0009396F
		public override Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x00095778 File Offset: 0x00093978
		public override bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool disposed = base.disposed;
			bool flag;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				uvs = default(RectInt);
				flag = false;
			}
			else
			{
				bool flag2 = this.m_Allocator == null;
				if (flag2)
				{
					this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, this.m_MaxSize, 0);
				}
				bool flag3 = !this.m_Allocator.TryAllocate(width, height, out uvs);
				if (flag3)
				{
					flag = false;
				}
				else
				{
					uvs = new RectInt(uvs.x, uvs.y, width, height);
					this.CreateOrExpandTexture();
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00095808 File Offset: 0x00093A08
		public override void SetTexel(int x, int y, Color color)
		{
			bool disposed = base.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.m_Texels.IsCreated;
				if (flag)
				{
					this.m_Texels = this.m_Texture.GetRawTextureData<T>();
				}
				this.m_Texels[x + y * this.m_Texture.width] = this.m_Convert(color);
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00095878 File Offset: 0x00093A78
		public override void UpdateTexture()
		{
			bool disposed = base.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_Texture == null || !this.m_Texels.IsCreated;
				if (!flag)
				{
					this.m_Texture.Apply(false, false);
					this.m_Texels = default(NativeArray<T>);
				}
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000958DC File Offset: 0x00093ADC
		private void CreateOrExpandTexture()
		{
			int newWidth = this.m_Allocator.physicalWidth;
			int newHeight = this.m_Allocator.physicalHeight;
			bool copy = false;
			bool flag = this.m_Texture != null;
			if (flag)
			{
				bool flag2 = this.m_Texture.width == newWidth && this.m_Texture.height == newHeight;
				if (flag2)
				{
					return;
				}
				copy = true;
			}
			Texture2D newTexture = new Texture2D(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, this.m_Format, false)
			{
				name = "UIR Shader Info " + BaseShaderInfoStorage.s_TextureCounter++.ToString(),
				hideFlags = HideFlags.HideAndDontSave,
				filterMode = FilterMode.Point
			};
			bool flag3 = copy;
			if (flag3)
			{
				NativeArray<T> oldTexels = (this.m_Texels.IsCreated ? this.m_Texels : this.m_Texture.GetRawTextureData<T>());
				NativeArray<T> newTexels = newTexture.GetRawTextureData<T>();
				ShaderInfoStorage<T>.CpuBlit(oldTexels, this.m_Texture.width, this.m_Texture.height, newTexels, newTexture.width, newTexture.height);
				this.m_Texels = newTexels;
			}
			else
			{
				this.m_Texels = default(NativeArray<T>);
			}
			UIRUtility.Destroy(this.m_Texture);
			this.m_Texture = newTexture;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00095A28 File Offset: 0x00093C28
		private static void CpuBlit(NativeArray<T> src, int srcWidth, int srcHeight, NativeArray<T> dst, int dstWidth, int dstHeight)
		{
			Debug.Assert(dstWidth >= srcWidth && dstHeight >= srcHeight);
			int widthDiff = dstWidth - srcWidth;
			int heightDiff = dstHeight - srcHeight;
			int srcCount = srcWidth * srcHeight;
			int srcIndex = 0;
			int dstIndex = 0;
			int srcBreak = srcWidth;
			while (srcIndex < srcCount)
			{
				while (srcIndex < srcBreak)
				{
					dst[dstIndex] = src[srcIndex];
					dstIndex++;
					srcIndex++;
				}
				srcBreak += srcWidth;
				dstIndex += widthDiff;
			}
		}

		// Token: 0x040012D6 RID: 4822
		private readonly int m_InitialSize;

		// Token: 0x040012D7 RID: 4823
		private readonly int m_MaxSize;

		// Token: 0x040012D8 RID: 4824
		private readonly TextureFormat m_Format;

		// Token: 0x040012D9 RID: 4825
		private readonly Func<Color, T> m_Convert;

		// Token: 0x040012DA RID: 4826
		private UIRAtlasAllocator m_Allocator;

		// Token: 0x040012DB RID: 4827
		private Texture2D m_Texture;

		// Token: 0x040012DC RID: 4828
		private NativeArray<T> m_Texels;
	}
}
