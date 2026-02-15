using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000160 RID: 352
	[Obsolete("Deprecated in favor of RTHandle", true)]
	public struct RenderTargetHandle
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00024EC6 File Offset: 0x000230C6
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x00024EBD File Offset: 0x000230BD
		public int id { readonly get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00024ED7 File Offset: 0x000230D7
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x00024ECE File Offset: 0x000230CE
		private RenderTargetIdentifier rtid { readonly get; set; }

		// Token: 0x060007AC RID: 1964 RVA: 0x00024EDF File Offset: 0x000230DF
		public RenderTargetHandle(RenderTargetIdentifier renderTargetIdentifier)
		{
			this.id = -2;
			this.rtid = renderTargetIdentifier;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00024EF0 File Offset: 0x000230F0
		public RenderTargetHandle(RTHandle rtHandle)
		{
			if (rtHandle.nameID == BuiltinRenderTextureType.CameraTarget)
			{
				this.id = -1;
			}
			else if (rtHandle.name.Length == 0)
			{
				this.id = -2;
			}
			else
			{
				this.id = Shader.PropertyToID(rtHandle.name);
			}
			this.rtid = rtHandle.nameID;
			if (rtHandle.rt != null && this.id != this.rtid)
			{
				this.id = -2;
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00024F7B File Offset: 0x0002317B
		internal static RenderTargetHandle GetCameraTarget(ref CameraData cameraData)
		{
			if (cameraData.xr.enabled)
			{
				return new RenderTargetHandle(cameraData.xr.renderTarget);
			}
			return RenderTargetHandle.CameraTarget;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00024FA0 File Offset: 0x000231A0
		public void Init(string shaderProperty)
		{
			this.id = Shader.PropertyToID(shaderProperty);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00024EDF File Offset: 0x000230DF
		public void Init(RenderTargetIdentifier renderTargetIdentifier)
		{
			this.id = -2;
			this.rtid = renderTargetIdentifier;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00024FAE File Offset: 0x000231AE
		public RenderTargetIdentifier Identifier()
		{
			if (this.id == -1)
			{
				return BuiltinRenderTextureType.CameraTarget;
			}
			if (this.id == -2)
			{
				return this.rtid;
			}
			return new RenderTargetIdentifier(this.id, 0, CubemapFace.Unknown, -1);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00024FDF File Offset: 0x000231DF
		public bool HasInternalRenderTargetId()
		{
			return this.id == -2;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00024FEB File Offset: 0x000231EB
		public bool Equals(RenderTargetHandle other)
		{
			if (this.id == -2 || other.id == -2)
			{
				return this.Identifier() == other.Identifier();
			}
			return this.id == other.id;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00025024 File Offset: 0x00023224
		public override bool Equals(object obj)
		{
			return obj != null && obj is RenderTargetHandle && this.Equals((RenderTargetHandle)obj);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00025041 File Offset: 0x00023241
		public override int GetHashCode()
		{
			return this.id;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00025049 File Offset: 0x00023249
		public static bool operator ==(RenderTargetHandle c1, RenderTargetHandle c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00025053 File Offset: 0x00023253
		public static bool operator !=(RenderTargetHandle c1, RenderTargetHandle c2)
		{
			return !c1.Equals(c2);
		}

		// Token: 0x0400081A RID: 2074
		public static readonly RenderTargetHandle CameraTarget = new RenderTargetHandle
		{
			id = -1
		};
	}
}
