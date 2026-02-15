using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200018E RID: 398
	internal static class AppContextSwitches
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0003CF74 File Offset: 0x0003B174
		public static bool NoAsyncCurrentCulture
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Globalization.NoAsyncCurrentCulture", ref AppContextSwitches._noAsyncCurrentCulture);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0003CF85 File Offset: 0x0003B185
		public static bool EnforceJapaneseEraYearRanges
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchEnforceJapaneseEraYearRanges, ref AppContextSwitches._enforceJapaneseEraYearRanges);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003CF96 File Offset: 0x0003B196
		public static bool FormatJapaneseFirstYearAsANumber
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchFormatJapaneseFirstYearAsANumber, ref AppContextSwitches._formatJapaneseFirstYearAsANumber);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0003CFA7 File Offset: 0x0003B1A7
		public static bool EnforceLegacyJapaneseDateParsing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchEnforceLegacyJapaneseDateParsing, ref AppContextSwitches._enforceLegacyJapaneseDateParsing);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0003CFB8 File Offset: 0x0003B1B8
		public static bool SetActorAsReferenceWhenCopyingClaimsIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Security.ClaimsIdentity.SetActorAsReferenceWhenCopyingClaimsIdentity", ref AppContextSwitches._cloneActor);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x0003CFD0 File Offset: 0x0003B1D0
		private static bool DisableCaching { get; set; }

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003CFD8 File Offset: 0x0003B1D8
		static AppContextSwitches()
		{
			bool flag;
			if (AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out flag))
			{
				AppContextSwitches.DisableCaching = flag;
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003CFF9 File Offset: 0x0003B1F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || AppContextSwitches.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003D010 File Offset: 0x0003B210
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			bool flag;
			AppContext.TryGetSwitch(switchName, out flag);
			if (AppContextSwitches.DisableCaching)
			{
				return flag;
			}
			switchValue = (flag ? 1 : (-1));
			return flag;
		}

		// Token: 0x040005C0 RID: 1472
		private static int _noAsyncCurrentCulture;

		// Token: 0x040005C1 RID: 1473
		private static int _enforceJapaneseEraYearRanges;

		// Token: 0x040005C2 RID: 1474
		private static int _formatJapaneseFirstYearAsANumber;

		// Token: 0x040005C3 RID: 1475
		private static int _enforceLegacyJapaneseDateParsing;

		// Token: 0x040005C4 RID: 1476
		private static int _cloneActor;
	}
}
