using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B8 RID: 696
	internal struct Spacing
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0004E314 File Offset: 0x0004C514
		public float horizontal
		{
			get
			{
				return this.left + this.right;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0004E334 File Offset: 0x0004C534
		public float vertical
		{
			get
			{
				return this.top + this.bottom;
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0004E353 File Offset: 0x0004C553
		public Spacing(float left, float top, float right, float bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0004E374 File Offset: 0x0004C574
		public static Rect operator -(Rect r, Spacing a)
		{
			r.x += a.left;
			r.y += a.top;
			r.width = Mathf.Max(0f, r.width - a.horizontal);
			r.height = Mathf.Max(0f, r.height - a.vertical);
			return r;
		}

		// Token: 0x04000AE2 RID: 2786
		public float left;

		// Token: 0x04000AE3 RID: 2787
		public float top;

		// Token: 0x04000AE4 RID: 2788
		public float right;

		// Token: 0x04000AE5 RID: 2789
		public float bottom;
	}
}
