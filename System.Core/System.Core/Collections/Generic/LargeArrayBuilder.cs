using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000154 RID: 340
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x0002C202 File Offset: 0x0002A402
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002C210 File Offset: 0x0002A410
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = Array.Empty<T>());
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0002C23F File Offset: 0x0002A43F
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002C248 File Offset: 0x0002A448
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(T item)
		{
			int index = this._index;
			T[] current = this._current;
			if (index >= current.Length)
			{
				this.AddWithBufferAllocation(item);
			}
			else
			{
				current[index] = item;
				this._index = index + 1;
			}
			this._count++;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002C294 File Offset: 0x0002A494
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item)
		{
			this.AllocateBuffer();
			T[] current = this._current;
			int index = this._index;
			this._index = index + 1;
			current[index] = item;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
		public void AddRange(IEnumerable<T> items)
		{
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				T[] current = this._current;
				int num = this._index;
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					if (num >= current.Length)
					{
						this.AddWithBufferAllocation(t, ref current, ref num);
					}
					else
					{
						current[num] = t;
					}
					num++;
				}
				this._count += num - this._index;
				this._index = num;
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002C350 File Offset: 0x0002A550
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item, ref T[] destination, ref int index)
		{
			this._count += index - this._index;
			this._index = index;
			this.AllocateBuffer();
			destination = this._current;
			index = this._index;
			this._current[index] = item;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002C3A0 File Offset: 0x0002A5A0
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			while (count > 0)
			{
				T[] buffer = this.GetBuffer(num);
				int num2 = Math.Min(count, buffer.Length);
				Array.Copy(buffer, 0, array, arrayIndex, num2);
				count -= num2;
				arrayIndex += num2;
				num++;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002C3E0 File Offset: 0x0002A5E0
		public CopyPosition CopyTo(CopyPosition position, T[] array, int arrayIndex, int count)
		{
			LargeArrayBuilder<T>.<>c__DisplayClass17_0 CS$<>8__locals1;
			CS$<>8__locals1.count = count;
			CS$<>8__locals1.array = array;
			CS$<>8__locals1.arrayIndex = arrayIndex;
			int num = position.Row;
			int column = position.Column;
			T[] array2 = this.GetBuffer(num);
			int num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|17_0(array2, column, ref CS$<>8__locals1);
			if (CS$<>8__locals1.count == 0)
			{
				return new CopyPosition(num, column + num2).Normalize(array2.Length);
			}
			do
			{
				array2 = this.GetBuffer(++num);
				num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|17_0(array2, 0, ref CS$<>8__locals1);
			}
			while (CS$<>8__locals1.count > 0);
			return new CopyPosition(num, num2).Normalize(array2.Length);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002C47C File Offset: 0x0002A67C
		public T[] GetBuffer(int index)
		{
			if (index == 0)
			{
				return this._first;
			}
			if (index > this._buffers.Count)
			{
				return this._current;
			}
			return this._buffers[index - 1];
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		public T[] ToArray()
		{
			T[] array;
			if (this.TryMove(out array))
			{
				return array;
			}
			array = new T[this._count];
			this.CopyTo(array, 0, this._count);
			return array;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002C4FC File Offset: 0x0002A6FC
		private void AllocateBuffer()
		{
			if (this._count < 8)
			{
				int num = Math.Min((this._count == 0) ? 4 : (this._count * 2), this._maxCapacity);
				this._current = new T[num];
				Array.Copy(this._first, 0, this._current, 0, this._count);
				this._first = this._current;
				return;
			}
			int num2;
			if (this._count == 8)
			{
				num2 = 8;
			}
			else
			{
				this._buffers.Add(this._current);
				num2 = Math.Min(this._count, this._maxCapacity - this._count);
			}
			this._current = new T[num2];
			this._index = 0;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
		[CompilerGenerated]
		internal static int <CopyTo>g__CopyToCore|17_0(T[] sourceBuffer, int sourceIndex, ref LargeArrayBuilder<T>.<>c__DisplayClass17_0 A_2)
		{
			int num = Math.Min(sourceBuffer.Length - sourceIndex, A_2.count);
			Array.Copy(sourceBuffer, sourceIndex, A_2.array, A_2.arrayIndex, num);
			A_2.arrayIndex += num;
			A_2.count -= num;
			return num;
		}

		// Token: 0x0400035D RID: 861
		private readonly int _maxCapacity;

		// Token: 0x0400035E RID: 862
		private T[] _first;

		// Token: 0x0400035F RID: 863
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x04000360 RID: 864
		private T[] _current;

		// Token: 0x04000361 RID: 865
		private int _index;

		// Token: 0x04000362 RID: 866
		private int _count;
	}
}
