using System;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003D RID: 61
	public abstract class MoveNextSource : IUniTaskSource<bool>, IUniTaskSource, IValueTaskSource, IValueTaskSource<bool>
	{
		// Token: 0x06000130 RID: 304 RVA: 0x000046C3 File Offset: 0x000028C3
		public bool GetResult(short token)
		{
			return this.completionSource.GetResult(token);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000046D1 File Offset: 0x000028D1
		public UniTaskStatus GetStatus(short token)
		{
			return this.completionSource.GetStatus(token);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000046DF File Offset: 0x000028DF
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.completionSource.OnCompleted(continuation, state, token);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000046EF File Offset: 0x000028EF
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.completionSource.UnsafeGetStatus();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000046FC File Offset: 0x000028FC
		void IUniTaskSource.GetResult(short token)
		{
			this.completionSource.GetResult(token);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000470C File Offset: 0x0000290C
		protected bool TryGetResult<T>(UniTask<T>.Awaiter awaiter, out T result)
		{
			bool flag;
			try
			{
				result = awaiter.GetResult();
				flag = true;
			}
			catch (Exception ex)
			{
				this.completionSource.TrySetException(ex);
				result = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004754 File Offset: 0x00002954
		protected bool TryGetResult(UniTask.Awaiter awaiter)
		{
			bool flag;
			try
			{
				awaiter.GetResult();
				flag = true;
			}
			catch (Exception ex)
			{
				this.completionSource.TrySetException(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400009C RID: 156
		protected UniTaskCompletionSourceCore<bool> completionSource;
	}
}
