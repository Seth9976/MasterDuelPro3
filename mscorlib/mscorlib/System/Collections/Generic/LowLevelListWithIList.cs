using System;

namespace System.Collections.Generic
{
	// Token: 0x02000769 RID: 1897
	internal sealed class LowLevelListWithIList<T> : LowLevelList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06003C5F RID: 15455 RVA: 0x000E923A File Offset: 0x000E743A
		public LowLevelListWithIList()
		{
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000E9242 File Offset: 0x000E7442
		public LowLevelListWithIList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x000E924B File Offset: 0x000E744B
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new LowLevelListWithIList<T>.Enumerator(this);
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x000E924B File Offset: 0x000E744B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new LowLevelListWithIList<T>.Enumerator(this);
		}

		// Token: 0x0200076A RID: 1898
		private struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003C64 RID: 15460 RVA: 0x000E9258 File Offset: 0x000E7458
			internal Enumerator(LowLevelListWithIList<T> list)
			{
				this._list = list;
				this._index = 0;
				this._version = list._version;
				this._current = default(T);
			}

			// Token: 0x06003C65 RID: 15461 RVA: 0x00002C89 File Offset: 0x00000E89
			public void Dispose()
			{
			}

			// Token: 0x06003C66 RID: 15462 RVA: 0x000E9280 File Offset: 0x000E7480
			public bool MoveNext()
			{
				LowLevelListWithIList<T> list = this._list;
				if (this._version == list._version && this._index < list._size)
				{
					this._current = list._items[this._index];
					this._index++;
					return true;
				}
				return this.MoveNextRare();
			}

			// Token: 0x06003C67 RID: 15463 RVA: 0x000E92DD File Offset: 0x000E74DD
			private bool MoveNextRare()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException();
				}
				this._index = this._list._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x170009D8 RID: 2520
			// (get) Token: 0x06003C68 RID: 15464 RVA: 0x000E9318 File Offset: 0x000E7518
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170009D9 RID: 2521
			// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000E9320 File Offset: 0x000E7520
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._list._size + 1)
					{
						throw new InvalidOperationException();
					}
					return this.Current;
				}
			}

			// Token: 0x06003C6A RID: 15466 RVA: 0x000E9350 File Offset: 0x000E7550
			void IEnumerator.Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04001F4D RID: 8013
			private LowLevelListWithIList<T> _list;

			// Token: 0x04001F4E RID: 8014
			private int _index;

			// Token: 0x04001F4F RID: 8015
			private int _version;

			// Token: 0x04001F50 RID: 8016
			private T _current;
		}
	}
}
