using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000232 RID: 562
	internal class MinimumQueue<T>
	{
		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002CAF0 File Offset: 0x0002ACF0
		public MinimumQueue(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.array = new T[capacity];
			this.head = (this.tail = (this.size = 0));
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0002CB37 File Offset: 0x0002AD37
		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.size;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002CB3F File Offset: 0x0002AD3F
		public T Peek()
		{
			if (this.size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			return this.array[this.head];
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002CB60 File Offset: 0x0002AD60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Enqueue(T item)
		{
			if (this.size == this.array.Length)
			{
				this.Grow();
			}
			this.array[this.tail] = item;
			this.MoveNext(ref this.tail);
			this.size++;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Dequeue()
		{
			if (this.size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			int head = this.head;
			T[] array = this.array;
			T removed = array[head];
			array[head] = default(T);
			this.MoveNext(ref this.head);
			this.size--;
			return removed;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002CC0C File Offset: 0x0002AE0C
		private void Grow()
		{
			int newcapacity = (int)((long)this.array.Length * 200L / 100L);
			if (newcapacity < this.array.Length + 4)
			{
				newcapacity = this.array.Length + 4;
			}
			this.SetCapacity(newcapacity);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002CC50 File Offset: 0x0002AE50
		private void SetCapacity(int capacity)
		{
			T[] newarray = new T[capacity];
			if (this.size > 0)
			{
				if (this.head < this.tail)
				{
					Array.Copy(this.array, this.head, newarray, 0, this.size);
				}
				else
				{
					Array.Copy(this.array, this.head, newarray, 0, this.array.Length - this.head);
					Array.Copy(this.array, 0, newarray, this.array.Length - this.head, this.tail);
				}
			}
			this.array = newarray;
			this.head = 0;
			this.tail = ((this.size == capacity) ? 0 : this.size);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002CD00 File Offset: 0x0002AF00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void MoveNext(ref int index)
		{
			int tmp = index + 1;
			if (tmp == this.array.Length)
			{
				tmp = 0;
			}
			index = tmp;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002CD22 File Offset: 0x0002AF22
		private void ThrowForEmptyQueue()
		{
			throw new InvalidOperationException("EmptyQueue");
		}

		// Token: 0x0400066D RID: 1645
		private const int MinimumGrow = 4;

		// Token: 0x0400066E RID: 1646
		private const int GrowFactor = 200;

		// Token: 0x0400066F RID: 1647
		private T[] array;

		// Token: 0x04000670 RID: 1648
		private int head;

		// Token: 0x04000671 RID: 1649
		private int tail;

		// Token: 0x04000672 RID: 1650
		private int size;
	}
}
