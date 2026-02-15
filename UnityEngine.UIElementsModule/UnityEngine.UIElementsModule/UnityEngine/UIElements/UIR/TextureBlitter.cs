using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200055D RID: 1373
	internal class TextureBlitter : IDisposable
	{
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x00096465 File Offset: 0x00094665
		// (set) Token: 0x060025CF RID: 9679 RVA: 0x0009646D File Offset: 0x0009466D
		private protected bool disposed { protected get; private set; }

		// Token: 0x060025D0 RID: 9680 RVA: 0x00096476 File Offset: 0x00094676
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x00096488 File Offset: 0x00094688
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_BlitMaterial);
					this.m_BlitMaterial = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000964C8 File Offset: 0x000946C8
		static TextureBlitter()
		{
			for (int i = 0; i < 8; i++)
			{
				TextureBlitter.k_TextureIds[i] = Shader.PropertyToID("_MainTex" + i.ToString());
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0009651D File Offset: 0x0009471D
		public TextureBlitter(int capacity = 512)
		{
			this.m_PendingBlits = new List<TextureBlitter.BlitInfo>(capacity);
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00096540 File Offset: 0x00094740
		public void QueueBlit(Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_PendingBlits.Add(new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				});
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000965A8 File Offset: 0x000947A8
		public void BlitOneNow(RenderTexture dst, Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_SingleBlit[0] = new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				};
				this.BeginBlit(dst);
				this.DoBlit(this.m_SingleBlit, 0);
				this.EndBlit();
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x00096630 File Offset: 0x00094830
		public void Commit(RenderTexture dst)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_PendingBlits.Count == 0;
				if (!flag)
				{
					this.BeginBlit(dst);
					for (int i = 0; i < this.m_PendingBlits.Count; i += 8)
					{
						this.DoBlit(this.m_PendingBlits, i);
					}
					this.EndBlit();
					this.m_PendingBlits.Clear();
				}
			}
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000966AC File Offset: 0x000948AC
		private void BeginBlit(RenderTexture dst)
		{
			bool flag = this.m_BlitMaterial == null;
			if (flag)
			{
				Shader blitShader = Shader.Find(Shaders.k_AtlasBlit);
				this.m_BlitMaterial = new Material(blitShader);
				this.m_BlitMaterial.hideFlags |= HideFlags.DontSaveInEditor;
			}
			bool flag2 = this.m_Properties == null;
			if (flag2)
			{
				this.m_Properties = new MaterialPropertyBlock();
			}
			this.m_Viewport = Utility.GetActiveViewport();
			this.m_PrevRT = RenderTexture.active;
			GL.LoadPixelMatrix(0f, (float)dst.width, 0f, (float)dst.height);
			Graphics.SetRenderTarget(dst);
			this.m_BlitMaterial.SetPass(0);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00096758 File Offset: 0x00094958
		private void DoBlit(IList<TextureBlitter.BlitInfo> blitInfos, int startIndex)
		{
			int usedSlots = Mathf.Min(blitInfos.Count - startIndex, 8);
			int stopIndex = startIndex + usedSlots;
			int blitIndex = startIndex;
			int slotIndex = 0;
			while (blitIndex < stopIndex)
			{
				Texture texture = blitInfos[blitIndex].src;
				bool flag = texture != null;
				if (flag)
				{
					this.m_Properties.SetTexture(TextureBlitter.k_TextureIds[slotIndex], texture);
				}
				blitIndex++;
				slotIndex++;
			}
			Utility.SetPropertyBlock(this.m_Properties);
			GL.Begin(7);
			int blitIndex2 = startIndex;
			int slotIndex2 = 0;
			while (blitIndex2 < stopIndex)
			{
				TextureBlitter.BlitInfo current = blitInfos[blitIndex2];
				float srcTexelWidth = 1f / (float)current.src.width;
				float srcTexelHeight = 1f / (float)current.src.height;
				float dstLeft = (float)(current.dstPos.x - current.border);
				float dstBottom = (float)(current.dstPos.y - current.border);
				float dstRight = (float)(current.dstPos.x + current.srcRect.width + current.border);
				float dstTop = (float)(current.dstPos.y + current.srcRect.height + current.border);
				float srcLeft = (float)(current.srcRect.x - current.border) * srcTexelWidth;
				float srcBottom = (float)(current.srcRect.y - current.border) * srcTexelHeight;
				float srcRight = (float)(current.srcRect.xMax + current.border) * srcTexelWidth;
				float srcTop = (float)(current.srcRect.yMax + current.border) * srcTexelHeight;
				GL.Color(current.tint);
				GL.TexCoord3(srcLeft, srcBottom, (float)slotIndex2);
				GL.Vertex3(dstLeft, dstBottom, 0f);
				GL.Color(current.tint);
				GL.TexCoord3(srcLeft, srcTop, (float)slotIndex2);
				GL.Vertex3(dstLeft, dstTop, 0f);
				GL.Color(current.tint);
				GL.TexCoord3(srcRight, srcTop, (float)slotIndex2);
				GL.Vertex3(dstRight, dstTop, 0f);
				GL.Color(current.tint);
				GL.TexCoord3(srcRight, srcBottom, (float)slotIndex2);
				GL.Vertex3(dstRight, dstBottom, 0f);
				blitIndex2++;
				slotIndex2++;
			}
			GL.End();
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000969BC File Offset: 0x00094BBC
		private void EndBlit()
		{
			Graphics.SetRenderTarget(this.m_PrevRT);
			GL.Viewport(new Rect((float)this.m_Viewport.x, (float)this.m_Viewport.y, (float)this.m_Viewport.width, (float)this.m_Viewport.height));
		}

		// Token: 0x040012FD RID: 4861
		private static readonly int[] k_TextureIds = new int[8];

		// Token: 0x040012FE RID: 4862
		private static ProfilerMarker s_CommitSampler = new ProfilerMarker("UIR.TextureBlitter.Commit");

		// Token: 0x040012FF RID: 4863
		private TextureBlitter.BlitInfo[] m_SingleBlit = new TextureBlitter.BlitInfo[1];

		// Token: 0x04001300 RID: 4864
		private Material m_BlitMaterial;

		// Token: 0x04001301 RID: 4865
		private MaterialPropertyBlock m_Properties;

		// Token: 0x04001302 RID: 4866
		private RectInt m_Viewport;

		// Token: 0x04001303 RID: 4867
		private RenderTexture m_PrevRT;

		// Token: 0x04001304 RID: 4868
		private List<TextureBlitter.BlitInfo> m_PendingBlits;

		// Token: 0x0200055E RID: 1374
		private struct BlitInfo
		{
			// Token: 0x04001306 RID: 4870
			public Texture src;

			// Token: 0x04001307 RID: 4871
			public RectInt srcRect;

			// Token: 0x04001308 RID: 4872
			public Vector2Int dstPos;

			// Token: 0x04001309 RID: 4873
			public int border;

			// Token: 0x0400130A RID: 4874
			public Color tint;
		}
	}
}
