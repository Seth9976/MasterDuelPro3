using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000152 RID: 338
	[NativeClass("Vector4f")]
	[Il2CppEagerStaticClassConstruction]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/Math/Vector4.h")]
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		// Token: 0x1700026B RID: 619
		public float this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.x;
					break;
				case 1:
					num = this.y;
					break;
				case 2:
					num = this.z;
					break;
				case 3:
					num = this.w;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
				return num;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0001EEA9 File Offset: 0x0001D0A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0001EEC9 File Offset: 0x0001D0C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0f;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0001EEA9 File Offset: 0x0001D0A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0001EF14 File Offset: 0x0001D114
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0001EF8C File Offset: 0x0001D18C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector4 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
			this.w *= scale.w;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0001F030 File Offset: 0x0001D230
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector4 v;
			bool flag;
			if (other is Vector4)
			{
				v = (Vector4)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(v);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0001F064 File Offset: 0x0001D264
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector4 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z && this.w == other.w;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 a)
		{
			float mag = Vector4.Magnitude(a);
			bool flag = mag > 1E-05f;
			Vector4 vector;
			if (flag)
			{
				vector = a / mag;
			}
			else
			{
				vector = Vector4.zero;
			}
			return vector;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		public Vector4 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.Normalize(this);
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0001F108 File Offset: 0x0001D308
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0001F154 File Offset: 0x0001D354
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector4 a)
		{
			return (float)Math.Sqrt((double)Vector4.Dot(a, a));
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0001F174 File Offset: 0x0001D374
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)Math.Sqrt((double)Vector4.Dot(this, this));
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0001F1A0 File Offset: 0x0001D3A0
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.Dot(this, this);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		public static Vector4 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.zeroVector;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		public static Vector4 one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.oneVector;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0001F240 File Offset: 0x0001D440
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0001F28C File Offset: 0x0001D48C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0001F2C0 File Offset: 0x0001D4C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0001F330 File Offset: 0x0001D530
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0001F368 File Offset: 0x0001D568
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			float diffx = lhs.x - rhs.x;
			float diffy = lhs.y - rhs.y;
			float diffz = lhs.z - rhs.z;
			float diffw = lhs.w - rhs.w;
			float sqrmag = diffx * diffx + diffy * diffy + diffz * diffz + diffw * diffw;
			return sqrmag < 9.9999994E-11f;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0001F3EC File Offset: 0x0001D5EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0001F41C File Offset: 0x0001D61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0001F448 File Offset: 0x0001D648
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0001F478 File Offset: 0x0001D678
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0001F49C File Offset: 0x0001D69C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
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
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x040005CD RID: 1485
		public const float kEpsilon = 1E-05f;

		// Token: 0x040005CE RID: 1486
		public float x;

		// Token: 0x040005CF RID: 1487
		public float y;

		// Token: 0x040005D0 RID: 1488
		public float z;

		// Token: 0x040005D1 RID: 1489
		public float w;

		// Token: 0x040005D2 RID: 1490
		private static readonly Vector4 zeroVector = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x040005D3 RID: 1491
		private static readonly Vector4 oneVector = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x040005D4 RID: 1492
		private static readonly Vector4 positiveInfinityVector = new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040005D5 RID: 1493
		private static readonly Vector4 negativeInfinityVector = new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
