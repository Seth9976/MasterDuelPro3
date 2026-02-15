using System;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000151 RID: 337
	internal struct PostProcessPasses : IDisposable
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00023496 File Offset: 0x00021696
		public ColorGradingLutPass colorGradingLutPass
		{
			get
			{
				return this.m_ColorGradingLutPass;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0002349E File Offset: 0x0002169E
		public PostProcessPass postProcessPass
		{
			get
			{
				return this.m_PostProcessPass;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x000234A6 File Offset: 0x000216A6
		public PostProcessPass finalPostProcessPass
		{
			get
			{
				return this.m_FinalPostProcessPass;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000234AE File Offset: 0x000216AE
		public RTHandle afterPostProcessColor
		{
			get
			{
				return this.m_AfterPostProcessColor;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x000234B6 File Offset: 0x000216B6
		public RTHandle colorGradingLut
		{
			get
			{
				return this.m_ColorGradingLut;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000234BE File Offset: 0x000216BE
		public bool isCreated
		{
			get
			{
				return this.m_CurrentPostProcessData != null;
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000234CC File Offset: 0x000216CC
		public PostProcessPasses(PostProcessData rendererPostProcessData, ref PostProcessParams postProcessParams)
		{
			this.m_ColorGradingLutPass = null;
			this.m_PostProcessPass = null;
			this.m_FinalPostProcessPass = null;
			this.m_CurrentPostProcessData = null;
			this.m_AfterPostProcessColor = null;
			this.m_ColorGradingLut = null;
			this.m_RendererPostProcessData = rendererPostProcessData;
			this.m_BlitMaterial = postProcessParams.blitMaterial;
			this.Recreate(rendererPostProcessData, ref postProcessParams);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00023520 File Offset: 0x00021720
		public void Recreate(PostProcessData data, ref PostProcessParams ppParams)
		{
			if (this.m_RendererPostProcessData)
			{
				data = this.m_RendererPostProcessData;
			}
			if (data == this.m_CurrentPostProcessData)
			{
				return;
			}
			if (this.m_CurrentPostProcessData != null)
			{
				ColorGradingLutPass colorGradingLutPass = this.m_ColorGradingLutPass;
				if (colorGradingLutPass != null)
				{
					colorGradingLutPass.Cleanup();
				}
				PostProcessPass postProcessPass = this.m_PostProcessPass;
				if (postProcessPass != null)
				{
					postProcessPass.Cleanup();
				}
				PostProcessPass finalPostProcessPass = this.m_FinalPostProcessPass;
				if (finalPostProcessPass != null)
				{
					finalPostProcessPass.Cleanup();
				}
				this.m_ColorGradingLutPass = null;
				this.m_PostProcessPass = null;
				this.m_FinalPostProcessPass = null;
				this.m_CurrentPostProcessData = null;
			}
			if (data != null)
			{
				this.m_ColorGradingLutPass = new ColorGradingLutPass(RenderPassEvent.BeforeRenderingPrePasses, data);
				this.m_PostProcessPass = new PostProcessPass((RenderPassEvent)599, data, ref ppParams);
				this.m_FinalPostProcessPass = new PostProcessPass((RenderPassEvent)999, data, ref ppParams);
				this.m_CurrentPostProcessData = data;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000235F4 File Offset: 0x000217F4
		public void Dispose()
		{
			ColorGradingLutPass colorGradingLutPass = this.m_ColorGradingLutPass;
			if (colorGradingLutPass != null)
			{
				colorGradingLutPass.Cleanup();
			}
			PostProcessPass postProcessPass = this.m_PostProcessPass;
			if (postProcessPass != null)
			{
				postProcessPass.Cleanup();
			}
			PostProcessPass finalPostProcessPass = this.m_FinalPostProcessPass;
			if (finalPostProcessPass != null)
			{
				finalPostProcessPass.Cleanup();
			}
			RTHandle afterPostProcessColor = this.m_AfterPostProcessColor;
			if (afterPostProcessColor != null)
			{
				afterPostProcessColor.Release();
			}
			RTHandle colorGradingLut = this.m_ColorGradingLut;
			if (colorGradingLut == null)
			{
				return;
			}
			colorGradingLut.Release();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00023658 File Offset: 0x00021858
		internal void ReleaseRenderTargets()
		{
			RTHandle afterPostProcessColor = this.m_AfterPostProcessColor;
			if (afterPostProcessColor != null)
			{
				afterPostProcessColor.Release();
			}
			PostProcessPass postProcessPass = this.m_PostProcessPass;
			if (postProcessPass != null)
			{
				postProcessPass.Dispose();
			}
			PostProcessPass finalPostProcessPass = this.m_FinalPostProcessPass;
			if (finalPostProcessPass != null)
			{
				finalPostProcessPass.Dispose();
			}
			RTHandle colorGradingLut = this.m_ColorGradingLut;
			if (colorGradingLut == null)
			{
				return;
			}
			colorGradingLut.Release();
		}

		// Token: 0x040007C7 RID: 1991
		private ColorGradingLutPass m_ColorGradingLutPass;

		// Token: 0x040007C8 RID: 1992
		private PostProcessPass m_PostProcessPass;

		// Token: 0x040007C9 RID: 1993
		private PostProcessPass m_FinalPostProcessPass;

		// Token: 0x040007CA RID: 1994
		internal RTHandle m_AfterPostProcessColor;

		// Token: 0x040007CB RID: 1995
		internal RTHandle m_ColorGradingLut;

		// Token: 0x040007CC RID: 1996
		private PostProcessData m_RendererPostProcessData;

		// Token: 0x040007CD RID: 1997
		private PostProcessData m_CurrentPostProcessData;

		// Token: 0x040007CE RID: 1998
		private Material m_BlitMaterial;
	}
}
