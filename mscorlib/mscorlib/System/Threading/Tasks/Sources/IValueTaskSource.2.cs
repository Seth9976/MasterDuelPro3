using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x020002D9 RID: 729
	public interface IValueTaskSource<out TResult>
	{
		// Token: 0x060019E6 RID: 6630
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x060019E7 RID: 6631
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x060019E8 RID: 6632
		TResult GetResult(short token);
	}
}
