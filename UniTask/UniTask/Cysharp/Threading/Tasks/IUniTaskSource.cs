using System;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000039 RID: 57
	public interface IUniTaskSource : IValueTaskSource
	{
		// Token: 0x06000119 RID: 281
		UniTaskStatus GetStatus(short token);

		// Token: 0x0600011A RID: 282
		void OnCompleted(Action<object> continuation, object state, short token);

		// Token: 0x0600011B RID: 283
		void GetResult(short token);

		// Token: 0x0600011C RID: 284
		UniTaskStatus UnsafeGetStatus();

		// Token: 0x0600011D RID: 285 RVA: 0x00004478 File Offset: 0x00002678
		ValueTaskSourceStatus GetStatus(short token)
		{
			return (ValueTaskSourceStatus)this.GetStatus(token);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004481 File Offset: 0x00002681
		void GetResult(short token)
		{
			this.GetResult(token);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000448A File Offset: 0x0000268A
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			this.OnCompleted(continuation, state, token);
		}
	}
}
