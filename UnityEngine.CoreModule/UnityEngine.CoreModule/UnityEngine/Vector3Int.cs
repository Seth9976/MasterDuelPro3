using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000151 RID: 337
	[Il2CppEagerStaticClassConstruction]
	[DefaultMember("Item")]
	[UsedByNativeCode]
	public struct Vector3Int : IEquatable<Vector3Int>, IFormattable
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0001E990 File Offset: 0x0001CB90
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x0001E9CC File Offset: 0x0001CBCC
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		public int z
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Z;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0001E9FA File Offset: 0x0001CBFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3Int(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0001EA14 File Offset: 0x0001CC14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0001EA64 File Offset: 0x0001CC64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vector3Int v)
		{
			return new Vector3((float)v.x, (float)v.y, (float)v.z);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0001EAE4 File Offset: 0x0001CCE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0001EB28 File Offset: 0x0001CD28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0001EB6C File Offset: 0x0001CD6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator *(Vector3Int a, int b)
		{
			return new Vector3Int(a.x * b, a.y * b, a.z * b);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0001EBA0 File Offset: 0x0001CDA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator /(Vector3Int a, int b)
		{
			return new Vector3Int(a.x / b, a.y / b, a.z / b);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0001EC1C File Offset: 0x0001CE1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector3Int v;
			bool flag;
			if (other is Vector3Int)
			{
				v = (Vector3Int)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(v);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0001EC50 File Offset: 0x0001CE50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector3Int other)
		{
			return this == other;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0001EC70 File Offset: 0x0001CE70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			int yHash = this.y.GetHashCode();
			int zHash = this.z.GetHashCode();
			return this.x.GetHashCode() ^ (yHash << 4) ^ (yHash >> 28) ^ (zHash >> 4) ^ (zHash << 28);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0001ECC0 File Offset: 0x0001CEC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0001ECDC File Offset: 0x0001CEDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0001ED4C File Offset: 0x0001CF4C
		public static Vector3Int zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Zero;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0001ED64 File Offset: 0x0001CF64
		public static Vector3Int one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_One;
			}
		}

		// Token: 0x040005C2 RID: 1474
		private int m_X;

		// Token: 0x040005C3 RID: 1475
		private int m_Y;

		// Token: 0x040005C4 RID: 1476
		private int m_Z;

		// Token: 0x040005C5 RID: 1477
		private static readonly Vector3Int s_Zero = new Vector3Int(0, 0, 0);

		// Token: 0x040005C6 RID: 1478
		private static readonly Vector3Int s_One = new Vector3Int(1, 1, 1);

		// Token: 0x040005C7 RID: 1479
		private static readonly Vector3Int s_Up = new Vector3Int(0, 1, 0);

		// Token: 0x040005C8 RID: 1480
		private static readonly Vector3Int s_Down = new Vector3Int(0, -1, 0);

		// Token: 0x040005C9 RID: 1481
		private static readonly Vector3Int s_Left = new Vector3Int(-1, 0, 0);

		// Token: 0x040005CA RID: 1482
		private static readonly Vector3Int s_Right = new Vector3Int(1, 0, 0);

		// Token: 0x040005CB RID: 1483
		private static readonly Vector3Int s_Forward = new Vector3Int(0, 0, 1);

		// Token: 0x040005CC RID: 1484
		private static readonly Vector3Int s_Back = new Vector3Int(0, 0, -1);
	}
}
