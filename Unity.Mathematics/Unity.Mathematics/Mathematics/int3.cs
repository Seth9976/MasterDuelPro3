using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000046 RID: 70
	[DebuggerTypeProxy(typeof(int3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int3 : IEquatable<int3>, IFormattable
	{
		// Token: 0x060019F0 RID: 6640 RVA: 0x0004C495 File Offset: 0x0004A695
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0004C4AC File Offset: 0x0004A6AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int x, int2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0004C4CD File Offset: 0x0004A6CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int2 xy, int z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0004C4EE File Offset: 0x0004A6EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0004C514 File Offset: 0x0004A714
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(int v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0004C52B File Offset: 0x0004A72B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(bool v)
		{
			this.x = (v ? 1 : 0);
			this.y = (v ? 1 : 0);
			this.z = (v ? 1 : 0);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0004C554 File Offset: 0x0004A754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(bool3 v)
		{
			this.x = (v.x ? 1 : 0);
			this.y = (v.y ? 1 : 0);
			this.z = (v.z ? 1 : 0);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0004C514 File Offset: 0x0004A714
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(uint v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0004C58C File Offset: 0x0004A78C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(uint3 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0004C5B2 File Offset: 0x0004A7B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(float v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0004C5CC File Offset: 0x0004A7CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(float3 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0004C5B2 File Offset: 0x0004A7B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(double v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0004C5F5 File Offset: 0x0004A7F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3(double3 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0000E39E File Offset: 0x0000C59E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int3(int v)
		{
			return new int3(v);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(bool v)
		{
			return new int3(v);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0000E3AE File Offset: 0x0000C5AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(bool3 v)
		{
			return new int3(v);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0000E3B6 File Offset: 0x0000C5B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(uint v)
		{
			return new int3(v);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0000E3BE File Offset: 0x0000C5BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(uint3 v)
		{
			return new int3(v);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0000E3C6 File Offset: 0x0000C5C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(float v)
		{
			return new int3(v);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0000E3CE File Offset: 0x0000C5CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(float3 v)
		{
			return new int3(v);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0000E3D6 File Offset: 0x0000C5D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(double v)
		{
			return new int3(v);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0000E3DE File Offset: 0x0000C5DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3(double3 v)
		{
			return new int3(v);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0004C61E File Offset: 0x0004A81E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator *(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0004C64C File Offset: 0x0004A84C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator *(int3 lhs, int rhs)
		{
			return new int3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0004C66B File Offset: 0x0004A86B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator *(int lhs, int3 rhs)
		{
			return new int3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0004C68A File Offset: 0x0004A88A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator +(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0004C6B8 File Offset: 0x0004A8B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator +(int3 lhs, int rhs)
		{
			return new int3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0004C6D7 File Offset: 0x0004A8D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator +(int lhs, int3 rhs)
		{
			return new int3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0004C6F6 File Offset: 0x0004A8F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator -(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0004C724 File Offset: 0x0004A924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator -(int3 lhs, int rhs)
		{
			return new int3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0004C743 File Offset: 0x0004A943
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator -(int lhs, int3 rhs)
		{
			return new int3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0004C762 File Offset: 0x0004A962
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator /(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0004C790 File Offset: 0x0004A990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator /(int3 lhs, int rhs)
		{
			return new int3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0004C7AF File Offset: 0x0004A9AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator /(int lhs, int3 rhs)
		{
			return new int3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0004C7CE File Offset: 0x0004A9CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator %(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0004C7FC File Offset: 0x0004A9FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator %(int3 lhs, int rhs)
		{
			return new int3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0004C81B File Offset: 0x0004AA1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator %(int lhs, int3 rhs)
		{
			return new int3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0004C83C File Offset: 0x0004AA3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ++(int3 val)
		{
			int num = val.x + 1;
			val.x = num;
			int num2 = num;
			num = val.y + 1;
			val.y = num;
			int num3 = num;
			num = val.z + 1;
			val.z = num;
			return new int3(num2, num3, num);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0004C87C File Offset: 0x0004AA7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator --(int3 val)
		{
			int num = val.x - 1;
			val.x = num;
			int num2 = num;
			num = val.y - 1;
			val.y = num;
			int num3 = num;
			num = val.z - 1;
			val.z = num;
			return new int3(num2, num3, num);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0004C8BB File Offset: 0x0004AABB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0004C8EC File Offset: 0x0004AAEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(int3 lhs, int rhs)
		{
			return new bool3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0004C90E File Offset: 0x0004AB0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(int lhs, int3 rhs)
		{
			return new bool3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0004C930 File Offset: 0x0004AB30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0004C96A File Offset: 0x0004AB6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(int3 lhs, int rhs)
		{
			return new bool3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0004C995 File Offset: 0x0004AB95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(int lhs, int3 rhs)
		{
			return new bool3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0004C9C0 File Offset: 0x0004ABC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0004C9F1 File Offset: 0x0004ABF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(int3 lhs, int rhs)
		{
			return new bool3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0004CA13 File Offset: 0x0004AC13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(int lhs, int3 rhs)
		{
			return new bool3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0004CA35 File Offset: 0x0004AC35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0004CA6F File Offset: 0x0004AC6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(int3 lhs, int rhs)
		{
			return new bool3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0004CA9A File Offset: 0x0004AC9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(int lhs, int3 rhs)
		{
			return new bool3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0004CAC5 File Offset: 0x0004ACC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator -(int3 val)
		{
			return new int3(-val.x, -val.y, -val.z);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0004CAE1 File Offset: 0x0004ACE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator +(int3 val)
		{
			return new int3(val.x, val.y, val.z);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0004CAFA File Offset: 0x0004ACFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator <<(int3 x, int n)
		{
			return new int3(x.x << n, x.y << n, x.z << n);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0004CB22 File Offset: 0x0004AD22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator >>(int3 x, int n)
		{
			return new int3(x.x >> n, x.y >> n, x.z >> n);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0004CB4A File Offset: 0x0004AD4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0004CB7B File Offset: 0x0004AD7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(int3 lhs, int rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0004CB9D File Offset: 0x0004AD9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(int lhs, int3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0004CBBF File Offset: 0x0004ADBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(int3 lhs, int3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0004CBF9 File Offset: 0x0004ADF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(int3 lhs, int rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0004CC24 File Offset: 0x0004AE24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(int lhs, int3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0004CC4F File Offset: 0x0004AE4F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ~(int3 val)
		{
			return new int3(~val.x, ~val.y, ~val.z);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0004CC6B File Offset: 0x0004AE6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator &(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0004CC99 File Offset: 0x0004AE99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator &(int3 lhs, int rhs)
		{
			return new int3(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0004CCB8 File Offset: 0x0004AEB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator &(int lhs, int3 rhs)
		{
			return new int3(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0004CCD7 File Offset: 0x0004AED7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator |(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0004CD05 File Offset: 0x0004AF05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator |(int3 lhs, int rhs)
		{
			return new int3(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0004CD24 File Offset: 0x0004AF24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator |(int lhs, int3 rhs)
		{
			return new int3(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0004CD43 File Offset: 0x0004AF43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ^(int3 lhs, int3 rhs)
		{
			return new int3(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0004CD71 File Offset: 0x0004AF71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ^(int3 lhs, int rhs)
		{
			return new int3(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0004CD90 File Offset: 0x0004AF90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 operator ^(int lhs, int3 rhs)
		{
			return new int3(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z);
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0004CDAF File Offset: 0x0004AFAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x0004CDCE File Offset: 0x0004AFCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0004CDED File Offset: 0x0004AFED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x0004CE0C File Offset: 0x0004B00C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0004CE2B File Offset: 0x0004B02B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x0004CE4A File Offset: 0x0004B04A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0004CE69 File Offset: 0x0004B069
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0004CE88 File Offset: 0x0004B088
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0004CEA7 File Offset: 0x0004B0A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0004CEC6 File Offset: 0x0004B0C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0004CEE5 File Offset: 0x0004B0E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0004CF04 File Offset: 0x0004B104
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x0004CF23 File Offset: 0x0004B123
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0004CF42 File Offset: 0x0004B142
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0004CF61 File Offset: 0x0004B161
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0004CF80 File Offset: 0x0004B180
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0004CF9F File Offset: 0x0004B19F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0004CFBE File Offset: 0x0004B1BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0004CFDD File Offset: 0x0004B1DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0004D01B File Offset: 0x0004B21B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x0004D03A File Offset: 0x0004B23A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x0004D059 File Offset: 0x0004B259
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x0004D078 File Offset: 0x0004B278
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0004D097 File Offset: 0x0004B297
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x0004D0B6 File Offset: 0x0004B2B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0004D0D5 File Offset: 0x0004B2D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0004D0F4 File Offset: 0x0004B2F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0004D113 File Offset: 0x0004B313
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0004D132 File Offset: 0x0004B332
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0004D151 File Offset: 0x0004B351
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0004D170 File Offset: 0x0004B370
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0004D18F File Offset: 0x0004B38F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0004D1AE File Offset: 0x0004B3AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x0004D1CD File Offset: 0x0004B3CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0004D1EC File Offset: 0x0004B3EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0004D20B File Offset: 0x0004B40B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0004D22A File Offset: 0x0004B42A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0004D249 File Offset: 0x0004B449
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0004D268 File Offset: 0x0004B468
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x0004D287 File Offset: 0x0004B487
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0004D2A6 File Offset: 0x0004B4A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0004D2C5 File Offset: 0x0004B4C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0004D2E4 File Offset: 0x0004B4E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0004D303 File Offset: 0x0004B503
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0004D322 File Offset: 0x0004B522
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0004D341 File Offset: 0x0004B541
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x0004D360 File Offset: 0x0004B560
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0004D37F File Offset: 0x0004B57F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0004D39E File Offset: 0x0004B59E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0004D3BD File Offset: 0x0004B5BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0004D3DC File Offset: 0x0004B5DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0004D3FB File Offset: 0x0004B5FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0004D41A File Offset: 0x0004B61A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0004D439 File Offset: 0x0004B639
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0004D458 File Offset: 0x0004B658
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0004D477 File Offset: 0x0004B677
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0004D496 File Offset: 0x0004B696
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x0004D4B5 File Offset: 0x0004B6B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x0004D4D4 File Offset: 0x0004B6D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x0004D4F3 File Offset: 0x0004B6F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0004D512 File Offset: 0x0004B712
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0004D531 File Offset: 0x0004B731
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0004D550 File Offset: 0x0004B750
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0004D56F File Offset: 0x0004B76F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0004D58E File Offset: 0x0004B78E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0004D5AD File Offset: 0x0004B7AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0004D5CC File Offset: 0x0004B7CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0004D5EB File Offset: 0x0004B7EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0004D60A File Offset: 0x0004B80A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0004D629 File Offset: 0x0004B829
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x0004D648 File Offset: 0x0004B848
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0004D667 File Offset: 0x0004B867
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0004D686 File Offset: 0x0004B886
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0004D6A5 File Offset: 0x0004B8A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0004D6C4 File Offset: 0x0004B8C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0004D6E3 File Offset: 0x0004B8E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0004D702 File Offset: 0x0004B902
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0004D721 File Offset: 0x0004B921
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0004D740 File Offset: 0x0004B940
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0004D75F File Offset: 0x0004B95F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0004D77E File Offset: 0x0004B97E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0004D797 File Offset: 0x0004B997
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0004D7B0 File Offset: 0x0004B9B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0004D7C9 File Offset: 0x0004B9C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0004D7E2 File Offset: 0x0004B9E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0004CAE1 File Offset: 0x0004ACE1
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0004C4EE File Offset: 0x0004A6EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0004D7FB File Offset: 0x0004B9FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0004D814 File Offset: 0x0004BA14
		// (set) Token: 0x06001A91 RID: 6801 RVA: 0x0004D82D File Offset: 0x0004BA2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0004D853 File Offset: 0x0004BA53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.z);
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0004D86C File Offset: 0x0004BA6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.x);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0004D885 File Offset: 0x0004BA85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.y);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0004D89E File Offset: 0x0004BA9E
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x0004D8B7 File Offset: 0x0004BAB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0004D8DD File Offset: 0x0004BADD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0004D8F6 File Offset: 0x0004BAF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0004D90F File Offset: 0x0004BB0F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0004D928 File Offset: 0x0004BB28
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x0004D941 File Offset: 0x0004BB41
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0004D967 File Offset: 0x0004BB67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0004D980 File Offset: 0x0004BB80
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0004D999 File Offset: 0x0004BB99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0004D9B2 File Offset: 0x0004BBB2
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x0004D9CB File Offset: 0x0004BBCB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0004D9F1 File Offset: 0x0004BBF1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0004DA0A File Offset: 0x0004BC0A
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x0004DA23 File Offset: 0x0004BC23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0004DA49 File Offset: 0x0004BC49
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0004DA62 File Offset: 0x0004BC62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0004DA7B File Offset: 0x0004BC7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.x);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0004DA94 File Offset: 0x0004BC94
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.y);
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0004DAAD File Offset: 0x0004BCAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0004DAC6 File Offset: 0x0004BCC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.x);
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0004DAD9 File Offset: 0x0004BCD9
		// (set) Token: 0x06001AAB RID: 6827 RVA: 0x0004DAEC File Offset: 0x0004BCEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0004DB06 File Offset: 0x0004BD06
		// (set) Token: 0x06001AAD RID: 6829 RVA: 0x0004DB19 File Offset: 0x0004BD19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0004DB33 File Offset: 0x0004BD33
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x0004DB46 File Offset: 0x0004BD46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0004DB60 File Offset: 0x0004BD60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.y);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0004DB73 File Offset: 0x0004BD73
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0004DB86 File Offset: 0x0004BD86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0004DBA0 File Offset: 0x0004BDA0
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x0004DBB3 File Offset: 0x0004BDB3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0004DBCD File Offset: 0x0004BDCD
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x0004DBE0 File Offset: 0x0004BDE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0004DBFA File Offset: 0x0004BDFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.z);
			}
		}

		// Token: 0x17000845 RID: 2117
		public unsafe int this[int index]
		{
			get
			{
				fixed (int3* ptr = &this)
				{
					return ((int*)ptr)[index];
				}
			}
			set
			{
				fixed (int* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0004DC48 File Offset: 0x0004BE48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0004DC78 File Offset: 0x0004BE78
		public override bool Equals(object o)
		{
			if (o is int3)
			{
				int3 converted = (int3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0004DC9D File Offset: 0x0004BE9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0004DCAA File Offset: 0x0004BEAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0004DCD7 File Offset: 0x0004BED7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int3({0}, {1}, {2})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x0400010B RID: 267
		public int x;

		// Token: 0x0400010C RID: 268
		public int y;

		// Token: 0x0400010D RID: 269
		public int z;

		// Token: 0x0400010E RID: 270
		public static readonly int3 zero;

		// Token: 0x02000047 RID: 71
		internal sealed class DebuggerProxy
		{
			// Token: 0x06001ABF RID: 6847 RVA: 0x0004DD0A File Offset: 0x0004BF0A
			public DebuggerProxy(int3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x0400010F RID: 271
			public int x;

			// Token: 0x04000110 RID: 272
			public int y;

			// Token: 0x04000111 RID: 273
			public int z;
		}
	}
}
