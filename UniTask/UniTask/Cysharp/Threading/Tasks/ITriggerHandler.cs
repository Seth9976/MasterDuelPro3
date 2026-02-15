using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000072 RID: 114
	public interface ITriggerHandler<T>
	{
		// Token: 0x0600018A RID: 394
		void OnNext(T value);

		// Token: 0x0600018B RID: 395
		void OnError(Exception ex);

		// Token: 0x0600018C RID: 396
		void OnCompleted();

		// Token: 0x0600018D RID: 397
		void OnCanceled(CancellationToken cancellationToken);

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600018E RID: 398
		// (set) Token: 0x0600018F RID: 399
		ITriggerHandler<T> Prev { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000190 RID: 400
		// (set) Token: 0x06000191 RID: 401
		ITriggerHandler<T> Next { get; set; }
	}
}
