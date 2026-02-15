using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000150 RID: 336
	[DefaultMember("Item")]
	[UsedByNativeCode]
	[NativeType("Runtime/Math/Vector2Int.h")]
	[Il2CppEagerStaticClassConstruction]
	public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x0001E60C File Offset: 0x0001C80C
		public int x
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_X;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0001E618 File Offset: 0x0001C818
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x0001E630 File Offset: 0x0001C830
		public int y
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Y;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0001E63A File Offset: 0x0001C83A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0001E64C File Offset: 0x0001C84C
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0001E680 File Offset: 0x0001C880
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vector2Int v)
		{
			return new Vector2((float)v.x, (float)v.y);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int FloorToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0001E718 File Offset: 0x0001C918
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0001E750 File Offset: 0x0001C950
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator *(int a, Vector2Int b)
		{
			return new Vector2Int(a * b.x, a * b.y);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0001E77C File Offset: 0x0001C97C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator /(Vector2Int a, int b)
		{
			return new Vector2Int(a.x / b, a.y / b);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0001E7FC File Offset: 0x0001C9FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector2Int v;
			bool flag;
			if (other is Vector2Int)
			{
				v = (Vector2Int)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(v);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0001E830 File Offset: 0x0001CA30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector2Int other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0001E864 File Offset: 0x0001CA64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (this.x * 73856093) ^ (this.y * 83492791);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0001E890 File Offset: 0x0001CA90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0001E8AC File Offset: 0x0001CAAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0001E908 File Offset: 0x0001CB08
		public static Vector2Int zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Zero;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0001E920 File Offset: 0x0001CB20
		public static Vector2Int one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_One;
			}
		}

		// Token: 0x040005BA RID: 1466
		private int m_X;

		// Token: 0x040005BB RID: 1467
		private int m_Y;

		// Token: 0x040005BC RID: 1468
		private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);

		// Token: 0x040005BD RID: 1469
		private static readonly Vector2Int s_One = new Vector2Int(1, 1);

		// Token: 0x040005BE RID: 1470
		private static readonly Vector2Int s_Up = new Vector2Int(0, 1);

		// Token: 0x040005BF RID: 1471
		private static readonly Vector2Int s_Down = new Vector2Int(0, -1);

		// Token: 0x040005C0 RID: 1472
		private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);

		// Token: 0x040005C1 RID: 1473
		private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
	}
}
