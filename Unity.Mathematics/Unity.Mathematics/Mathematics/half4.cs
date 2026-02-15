using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200003F RID: 63
	[DebuggerTypeProxy(typeof(half4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct half4 : IEquatable<half4>, IFormattable
	{
		// Token: 0x060016F7 RID: 5879 RVA: 0x00045BC2 File Offset: 0x00043DC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half x, half y, half z, half w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00045BE1 File Offset: 0x00043DE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half x, half y, half2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00045C09 File Offset: 0x00043E09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half x, half2 yz, half w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00045C31 File Offset: 0x00043E31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half x, half3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00045C5E File Offset: 0x00043E5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half2 xy, half z, half w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00045C86 File Offset: 0x00043E86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half2 xy, half2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00045CB8 File Offset: 0x00043EB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half3 xyz, half w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00045CE5 File Offset: 0x00043EE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00045D17 File Offset: 0x00043F17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00045D35 File Offset: 0x00043F35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(float v)
		{
			this.x = (half)v;
			this.y = (half)v;
			this.z = (half)v;
			this.w = (half)v;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00045D68 File Offset: 0x00043F68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(float4 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
			this.z = (half)v.z;
			this.w = (half)v.w;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00045DB9 File Offset: 0x00043FB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(double v)
		{
			this.x = (half)v;
			this.y = (half)v;
			this.z = (half)v;
			this.w = (half)v;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00045DEC File Offset: 0x00043FEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half4(double4 v)
		{
			this.x = (half)v.x;
			this.y = (half)v.y;
			this.z = (half)v.z;
			this.w = (half)v.w;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0000DBB6 File Offset: 0x0000BDB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator half4(half v)
		{
			return new half4(v);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half4(float v)
		{
			return new half4(v);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0000DBC6 File Offset: 0x0000BDC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half4(float4 v)
		{
			return new half4(v);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0000DBCE File Offset: 0x0000BDCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half4(double v)
		{
			return new half4(v);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0000DBD6 File Offset: 0x0000BDD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half4(double4 v)
		{
			return new half4(v);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00045E40 File Offset: 0x00044040
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(half4 lhs, half4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00045E96 File Offset: 0x00044096
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(half4 lhs, half rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00045ECD File Offset: 0x000440CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(half lhs, half4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00045F04 File Offset: 0x00044104
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(half4 lhs, half4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00045F5A File Offset: 0x0004415A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(half4 lhs, half rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00045F91 File Offset: 0x00044191
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(half lhs, half4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00045FC8 File Offset: 0x000441C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00045FE7 File Offset: 0x000441E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00046006 File Offset: 0x00044206
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00046025 File Offset: 0x00044225
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00046044 File Offset: 0x00044244
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00046063 File Offset: 0x00044263
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00046082 File Offset: 0x00044282
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x000460A1 File Offset: 0x000442A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x000460C0 File Offset: 0x000442C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x000460DF File Offset: 0x000442DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x000460FE File Offset: 0x000442FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0004611D File Offset: 0x0004431D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0004613C File Offset: 0x0004433C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0004615B File Offset: 0x0004435B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0004617A File Offset: 0x0004437A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x00046199 File Offset: 0x00044399
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x000461B8 File Offset: 0x000443B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000461D7 File Offset: 0x000443D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x000461F6 File Offset: 0x000443F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00046215 File Offset: 0x00044415
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00046234 File Offset: 0x00044434
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x00046253 File Offset: 0x00044453
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00046272 File Offset: 0x00044472
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x00046291 File Offset: 0x00044491
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x000462B0 File Offset: 0x000444B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x000462CF File Offset: 0x000444CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x000462EE File Offset: 0x000444EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0004630D File Offset: 0x0004450D
		// (set) Token: 0x0600172B RID: 5931 RVA: 0x00045CE5 File Offset: 0x00043EE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0004632C File Offset: 0x0004452C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0004634B File Offset: 0x0004454B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0004636A File Offset: 0x0004456A
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x00046389 File Offset: 0x00044589
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x000463BB File Offset: 0x000445BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x000463DA File Offset: 0x000445DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x000463F9 File Offset: 0x000445F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00046418 File Offset: 0x00044618
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x00046437 File Offset: 0x00044637
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00046456 File Offset: 0x00044656
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00046475 File Offset: 0x00044675
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00046494 File Offset: 0x00044694
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x000464B3 File Offset: 0x000446B3
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x000464D2 File Offset: 0x000446D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00046504 File Offset: 0x00044704
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00046523 File Offset: 0x00044723
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00046542 File Offset: 0x00044742
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00046561 File Offset: 0x00044761
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00046580 File Offset: 0x00044780
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0004659F File Offset: 0x0004479F
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x000465BE File Offset: 0x000447BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x000465F0 File Offset: 0x000447F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0004660F File Offset: 0x0004480F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0004662E File Offset: 0x0004482E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0004664D File Offset: 0x0004484D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0004666C File Offset: 0x0004486C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0004668B File Offset: 0x0004488B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x000466AA File Offset: 0x000448AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x000466C9 File Offset: 0x000448C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x000466E8 File Offset: 0x000448E8
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x00046707 File Offset: 0x00044907
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00046739 File Offset: 0x00044939
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00046758 File Offset: 0x00044958
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00046777 File Offset: 0x00044977
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x00046796 File Offset: 0x00044996
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x000467C8 File Offset: 0x000449C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x000467E7 File Offset: 0x000449E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00046806 File Offset: 0x00044A06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00046825 File Offset: 0x00044A25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00046844 File Offset: 0x00044A44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00046863 File Offset: 0x00044A63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00046882 File Offset: 0x00044A82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x000468A1 File Offset: 0x00044AA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x000468C0 File Offset: 0x00044AC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x000468DF File Offset: 0x00044ADF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x000468FE File Offset: 0x00044AFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0004691D File Offset: 0x00044B1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0004693C File Offset: 0x00044B3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0004695B File Offset: 0x00044B5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0004697A File Offset: 0x00044B7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00046999 File Offset: 0x00044B99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x000469B8 File Offset: 0x00044BB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x000469D7 File Offset: 0x00044BD7
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x000469F6 File Offset: 0x00044BF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x00046A28 File Offset: 0x00044C28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00046A47 File Offset: 0x00044C47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00046A66 File Offset: 0x00044C66
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x00046A85 File Offset: 0x00044C85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00046AB7 File Offset: 0x00044CB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00046AD6 File Offset: 0x00044CD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00046AF5 File Offset: 0x00044CF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00046B14 File Offset: 0x00044D14
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00046B33 File Offset: 0x00044D33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x00046B52 File Offset: 0x00044D52
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x00046B71 File Offset: 0x00044D71
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00046B90 File Offset: 0x00044D90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x00046BAF File Offset: 0x00044DAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00046BCE File Offset: 0x00044DCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00046BED File Offset: 0x00044DED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00046C0C File Offset: 0x00044E0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00046C2B File Offset: 0x00044E2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00046C4A File Offset: 0x00044E4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x00046C69 File Offset: 0x00044E69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00046C88 File Offset: 0x00044E88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00046CA7 File Offset: 0x00044EA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00046CC6 File Offset: 0x00044EC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00046CE5 File Offset: 0x00044EE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00046D04 File Offset: 0x00044F04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x00046D23 File Offset: 0x00044F23
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x00046D42 File Offset: 0x00044F42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x00046D74 File Offset: 0x00044F74
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x00046D93 File Offset: 0x00044F93
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x00046DB2 File Offset: 0x00044FB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x00046DD1 File Offset: 0x00044FD1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00046DF0 File Offset: 0x00044FF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00046E0F File Offset: 0x0004500F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x00046E2E File Offset: 0x0004502E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x00046E4D File Offset: 0x0004504D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00046E6C File Offset: 0x0004506C
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00046E8B File Offset: 0x0004508B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x00046EBD File Offset: 0x000450BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00046EDC File Offset: 0x000450DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00046EFB File Offset: 0x000450FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x00046F1A File Offset: 0x0004511A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x00046F39 File Offset: 0x00045139
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x00046F58 File Offset: 0x00045158
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x00046F77 File Offset: 0x00045177
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00046FA9 File Offset: 0x000451A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x00046FC8 File Offset: 0x000451C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00046FE7 File Offset: 0x000451E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x00047006 File Offset: 0x00045206
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x00047025 File Offset: 0x00045225
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x00047044 File Offset: 0x00045244
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x00047063 File Offset: 0x00045263
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00047095 File Offset: 0x00045295
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x000470B4 File Offset: 0x000452B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x000470D3 File Offset: 0x000452D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x000470F2 File Offset: 0x000452F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00047111 File Offset: 0x00045311
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00047130 File Offset: 0x00045330
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0004714F File Offset: 0x0004534F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0004716E File Offset: 0x0004536E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0004718D File Offset: 0x0004538D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x000471AC File Offset: 0x000453AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x000471CB File Offset: 0x000453CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x000471EA File Offset: 0x000453EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00047209 File Offset: 0x00045409
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00047228 File Offset: 0x00045428
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x00047247 File Offset: 0x00045447
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x00047266 File Offset: 0x00045466
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00047298 File Offset: 0x00045498
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x000472B7 File Offset: 0x000454B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x000472D6 File Offset: 0x000454D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000472F5 File Offset: 0x000454F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x00047314 File Offset: 0x00045514
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x00047333 File Offset: 0x00045533
		// (set) Token: 0x060017AA RID: 6058 RVA: 0x00047352 File Offset: 0x00045552
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x00047384 File Offset: 0x00045584
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000473A3 File Offset: 0x000455A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x000473C2 File Offset: 0x000455C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000473E1 File Offset: 0x000455E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x00047400 File Offset: 0x00045600
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0004741F File Offset: 0x0004561F
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x0004743E File Offset: 0x0004563E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x00047470 File Offset: 0x00045670
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0004748F File Offset: 0x0004568F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000474AE File Offset: 0x000456AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000474CD File Offset: 0x000456CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000474EC File Offset: 0x000456EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0004750B File Offset: 0x0004570B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0004752A File Offset: 0x0004572A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00047549 File Offset: 0x00045749
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x00047568 File Offset: 0x00045768
		// (set) Token: 0x060017BB RID: 6075 RVA: 0x00047587 File Offset: 0x00045787
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000475B9 File Offset: 0x000457B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000475D8 File Offset: 0x000457D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000475F7 File Offset: 0x000457F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00047616 File Offset: 0x00045816
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00047635 File Offset: 0x00045835
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00047654 File Offset: 0x00045854
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x00047673 File Offset: 0x00045873
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00047692 File Offset: 0x00045892
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000476B1 File Offset: 0x000458B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000476D0 File Offset: 0x000458D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x000476EF File Offset: 0x000458EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0004770E File Offset: 0x0004590E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0004772D File Offset: 0x0004592D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0004774C File Offset: 0x0004594C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0004776B File Offset: 0x0004596B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0004778A File Offset: 0x0004598A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000477A9 File Offset: 0x000459A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000477C8 File Offset: 0x000459C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000477E7 File Offset: 0x000459E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x00047806 File Offset: 0x00045A06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00047825 File Offset: 0x00045A25
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x00047844 File Offset: 0x00045A44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00047876 File Offset: 0x00045A76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00047895 File Offset: 0x00045A95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000478B4 File Offset: 0x00045AB4
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000478D3 File Offset: 0x00045AD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00047905 File Offset: 0x00045B05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00047924 File Offset: 0x00045B24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00047943 File Offset: 0x00045B43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x00047962 File Offset: 0x00045B62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00047981 File Offset: 0x00045B81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x000479A0 File Offset: 0x00045BA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x000479BF File Offset: 0x00045BBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x000479DE File Offset: 0x00045BDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x000479FD File Offset: 0x00045BFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x00047A1C File Offset: 0x00045C1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00047A3B File Offset: 0x00045C3B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00047A5A File Offset: 0x00045C5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00047A79 File Offset: 0x00045C79
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00047A98 File Offset: 0x00045C98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00047AB7 File Offset: 0x00045CB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00047AD6 File Offset: 0x00045CD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00047AF5 File Offset: 0x00045CF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x00047B14 File Offset: 0x00045D14
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x00047B33 File Offset: 0x00045D33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00047B65 File Offset: 0x00045D65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x00047B84 File Offset: 0x00045D84
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00047BA3 File Offset: 0x00045DA3
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x00047BC2 File Offset: 0x00045DC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00047BF4 File Offset: 0x00045DF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00047C13 File Offset: 0x00045E13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00047C32 File Offset: 0x00045E32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00047C51 File Offset: 0x00045E51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x00047C70 File Offset: 0x00045E70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00047C8F File Offset: 0x00045E8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00047CAE File Offset: 0x00045EAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00047CCD File Offset: 0x00045ECD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00047CEC File Offset: 0x00045EEC
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x00047D0B File Offset: 0x00045F0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00047D3D File Offset: 0x00045F3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00047D5C File Offset: 0x00045F5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00047D7B File Offset: 0x00045F7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00047D9A File Offset: 0x00045F9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00047DB9 File Offset: 0x00045FB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00047DD8 File Offset: 0x00045FD8
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x00047DF7 File Offset: 0x00045FF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00047E29 File Offset: 0x00046029
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00047E48 File Offset: 0x00046048
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00047E67 File Offset: 0x00046067
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00047E86 File Offset: 0x00046086
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00047EA5 File Offset: 0x000460A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00047EC4 File Offset: 0x000460C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00047EE3 File Offset: 0x000460E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00047F02 File Offset: 0x00046102
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00047F21 File Offset: 0x00046121
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x00047F40 File Offset: 0x00046140
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00047F72 File Offset: 0x00046172
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x00047F91 File Offset: 0x00046191
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x00047FB0 File Offset: 0x000461B0
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x00047FCF File Offset: 0x000461CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x00048001 File Offset: 0x00046201
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x00048020 File Offset: 0x00046220
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0004803F File Offset: 0x0004623F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0004805E File Offset: 0x0004625E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0004807D File Offset: 0x0004627D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0004809C File Offset: 0x0004629C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x000480BB File Offset: 0x000462BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x000480DA File Offset: 0x000462DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x000480F9 File Offset: 0x000462F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00048118 File Offset: 0x00046318
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00048137 File Offset: 0x00046337
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00048156 File Offset: 0x00046356
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00048175 File Offset: 0x00046375
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00048194 File Offset: 0x00046394
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x000481B3 File Offset: 0x000463B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x000481D2 File Offset: 0x000463D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x000481F1 File Offset: 0x000463F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x00048210 File Offset: 0x00046410
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0004822F File Offset: 0x0004642F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0004824E File Offset: 0x0004644E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0004826D File Offset: 0x0004646D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x0004828C File Offset: 0x0004648C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x000482AB File Offset: 0x000464AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x000482CA File Offset: 0x000464CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x000482E9 File Offset: 0x000464E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00048308 File Offset: 0x00046508
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x00048327 File Offset: 0x00046527
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00048346 File Offset: 0x00046546
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0004835F File Offset: 0x0004655F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x00048378 File Offset: 0x00046578
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00048391 File Offset: 0x00046591
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.x, this.w);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x000483AA File Offset: 0x000465AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x000483C3 File Offset: 0x000465C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x000483DC File Offset: 0x000465DC
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x000483F5 File Offset: 0x000465F5
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

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x0004841B File Offset: 0x0004661B
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x00048434 File Offset: 0x00046634
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0004845A File Offset: 0x0004665A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00048473 File Offset: 0x00046673
		// (set) Token: 0x06001833 RID: 6195 RVA: 0x0004848C File Offset: 0x0004668C
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

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x000484B2 File Offset: 0x000466B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.z);
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x000484CB File Offset: 0x000466CB
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x000484E4 File Offset: 0x000466E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0004850A File Offset: 0x0004670A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.w, this.x);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x00048523 File Offset: 0x00046723
		// (set) Token: 0x06001839 RID: 6201 RVA: 0x0004853C File Offset: 0x0004673C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00048562 File Offset: 0x00046762
		// (set) Token: 0x0600183B RID: 6203 RVA: 0x0004857B File Offset: 0x0004677B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x000485A1 File Offset: 0x000467A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.x, this.w, this.w);
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x000485BA File Offset: 0x000467BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x000485D3 File Offset: 0x000467D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x000485EC File Offset: 0x000467EC
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x00048605 File Offset: 0x00046805
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

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0004862B File Offset: 0x0004682B
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x00048644 File Offset: 0x00046844
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0004866A File Offset: 0x0004686A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00048683 File Offset: 0x00046883
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0004869C File Offset: 0x0004689C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x000486B5 File Offset: 0x000468B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.y, this.w);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x000486CE File Offset: 0x000468CE
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x000486E7 File Offset: 0x000468E7
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

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0004870D File Offset: 0x0004690D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00048726 File Offset: 0x00046926
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.z);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0004873F File Offset: 0x0004693F
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x00048758 File Offset: 0x00046958
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0004877E File Offset: 0x0004697E
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x00048797 File Offset: 0x00046997
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000487BD File Offset: 0x000469BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.w, this.y);
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x000487D6 File Offset: 0x000469D6
		// (set) Token: 0x06001851 RID: 6225 RVA: 0x000487EF File Offset: 0x000469EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x00048815 File Offset: 0x00046A15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.y, this.w, this.w);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0004882E File Offset: 0x00046A2E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x00048847 File Offset: 0x00046A47
		// (set) Token: 0x06001855 RID: 6229 RVA: 0x00048860 File Offset: 0x00046A60
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

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x00048886 File Offset: 0x00046A86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0004889F File Offset: 0x00046A9F
		// (set) Token: 0x06001858 RID: 6232 RVA: 0x000488B8 File Offset: 0x00046AB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x000488DE File Offset: 0x00046ADE
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x000488F7 File Offset: 0x00046AF7
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

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0004891D File Offset: 0x00046B1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x00048936 File Offset: 0x00046B36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0004894F File Offset: 0x00046B4F
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x00048968 File Offset: 0x00046B68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0004898E File Offset: 0x00046B8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000489A7 File Offset: 0x00046BA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x000489C0 File Offset: 0x00046BC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x000489D9 File Offset: 0x00046BD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.z, this.w);
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x000489F2 File Offset: 0x00046BF2
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x00048A0B File Offset: 0x00046C0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x00048A31 File Offset: 0x00046C31
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x00048A4A File Offset: 0x00046C4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x00048A70 File Offset: 0x00046C70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.w, this.z);
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x00048A89 File Offset: 0x00046C89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.z, this.w, this.w);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00048AA2 File Offset: 0x00046CA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.x, this.x);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x00048ABB File Offset: 0x00046CBB
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x00048AD4 File Offset: 0x00046CD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x00048AFA File Offset: 0x00046CFA
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x00048B13 File Offset: 0x00046D13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x00048B39 File Offset: 0x00046D39
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.x, this.w);
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00048B52 File Offset: 0x00046D52
		// (set) Token: 0x06001870 RID: 6256 RVA: 0x00048B6B File Offset: 0x00046D6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x00048B91 File Offset: 0x00046D91
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.y, this.y);
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x00048BAA File Offset: 0x00046DAA
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x00048BC3 File Offset: 0x00046DC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x00048BE9 File Offset: 0x00046DE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.y, this.w);
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00048C02 File Offset: 0x00046E02
		// (set) Token: 0x06001876 RID: 6262 RVA: 0x00048C1B File Offset: 0x00046E1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00048C41 File Offset: 0x00046E41
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x00048C5A File Offset: 0x00046E5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00048C80 File Offset: 0x00046E80
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.z, this.z);
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x00048C99 File Offset: 0x00046E99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.z, this.w);
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00048CB2 File Offset: 0x00046EB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.w, this.x);
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00048CCB File Offset: 0x00046ECB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.w, this.y);
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x00048CE4 File Offset: 0x00046EE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.w, this.z);
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00048CFD File Offset: 0x00046EFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half3(this.w, this.w, this.w);
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00048D16 File Offset: 0x00046F16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.x);
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00048D29 File Offset: 0x00046F29
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x00048D3C File Offset: 0x00046F3C
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

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00048D56 File Offset: 0x00046F56
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x00048D69 File Offset: 0x00046F69
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

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x00048D83 File Offset: 0x00046F83
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x00048D96 File Offset: 0x00046F96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00048DB0 File Offset: 0x00046FB0
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x00048DC3 File Offset: 0x00046FC3
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

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x00048DDD File Offset: 0x00046FDD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.y);
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00048DF0 File Offset: 0x00046FF0
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x00048E03 File Offset: 0x00047003
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00048E1D File Offset: 0x0004701D
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x00048E30 File Offset: 0x00047030
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00048E4A File Offset: 0x0004704A
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x00048E5D File Offset: 0x0004705D
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

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00048E77 File Offset: 0x00047077
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x00048E8A File Offset: 0x0004708A
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

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00048EA4 File Offset: 0x000470A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.z);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00048EB7 File Offset: 0x000470B7
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x00048ECA File Offset: 0x000470CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00048EE4 File Offset: 0x000470E4
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x00048EF7 File Offset: 0x000470F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x00048F11 File Offset: 0x00047111
		// (set) Token: 0x06001897 RID: 6295 RVA: 0x00048F24 File Offset: 0x00047124
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00048F3E File Offset: 0x0004713E
		// (set) Token: 0x06001899 RID: 6297 RVA: 0x00048F51 File Offset: 0x00047151
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x00048F6B File Offset: 0x0004716B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public half2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new half2(this.w, this.w);
			}
		}

		// Token: 0x170007AF RID: 1967
		public unsafe half this[int index]
		{
			get
			{
				fixed (half4* ptr = &this)
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

		// Token: 0x0600189D RID: 6301 RVA: 0x00048FCC File Offset: 0x000471CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(half4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00049028 File Offset: 0x00047228
		public override bool Equals(object o)
		{
			if (o is half4)
			{
				half4 converted = (half4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0004904D File Offset: 0x0004724D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0004905C File Offset: 0x0004725C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("half4({0}, {1}, {2}, {3})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000490B4 File Offset: 0x000472B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("half4({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000F0 RID: 240
		public half x;

		// Token: 0x040000F1 RID: 241
		public half y;

		// Token: 0x040000F2 RID: 242
		public half z;

		// Token: 0x040000F3 RID: 243
		public half w;

		// Token: 0x040000F4 RID: 244
		public static readonly half4 zero;

		// Token: 0x02000040 RID: 64
		internal sealed class DebuggerProxy
		{
			// Token: 0x060018A2 RID: 6306 RVA: 0x00049111 File Offset: 0x00047311
			public DebuggerProxy(half4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x040000F5 RID: 245
			public half x;

			// Token: 0x040000F6 RID: 246
			public half y;

			// Token: 0x040000F7 RID: 247
			public half z;

			// Token: 0x040000F8 RID: 248
			public half w;
		}
	}
}
