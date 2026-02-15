using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200022A RID: 554
	internal sealed class ArrayPool<T>
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x0002BA94 File Offset: 0x00029C94
		private ArrayPool()
		{
			this.buckets = new MinimumQueue<T[]>[18];
			this.locks = new SpinLock[18];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i] = new MinimumQueue<T[]>(4);
				this.locks[i] = new SpinLock(false);
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002BAF4 File Offset: 0x00029CF4
		public T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException("minimumLength");
			}
			if (minimumLength == 0)
			{
				return ArrayPool<T>.EmptyArray;
			}
			int size = ArrayPool<T>.CalculateSize(minimumLength);
			int index = ArrayPool<T>.GetQueueIndex(size);
			if (index != -1)
			{
				MinimumQueue<T[]> q = this.buckets[index];
				bool lockTaken = false;
				try
				{
					this.locks[index].Enter(ref lockTaken);
					if (q.Count != 0)
					{
						return q.Dequeue();
					}
				}
				finally
				{
					if (lockTaken)
					{
						this.locks[index].Exit(false);
					}
				}
			}
			return new T[size];
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002BB8C File Offset: 0x00029D8C
		public void Return(T[] array, bool clearArray = false)
		{
			if (array == null || array.Length == 0)
			{
				return;
			}
			int index = ArrayPool<T>.GetQueueIndex(array.Length);
			if (index != -1)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				MinimumQueue<T[]> q = this.buckets[index];
				bool lockTaken = false;
				try
				{
					this.locks[index].Enter(ref lockTaken);
					if (q.Count <= 50)
					{
						q.Enqueue(array);
					}
				}
				finally
				{
					if (lockTaken)
					{
						this.locks[index].Exit(false);
					}
				}
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002BC14 File Offset: 0x00029E14
		private static int CalculateSize(int size)
		{
			size--;
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			size++;
			if (size < 8)
			{
				size = 8;
			}
			return size;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002BC4C File Offset: 0x00029E4C
		private static int GetQueueIndex(int size)
		{
			if (size <= 2048)
			{
				if (size <= 64)
				{
					if (size <= 16)
					{
						if (size == 8)
						{
							return 0;
						}
						if (size == 16)
						{
							return 1;
						}
					}
					else
					{
						if (size == 32)
						{
							return 2;
						}
						if (size == 64)
						{
							return 3;
						}
					}
				}
				else if (size <= 256)
				{
					if (size == 128)
					{
						return 4;
					}
					if (size == 256)
					{
						return 5;
					}
				}
				else
				{
					if (size == 512)
					{
						return 6;
					}
					if (size == 1024)
					{
						return 7;
					}
					if (size == 2048)
					{
						return 8;
					}
				}
			}
			else if (size <= 32768)
			{
				if (size <= 8192)
				{
					if (size == 4096)
					{
						return 9;
					}
					if (size == 8192)
					{
						return 10;
					}
				}
				else
				{
					if (size == 16384)
					{
						return 11;
					}
					if (size == 32768)
					{
						return 12;
					}
				}
			}
			else if (size <= 131072)
			{
				if (size == 65536)
				{
					return 13;
				}
				if (size == 131072)
				{
					return 14;
				}
			}
			else
			{
				if (size == 262144)
				{
					return 15;
				}
				if (size == 524288)
				{
					return 16;
				}
				if (size == 1048576)
				{
					return 17;
				}
			}
			return -1;
		}

		// Token: 0x04000655 RID: 1621
		private const int DefaultMaxNumberOfArraysPerBucket = 50;

		// Token: 0x04000656 RID: 1622
		private static readonly T[] EmptyArray = new T[0];

		// Token: 0x04000657 RID: 1623
		public static readonly ArrayPool<T> Shared = new ArrayPool<T>();

		// Token: 0x04000658 RID: 1624
		private readonly MinimumQueue<T[]>[] buckets;

		// Token: 0x04000659 RID: 1625
		private readonly SpinLock[] locks;
	}
}
