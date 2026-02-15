using System;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003A RID: 58
	public interface IUniTaskSource<out T> : IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
	{
		// Token: 0x06000120 RID: 288
		T GetResult(short token);

		// Token: 0x06000121 RID: 289 RVA: 0x00004478 File Offset: 0x00002678
		UniTaskStatus GetStatus(short token)
		{
			return this.GetStatus(token);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000448A File Offset: 0x0000268A
		void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.OnCompleted(continuation, state, token);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004478 File Offset: 0x00002678
		ValueTaskSourceStatus GetStatus(short token)
		{
			return (ValueTaskSourceStatus)this.GetStatus(token);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004495 File Offset: 0x00002695
		T GetResult(short token)
		{
			return this.GetResult(token);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000448A File Offset: 0x0000268A
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			this.OnCompleted(continuation, state, token);
		}
	}
}
