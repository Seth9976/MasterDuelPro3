using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000193 RID: 403
	internal static class ClassLibraryInitializer
	{
		// Token: 0x06000FEE RID: 4078 RVA: 0x00021996 File Offset: 0x0001FB96
		[RequiredByNativeCode]
		private static void Init()
		{
			UnityLogWriter.Init();
		}
	}
}
