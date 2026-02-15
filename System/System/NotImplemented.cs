using System;

namespace System
{
	// Token: 0x020000EA RID: 234
	internal static class NotImplemented
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0001148B File Offset: 0x0000F68B
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}
	}
}
