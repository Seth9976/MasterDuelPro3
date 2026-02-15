using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000036 RID: 54
	[StructLayout(LayoutKind.Auto)]
	public readonly struct UniTaskCancelableAsyncEnumerable<T>
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00004420 File Offset: 0x00002620
		internal UniTaskCancelableAsyncEnumerable(IUniTaskAsyncEnumerable<T> enumerable, CancellationToken cancellationToken)
		{
			this.enumerable = enumerable;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004430 File Offset: 0x00002630
		public UniTaskCancelableAsyncEnumerable<T>.Enumerator GetAsyncEnumerator()
		{
			return new UniTaskCancelableAsyncEnumerable<T>.Enumerator(this.enumerable.GetAsyncEnumerator(this.cancellationToken));
		}

		// Token: 0x04000091 RID: 145
		private readonly IUniTaskAsyncEnumerable<T> enumerable;

		// Token: 0x04000092 RID: 146
		private readonly CancellationToken cancellationToken;

		// Token: 0x02000037 RID: 55
		[StructLayout(LayoutKind.Auto)]
		public readonly struct Enumerator
		{
			// Token: 0x06000115 RID: 277 RVA: 0x00004448 File Offset: 0x00002648
			internal Enumerator(IUniTaskAsyncEnumerator<T> enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x06000116 RID: 278 RVA: 0x00004451 File Offset: 0x00002651
			public T Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06000117 RID: 279 RVA: 0x0000445E File Offset: 0x0000265E
			public UniTask<bool> MoveNextAsync()
			{
				return this.enumerator.MoveNextAsync();
			}

			// Token: 0x06000118 RID: 280 RVA: 0x0000446B File Offset: 0x0000266B
			public UniTask DisposeAsync()
			{
				return this.enumerator.DisposeAsync();
			}

			// Token: 0x04000093 RID: 147
			private readonly IUniTaskAsyncEnumerator<T> enumerator;
		}
	}
}
