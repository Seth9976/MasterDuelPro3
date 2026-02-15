using System;

namespace System.Drawing
{
	// Token: 0x02000008 RID: 8
	internal static class ColorUtil
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002160 File Offset: 0x00000360
		public static Color FromKnownColor(KnownColor color)
		{
			return Color.FromKnownColor(color);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002168 File Offset: 0x00000368
		public static bool IsSystemColor(this Color color)
		{
			return color.IsSystemColor;
		}
	}
}
