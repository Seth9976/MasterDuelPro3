using System;
using System.Buffers;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000757 RID: 1879
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	[DefaultMember("Item")]
	internal ref struct ValueListBuilder<T>
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000E7903 File Offset: 0x000E5B03
		public ValueListBuilder(Span<T> initialSpan)
		{
			this._span = initialSpan;
			this._arrayFromPool = null;
			this._pos = 0;
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x000E791A File Offset: 0x000E5B1A
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x000E7924 File Offset: 0x000E5B24
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

		// Token: 0x06003BF3 RID: 15347 RVA: 0x000E7967 File Offset: 0x000E5B67
		public ReadOnlySpan<T> AsSpan()
		{
			return this._span.Slice(0, this._pos);
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x000E7980 File Offset: 0x000E5B80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this._arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(this._arrayFromPool, false);
				this._arrayFromPool = null;
			}
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000E79A4 File Offset: 0x000E5BA4
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

		// Token: 0x04001F24 RID: 7972
		private Span<T> _span;

		// Token: 0x04001F25 RID: 7973
		private T[] _arrayFromPool;

		// Token: 0x04001F26 RID: 7974
		private int _pos;
	}
}
