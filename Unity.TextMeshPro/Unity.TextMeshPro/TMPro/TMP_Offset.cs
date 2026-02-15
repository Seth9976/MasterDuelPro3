using System;

namespace TMPro
{
	// Token: 0x02000018 RID: 24
	public struct TMP_Offset
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002AAD File Offset: 0x00000CAD
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002AB5 File Offset: 0x00000CB5
		public float left
		{
			get
			{
				return this.m_Left;
			}
			set
			{
				this.m_Left = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002ABE File Offset: 0x00000CBE
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public float right
		{
			get
			{
				return this.m_Right;
			}
			set
			{
				this.m_Right = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002ACF File Offset: 0x00000CCF
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public float top
		{
			get
			{
				return this.m_Top;
			}
			set
			{
				this.m_Top = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public float bottom
		{
			get
			{
				return this.m_Bottom;
			}
			set
			{
				this.m_Bottom = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002AAD File Offset: 0x00000CAD
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public float horizontal
		{
			get
			{
				return this.m_Left;
			}
			set
			{
				this.m_Left = value;
				this.m_Right = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002ACF File Offset: 0x00000CCF
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002B01 File Offset: 0x00000D01
		public float vertical
		{
			get
			{
				return this.m_Top;
			}
			set
			{
				this.m_Top = value;
				this.m_Bottom = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002B11 File Offset: 0x00000D11
		public static TMP_Offset zero
		{
			get
			{
				return TMP_Offset.k_ZeroOffset;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002B18 File Offset: 0x00000D18
		public TMP_Offset(float left, float right, float top, float bottom)
		{
			this.m_Left = left;
			this.m_Right = right;
			this.m_Top = top;
			this.m_Bottom = bottom;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B37 File Offset: 0x00000D37
		public TMP_Offset(float horizontal, float vertical)
		{
			this.m_Left = horizontal;
			this.m_Right = horizontal;
			this.m_Top = vertical;
			this.m_Bottom = vertical;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B55 File Offset: 0x00000D55
		public static bool operator ==(TMP_Offset lhs, TMP_Offset rhs)
		{
			return lhs.m_Left == rhs.m_Left && lhs.m_Right == rhs.m_Right && lhs.m_Top == rhs.m_Top && lhs.m_Bottom == rhs.m_Bottom;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002B91 File Offset: 0x00000D91
		public static bool operator !=(TMP_Offset lhs, TMP_Offset rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002B9D File Offset: 0x00000D9D
		public static TMP_Offset operator *(TMP_Offset a, float b)
		{
			return new TMP_Offset(a.m_Left * b, a.m_Right * b, a.m_Top * b, a.m_Bottom * b);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002BE9 File Offset: 0x00000DE9
		public bool Equals(TMP_Offset other)
		{
			return base.Equals(other);
		}

		// Token: 0x0400003E RID: 62
		private float m_Left;

		// Token: 0x0400003F RID: 63
		private float m_Right;

		// Token: 0x04000040 RID: 64
		private float m_Top;

		// Token: 0x04000041 RID: 65
		private float m_Bottom;

		// Token: 0x04000042 RID: 66
		private static readonly TMP_Offset k_ZeroOffset = new TMP_Offset(0f, 0f, 0f, 0f);
	}
}
