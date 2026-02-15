using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001F RID: 31
	public abstract class ChannelReader<T>
	{
		// Token: 0x060000AE RID: 174
		public abstract bool TryRead(out T item);

		// Token: 0x060000AF RID: 175
		public abstract UniTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B0 RID: 176
		public abstract UniTask Completion { get; }

		// Token: 0x060000B1 RID: 177 RVA: 0x0000351C File Offset: 0x0000171C
		public virtual UniTask<T> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			T item;
			if (this.TryRead(out item))
			{
				return UniTask.FromResult<T>(item);
			}
			return this.ReadAsyncCore(cancellationToken);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003544 File Offset: 0x00001744
		private async UniTask<T> ReadAsyncCore(CancellationToken cancellationToken = default(CancellationToken))
		{
			UniTask<bool>.Awaiter awaiter = this.WaitToReadAsync(cancellationToken).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				UniTask<bool>.Awaiter awaiter2;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<bool>.Awaiter);
			}
			T item;
			if (awaiter.GetResult() && this.TryRead(out item))
			{
				return item;
			}
			throw new ChannelClosedException();
		}

		// Token: 0x060000B3 RID: 179
		public abstract IUniTaskAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
