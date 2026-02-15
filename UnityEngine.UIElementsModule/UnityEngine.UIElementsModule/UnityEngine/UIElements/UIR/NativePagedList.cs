using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200053E RID: 1342
	internal class NativePagedList<T> : IDisposable where T : struct
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x000906B0 File Offset: 0x0008E8B0
		public NativePagedList(int poolCapacity, Allocator firstPageAllocator = Allocator.Persistent, Allocator otherPagesAllocator = Allocator.Persistent)
		{
			Debug.Assert(poolCapacity > 0);
			this.k_PoolCapacity = Mathf.NextPowerOfTwo(poolCapacity);
			this.m_FirstPageAllocator = firstPageAllocator;
			this.m_OtherPagesAllocator = otherPagesAllocator;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00090704 File Offset: 0x0008E904
		public void Add(ref T data)
		{
			bool flag = this.m_CountInLastPage < this.m_LastPage.Length;
			if (flag)
			{
				int countInLastPage = this.m_CountInLastPage;
				this.m_CountInLastPage = countInLastPage + 1;
				this.m_LastPage[countInLastPage] = data;
			}
			else
			{
				int newPageSize = ((this.m_Pages.Count > 0) ? (this.m_LastPage.Length << 1) : this.k_PoolCapacity);
				Allocator allocator = ((this.m_Pages.Count == 0) ? this.m_FirstPageAllocator : this.m_OtherPagesAllocator);
				this.m_LastPage = new NativeArray<T>(newPageSize, allocator, NativeArrayOptions.UninitializedMemory);
				this.m_Pages.Add(this.m_LastPage);
				this.m_LastPage[0] = data;
				this.m_CountInLastPage = 1;
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000907C9 File Offset: 0x0008E9C9
		public void Add(T data)
		{
			this.Add(ref data);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000907D8 File Offset: 0x0008E9D8
		public List<NativeSlice<T>> GetPages()
		{
			this.m_Enumerator.Clear();
			bool flag = this.m_Pages.Count > 0;
			if (flag)
			{
				int last = this.m_Pages.Count - 1;
				for (int i = 0; i < last; i++)
				{
					this.m_Enumerator.Add(this.m_Pages[i]);
				}
				bool flag2 = this.m_CountInLastPage > 0;
				if (flag2)
				{
					this.m_Enumerator.Add(this.m_LastPage.Slice(0, this.m_CountInLastPage));
				}
			}
			return this.m_Enumerator;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0009087C File Offset: 0x0008EA7C
		public int GetCount()
		{
			int count = this.m_CountInLastPage;
			for (int i = 0; i < this.m_Pages.Count - 1; i++)
			{
				count += this.m_Pages[i].Length;
			}
			return count;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000908CC File Offset: 0x0008EACC
		public void Reset()
		{
			bool flag = this.m_Pages.Count > 1;
			if (flag)
			{
				this.m_LastPage = this.m_Pages[0];
				for (int i = 1; i < this.m_Pages.Count; i++)
				{
					this.m_Pages[i].Dispose();
				}
				this.m_Pages.Clear();
				this.m_Pages.Add(this.m_LastPage);
			}
			this.m_CountInLastPage = 0;
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x00090954 File Offset: 0x0008EB54
		// (set) Token: 0x06002511 RID: 9489 RVA: 0x0009095C File Offset: 0x0008EB5C
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002512 RID: 9490 RVA: 0x00090965 File Offset: 0x0008EB65
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x00090978 File Offset: 0x0008EB78
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.m_Pages.Count; i++)
					{
						this.m_Pages[i].Dispose();
					}
					this.m_Pages.Clear();
					this.m_CountInLastPage = 0;
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001219 RID: 4633
		private readonly int k_PoolCapacity;

		// Token: 0x0400121A RID: 4634
		private List<NativeArray<T>> m_Pages = new List<NativeArray<T>>(8);

		// Token: 0x0400121B RID: 4635
		private NativeArray<T> m_LastPage;

		// Token: 0x0400121C RID: 4636
		private int m_CountInLastPage;

		// Token: 0x0400121D RID: 4637
		private Allocator m_FirstPageAllocator;

		// Token: 0x0400121E RID: 4638
		private Allocator m_OtherPagesAllocator;

		// Token: 0x0400121F RID: 4639
		private List<NativeSlice<T>> m_Enumerator = new List<NativeSlice<T>>(8);

		// Token: 0x0200053F RID: 1343
		public struct Enumerator
		{
			// Token: 0x06002514 RID: 9492 RVA: 0x000909E8 File Offset: 0x0008EBE8
			public Enumerator(NativePagedList<T> nativePagedList, int offset)
			{
				this.m_IndexInCurrentPage = 0;
				this.m_IndexOfCurrentPage = 0;
				this.m_CountInCurrentPage = 0;
				this.m_NativePagedList = nativePagedList;
				for (int i = 0; i < this.m_NativePagedList.m_Pages.Count - 1; i++)
				{
					this.m_CountInCurrentPage = this.m_NativePagedList.m_Pages[i].Length;
					bool flag = offset >= this.m_CountInCurrentPage;
					if (!flag)
					{
						this.m_IndexInCurrentPage = offset;
						this.m_IndexOfCurrentPage = i;
						this.m_CurrentPage = this.m_NativePagedList.m_Pages[this.m_IndexOfCurrentPage];
						return;
					}
					offset -= this.m_CountInCurrentPage;
				}
				this.m_IndexOfCurrentPage = this.m_NativePagedList.m_Pages.Count - 1;
				this.m_CountInCurrentPage = this.m_NativePagedList.m_CountInLastPage;
				this.m_IndexInCurrentPage = offset;
				this.m_CurrentPage = this.m_NativePagedList.m_LastPage;
			}

			// Token: 0x06002515 RID: 9493 RVA: 0x00090AE4 File Offset: 0x0008ECE4
			public bool HasNext()
			{
				return this.m_IndexInCurrentPage < this.m_CountInCurrentPage;
			}

			// Token: 0x06002516 RID: 9494 RVA: 0x00090B04 File Offset: 0x0008ED04
			public T GetNext()
			{
				bool flag = !this.HasNext();
				if (flag)
				{
					throw new InvalidOperationException("No more elements");
				}
				T result = this.m_CurrentPage[this.m_IndexInCurrentPage];
				this.m_IndexInCurrentPage++;
				bool flag2 = this.m_IndexInCurrentPage == this.m_CountInCurrentPage;
				if (flag2)
				{
					this.m_IndexInCurrentPage = 0;
					this.m_IndexOfCurrentPage++;
					int pageCount = this.m_NativePagedList.m_Pages.Count;
					bool flag3 = this.m_IndexOfCurrentPage < pageCount;
					if (flag3)
					{
						bool flag4 = this.m_IndexOfCurrentPage < pageCount - 1;
						if (flag4)
						{
							this.m_CountInCurrentPage = this.m_NativePagedList.m_Pages[this.m_IndexOfCurrentPage].Length;
						}
						else
						{
							this.m_CountInCurrentPage = this.m_NativePagedList.m_CountInLastPage;
						}
					}
					else
					{
						this.m_IndexOfCurrentPage = pageCount - 1;
						this.m_CountInCurrentPage = this.m_NativePagedList.m_CountInLastPage;
						this.m_IndexInCurrentPage = this.m_CountInCurrentPage;
					}
					this.m_CurrentPage = this.m_NativePagedList.m_Pages[this.m_IndexOfCurrentPage];
				}
				return result;
			}

			// Token: 0x04001221 RID: 4641
			private NativePagedList<T> m_NativePagedList;

			// Token: 0x04001222 RID: 4642
			private NativeArray<T> m_CurrentPage;

			// Token: 0x04001223 RID: 4643
			private int m_IndexInCurrentPage;

			// Token: 0x04001224 RID: 4644
			private int m_IndexOfCurrentPage;

			// Token: 0x04001225 RID: 4645
			private int m_CountInCurrentPage;
		}
	}
}
