using System;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	public sealed class GUILayoutOption
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000042A6 File Offset: 0x000024A6
		internal GUILayoutOption(GUILayoutOption.Type type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x0400005D RID: 93
		internal GUILayoutOption.Type type;

		// Token: 0x0400005E RID: 94
		internal object value;

		// Token: 0x02000011 RID: 17
		internal enum Type
		{
			// Token: 0x04000060 RID: 96
			fixedWidth,
			// Token: 0x04000061 RID: 97
			fixedHeight,
			// Token: 0x04000062 RID: 98
			minWidth,
			// Token: 0x04000063 RID: 99
			maxWidth,
			// Token: 0x04000064 RID: 100
			minHeight,
			// Token: 0x04000065 RID: 101
			maxHeight,
			// Token: 0x04000066 RID: 102
			stretchWidth,
			// Token: 0x04000067 RID: 103
			stretchHeight,
			// Token: 0x04000068 RID: 104
			alignStart,
			// Token: 0x04000069 RID: 105
			alignMiddle,
			// Token: 0x0400006A RID: 106
			alignEnd,
			// Token: 0x0400006B RID: 107
			alignJustify,
			// Token: 0x0400006C RID: 108
			equalSize,
			// Token: 0x0400006D RID: 109
			spacing
		}
	}
}
