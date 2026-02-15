using System;

namespace System.Globalization
{
	// Token: 0x020006B0 RID: 1712
	internal static class GlobalizationMode
	{
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000CE6BB File Offset: 0x000CC8BB
		internal static bool Invariant { get; } = GlobalizationMode.GetGlobalizationInvariantMode();

		// Token: 0x0600356C RID: 13676 RVA: 0x00033991 File Offset: 0x00031B91
		private static bool GetGlobalizationInvariantMode()
		{
			return false;
		}
	}
}
