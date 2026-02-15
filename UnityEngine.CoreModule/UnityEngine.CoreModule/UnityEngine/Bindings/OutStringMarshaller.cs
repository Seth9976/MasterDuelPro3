using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000246 RID: 582
	[VisibleToOtherModules]
	internal ref struct OutStringMarshaller
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x0002BAB4 File Offset: 0x00029CB4
		public unsafe static string GetStringAndDispose(ManagedSpanWrapper managedSpan)
		{
			bool flag = managedSpan.length == 0;
			string text;
			if (flag)
			{
				text = ((managedSpan.begin == null) ? null : string.Empty);
			}
			else
			{
				string outString = new string((char*)managedSpan.begin, 0, managedSpan.length);
				BindingsAllocator.Free(managedSpan.begin);
				text = outString;
			}
			return text;
		}
	}
}
