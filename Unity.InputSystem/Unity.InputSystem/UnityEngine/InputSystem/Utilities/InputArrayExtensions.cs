using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200023F RID: 575
	internal static class InputArrayExtensions
	{
		// Token: 0x060014FE RID: 5374 RVA: 0x0005F8B8 File Offset: 0x0005DAB8
		public static int IndexOfReference<TValue>(this InlinedArray<TValue> array, TValue value) where TValue : class
		{
			for (int i = 0; i < array.length; i++)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0005F8F0 File Offset: 0x0005DAF0
		public static bool Contains<TValue>(this InlinedArray<TValue> array, TValue value)
		{
			for (int i = 0; i < array.length; i++)
			{
				TValue tvalue = array[i];
				if (tvalue.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0005F92F File Offset: 0x0005DB2F
		public static bool ContainsReference<TValue>(this InlinedArray<TValue> array, TValue value) where TValue : class
		{
			return array.IndexOfReference(value) != -1;
		}
	}
}
