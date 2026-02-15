using System;

namespace System.Threading
{
	// Token: 0x0200023D RID: 573
	internal class SparselyPopulatedArray<T> where T : class
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x00055278 File Offset: 0x00053478
		internal SparselyPopulatedArray(int initialSize)
		{
			this._head = (this._tail = new SparselyPopulatedArrayFragment<T>(initialSize));
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000552A2 File Offset: 0x000534A2
		internal SparselyPopulatedArrayFragment<T> Tail
		{
			get
			{
				return this._tail;
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x000552AC File Offset: 0x000534AC
		internal SparselyPopulatedArrayAddInfo<T> Add(T element)
		{
			SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment2;
			int num2;
			for (;;)
			{
				SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment = this._tail;
				while (sparselyPopulatedArrayFragment._next != null)
				{
					sparselyPopulatedArrayFragment = (this._tail = sparselyPopulatedArrayFragment._next);
				}
				for (sparselyPopulatedArrayFragment2 = sparselyPopulatedArrayFragment; sparselyPopulatedArrayFragment2 != null; sparselyPopulatedArrayFragment2 = sparselyPopulatedArrayFragment2._prev)
				{
					if (sparselyPopulatedArrayFragment2._freeCount < 1)
					{
						sparselyPopulatedArrayFragment2._freeCount--;
					}
					if (sparselyPopulatedArrayFragment2._freeCount > 0 || sparselyPopulatedArrayFragment2._freeCount < -10)
					{
						int length = sparselyPopulatedArrayFragment2.Length;
						int num = (length - sparselyPopulatedArrayFragment2._freeCount) % length;
						if (num < 0)
						{
							num = 0;
							sparselyPopulatedArrayFragment2._freeCount--;
						}
						for (int i = 0; i < length; i++)
						{
							num2 = (num + i) % length;
							if (sparselyPopulatedArrayFragment2._elements[num2] == null && Interlocked.CompareExchange<T>(ref sparselyPopulatedArrayFragment2._elements[num2], element, default(T)) == null)
							{
								goto Block_5;
							}
						}
					}
				}
				SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment3 = new SparselyPopulatedArrayFragment<T>((sparselyPopulatedArrayFragment._elements.Length == 4096) ? 4096 : (sparselyPopulatedArrayFragment._elements.Length * 2), sparselyPopulatedArrayFragment);
				if (Interlocked.CompareExchange<SparselyPopulatedArrayFragment<T>>(ref sparselyPopulatedArrayFragment._next, sparselyPopulatedArrayFragment3, null) == null)
				{
					this._tail = sparselyPopulatedArrayFragment3;
				}
			}
			Block_5:
			int num3 = sparselyPopulatedArrayFragment2._freeCount - 1;
			sparselyPopulatedArrayFragment2._freeCount = ((num3 > 0) ? num3 : 0);
			return new SparselyPopulatedArrayAddInfo<T>(sparselyPopulatedArrayFragment2, num2);
		}

		// Token: 0x04000A60 RID: 2656
		private readonly SparselyPopulatedArrayFragment<T> _head;

		// Token: 0x04000A61 RID: 2657
		private volatile SparselyPopulatedArrayFragment<T> _tail;
	}
}
