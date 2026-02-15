using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000120 RID: 288
	internal static class UniTaskCompletionSourceCoreShared
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x000209C0 File Offset: 0x0001EBC0
		private static void CompletionSentinel(object _)
		{
			throw new InvalidOperationException("The sentinel delegate should never be invoked.");
		}

		// Token: 0x0400043E RID: 1086
		internal static readonly Action<object> s_sentinel = new Action<object>(UniTaskCompletionSourceCoreShared.CompletionSentinel);
	}
}
