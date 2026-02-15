using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200075A RID: 1882
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x06003BFD RID: 15357 RVA: 0x000E7B37 File Offset: 0x000E5D37
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000E7B44 File Offset: 0x000E5D44
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = Array.Empty<T>());
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x000E7B74 File Offset: 0x000E5D74
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

		// Token: 0x06003C00 RID: 15360 RVA: 0x000E7C00 File Offset: 0x000E5E00
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

		// Token: 0x06003C01 RID: 15361 RVA: 0x000E7C50 File Offset: 0x000E5E50
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

		// Token: 0x06003C02 RID: 15362 RVA: 0x000E7C8F File Offset: 0x000E5E8F
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

		// Token: 0x06003C03 RID: 15363 RVA: 0x000E7CC0 File Offset: 0x000E5EC0
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

		// Token: 0x06003C04 RID: 15364 RVA: 0x000E7CF4 File Offset: 0x000E5EF4
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x000E7D10 File Offset: 0x000E5F10
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

		// Token: 0x04001F29 RID: 7977
		private readonly int _maxCapacity;

		// Token: 0x04001F2A RID: 7978
		private T[] _first;

		// Token: 0x04001F2B RID: 7979
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x04001F2C RID: 7980
		private T[] _current;

		// Token: 0x04001F2D RID: 7981
		private int _index;

		// Token: 0x04001F2E RID: 7982
		private int _count;
	}
}
