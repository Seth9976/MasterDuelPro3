using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000059 RID: 89
	internal static class SetPropertyUtility
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000FF3C File Offset: 0x0000E13C
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000FF8B File Offset: 0x0000E18B
		public static bool SetEquatableStruct<T>(ref T currentValue, T newValue) where T : IEquatable<T>
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000FFA6 File Offset: 0x0000E1A6
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
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
