using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000245 RID: 581
	[VisibleToOtherModules]
	internal static class StringMarshaller
	{
		// Token: 0x060014A6 RID: 5286 RVA: 0x0002BA60 File Offset: 0x00029C60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool TryMarshalEmptyOrNullString(string s, ref ManagedSpanWrapper managedSpanWrapper)
		{
			bool flag = s == null;
			bool flag2;
			if (flag)
			{
				managedSpanWrapper = default(ManagedSpanWrapper);
				flag2 = true;
			}
			else
			{
				bool flag3 = s.Length == 0;
				if (flag3)
				{
					managedSpanWrapper = new ManagedSpanWrapper((void*)((UIntPtr)1UL), 0);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}
	}
}
