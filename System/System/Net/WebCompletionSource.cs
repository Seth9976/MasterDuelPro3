using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000433 RID: 1075
	internal class WebCompletionSource<T>
	{
		// Token: 0x06001B31 RID: 6961 RVA: 0x0007648E File Offset: 0x0007468E
		public WebCompletionSource(bool runAsync = true)
		{
			this.completion = new TaskCompletionSource<WebCompletionSource<T>.Result>(runAsync ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x000764A9 File Offset: 0x000746A9
		internal WebCompletionSource<T>.Result CurrentResult
		{
			get
			{
				return this.currentResult;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x000764B1 File Offset: 0x000746B1
		internal Task Task
		{
			get
			{
				return this.completion.Task;
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000764C0 File Offset: 0x000746C0
		public bool TrySetCompleted(T argument)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(argument);
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000764F4 File Offset: 0x000746F4
		public bool TrySetCompleted()
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Completed, null);
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00076526 File Offset: 0x00074726
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(new OperationCanceledException());
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00076534 File Offset: 0x00074734
		public bool TrySetCanceled(OperationCanceledException error)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Canceled, ExceptionDispatchInfo.Capture(error));
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0007656C File Offset: 0x0007476C
		public bool TrySetException(Exception error)
		{
			WebCompletionSource<T>.Result result = new WebCompletionSource<T>.Result(WebCompletionSource<T>.Status.Faulted, ExceptionDispatchInfo.Capture(error));
			return Interlocked.CompareExchange<WebCompletionSource<T>.Result>(ref this.currentResult, result, null) == null && this.completion.TrySetResult(result);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000765A3 File Offset: 0x000747A3
		public void ThrowOnError()
		{
			if (!this.completion.Task.IsCompleted)
			{
				return;
			}
			ExceptionDispatchInfo error = this.completion.Task.Result.Error;
			if (error == null)
			{
				return;
			}
			error.Throw();
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000765D8 File Offset: 0x000747D8
		public async Task<T> WaitForCompletion()
		{
			WebCompletionSource<T>.Result result = await this.completion.Task.ConfigureAwait(false);
			if (result.Status == WebCompletionSource<T>.Status.Completed)
			{
				return result.Argument;
			}
			result.Error.Throw();
			throw new InvalidOperationException("Should never happen.");
		}

		// Token: 0x040011C4 RID: 4548
		private TaskCompletionSource<WebCompletionSource<T>.Result> completion;

		// Token: 0x040011C5 RID: 4549
		private WebCompletionSource<T>.Result currentResult;

		// Token: 0x02000434 RID: 1076
		internal enum Status
		{
			// Token: 0x040011C7 RID: 4551
			Running,
			// Token: 0x040011C8 RID: 4552
			Completed,
			// Token: 0x040011C9 RID: 4553
			Canceled,
			// Token: 0x040011CA RID: 4554
			Faulted
		}

		// Token: 0x02000435 RID: 1077
		internal class Result
		{
			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0007661B File Offset: 0x0007481B
			public WebCompletionSource<T>.Status Status { get; }

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001B3C RID: 6972 RVA: 0x00076623 File Offset: 0x00074823
			public bool Success
			{
				get
				{
					return this.Status == WebCompletionSource<T>.Status.Completed;
				}
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0007662E File Offset: 0x0007482E
			public ExceptionDispatchInfo Error { get; }

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06001B3E RID: 6974 RVA: 0x00076636 File Offset: 0x00074836
			public T Argument { get; }

			// Token: 0x06001B3F RID: 6975 RVA: 0x0007663E File Offset: 0x0007483E
			public Result(T argument)
			{
				this.Status = WebCompletionSource<T>.Status.Completed;
				this.Argument = argument;
			}

			// Token: 0x06001B40 RID: 6976 RVA: 0x00076654 File Offset: 0x00074854
			public Result(WebCompletionSource<T>.Status state, ExceptionDispatchInfo error)
			{
				this.Status = state;
				this.Error = error;
			}
		}
	}
}
