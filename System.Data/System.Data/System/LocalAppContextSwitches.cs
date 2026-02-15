using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000006 RID: 6
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020C2 File Offset: 0x000002C2
		public static bool AllowArbitraryTypeInstantiation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.AllowArbitraryDataSetTypeInstantiation", ref global::System.LocalAppContextSwitches.s_allowArbitraryTypeInstantiation);
			}
		}

		// Token: 0x04000003 RID: 3
		private static int s_allowArbitraryTypeInstantiation;
	}
}
