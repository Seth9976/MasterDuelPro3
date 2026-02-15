using System;

namespace Mono.Net.Security
{
	// Token: 0x0200007C RID: 124
	internal static class NoReflectionHelper
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00007F3A File Offset: 0x0000613A
		internal static object GetProvider()
		{
			return MonoTlsProviderFactory.GetProvider();
		}
	}
}
