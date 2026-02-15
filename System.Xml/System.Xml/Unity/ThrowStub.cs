using System;

namespace Unity
{
	// Token: 0x0200039B RID: 923
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060027C7 RID: 10183 RVA: 0x000DBADF File Offset: 0x000D9CDF
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
