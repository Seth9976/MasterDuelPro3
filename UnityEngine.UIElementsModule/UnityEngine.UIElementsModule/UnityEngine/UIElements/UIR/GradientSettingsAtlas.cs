using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000526 RID: 1318
	internal class GradientSettingsAtlas : IDisposable
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x0008B23C File Offset: 0x0008943C
		internal int length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x0008B254 File Offset: 0x00089454
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x0008B25C File Offset: 0x0008945C
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002490 RID: 9360 RVA: 0x0008B265 File Offset: 0x00089465
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0008B278 File Offset: 0x00089478
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_Atlas);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0008B2AF File Offset: 0x000894AF
		public GradientSettingsAtlas(int length = 4096)
		{
			this.m_Length = length;
			this.m_ElemWidth = 3;
			this.Reset();
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0008B2D0 File Offset: 0x000894D0
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator = new BestFitAllocator((uint)this.m_Length);
				UIRUtility.Destroy(this.m_Atlas);
				this.m_RawAtlas = default(GradientSettingsAtlas.RawTexture);
				this.MustCommit = false;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x0008B324 File Offset: 0x00089524
		public Texture2D atlas
		{
			get
			{
				return this.m_Atlas;
			}
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x0008B33C File Offset: 0x0008953C
		public Alloc Add(int count)
		{
			Debug.Assert(count > 0);
			bool disposed = this.disposed;
			Alloc alloc2;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				alloc2 = default(Alloc);
			}
			else
			{
				Alloc alloc = this.m_Allocator.Allocate((uint)count);
				alloc2 = alloc;
			}
			return alloc2;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x0008B384 File Offset: 0x00089584
		public void Write(Alloc alloc, GradientSettings[] settings, GradientRemap remap)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_RawAtlas.rgba == null;
				if (flag)
				{
					this.m_RawAtlas = new GradientSettingsAtlas.RawTexture
					{
						rgba = new Color32[this.m_ElemWidth * this.m_Length],
						width = this.m_ElemWidth,
						height = this.m_Length
					};
					int size = this.m_ElemWidth * this.m_Length;
					for (int i = 0; i < size; i++)
					{
						this.m_RawAtlas.rgba[i] = Color.black;
					}
				}
				int destY = (int)alloc.start;
				int j = 0;
				int settingsCount = settings.Length;
				while (j < settingsCount)
				{
					int destX = 0;
					GradientSettings entry = settings[j];
					Debug.Assert(remap == null || destY == remap.destIndex);
					bool flag2 = entry.gradientType == GradientType.Radial;
					if (flag2)
					{
						Vector2 focus = entry.radialFocus;
						focus += Vector2.one;
						focus /= 2f;
						focus.y = 1f - focus.y;
						this.m_RawAtlas.WriteRawFloat4Packed(0.003921569f, (float)entry.addressMode / 255f, focus.x, focus.y, destX++, destY);
					}
					else
					{
						bool flag3 = entry.gradientType == GradientType.Linear;
						if (flag3)
						{
							this.m_RawAtlas.WriteRawFloat4Packed(0f, (float)entry.addressMode / 255f, 0f, 0f, destX++, destY);
						}
					}
					Vector2Int pos = new Vector2Int(entry.location.x, entry.location.y);
					Vector2 size2 = new Vector2((float)(entry.location.width - 1), (float)(entry.location.height - 1));
					bool flag4 = remap != null;
					if (flag4)
					{
						pos = new Vector2Int(remap.location.x, remap.location.y);
						size2 = new Vector2((float)(remap.location.width - 1), (float)(remap.location.height - 1));
					}
					this.m_RawAtlas.WriteRawInt2Packed(pos.x, pos.y, destX++, destY);
					this.m_RawAtlas.WriteRawInt2Packed((int)size2.x, (int)size2.y, destX++, destY);
					remap = ((remap != null) ? remap.next : null);
					destY++;
					j++;
				}
				this.MustCommit = true;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x0008B645 File Offset: 0x00089845
		// (set) Token: 0x06002498 RID: 9368 RVA: 0x0008B64D File Offset: 0x0008984D
		public bool MustCommit { get; private set; }

		// Token: 0x06002499 RID: 9369 RVA: 0x0008B658 File Offset: 0x00089858
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.MustCommit;
				if (!flag)
				{
					this.PrepareAtlas();
					this.m_Atlas.SetPixels32(this.m_RawAtlas.rgba);
					this.m_Atlas.Apply();
					this.MustCommit = false;
				}
			}
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x0008B6BC File Offset: 0x000898BC
		private void PrepareAtlas()
		{
			bool flag = this.m_Atlas != null;
			if (!flag)
			{
				this.m_Atlas = new Texture2D(this.m_ElemWidth, this.m_Length, TextureFormat.ARGB32, 0, true)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "GradientSettings " + GradientSettingsAtlas.s_TextureCounter++.ToString(),
					filterMode = FilterMode.Point
				};
			}
		}

		// Token: 0x04001174 RID: 4468
		private static ProfilerMarker s_MarkerWrite = new ProfilerMarker("UIR.GradientSettingsAtlas.Write");

		// Token: 0x04001175 RID: 4469
		private static ProfilerMarker s_MarkerCommit = new ProfilerMarker("UIR.GradientSettingsAtlas.Commit");

		// Token: 0x04001176 RID: 4470
		private readonly int m_Length;

		// Token: 0x04001177 RID: 4471
		private readonly int m_ElemWidth;

		// Token: 0x04001178 RID: 4472
		private BestFitAllocator m_Allocator;

		// Token: 0x04001179 RID: 4473
		private Texture2D m_Atlas;

		// Token: 0x0400117A RID: 4474
		private GradientSettingsAtlas.RawTexture m_RawAtlas;

		// Token: 0x0400117B RID: 4475
		private static int s_TextureCounter;

		// Token: 0x02000527 RID: 1319
		private struct RawTexture
		{
			// Token: 0x0600249C RID: 9372 RVA: 0x0008B750 File Offset: 0x00089950
			public void WriteRawInt2Packed(int v0, int v1, int destX, int destY)
			{
				byte r = (byte)(v0 / 255);
				byte g = (byte)(v0 - (int)(r * byte.MaxValue));
				byte b = (byte)(v1 / 255);
				byte a = (byte)(v1 - (int)(b * byte.MaxValue));
				int offset = destY * this.width + destX;
				this.rgba[offset] = new Color32(r, g, b, a);
			}

			// Token: 0x0600249D RID: 9373 RVA: 0x0008B7AC File Offset: 0x000899AC
			public void WriteRawFloat4Packed(float f0, float f1, float f2, float f3, int destX, int destY)
			{
				byte r = (byte)(f0 * 255f + 0.5f);
				byte g = (byte)(f1 * 255f + 0.5f);
				byte b = (byte)(f2 * 255f + 0.5f);
				byte a = (byte)(f3 * 255f + 0.5f);
				int offset = destY * this.width + destX;
				this.rgba[offset] = new Color32(r, g, b, a);
			}

			// Token: 0x0400117E RID: 4478
			public Color32[] rgba;

			// Token: 0x0400117F RID: 4479
			public int width;

			// Token: 0x04001180 RID: 4480
			public int height;
		}
	}
}
