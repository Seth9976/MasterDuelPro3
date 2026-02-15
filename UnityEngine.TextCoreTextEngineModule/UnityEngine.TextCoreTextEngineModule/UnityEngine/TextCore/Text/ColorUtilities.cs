using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000018 RID: 24
	internal static class ColorUtilities
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003C30 File Offset: 0x00001E30
		internal static bool CompareColors(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003C80 File Offset: 0x00001E80
		internal static Color32 MultiplyColors(Color32 c1, Color32 c2)
		{
			byte r = (byte)((float)c1.r / 255f * ((float)c2.r / 255f) * 255f);
			byte g = (byte)((float)c1.g / 255f * ((float)c2.g / 255f) * 255f);
			byte b = (byte)((float)c1.b / 255f * ((float)c2.b / 255f) * 255f);
			byte a = (byte)((float)c1.a / 255f * ((float)c2.a / 255f) * 255f);
			return new Color32(r, g, b, a);
		}
	}
}
