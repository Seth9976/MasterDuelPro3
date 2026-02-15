using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200011A RID: 282
	internal class ProbeVolumeDebugColorPreferences
	{
		// Token: 0x040004F4 RID: 1268
		internal static Func<Color> GetDetailSubdivisionColor;

		// Token: 0x040004F5 RID: 1269
		internal static Func<Color> GetMediumSubdivisionColor;

		// Token: 0x040004F6 RID: 1270
		internal static Func<Color> GetLowSubdivisionColor;

		// Token: 0x040004F7 RID: 1271
		internal static Func<Color> GetVeryLowSubdivisionColor;

		// Token: 0x040004F8 RID: 1272
		internal static Func<Color> GetSparseSubdivisionColor;

		// Token: 0x040004F9 RID: 1273
		internal static Func<Color> GetSparsestSubdivisionColor;

		// Token: 0x040004FA RID: 1274
		internal static Color s_DetailSubdivision = new Color32(135, 35, byte.MaxValue, byte.MaxValue);

		// Token: 0x040004FB RID: 1275
		internal static Color s_MediumSubdivision = new Color32(54, 208, 228, byte.MaxValue);

		// Token: 0x040004FC RID: 1276
		internal static Color s_LowSubdivision = new Color32(byte.MaxValue, 100, 45, byte.MaxValue);

		// Token: 0x040004FD RID: 1277
		internal static Color s_VeryLowSubdivision = new Color32(52, 87, byte.MaxValue, byte.MaxValue);

		// Token: 0x040004FE RID: 1278
		internal static Color s_SparseSubdivision = new Color32(byte.MaxValue, 71, 97, byte.MaxValue);

		// Token: 0x040004FF RID: 1279
		internal static Color s_SparsestSubdivision = new Color32(200, 227, 39, byte.MaxValue);
	}
}
