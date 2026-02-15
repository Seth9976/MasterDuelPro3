using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000558 RID: 1368
	internal class TempAllocator<T> : IDisposable where T : struct
	{
		// Token: 0x060025B9 RID: 9657 RVA: 0x00095C38 File Offset: 0x00093E38
		public TempAllocator(int poolCapacity, int excessMinCapacity, int excessMaxCapacity)
		{
			Debug.Assert(poolCapacity >= 1);
			Debug.Assert(excessMinCapacity >= 1);
			Debug.Assert(excessMinCapacity <= excessMaxCapacity);
			this.m_ExcessMinCapacity = excessMinCapacity;
			this.m_ExcessMaxCapacity = excessMaxCapacity;
			this.m_NextExcessSize = this.m_ExcessMinCapacity;
			this.m_Pool = default(TempAllocator<T>.Page);
			this.m_Pool.array = new NativeArray<T>(poolCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			this.m_Excess = new List<TempAllocator<T>.Page>(8);
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x00095CB9 File Offset: 0x00093EB9
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x00095CC1 File Offset: 0x00093EC1
		private protected bool disposed { protected get; private set; }

		// Token: 0x060025BC RID: 9660 RVA: 0x00095CCA File Offset: 0x00093ECA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x00095CDC File Offset: 0x00093EDC
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.Reset();
					this.m_Pool.array.Dispose();
					this.m_Pool.used = 0;
				}
				this.disposed = true;
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x00095D2C File Offset: 0x00093F2C
		public NativeSlice<T> Alloc(int count)
		{
			bool flag = count > 0;
			NativeSlice<T> nativeSlice;
			if (flag)
			{
				NativeSlice<T> slice = this.DoAlloc(count);
				nativeSlice = slice;
			}
			else
			{
				nativeSlice = default(NativeSlice<T>);
			}
			return nativeSlice;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00095D5C File Offset: 0x00093F5C
		private NativeSlice<T> DoAlloc(int count)
		{
			Debug.Assert(!this.disposed);
			int nextCount = this.m_Pool.used + count;
			bool flag = nextCount <= this.m_Pool.array.Length;
			NativeSlice<T> nativeSlice;
			if (flag)
			{
				NativeSlice<T> slice = this.m_Pool.array.Slice(this.m_Pool.used, count);
				this.m_Pool.used = nextCount;
				nativeSlice = slice;
			}
			else
			{
				bool flag2 = count > this.m_ExcessMaxCapacity;
				if (flag2)
				{
					TempAllocator<T>.Page p = new TempAllocator<T>.Page
					{
						array = new NativeArray<T>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory),
						used = count
					};
					this.m_Excess.Add(p);
					nativeSlice = p.array.Slice(0, count);
				}
				else
				{
					for (int i = this.m_Excess.Count - 1; i >= 0; i--)
					{
						TempAllocator<T>.Page p2 = this.m_Excess[i];
						nextCount = p2.used + count;
						bool flag3 = nextCount <= p2.array.Length;
						if (flag3)
						{
							NativeSlice<T> slice2 = p2.array.Slice(p2.used, count);
							p2.used = nextCount;
							this.m_Excess[i] = p2;
							return slice2;
						}
					}
					while (count > this.m_NextExcessSize)
					{
						this.m_NextExcessSize <<= 1;
					}
					TempAllocator<T>.Page p3 = new TempAllocator<T>.Page
					{
						array = new NativeArray<T>(this.m_NextExcessSize, Allocator.TempJob, NativeArrayOptions.UninitializedMemory),
						used = count
					};
					this.m_Excess.Add(p3);
					this.m_NextExcessSize = Mathf.Min(this.m_NextExcessSize << 1, this.m_ExcessMaxCapacity);
					nativeSlice = p3.array.Slice(0, count);
				}
			}
			return nativeSlice;
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x00095F37 File Offset: 0x00094137
		public void Reset()
		{
			this.ReleaseExcess();
			this.m_Pool.used = 0;
			this.m_NextExcessSize = this.m_ExcessMinCapacity;
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00095F5C File Offset: 0x0009415C
		private void ReleaseExcess()
		{
			foreach (TempAllocator<T>.Page p in this.m_Excess)
			{
				NativeArray<T> array = p.array;
				array.Dispose();
			}
			this.m_Excess.Clear();
		}

		// Token: 0x040012E9 RID: 4841
		private readonly int m_ExcessMinCapacity;

		// Token: 0x040012EA RID: 4842
		private readonly int m_ExcessMaxCapacity;

		// Token: 0x040012EB RID: 4843
		private TempAllocator<T>.Page m_Pool;

		// Token: 0x040012EC RID: 4844
		private List<TempAllocator<T>.Page> m_Excess;

		// Token: 0x040012ED RID: 4845
		private int m_NextExcessSize;

		// Token: 0x02000559 RID: 1369
		private struct Page
		{
			// Token: 0x040012EF RID: 4847
			public NativeArray<T> array;

			// Token: 0x040012F0 RID: 4848
			public int used;
		}
	}
}
