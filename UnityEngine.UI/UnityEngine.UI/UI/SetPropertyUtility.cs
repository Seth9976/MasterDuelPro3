using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200006C RID: 108
	internal static class SetPropertyUtility
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x00014578 File Offset: 0x00012778
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000145C7 File Offset: 0x000127C7
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000145E8 File Offset: 0x000127E8
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
