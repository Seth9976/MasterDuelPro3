using System;

namespace Unity
{
	// Token: 0x0200004F RID: 79
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00007D4C File Offset: 0x00005F4C
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
