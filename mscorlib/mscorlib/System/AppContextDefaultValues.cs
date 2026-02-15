using System;

namespace System
{
	// Token: 0x020001B4 RID: 436
	internal static class AppContextDefaultValues
	{
		// Token: 0x0600105D RID: 4189 RVA: 0x00002C89 File Offset: 0x00000E89
		public static void PopulateDefaultValues()
		{
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00045846 File Offset: 0x00043A46
		public static bool TryGetSwitchOverride(string switchName, out bool overrideValue)
		{
			overrideValue = false;
			return false;
		}

		// Token: 0x040006BB RID: 1723
		internal static readonly string SwitchEnforceJapaneseEraYearRanges = "Switch.System.Globalization.EnforceJapaneseEraYearRanges";

		// Token: 0x040006BC RID: 1724
		internal static readonly string SwitchFormatJapaneseFirstYearAsANumber = "Switch.System.Globalization.FormatJapaneseFirstYearAsANumber";

		// Token: 0x040006BD RID: 1725
		internal static readonly string SwitchEnforceLegacyJapaneseDateParsing = "Switch.System.Globalization.EnforceLegacyJapaneseDateParsing";
	}
}
