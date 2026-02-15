using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000050 RID: 80
	internal struct Offset
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00021538 File Offset: 0x0001F738
		public float left
		{
			get
			{
				return this.m_Left;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00021550 File Offset: 0x0001F750
		public float right
		{
			get
			{
				return this.m_Right;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00021568 File Offset: 0x0001F768
		public float top
		{
			get
			{
				return this.m_Top;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00021580 File Offset: 0x0001F780
		public float bottom
		{
			get
			{
				return this.m_Bottom;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00021598 File Offset: 0x0001F798
		public static Offset zero
		{
			get
			{
				return Offset.k_ZeroOffset;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000215AF File Offset: 0x0001F7AF
		public Offset(float left, float right, float top, float bottom)
		{
			this.m_Left = left;
			this.m_Right = right;
			this.m_Top = top;
			this.m_Bottom = bottom;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000215D0 File Offset: 0x0001F7D0
		public static bool operator ==(Offset lhs, Offset rhs)
		{
			return lhs.m_Left == rhs.m_Left && lhs.m_Right == rhs.m_Right && lhs.m_Top == rhs.m_Top && lhs.m_Bottom == rhs.m_Bottom;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00021620 File Offset: 0x0001F820
		public static Offset operator *(Offset a, float b)
		{
			return new Offset(a.m_Left * b, a.m_Right * b, a.m_Top * b, a.m_Bottom * b);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00021658 File Offset: 0x0001F858
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0002167C File Offset: 0x0001F87C
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x04000304 RID: 772
		private float m_Left;

		// Token: 0x04000305 RID: 773
		private float m_Right;

		// Token: 0x04000306 RID: 774
		private float m_Top;

		// Token: 0x04000307 RID: 775
		private float m_Bottom;

		// Token: 0x04000308 RID: 776
		private static readonly Offset k_ZeroOffset = new Offset(0f, 0f, 0f, 0f);
	}
}
