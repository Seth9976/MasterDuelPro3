using System;

namespace Unity
{
	// Token: 0x02000015 RID: 21
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002457 File Offset: 0x00000657
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
