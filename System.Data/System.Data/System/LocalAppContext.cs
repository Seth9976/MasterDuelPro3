using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000007 RID: 7
	internal class LocalAppContext
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020D3 File Offset: 0x000002D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020EC File Offset: 0x000002EC
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			bool flag;
			AppContext.TryGetSwitch(switchName, out flag);
			if (LocalAppContext.DisableCaching)
			{
				return flag;
			}
			switchValue = (flag ? 1 : (-1));
			return flag;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002115 File Offset: 0x00000315
		private static bool DisableCaching
		{
			get
			{
				return LazyInitializer.EnsureInitialized<bool>(ref LocalAppContext.s_disableCaching, ref LocalAppContext.s_isDisableCachingInitialized, ref LocalAppContext.s_syncObject, delegate
				{
					bool flag;
					AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out flag);
					return flag;
				});
			}
		}

		// Token: 0x04000004 RID: 4
		private static bool s_isDisableCachingInitialized;

		// Token: 0x04000005 RID: 5
		private static bool s_disableCaching;

		// Token: 0x04000006 RID: 6
		private static object s_syncObject;
	}
}
