using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x020002D8 RID: 728
	public interface IValueTaskSource
	{
		// Token: 0x060019E3 RID: 6627
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x060019E4 RID: 6628
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x060019E5 RID: 6629
		void GetResult(short token);
	}
}
