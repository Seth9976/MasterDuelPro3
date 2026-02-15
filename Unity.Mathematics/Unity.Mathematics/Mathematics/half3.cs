using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200003D RID: 61
	[DebuggerTypeProxy(typeof(half3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct half3 : IEquatable<half3>, IFormattable
	{
		// Token: 0x0600165A RID: 5722 RVA: 0x0004499A File Offset: 0x00042B9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half x, half y, half z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000449B1 File Offset: 0x00042BB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half x, half2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000449D2 File Offset: 0x00042BD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half2 xy, half z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000449F3 File Offset: 0x00042BF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00044A19 File Offset: 0x00042C19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00044A30 File Offset: 0x00042C30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(float v)
		{
			this.x = (half)v;
			this.y = (half)v;
			this.z = (half)v;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00044A56 File Offset: 0x00042C56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(float3 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
			this.z = (half)v.z;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00044A8B File Offset: 0x00042C8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(double v)
		{
			this.x = (half)v;
			this.y = (half)v;
			this.z = (half)v;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00044AB1 File Offset: 0x00042CB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half3(double3 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
			this.z = (half)v.z;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0000DA92 File Offset: 0x0000BC92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator half3(half v)
		{
			return new half3(v);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0000DA9A File Offset: 0x0000BC9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half3(float v)
		{
			return new half3(v);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0000DAA2 File Offset: 0x0000BCA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half3(float3 v)
		{
			return new half3(v);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0000DAAA File Offset: 0x0000BCAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half3(double v)
		{
			return new half3(v);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0000DAB2 File Offset: 0x0000BCB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half3(double3 v)
		{
			return new half3(v);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00044AE6 File Offset: 0x00042CE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(half3 lhs, half3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00044B20 File Offset: 0x00042D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(half3 lhs, half rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00044B4B File Offset: 0x00042D4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(half lhs, half3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00044B76 File Offset: 0x00042D76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(half3 lhs, half3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00044BB0 File Offset: 0x00042DB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(half3 lhs, half rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00044BDB File Offset: 0x00042DDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(half lhs, half3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x00044C06 File Offset: 0x00042E06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00044C25 File Offset: 0x00042E25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00044C44 File Offset: 0x00042E44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00044C63 File Offset: 0x00042E63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x00044C82 File Offset: 0x00042E82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x00044CA1 File Offset: 0x00042EA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x00044CC0 File Offset: 0x00042EC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00044CDF File Offset: 0x00042EDF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x00044CFE File Offset: 0x00042EFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00044D1D File Offset: 0x00042F1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x00044D3C File Offset: 0x00042F3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00044D5B File Offset: 0x00042F5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00044D7A File Offset: 0x00042F7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00044D99 File Offset: 0x00042F99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00044DB8 File Offset: 0x00042FB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00044DD7 File Offset: 0x00042FD7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00044DF6 File Offset: 0x00042FF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00044E15 File Offset: 0x00043015
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00044E34 File Offset: 0x00043034
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x00044E53 File Offset: 0x00043053
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00044E72 File Offset: 0x00043072
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00044E91 File Offset: 0x00043091
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00044EB0 File Offset: 0x000430B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00044ECF File Offset: 0x000430CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00044EEE File Offset: 0x000430EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00044F0D File Offset: 0x0004310D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00044F2C File Offset: 0x0004312C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00044F4B File Offset: 0x0004314B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x00044F6A File Offset: 0x0004316A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00044F89 File Offset: 0x00043189
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00044FA8 File Offset: 0x000431A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00044FC7 File Offset: 0x000431C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x00044FE6 File Offset: 0x000431E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00045005 File Offset: 0x00043205
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00045024 File Offset: 0x00043224
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00045043 File Offset: 0x00043243
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00045062 File Offset: 0x00043262
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x00045081 File Offset: 0x00043281
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x000450A0 File Offset: 0x000432A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x000450BF File Offset: 0x000432BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000450DE File Offset: 0x000432DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x000450FD File Offset: 0x000432FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0004511C File Offset: 0x0004331C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0004513B File Offset: 0x0004333B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0004515A File Offset: 0x0004335A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00045179 File Offset: 0x00043379
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00045198 File Offset: 0x00043398
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x000451B7 File Offset: 0x000433B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x000451D6 File Offset: 0x000433D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x000451F5 File Offset: 0x000433F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00045214 File Offset: 0x00043414
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00045233 File Offset: 0x00043433
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00045252 File Offset: 0x00043452
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00045271 File Offset: 0x00043471
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00045290 File Offset: 0x00043490
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x000452AF File Offset: 0x000434AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000452CE File Offset: 0x000434CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000452ED File Offset: 0x000434ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0004530C File Offset: 0x0004350C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0004532B File Offset: 0x0004352B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0004534A File Offset: 0x0004354A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00045369 File Offset: 0x00043569
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00045388 File Offset: 0x00043588
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x000453A7 File Offset: 0x000435A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000453C6 File Offset: 0x000435C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x000453E5 File Offset: 0x000435E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00045404 File Offset: 0x00043604
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00045423 File Offset: 0x00043623
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00045442 File Offset: 0x00043642
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00045461 File Offset: 0x00043661
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00045480 File Offset: 0x00043680
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0004549F File Offset: 0x0004369F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000454BE File Offset: 0x000436BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x000454DD File Offset: 0x000436DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000454FC File Offset: 0x000436FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0004551B File Offset: 0x0004371B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0004553A File Offset: 0x0004373A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x00045559 File Offset: 0x00043759
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00045578 File Offset: 0x00043778
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x00045597 File Offset: 0x00043797
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x000455B6 File Offset: 0x000437B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x000455D5 File Offset: 0x000437D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.x);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x000455EE File Offset: 0x000437EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.y);
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00045607 File Offset: 0x00043807
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00045620 File Offset: 0x00043820
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.x);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00045639 File Offset: 0x00043839
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.y);
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00045652 File Offset: 0x00043852
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x000449F3 File Offset: 0x00042BF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0004566B File Offset: 0x0004386B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00045684 File Offset: 0x00043884
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0004569D File Offset: 0x0004389D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000456C3 File Offset: 0x000438C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000456DC File Offset: 0x000438DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000456F5 File Offset: 0x000438F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0004570E File Offset: 0x0004390E
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x00045727 File Offset: 0x00043927
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x0004574D File Offset: 0x0004394D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00045766 File Offset: 0x00043966
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x0004577F File Offset: 0x0004397F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00045798 File Offset: 0x00043998
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x000457B1 File Offset: 0x000439B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000457D7 File Offset: 0x000439D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.y);
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x000457F0 File Offset: 0x000439F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.z);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00045809 File Offset: 0x00043A09
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.x);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00045822 File Offset: 0x00043A22
		// (set) Token: 0x060016D7 RID: 5847 RVA: 0x0004583B File Offset: 0x00043A3B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00045861 File Offset: 0x00043A61
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.z);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0004587A File Offset: 0x00043A7A
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x00045893 File Offset: 0x00043A93
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x000458B9 File Offset: 0x00043AB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000458D2 File Offset: 0x00043AD2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x000458EB File Offset: 0x00043AEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00045904 File Offset: 0x00043B04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0004591D File Offset: 0x00043B1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00045936 File Offset: 0x00043B36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.x);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00045949 File Offset: 0x00043B49
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x0004595C File Offset: 0x00043B5C
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

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00045976 File Offset: 0x00043B76
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x00045989 File Offset: 0x00043B89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x000459A3 File Offset: 0x00043BA3
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x000459B6 File Offset: 0x00043BB6
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

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x000459D0 File Offset: 0x00043BD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.y);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x000459E3 File Offset: 0x00043BE3
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x000459F6 File Offset: 0x00043BF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00045A10 File Offset: 0x00043C10
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x00045A23 File Offset: 0x00043C23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00045A3D File Offset: 0x00043C3D
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x00045A50 File Offset: 0x00043C50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00045A6A File Offset: 0x00043C6A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.z);
			}
		}

		// Token: 0x1700065E RID: 1630
		public unsafe half this[int index]
		{
			get
			{
				fixed (half3* ptr = &this)
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

		// Token: 0x060016F1 RID: 5873 RVA: 0x00045AC9 File Offset: 0x00043CC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(half3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00045B04 File Offset: 0x00043D04
		public override bool Equals(object o)
		{
			if (o is half3)
			{
				half3 converted = (half3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00045B29 File Offset: 0x00043D29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00045B36 File Offset: 0x00043D36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("half3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00045B63 File Offset: 0x00043D63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("half3({0}, {1}, {2})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x040000E9 RID: 233
		public half x;

		// Token: 0x040000EA RID: 234
		public half y;

		// Token: 0x040000EB RID: 235
		public half z;

		// Token: 0x040000EC RID: 236
		public static readonly half3 zero;

		// Token: 0x0200003E RID: 62
		internal sealed class DebuggerProxy
		{
			// Token: 0x060016F6 RID: 5878 RVA: 0x00045B96 File Offset: 0x00043D96
			public DebuggerProxy(half3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x040000ED RID: 237
			public half x;

			// Token: 0x040000EE RID: 238
			public half y;

			// Token: 0x040000EF RID: 239
			public half z;
		}
	}
}
