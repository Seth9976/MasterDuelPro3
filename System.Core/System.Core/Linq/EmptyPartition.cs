using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Linq
{
	// Token: 0x02000050 RID: 80
	internal sealed class EmptyPartition<TElement> : IPartition<TElement>, IIListProvider<TElement>, IEnumerable<TElement>, IEnumerable, IEnumerator<TElement>, IDisposable, IEnumerator
	{
		// Token: 0x0600026A RID: 618 RVA: 0x00009F1D File Offset: 0x0000811D
		private EmptyPartition()
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00002050 File Offset: 0x00000250
		public IEnumerator<TElement> GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00002050 File Offset: 0x00000250
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B252 File Offset: 0x00009452
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000B258 File Offset: 0x00009458
		[ExcludeFromCodeCoverage]
		public TElement Current
		{
			get
			{
				return default(TElement);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000B270 File Offset: 0x00009470
		[ExcludeFromCodeCoverage]
		object IEnumerator.Current
		{
			get
			{
				return default(TElement);
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000063F7 File Offset: 0x000045F7
		void IEnumerator.Reset()
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A01D File Offset: 0x0000821D
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00002050 File Offset: 0x00000250
		public IPartition<TElement> Skip(int count)
		{
			return this;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00002050 File Offset: 0x00000250
		public IPartition<TElement> Take(int count)
		{
			return this;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000B28C File Offset: 0x0000948C
		public TElement TryGetElementAt(int index, out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000B2A8 File Offset: 0x000094A8
		public TElement TryGetFirst(out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public TElement TryGetLast(out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000B2DD File Offset: 0x000094DD
		public TElement[] ToArray()
		{
			return Array.Empty<TElement>();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000B2E4 File Offset: 0x000094E4
		public List<TElement> ToList()
		{
			return new List<TElement>();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B252 File Offset: 0x00009452
		public int GetCount(bool onlyIfCheap)
		{
			return 0;
		}

		// Token: 0x040000CF RID: 207
		public static readonly IPartition<TElement> Instance = new EmptyPartition<TElement>();
	}
}
