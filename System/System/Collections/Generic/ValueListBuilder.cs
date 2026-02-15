using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000313 RID: 787
	internal ref struct ValueListBuilder<T>
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x0005569D File Offset: 0x0005389D
		public ValueListBuilder(Span<T> initialSpan)
		{
			this._span = initialSpan;
			this._arrayFromPool = null;
			this._pos = 0;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000556B4 File Offset: 0x000538B4
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x1700041E RID: 1054
		public ref T this[int index]
		{
			get
			{
				return this._span[index];
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000556CC File Offset: 0x000538CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(T item)
		{
			int pos = this._pos;
			if (pos >= this._span.Length)
			{
				this.Grow();
			}
			*this._span[pos] = item;
			this._pos = pos + 1;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0005570F File Offset: 0x0005390F
		public ReadOnlySpan<T> AsSpan()
		{
			return this._span.Slice(0, this._pos);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00055728 File Offset: 0x00053928
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this._arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(this._arrayFromPool, false);
				this._arrayFromPool = null;
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0005574C File Offset: 0x0005394C
		private void Grow()
		{
			T[] array = ArrayPool<T>.Shared.Rent(this._span.Length * 2);
			this._span.TryCopyTo(array);
			T[] arrayFromPool = this._arrayFromPool;
			this._span = (this._arrayFromPool = array);
			if (arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(arrayFromPool, false);
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x000557AE File Offset: 0x000539AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe T Pop()
		{
			this._pos--;
			return *this._span[this._pos];
		}

		// Token: 0x04000B9F RID: 2975
		private Span<T> _span;

		// Token: 0x04000BA0 RID: 2976
		private T[] _arrayFromPool;

		// Token: 0x04000BA1 RID: 2977
		private int _pos;
	}
}
