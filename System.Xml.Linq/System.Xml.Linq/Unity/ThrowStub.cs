using System;

namespace Unity
{
	// Token: 0x02000026 RID: 38
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004B9E File Offset: 0x00002D9E
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
