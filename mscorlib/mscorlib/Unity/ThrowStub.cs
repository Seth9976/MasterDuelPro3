using System;

namespace Unity
{
	// Token: 0x02000831 RID: 2097
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06004181 RID: 16769 RVA: 0x000145B3 File Offset: 0x000127B3
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
