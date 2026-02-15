using System;

namespace Unity
{
	// Token: 0x02000502 RID: 1282
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x0000D54C File Offset: 0x0000B74C
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
