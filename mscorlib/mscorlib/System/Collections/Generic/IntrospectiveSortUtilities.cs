using System;

namespace System.Collections.Generic
{
	// Token: 0x0200076B RID: 1899
	internal static class IntrospectiveSortUtilities
	{
		// Token: 0x06003C6B RID: 15467 RVA: 0x000E9380 File Offset: 0x000E7580
		internal static int FloorLog2PlusOne(int n)
		{
			int num = 0;
			while (n >= 1)
			{
				num++;
				n /= 2;
			}
			return num;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000E939F File Offset: 0x000E759F
		internal static void ThrowOrIgnoreBadComparer(object comparer)
		{
			throw new ArgumentException(SR.Format("Unable to sort because the IComparer.Compare() method returns inconsistent results. Either a value does not compare equal to itself, or one value repeatedly compared to another value yields different results. IComparer: '{0}'.", comparer));
		}
	}
}
