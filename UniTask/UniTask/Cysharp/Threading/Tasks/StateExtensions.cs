using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000014 RID: 20
	public static class StateExtensions
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000030CE File Offset: 0x000012CE
		public static ReadOnlyAsyncReactiveProperty<T> ToReadOnlyAsyncReactiveProperty<T>(this IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return new ReadOnlyAsyncReactiveProperty<T>(source, cancellationToken);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000030D7 File Offset: 0x000012D7
		public static ReadOnlyAsyncReactiveProperty<T> ToReadOnlyAsyncReactiveProperty<T>(this IUniTaskAsyncEnumerable<T> source, T initialValue, CancellationToken cancellationToken)
		{
			return new ReadOnlyAsyncReactiveProperty<T>(initialValue, source, cancellationToken);
		}
	}
}
