using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public struct SHCoefficients : IEquatable<SHCoefficients>
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002B0A8 File Offset: 0x000292A8
		public SHCoefficients(SphericalHarmonicsL2 sh)
		{
			this.SHAr = SHCoefficients.GetSHA(sh, 0);
			this.SHAg = SHCoefficients.GetSHA(sh, 1);
			this.SHAb = SHCoefficients.GetSHA(sh, 2);
			this.SHBr = SHCoefficients.GetSHB(sh, 0);
			this.SHBg = SHCoefficients.GetSHB(sh, 1);
			this.SHBb = SHCoefficients.GetSHB(sh, 2);
			this.SHC = SHCoefficients.GetSHC(sh);
			this.ProbesOcclusion = Vector4.one;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002B11A File Offset: 0x0002931A
		public SHCoefficients(SphericalHarmonicsL2 sh, Vector4 probesOcclusion)
		{
			this = new SHCoefficients(sh);
			this.ProbesOcclusion = probesOcclusion;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002B12A File Offset: 0x0002932A
		private static Vector4 GetSHA(SphericalHarmonicsL2 sh, int i)
		{
			return new Vector4(sh[i, 3], sh[i, 1], sh[i, 2], sh[i, 0] - sh[i, 6]);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002B15F File Offset: 0x0002935F
		private static Vector4 GetSHB(SphericalHarmonicsL2 sh, int i)
		{
			return new Vector4(sh[i, 4], sh[i, 5], sh[i, 6] * 3f, sh[i, 7]);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002B190 File Offset: 0x00029390
		private static Vector4 GetSHC(SphericalHarmonicsL2 sh)
		{
			return new Vector4(sh[0, 8], sh[1, 8], sh[2, 8], 1f);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002B1B8 File Offset: 0x000293B8
		public bool Equals(SHCoefficients other)
		{
			return this.SHAr.Equals(other.SHAr) && this.SHAg.Equals(other.SHAg) && this.SHAb.Equals(other.SHAb) && this.SHBr.Equals(other.SHBr) && this.SHBg.Equals(other.SHBg) && this.SHBb.Equals(other.SHBb) && this.SHC.Equals(other.SHC) && this.ProbesOcclusion.Equals(other.ProbesOcclusion);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002B260 File Offset: 0x00029460
		public override bool Equals(object obj)
		{
			if (obj is SHCoefficients)
			{
				SHCoefficients other = (SHCoefficients)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002B285 File Offset: 0x00029485
		public override int GetHashCode()
		{
			return HashCode.Combine<Vector4, Vector4, Vector4, Vector4, Vector4, Vector4, Vector4, Vector4>(this.SHAr, this.SHAg, this.SHAb, this.SHBr, this.SHBg, this.SHBb, this.SHC, this.ProbesOcclusion);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002B2BC File Offset: 0x000294BC
		public static bool operator ==(SHCoefficients left, SHCoefficients right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002B2C6 File Offset: 0x000294C6
		public static bool operator !=(SHCoefficients left, SHCoefficients right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400081D RID: 2077
		public Vector4 SHAr;

		// Token: 0x0400081E RID: 2078
		public Vector4 SHAg;

		// Token: 0x0400081F RID: 2079
		public Vector4 SHAb;

		// Token: 0x04000820 RID: 2080
		public Vector4 SHBr;

		// Token: 0x04000821 RID: 2081
		public Vector4 SHBg;

		// Token: 0x04000822 RID: 2082
		public Vector4 SHBb;

		// Token: 0x04000823 RID: 2083
		public Vector4 SHC;

		// Token: 0x04000824 RID: 2084
		public Vector4 ProbesOcclusion;
	}
}
