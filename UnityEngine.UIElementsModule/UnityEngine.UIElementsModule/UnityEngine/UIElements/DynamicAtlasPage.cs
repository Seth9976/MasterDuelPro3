using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029D RID: 669
	internal class DynamicAtlasPage : IDisposable
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0004B5C0 File Offset: 0x000497C0
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x0004B5C8 File Offset: 0x000497C8
		public TextureId textureId { get; private set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0004B5D1 File Offset: 0x000497D1
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x0004B5D9 File Offset: 0x000497D9
		public RenderTexture atlas { get; private set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0004B5E2 File Offset: 0x000497E2
		public RenderTextureFormat format { get; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0004B5EA File Offset: 0x000497EA
		public FilterMode filterMode { get; }

		// Token: 0x0600121B RID: 4635 RVA: 0x0004B5F4 File Offset: 0x000497F4
		public DynamicAtlasPage(RenderTextureFormat format, FilterMode filterMode, Vector2Int minSize, Vector2Int maxSize)
		{
			this.textureId = TextureRegistry.instance.AllocAndAcquireDynamic();
			this.format = format;
			this.filterMode = filterMode;
			this.<minSize>k__BackingField = minSize;
			this.<maxSize>k__BackingField = maxSize;
			this.m_Allocator = new Allocator2D(minSize, maxSize, this.m_2Padding);
			this.m_Blitter = new TextureBlitter(64);
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004B666 File Offset: 0x00049866
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x0004B66E File Offset: 0x0004986E
		private protected bool disposed { protected get; private set; }

		// Token: 0x0600121E RID: 4638 RVA: 0x0004B677 File Offset: 0x00049877
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0004B68C File Offset: 0x0004988C
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.atlas != null;
					if (flag)
					{
						UIRUtility.Destroy(this.atlas);
						this.atlas = null;
					}
					bool flag2 = this.m_Allocator != null;
					if (flag2)
					{
						this.m_Allocator = null;
					}
					bool flag3 = this.m_Blitter != null;
					if (flag3)
					{
						this.m_Blitter.Dispose();
						this.m_Blitter = null;
					}
					bool flag4 = this.textureId != TextureId.invalid;
					if (flag4)
					{
						TextureRegistry.instance.Release(this.textureId);
						this.textureId = TextureId.invalid;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0004B754 File Offset: 0x00049954
		public bool TryAdd(Texture2D image, out Allocator2D.Alloc2D alloc, out RectInt rect)
		{
			bool disposed = this.disposed;
			bool flag;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				alloc = default(Allocator2D.Alloc2D);
				rect = default(RectInt);
				flag = false;
			}
			else
			{
				bool flag2 = !this.m_Allocator.TryAllocate(image.width + this.m_2Padding, image.height + this.m_2Padding, out alloc);
				if (flag2)
				{
					rect = default(RectInt);
					flag = false;
				}
				else
				{
					this.m_CurrentSize.x = Mathf.Max(this.m_CurrentSize.x, UIRUtility.GetNextPow2(alloc.rect.xMax));
					this.m_CurrentSize.y = Mathf.Max(this.m_CurrentSize.y, UIRUtility.GetNextPow2(alloc.rect.yMax));
					rect = new RectInt(alloc.rect.xMin + this.m_1Padding, alloc.rect.yMin + this.m_1Padding, image.width, image.height);
					this.Update(image, rect);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0004B86C File Offset: 0x00049A6C
		public void Update(Texture2D image, RectInt rect)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				Debug.Assert(image != null && rect.width > 0 && rect.height > 0);
				this.m_Blitter.QueueBlit(image, new RectInt(0, 0, image.width, image.height), new Vector2Int(rect.x, rect.y), true, Color.white);
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0004B8F0 File Offset: 0x00049AF0
		public void Remove(Allocator2D.Alloc2D alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				Debug.Assert(alloc.rect.width > 0 && alloc.rect.height > 0);
				this.m_Allocator.Free(alloc);
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0004B948 File Offset: 0x00049B48
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.UpdateAtlasTexture();
				this.m_Blitter.Commit(this.atlas);
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0004B984 File Offset: 0x00049B84
		private void UpdateAtlasTexture()
		{
			bool flag = this.atlas == null;
			if (flag)
			{
				this.atlas = this.CreateAtlasTexture();
			}
			else
			{
				bool flag2 = this.atlas.width != this.m_CurrentSize.x || this.atlas.height != this.m_CurrentSize.y;
				if (flag2)
				{
					RenderTexture newAtlas = this.CreateAtlasTexture();
					bool flag3 = newAtlas == null;
					if (flag3)
					{
						Debug.LogErrorFormat("Failed to allocate a render texture for the dynamic atlas. Current Size = {0}x{1}. Requested Size = {2}x{3}.", new object[]
						{
							this.atlas.width,
							this.atlas.height,
							this.m_CurrentSize.x,
							this.m_CurrentSize.y
						});
					}
					else
					{
						this.m_Blitter.BlitOneNow(newAtlas, this.atlas, new RectInt(0, 0, this.atlas.width, this.atlas.height), new Vector2Int(0, 0), false, Color.white);
					}
					UIRUtility.Destroy(this.atlas);
					this.atlas = newAtlas;
				}
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004BAB8 File Offset: 0x00049CB8
		private RenderTexture CreateAtlasTexture()
		{
			bool flag = this.m_CurrentSize.x == 0 || this.m_CurrentSize.y == 0;
			RenderTexture renderTexture;
			if (flag)
			{
				renderTexture = null;
			}
			else
			{
				renderTexture = new RenderTexture(this.m_CurrentSize.x, this.m_CurrentSize.y, 0, this.format)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "UIR Dynamic Atlas Page " + DynamicAtlasPage.s_TextureCounter++.ToString(),
					filterMode = this.filterMode
				};
			}
			return renderTexture;
		}

		// Token: 0x04000A6E RID: 2670
		private readonly int m_1Padding = 1;

		// Token: 0x04000A6F RID: 2671
		private readonly int m_2Padding = 2;

		// Token: 0x04000A70 RID: 2672
		private Allocator2D m_Allocator;

		// Token: 0x04000A71 RID: 2673
		private TextureBlitter m_Blitter;

		// Token: 0x04000A72 RID: 2674
		private Vector2Int m_CurrentSize;

		// Token: 0x04000A73 RID: 2675
		private static int s_TextureCounter;
	}
}
