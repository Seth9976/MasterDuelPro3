using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200010F RID: 271
	internal sealed class WhenEachEnumerable<T> : IUniTaskAsyncEnumerable<WhenEachResult<T>>
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x0001FDB2 File Offset: 0x0001DFB2
		public WhenEachEnumerable(IEnumerable<UniTask<T>> source)
		{
			this.source = source;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001FDC1 File Offset: 0x0001DFC1
		public IUniTaskAsyncEnumerator<WhenEachResult<T>> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new WhenEachEnumerable<T>.Enumerator(this.source, cancellationToken);
		}

		// Token: 0x0400041A RID: 1050
		private IEnumerable<UniTask<T>> source;

		// Token: 0x02000110 RID: 272
		private sealed class Enumerator : IUniTaskAsyncEnumerator<WhenEachResult<T>>, IUniTaskAsyncDisposable
		{
			// Token: 0x060006A5 RID: 1701 RVA: 0x0001FDCF File Offset: 0x0001DFCF
			public Enumerator(IEnumerable<UniTask<T>> source, CancellationToken cancellationToken)
			{
				this.source = source;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001FDE5 File Offset: 0x0001DFE5
			public WhenEachResult<T> Current
			{
				get
				{
					return this.channelEnumerator.Current;
				}
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				if (this.state == WhenEachState.NotRunning)
				{
					this.state = WhenEachState.Running;
					this.channel = Channel.CreateSingleConsumerUnbounded<WhenEachResult<T>>();
					this.channelEnumerator = this.channel.Reader.ReadAllAsync(default(CancellationToken)).GetAsyncEnumerator(this.cancellationToken);
					UniTask<T>[] array = this.source as UniTask<T>[];
					if (array != null)
					{
						WhenEachEnumerable<T>.Enumerator.ConsumeAll(this, array, array.Length);
					}
					else
					{
						using (ArrayPoolUtil.RentArray<UniTask<T>> rentArray = ArrayPoolUtil.Materialize<UniTask<T>>(this.source))
						{
							WhenEachEnumerable<T>.Enumerator.ConsumeAll(this, rentArray.Array, rentArray.Length);
						}
					}
				}
				return this.channelEnumerator.MoveNextAsync();
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
			private static void ConsumeAll(WhenEachEnumerable<T>.Enumerator self, UniTask<T>[] array, int length)
			{
				for (int i = 0; i < length; i++)
				{
					WhenEachEnumerable<T>.Enumerator.RunWhenEachTask(self, array[i], length).Forget();
				}
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
			private static async UniTaskVoid RunWhenEachTask(WhenEachEnumerable<T>.Enumerator self, UniTask<T> task, int length)
			{
				try
				{
					T result = await task;
					self.channel.Writer.TryWrite(new WhenEachResult<T>(result));
				}
				catch (Exception ex)
				{
					self.channel.Writer.TryWrite(new WhenEachResult<T>(ex));
				}
				if (Interlocked.Increment(ref self.completeCount) == length)
				{
					self.state = WhenEachState.Completed;
					self.channel.Writer.TryComplete(null);
				}
			}

			// Token: 0x060006AA RID: 1706 RVA: 0x0001FF3C File Offset: 0x0001E13C
			public async UniTask DisposeAsync()
			{
				if (this.channelEnumerator != null)
				{
					await this.channelEnumerator.DisposeAsync();
				}
				if (this.state != WhenEachState.Completed)
				{
					this.state = WhenEachState.Completed;
					this.channel.Writer.TryComplete(new OperationCanceledException());
				}
			}

			// Token: 0x0400041B RID: 1051
			private readonly IEnumerable<UniTask<T>> source;

			// Token: 0x0400041C RID: 1052
			private CancellationToken cancellationToken;

			// Token: 0x0400041D RID: 1053
			private Channel<WhenEachResult<T>> channel;

			// Token: 0x0400041E RID: 1054
			private IUniTaskAsyncEnumerator<WhenEachResult<T>> channelEnumerator;

			// Token: 0x0400041F RID: 1055
			private int completeCount;

			// Token: 0x04000420 RID: 1056
			private WhenEachState state;
		}
	}
}
