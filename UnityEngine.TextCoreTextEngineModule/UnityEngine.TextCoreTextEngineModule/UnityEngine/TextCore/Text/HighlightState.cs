using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000051 RID: 81
	internal struct HighlightState
	{
		// Token: 0x060001EE RID: 494 RVA: 0x000216BF File Offset: 0x0001F8BF
		public HighlightState(Color32 color, Offset padding)
		{
			this.color = color;
			this.padding = padding;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000216D0 File Offset: 0x0001F8D0
		public static bool operator ==(HighlightState lhs, HighlightState rhs)
		{
			return lhs.color.r == rhs.color.r && lhs.color.g == rhs.color.g && lhs.color.b == rhs.color.b && lhs.color.a == rhs.color.a && lhs.padding == rhs.padding;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00021758 File Offset: 0x0001F958
		public static bool operator !=(HighlightState lhs, HighlightState rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00021774 File Offset: 0x0001F974
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00021798 File Offset: 0x0001F998
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x04000309 RID: 777
		public Color32 color;

		// Token: 0x0400030A RID: 778
		public Offset padding;
	}
}
