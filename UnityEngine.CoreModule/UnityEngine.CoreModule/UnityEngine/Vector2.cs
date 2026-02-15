using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014F RID: 335
	[NativeClass("Vector2f")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[Il2CppEagerStaticClassConstruction]
	public struct Vector2 : IEquatable<Vector2>, IFormattable
	{
		// Token: 0x17000255 RID: 597
		public float this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				float num;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					num = this.y;
				}
				else
				{
					num = this.x;
				}
				return num;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0001DDBA File Offset: 0x0001BFBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0001DE18 File Offset: 0x0001C018
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
		{
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0001DE5C File Offset: 0x0001C05C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0001DE8D File Offset: 0x0001C08D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector2 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float mag = this.magnitude;
			bool flag = mag > 1E-05f;
			if (flag)
			{
				this /= mag;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0001DEF8 File Offset: 0x0001C0F8
		public Vector2 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2 v = new Vector2(this.x, this.y);
				v.Normalize();
				return v;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0001DF28 File Offset: 0x0001C128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0001DF44 File Offset: 0x0001C144
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector2 v;
			bool flag;
			if (other is Vector2)
			{
				v = (Vector2)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(v);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0001E00C File Offset: 0x0001C20C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector2 other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0001E040 File Offset: 0x0001C240
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0001E070 File Offset: 0x0001C270
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0001E0D4 File Offset: 0x0001C2D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(Vector2 from, Vector2 to)
		{
			float denominator = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
			bool flag = denominator < 1E-15f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				float dot = Mathf.Clamp(Vector2.Dot(from, to) / denominator, -1f, 1f);
				num = (float)Math.Acos((double)dot) * 57.29578f;
			}
			return num;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0001E138 File Offset: 0x0001C338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2 a, Vector2 b)
		{
			float diff_x = a.x - b.x;
			float diff_y = a.y - b.y;
			return (float)Math.Sqrt((double)(diff_x * diff_x + diff_y * diff_y));
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0001E174 File Offset: 0x0001C374
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude > maxLength * maxLength;
			Vector2 vector2;
			if (flag)
			{
				float mag = (float)Math.Sqrt((double)sqrMagnitude);
				float normalized_x = vector.x / mag;
				float normalized_y = vector.y / mag;
				vector2 = new Vector2(normalized_x * maxLength, normalized_y * maxLength);
			}
			else
			{
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0001E1C8 File Offset: 0x0001C3C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0001E1F8 File Offset: 0x0001C3F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Min(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0001E234 File Offset: 0x0001C434
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Max(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0001E270 File Offset: 0x0001C470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0001E2D8 File Offset: 0x0001C4D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0001E30C File Offset: 0x0001C50C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0001E340 File Offset: 0x0001C540
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0001E368 File Offset: 0x0001C568
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0001E390 File Offset: 0x0001C590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			float diff_x = lhs.x - rhs.x;
			float diff_y = lhs.y - rhs.y;
			return diff_x * diff_x + diff_y * diff_y < 9.9999994E-11f;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0001E41C File Offset: 0x0001C61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0001E438 File Offset: 0x0001C638
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0001E45C File Offset: 0x0001C65C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0001E484 File Offset: 0x0001C684
		public static Vector2 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.zeroVector;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0001E49C File Offset: 0x0001C69C
		public static Vector2 one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.oneVector;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		public static Vector2 up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.upVector;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0001E4CC File Offset: 0x0001C6CC
		public static Vector2 down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.downVector;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		public static Vector2 left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.leftVector;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		public static Vector2 right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.rightVector;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0001E514 File Offset: 0x0001C714
		public static Vector2 positiveInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.positiveInfinityVector;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0001E52C File Offset: 0x0001C72C
		public static Vector2 negativeInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.negativeInfinityVector;
			}
		}

		// Token: 0x040005AE RID: 1454
		public float x;

		// Token: 0x040005AF RID: 1455
		public float y;

		// Token: 0x040005B0 RID: 1456
		private static readonly Vector2 zeroVector = new Vector2(0f, 0f);

		// Token: 0x040005B1 RID: 1457
		private static readonly Vector2 oneVector = new Vector2(1f, 1f);

		// Token: 0x040005B2 RID: 1458
		private static readonly Vector2 upVector = new Vector2(0f, 1f);

		// Token: 0x040005B3 RID: 1459
		private static readonly Vector2 downVector = new Vector2(0f, -1f);

		// Token: 0x040005B4 RID: 1460
		private static readonly Vector2 leftVector = new Vector2(-1f, 0f);

		// Token: 0x040005B5 RID: 1461
		private static readonly Vector2 rightVector = new Vector2(1f, 0f);

		// Token: 0x040005B6 RID: 1462
		private static readonly Vector2 positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040005B7 RID: 1463
		private static readonly Vector2 negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

		// Token: 0x040005B8 RID: 1464
		public const float kEpsilon = 1E-05f;

		// Token: 0x040005B9 RID: 1465
		public const float kEpsilonNormalSqrt = 1E-15f;
	}
}
