using System;
using Mono.Net.Security;

namespace Mono.Security.Interface
{
	// Token: 0x02000043 RID: 67
	public static class MonoTlsProviderFactory
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000943E File Offset: 0x0000763E
		public static MonoTlsProvider GetProvider()
		{
			return (MonoTlsProvider)NoReflectionHelper.GetProvider();
		}
	}
}
