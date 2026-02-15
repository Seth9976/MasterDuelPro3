using System;
using UnityEngine.Scripting;

namespace UnityEngine.Bindings
{
	// Token: 0x02000243 RID: 579
	[VisibleToOtherModules]
	internal static class ExceptionMarshaller
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x0002BA44 File Offset: 0x00029C44
		[RequiredByNativeCode]
		private static void SetPendingException(Exception ex)
		{
			ExceptionMarshaller.s_pendingException = ex;
		}

		// Token: 0x040007A8 RID: 1960
		[ThreadStatic]
		private static Exception s_pendingException;
	}
}
