using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A5 RID: 421
	[Obsolete("BatchRendererGroupGlobals and associated cbuffer are now set automatically by Unity. Setting it manually is no longer necessary or supported.")]
	[Serializable]
	public struct BatchRendererGroupGlobals : IEquatable<BatchRendererGroupGlobals>
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002B2D4 File Offset: 0x000294D4
		public static BatchRendererGroupGlobals Default
		{
			get
			{
				BatchRendererGroupGlobals globals = default(BatchRendererGroupGlobals);
				globals.ProbesOcclusion = Vector4.one;
				globals.SpecCube0_HDR = ReflectionProbe.defaultTextureHDRDecodeValues;
				globals.SpecCube1_HDR = globals.SpecCube0_HDR;
				globals.SHCoefficients = new SHCoefficients(RenderSettings.ambientProbe);
				return globals;
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002B320 File Offset: 0x00029520
		public bool Equals(BatchRendererGroupGlobals other)
		{
			return this.ProbesOcclusion.Equals(other.ProbesOcclusion) && this.SpecCube0_HDR.Equals(other.SpecCube0_HDR) && this.SpecCube1_HDR.Equals(other.SpecCube1_HDR) && this.SHCoefficients.Equals(other.SHCoefficients);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002B37C File Offset: 0x0002957C
		public override bool Equals(object obj)
		{
			if (obj is BatchRendererGroupGlobals)
			{
				BatchRendererGroupGlobals other = (BatchRendererGroupGlobals)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002B3A1 File Offset: 0x000295A1
		public override int GetHashCode()
		{
			return HashCode.Combine<Vector4, Vector4, Vector4, SHCoefficients>(this.ProbesOcclusion, this.SpecCube0_HDR, this.SpecCube1_HDR, this.SHCoefficients);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002B3C0 File Offset: 0x000295C0
		public static bool operator ==(BatchRendererGroupGlobals left, BatchRendererGroupGlobals right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002B3CA File Offset: 0x000295CA
		public static bool operator !=(BatchRendererGroupGlobals left, BatchRendererGroupGlobals right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000825 RID: 2085
		public const string kGlobalsPropertyName = "unity_DOTSInstanceGlobalValues";

		// Token: 0x04000826 RID: 2086
		public static readonly int kGlobalsPropertyId = Shader.PropertyToID("unity_DOTSInstanceGlobalValues");

		// Token: 0x04000827 RID: 2087
		public Vector4 ProbesOcclusion;

		// Token: 0x04000828 RID: 2088
		public Vector4 SpecCube0_HDR;

		// Token: 0x04000829 RID: 2089
		public Vector4 SpecCube1_HDR;

		// Token: 0x0400082A RID: 2090
		public SHCoefficients SHCoefficients;
	}
}
