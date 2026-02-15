using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000146 RID: 326
	[Serializable]
	public struct SphericalHarmonicsL1
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x00020F14 File Offset: 0x0001F114
		public static SphericalHarmonicsL1 operator +(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr + rhs.shAr,
				shAg = lhs.shAg + rhs.shAg,
				shAb = lhs.shAb + rhs.shAb
			};
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00020F74 File Offset: 0x0001F174
		public static SphericalHarmonicsL1 operator -(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr - rhs.shAr,
				shAg = lhs.shAg - rhs.shAg,
				shAb = lhs.shAb - rhs.shAb
			};
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00020FD4 File Offset: 0x0001F1D4
		public static SphericalHarmonicsL1 operator *(SphericalHarmonicsL1 lhs, float rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr * rhs,
				shAg = lhs.shAg * rhs,
				shAb = lhs.shAb * rhs
			};
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00021024 File Offset: 0x0001F224
		public static SphericalHarmonicsL1 operator /(SphericalHarmonicsL1 lhs, float rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr / rhs,
				shAg = lhs.shAg / rhs,
				shAb = lhs.shAb / rhs
			};
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00021073 File Offset: 0x0001F273
		public static bool operator ==(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return lhs.shAr == rhs.shAr && lhs.shAg == rhs.shAg && lhs.shAb == rhs.shAb;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000210AE File Offset: 0x0001F2AE
		public static bool operator !=(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000210BA File Offset: 0x0001F2BA
		public override bool Equals(object other)
		{
			return other is SphericalHarmonicsL1 && this == (SphericalHarmonicsL1)other;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000210D8 File Offset: 0x0001F2D8
		public override int GetHashCode()
		{
			return ((391 + this.shAr.GetHashCode()) * 23 + this.shAg.GetHashCode()) * 23 + this.shAb.GetHashCode();
		}

		// Token: 0x04000614 RID: 1556
		public Vector4 shAr;

		// Token: 0x04000615 RID: 1557
		public Vector4 shAg;

		// Token: 0x04000616 RID: 1558
		public Vector4 shAb;

		// Token: 0x04000617 RID: 1559
		public static readonly SphericalHarmonicsL1 zero = new SphericalHarmonicsL1
		{
			shAr = Vector4.zero,
			shAg = Vector4.zero,
			shAb = Vector4.zero
		};
	}
}
