using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200003B RID: 59
	[DebuggerTypeProxy(typeof(half2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct half2 : IEquatable<half2>, IFormattable
	{
		// Token: 0x06001622 RID: 5666 RVA: 0x000443E8 File Offset: 0x000425E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(half x, half y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000443F8 File Offset: 0x000425F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(half2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00044412 File Offset: 0x00042612
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(half v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00044422 File Offset: 0x00042622
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(float v)
		{
			this.x = (half)v;
			this.y = (half)v;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0004443C File Offset: 0x0004263C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(float2 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00044460 File Offset: 0x00042660
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(double v)
		{
			this.x = (half)v;
			this.y = (half)v;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0004447A File Offset: 0x0004267A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half2(double2 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0000D9CF File Offset: 0x0000BBCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator half2(half v)
		{
			return new half2(v);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0000D9D7 File Offset: 0x0000BBD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half2(float v)
		{
			return new half2(v);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0000D9DF File Offset: 0x0000BBDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half2(float2 v)
		{
			return new half2(v);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0000D9E7 File Offset: 0x0000BBE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half2(double v)
		{
			return new half2(v);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0000D9EF File Offset: 0x0000BBEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half2(double2 v)
		{
			return new half2(v);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0004449E File Offset: 0x0004269E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(half2 lhs, half2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000444C7 File Offset: 0x000426C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(half2 lhs, half rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000444E6 File Offset: 0x000426E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(half lhs, half2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00044505 File Offset: 0x00042705
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(half2 lhs, half2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0004452E File Offset: 0x0004272E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(half2 lhs, half rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004454D File Offset: 0x0004274D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(half lhs, half2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0004456C File Offset: 0x0004276C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0004458B File Offset: 0x0004278B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x000445AA File Offset: 0x000427AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x000445C9 File Offset: 0x000427C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000445E8 File Offset: 0x000427E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00044607 File Offset: 0x00042807
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00044626 File Offset: 0x00042826
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00044645 File Offset: 0x00042845
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x00044664 File Offset: 0x00042864
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00044683 File Offset: 0x00042883
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x000446A2 File Offset: 0x000428A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000446C1 File Offset: 0x000428C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x000446E0 File Offset: 0x000428E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x000446FF File Offset: 0x000428FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0004471E File Offset: 0x0004291E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0004473D File Offset: 0x0004293D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0004475C File Offset: 0x0004295C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00044775 File Offset: 0x00042975
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.y);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0004478E File Offset: 0x0004298E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.x);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x000447A7 File Offset: 0x000429A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x000447C0 File Offset: 0x000429C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x000447D9 File Offset: 0x000429D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x000447F2 File Offset: 0x000429F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0004480B File Offset: 0x00042A0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00044824 File Offset: 0x00042A24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.x);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00044837 File Offset: 0x00042A37
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x000443F8 File Offset: 0x000425F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0004484A File Offset: 0x00042A4A
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x0004485D File Offset: 0x00042A5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00044877 File Offset: 0x00042A77
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.y);
			}
		}

		// Token: 0x170005E8 RID: 1512
		public unsafe half this[int index]
		{
			get
			{
				fixed (half2* ptr = &this)
				{
					return ((half*)ptr)[index];
				}
			}
			set
			{
				fixed (half* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000448D5 File Offset: 0x00042AD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(half2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00044900 File Offset: 0x00042B00
		public override bool Equals(object o)
		{
			if (o is half2)
			{
				half2 converted = (half2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00044925 File Offset: 0x00042B25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00044932 File Offset: 0x00042B32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("half2({0}, {1})", this.x, this.y);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00044954 File Offset: 0x00042B54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("half2({0}, {1})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x040000E4 RID: 228
		public half x;

		// Token: 0x040000E5 RID: 229
		public half y;

		// Token: 0x040000E6 RID: 230
		public static readonly half2 zero;

		// Token: 0x0200003C RID: 60
		internal sealed class DebuggerProxy
		{
			// Token: 0x06001659 RID: 5721 RVA: 0x0004497A File Offset: 0x00042B7A
			public DebuggerProxy(half2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x040000E7 RID: 231
			public half x;

			// Token: 0x040000E8 RID: 232
			public half y;
		}
	}
}
