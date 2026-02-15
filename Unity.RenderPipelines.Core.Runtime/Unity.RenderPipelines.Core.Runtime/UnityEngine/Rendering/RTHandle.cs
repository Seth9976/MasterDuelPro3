using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000196 RID: 406
	public class RTHandle
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x000281C4 File Offset: 0x000263C4
		public void SetCustomHandleProperties(in RTHandleProperties properties)
		{
			this.m_UseCustomHandleScales = true;
			this.m_CustomHandleProperties = properties;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000281D9 File Offset: 0x000263D9
		public void ClearCustomHandleProperties()
		{
			this.m_UseCustomHandleScales = false;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000281E2 File Offset: 0x000263E2
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x000281EA File Offset: 0x000263EA
		public Vector2 scaleFactor { get; internal set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x000281F3 File Offset: 0x000263F3
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x000281FB File Offset: 0x000263FB
		public bool useScaling { get; internal set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00028204 File Offset: 0x00026404
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0002820C File Offset: 0x0002640C
		public Vector2Int referenceSize { get; internal set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00028215 File Offset: 0x00026415
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				if (!this.m_UseCustomHandleScales)
				{
					return this.m_Owner.rtHandleProperties;
				}
				return this.m_CustomHandleProperties;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00028231 File Offset: 0x00026431
		public RenderTexture rt
		{
			get
			{
				return this.m_RT;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00028239 File Offset: 0x00026439
		public Texture externalTexture
		{
			get
			{
				return this.m_ExternalTexture;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00028241 File Offset: 0x00026441
		public RenderTargetIdentifier nameID
		{
			get
			{
				return this.m_NameID;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00028249 File Offset: 0x00026449
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00028251 File Offset: 0x00026451
		public bool isMSAAEnabled
		{
			get
			{
				return this.m_EnableMSAA;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00028259 File Offset: 0x00026459
		internal RTHandle(RTHandleSystem owner)
		{
			this.m_Owner = owner;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00028270 File Offset: 0x00026470
		public static implicit operator RenderTargetIdentifier(RTHandle handle)
		{
			if (handle == null)
			{
				return default(RenderTargetIdentifier);
			}
			return handle.nameID;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00028290 File Offset: 0x00026490
		public static implicit operator Texture(RTHandle handle)
		{
			if (handle == null)
			{
				return null;
			}
			if (!(handle.rt != null))
			{
				return handle.m_ExternalTexture;
			}
			return handle.rt;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000282B2 File Offset: 0x000264B2
		public static implicit operator RenderTexture(RTHandle handle)
		{
			if (handle == null)
			{
				return null;
			}
			return handle.rt;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000282BF File Offset: 0x000264BF
		internal void SetRenderTexture(RenderTexture rt, bool transferOwnership = true)
		{
			this.m_RT = rt;
			this.m_ExternalTexture = null;
			this.m_RTHasOwnership = transferOwnership;
			this.m_NameID = new RenderTargetIdentifier(rt);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000282E2 File Offset: 0x000264E2
		internal void SetTexture(Texture tex)
		{
			this.m_RT = null;
			this.m_ExternalTexture = tex;
			this.m_NameID = new RenderTargetIdentifier(tex);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000282FE File Offset: 0x000264FE
		internal void SetTexture(RenderTargetIdentifier tex)
		{
			this.m_RT = null;
			this.m_ExternalTexture = null;
			this.m_NameID = tex;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00028318 File Offset: 0x00026518
		public int GetInstanceID()
		{
			if (this.m_RT != null)
			{
				return this.m_RT.GetInstanceID();
			}
			if (this.m_ExternalTexture != null)
			{
				return this.m_ExternalTexture.GetInstanceID();
			}
			return this.m_NameID.GetHashCode();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002836C File Offset: 0x0002656C
		public void Release()
		{
			this.m_Owner.Remove(this);
			if (this.m_RTHasOwnership)
			{
				CoreUtils.Destroy(this.m_RT);
			}
			this.m_NameID = BuiltinRenderTextureType.None;
			this.m_RT = null;
			this.m_ExternalTexture = null;
			this.m_RTHasOwnership = true;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000283BC File Offset: 0x000265BC
		public Vector2Int GetScaledSize(Vector2Int refSize)
		{
			if (!this.useScaling)
			{
				return refSize;
			}
			if (this.scaleFunc != null)
			{
				return this.scaleFunc(refSize);
			}
			return new Vector2Int(Mathf.RoundToInt(this.scaleFactor.x * (float)refSize.x), Mathf.RoundToInt(this.scaleFactor.y * (float)refSize.y));
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00028420 File Offset: 0x00026620
		public Vector2Int GetScaledSize()
		{
			if (!this.useScaling)
			{
				return this.referenceSize;
			}
			if (this.scaleFunc != null)
			{
				return this.scaleFunc(this.referenceSize);
			}
			return new Vector2Int(Mathf.RoundToInt(this.scaleFactor.x * (float)this.referenceSize.x), Mathf.RoundToInt(this.scaleFactor.y * (float)this.referenceSize.y));
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002849B File Offset: 0x0002669B
		public void SwitchToFastMemory(CommandBuffer cmd, float residencyFraction = 1f, FastMemoryFlags flags = FastMemoryFlags.SpillTop, bool copyContents = false)
		{
			residencyFraction = Mathf.Clamp01(residencyFraction);
			cmd.SwitchIntoFastMemory(this.m_RT, flags, residencyFraction, copyContents);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000284BA File Offset: 0x000266BA
		public void CopyToFastMemory(CommandBuffer cmd, float residencyFraction = 1f, FastMemoryFlags flags = FastMemoryFlags.SpillTop)
		{
			this.SwitchToFastMemory(cmd, residencyFraction, flags, true);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000284C6 File Offset: 0x000266C6
		public void SwitchOutFastMemory(CommandBuffer cmd, bool copyContents = true)
		{
			cmd.SwitchOutOfFastMemory(this.m_RT, copyContents);
		}

		// Token: 0x040007B7 RID: 1975
		internal RTHandleSystem m_Owner;

		// Token: 0x040007B8 RID: 1976
		internal RenderTexture m_RT;

		// Token: 0x040007B9 RID: 1977
		internal Texture m_ExternalTexture;

		// Token: 0x040007BA RID: 1978
		internal RenderTargetIdentifier m_NameID;

		// Token: 0x040007BB RID: 1979
		internal bool m_EnableMSAA;

		// Token: 0x040007BC RID: 1980
		internal bool m_EnableRandomWrite;

		// Token: 0x040007BD RID: 1981
		internal bool m_EnableHWDynamicScale;

		// Token: 0x040007BE RID: 1982
		internal bool m_RTHasOwnership = true;

		// Token: 0x040007BF RID: 1983
		internal string m_Name;

		// Token: 0x040007C0 RID: 1984
		internal bool m_UseCustomHandleScales;

		// Token: 0x040007C1 RID: 1985
		internal RTHandleProperties m_CustomHandleProperties;

		// Token: 0x040007C3 RID: 1987
		internal ScaleFunc scaleFunc;
	}
}
