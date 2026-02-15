using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000019 RID: 25
	public struct HighlightState
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002C21 File Offset: 0x00000E21
		public HighlightState(Color32 color, TMP_Offset padding)
		{
			this.color = color;
			this.padding = padding;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002C31 File Offset: 0x00000E31
		public static bool operator ==(HighlightState lhs, HighlightState rhs)
		{
			return lhs.color.Compare(rhs.color) && lhs.padding == rhs.padding;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002C59 File Offset: 0x00000E59
		public static bool operator !=(HighlightState lhs, HighlightState rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002C65 File Offset: 0x00000E65
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002C77 File Offset: 0x00000E77
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002C8A File Offset: 0x00000E8A
		public bool Equals(HighlightState other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000043 RID: 67
		public Color32 color;

		// Token: 0x04000044 RID: 68
		public TMP_Offset padding;
	}
}
