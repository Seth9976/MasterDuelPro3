using System;
using System.Collections.Generic;

namespace System
{
	// Token: 0x0200018C RID: 396
	public static class AppContext
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x0003CE0C File Offset: 0x0003B00C
		private static void InitializeDefaultSwitchValues()
		{
			Dictionary<string, AppContext.SwitchValueState> dictionary = AppContext.s_switchMap;
			lock (dictionary)
			{
				if (!AppContext.s_defaultsInitialized)
				{
					AppContextDefaultValues.PopulateDefaultValues();
					AppContext.s_defaultsInitialized = true;
				}
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003CE5C File Offset: 0x0003B05C
		public static bool TryGetSwitch(string switchName, out bool isEnabled)
		{
			if (switchName == null)
			{
				throw new ArgumentNullException("switchName");
			}
			if (switchName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "switchName");
			}
			if (!AppContext.s_defaultsInitialized)
			{
				AppContext.InitializeDefaultSwitchValues();
			}
			isEnabled = false;
			Dictionary<string, AppContext.SwitchValueState> dictionary = AppContext.s_switchMap;
			lock (dictionary)
			{
				AppContext.SwitchValueState switchValueState;
				if (AppContext.s_switchMap.TryGetValue(switchName, out switchValueState))
				{
					if (switchValueState == AppContext.SwitchValueState.UnknownValue)
					{
						isEnabled = false;
						return false;
					}
					isEnabled = (switchValueState & AppContext.SwitchValueState.HasTrueValue) == AppContext.SwitchValueState.HasTrueValue;
					if ((switchValueState & AppContext.SwitchValueState.HasLookedForOverride) == AppContext.SwitchValueState.HasLookedForOverride)
					{
						return true;
					}
					bool flag2;
					if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out flag2))
					{
						isEnabled = flag2;
					}
					AppContext.s_switchMap[switchName] = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride;
					return true;
				}
				else
				{
					bool flag3;
					if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out flag3))
					{
						isEnabled = flag3;
						AppContext.s_switchMap[switchName] = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride;
						return true;
					}
					AppContext.s_switchMap[switchName] = AppContext.SwitchValueState.UnknownValue;
				}
			}
			return false;
		}

		// Token: 0x040005B9 RID: 1465
		private static readonly Dictionary<string, AppContext.SwitchValueState> s_switchMap = new Dictionary<string, AppContext.SwitchValueState>();

		// Token: 0x040005BA RID: 1466
		private static volatile bool s_defaultsInitialized = false;

		// Token: 0x0200018D RID: 397
		[Flags]
		private enum SwitchValueState
		{
			// Token: 0x040005BC RID: 1468
			HasFalseValue = 1,
			// Token: 0x040005BD RID: 1469
			HasTrueValue = 2,
			// Token: 0x040005BE RID: 1470
			HasLookedForOverride = 4,
			// Token: 0x040005BF RID: 1471
			UnknownValue = 8
		}
	}
}
