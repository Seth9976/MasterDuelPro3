using System;

namespace System.Threading
{
	// Token: 0x0200023F RID: 575
	internal class SparselyPopulatedArrayFragment<T> where T : class
	{
		// Token: 0x0600152E RID: 5422 RVA: 0x0005543A File Offset: 0x0005363A
		internal SparselyPopulatedArrayFragment(int size)
			: this(size, null)
		{
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00055444 File Offset: 0x00053644
		internal SparselyPopulatedArrayFragment(int size, SparselyPopulatedArrayFragment<T> prev)
		{
			this._elements = new T[size];
			this._freeCount = size;
			this._prev = prev;
		}

		// Token: 0x17000239 RID: 569
		internal T this[int index]
		{
			get
			{
				return Volatile.Read<T>(ref this._elements[index]);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0005547D File Offset: 0x0005367D
		internal int Length
		{
			get
			{
				return this._elements.Length;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00055487 File Offset: 0x00053687
		internal SparselyPopulatedArrayFragment<T> Prev
		{
			get
			{
				return this._prev;
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00055494 File Offset: 0x00053694
		internal T SafeAtomicRemove(int index, T expectedElement)
		{
			T t = Interlocked.CompareExchange<T>(ref this._elements[index], default(T), expectedElement);
			if (t != null)
			{
				this._freeCount++;
			}
			return t;
		}

		// Token: 0x04000A64 RID: 2660
		internal readonly T[] _elements;

		// Token: 0x04000A65 RID: 2661
		internal volatile int _freeCount;

		// Token: 0x04000A66 RID: 2662
		internal volatile SparselyPopulatedArrayFragment<T> _next;

		// Token: 0x04000A67 RID: 2663
		internal volatile SparselyPopulatedArrayFragment<T> _prev;
	}
}
