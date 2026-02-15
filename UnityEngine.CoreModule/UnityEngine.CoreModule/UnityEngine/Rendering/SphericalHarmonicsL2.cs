using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200036F RID: 879
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Math/SphericalHarmonicsL2.bindings.h")]
	public struct SphericalHarmonicsL2 : IEquatable<SphericalHarmonicsL2>
	{
		// Token: 0x17000380 RID: 896
		public float this[int rgb, int coefficient]
		{
			get
			{
				float num;
				switch (rgb * 9 + coefficient)
				{
				case 0:
					num = this.shr0;
					break;
				case 1:
					num = this.shr1;
					break;
				case 2:
					num = this.shr2;
					break;
				case 3:
					num = this.shr3;
					break;
				case 4:
					num = this.shr4;
					break;
				case 5:
					num = this.shr5;
					break;
				case 6:
					num = this.shr6;
					break;
				case 7:
					num = this.shr7;
					break;
				case 8:
					num = this.shr8;
					break;
				case 9:
					num = this.shg0;
					break;
				case 10:
					num = this.shg1;
					break;
				case 11:
					num = this.shg2;
					break;
				case 12:
					num = this.shg3;
					break;
				case 13:
					num = this.shg4;
					break;
				case 14:
					num = this.shg5;
					break;
				case 15:
					num = this.shg6;
					break;
				case 16:
					num = this.shg7;
					break;
				case 17:
					num = this.shg8;
					break;
				case 18:
					num = this.shb0;
					break;
				case 19:
					num = this.shb1;
					break;
				case 20:
					num = this.shb2;
					break;
				case 21:
					num = this.shb3;
					break;
				case 22:
					num = this.shb4;
					break;
				case 23:
					num = this.shb5;
					break;
				case 24:
					num = this.shb6;
					break;
				case 25:
					num = this.shb7;
					break;
				case 26:
					num = this.shb8;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
				return num;
			}
			set
			{
				switch (rgb * 9 + coefficient)
				{
				case 0:
					this.shr0 = value;
					break;
				case 1:
					this.shr1 = value;
					break;
				case 2:
					this.shr2 = value;
					break;
				case 3:
					this.shr3 = value;
					break;
				case 4:
					this.shr4 = value;
					break;
				case 5:
					this.shr5 = value;
					break;
				case 6:
					this.shr6 = value;
					break;
				case 7:
					this.shr7 = value;
					break;
				case 8:
					this.shr8 = value;
					break;
				case 9:
					this.shg0 = value;
					break;
				case 10:
					this.shg1 = value;
					break;
				case 11:
					this.shg2 = value;
					break;
				case 12:
					this.shg3 = value;
					break;
				case 13:
					this.shg4 = value;
					break;
				case 14:
					this.shg5 = value;
					break;
				case 15:
					this.shg6 = value;
					break;
				case 16:
					this.shg7 = value;
					break;
				case 17:
					this.shg8 = value;
					break;
				case 18:
					this.shb0 = value;
					break;
				case 19:
					this.shb1 = value;
					break;
				case 20:
					this.shb2 = value;
					break;
				case 21:
					this.shb3 = value;
					break;
				case 22:
					this.shb4 = value;
					break;
				case 23:
					this.shb5 = value;
					break;
				case 24:
					this.shb6 = value;
					break;
				case 25:
					this.shb7 = value;
					break;
				case 26:
					this.shb8 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00034B78 File Offset: 0x00032D78
		public override int GetHashCode()
		{
			int hash = 17;
			hash = hash * 23 + this.shr0.GetHashCode();
			hash = hash * 23 + this.shr1.GetHashCode();
			hash = hash * 23 + this.shr2.GetHashCode();
			hash = hash * 23 + this.shr3.GetHashCode();
			hash = hash * 23 + this.shr4.GetHashCode();
			hash = hash * 23 + this.shr5.GetHashCode();
			hash = hash * 23 + this.shr6.GetHashCode();
			hash = hash * 23 + this.shr7.GetHashCode();
			hash = hash * 23 + this.shr8.GetHashCode();
			hash = hash * 23 + this.shg0.GetHashCode();
			hash = hash * 23 + this.shg1.GetHashCode();
			hash = hash * 23 + this.shg2.GetHashCode();
			hash = hash * 23 + this.shg3.GetHashCode();
			hash = hash * 23 + this.shg4.GetHashCode();
			hash = hash * 23 + this.shg5.GetHashCode();
			hash = hash * 23 + this.shg6.GetHashCode();
			hash = hash * 23 + this.shg7.GetHashCode();
			hash = hash * 23 + this.shg8.GetHashCode();
			hash = hash * 23 + this.shb0.GetHashCode();
			hash = hash * 23 + this.shb1.GetHashCode();
			hash = hash * 23 + this.shb2.GetHashCode();
			hash = hash * 23 + this.shb3.GetHashCode();
			hash = hash * 23 + this.shb4.GetHashCode();
			hash = hash * 23 + this.shb5.GetHashCode();
			hash = hash * 23 + this.shb6.GetHashCode();
			hash = hash * 23 + this.shb7.GetHashCode();
			return hash * 23 + this.shb8.GetHashCode();
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00034D5C File Offset: 0x00032F5C
		public override bool Equals(object other)
		{
			return other is SphericalHarmonicsL2 && this.Equals((SphericalHarmonicsL2)other);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00034D88 File Offset: 0x00032F88
		public bool Equals(SphericalHarmonicsL2 other)
		{
			return this == other;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00034DA8 File Offset: 0x00032FA8
		public static bool operator ==(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return lhs.shr0 == rhs.shr0 && lhs.shr1 == rhs.shr1 && lhs.shr2 == rhs.shr2 && lhs.shr3 == rhs.shr3 && lhs.shr4 == rhs.shr4 && lhs.shr5 == rhs.shr5 && lhs.shr6 == rhs.shr6 && lhs.shr7 == rhs.shr7 && lhs.shr8 == rhs.shr8 && lhs.shg0 == rhs.shg0 && lhs.shg1 == rhs.shg1 && lhs.shg2 == rhs.shg2 && lhs.shg3 == rhs.shg3 && lhs.shg4 == rhs.shg4 && lhs.shg5 == rhs.shg5 && lhs.shg6 == rhs.shg6 && lhs.shg7 == rhs.shg7 && lhs.shg8 == rhs.shg8 && lhs.shb0 == rhs.shb0 && lhs.shb1 == rhs.shb1 && lhs.shb2 == rhs.shb2 && lhs.shb3 == rhs.shb3 && lhs.shb4 == rhs.shb4 && lhs.shb5 == rhs.shb5 && lhs.shb6 == rhs.shb6 && lhs.shb7 == rhs.shb7 && lhs.shb8 == rhs.shb8;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00034F70 File Offset: 0x00033170
		public static bool operator !=(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04000A3C RID: 2620
		private float shr0;

		// Token: 0x04000A3D RID: 2621
		private float shr1;

		// Token: 0x04000A3E RID: 2622
		private float shr2;

		// Token: 0x04000A3F RID: 2623
		private float shr3;

		// Token: 0x04000A40 RID: 2624
		private float shr4;

		// Token: 0x04000A41 RID: 2625
		private float shr5;

		// Token: 0x04000A42 RID: 2626
		private float shr6;

		// Token: 0x04000A43 RID: 2627
		private float shr7;

		// Token: 0x04000A44 RID: 2628
		private float shr8;

		// Token: 0x04000A45 RID: 2629
		private float shg0;

		// Token: 0x04000A46 RID: 2630
		private float shg1;

		// Token: 0x04000A47 RID: 2631
		private float shg2;

		// Token: 0x04000A48 RID: 2632
		private float shg3;

		// Token: 0x04000A49 RID: 2633
		private float shg4;

		// Token: 0x04000A4A RID: 2634
		private float shg5;

		// Token: 0x04000A4B RID: 2635
		private float shg6;

		// Token: 0x04000A4C RID: 2636
		private float shg7;

		// Token: 0x04000A4D RID: 2637
		private float shg8;

		// Token: 0x04000A4E RID: 2638
		private float shb0;

		// Token: 0x04000A4F RID: 2639
		private float shb1;

		// Token: 0x04000A50 RID: 2640
		private float shb2;

		// Token: 0x04000A51 RID: 2641
		private float shb3;

		// Token: 0x04000A52 RID: 2642
		private float shb4;

		// Token: 0x04000A53 RID: 2643
		private float shb5;

		// Token: 0x04000A54 RID: 2644
		private float shb6;

		// Token: 0x04000A55 RID: 2645
		private float shb7;

		// Token: 0x04000A56 RID: 2646
		private float shb8;
	}
}
