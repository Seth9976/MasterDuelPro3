using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x0200003F RID: 63
	[DebuggerDisplay("Key = {Key}")]
	[DebuggerTypeProxy(typeof(SystemLinq_GroupingDebugView<, >))]
	internal class Grouping<TKey, TElement> : IGrouping<TKey, TElement>, IEnumerable<TElement>, IEnumerable, IList<TElement>, ICollection<TElement>
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00009F1D File Offset: 0x0000811D
		internal Grouping()
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009F28 File Offset: 0x00008128
		internal void Add(TElement element)
		{
			if (this._elements.Length == this._count)
			{
				Array.Resize<TElement>(ref this._elements, checked(this._count * 2));
			}
			this._elements[this._count] = element;
			this._count++;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009F78 File Offset: 0x00008178
		public IEnumerator<TElement> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this._count; i = num + 1)
			{
				yield return this._elements[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009F87 File Offset: 0x00008187
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009F8F File Offset: 0x0000818F
		public TKey Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009F97 File Offset: 0x00008197
		int ICollection<TElement>.Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009F9F File Offset: 0x0000819F
		bool ICollection<TElement>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000063F7 File Offset: 0x000045F7
		void ICollection<TElement>.Add(TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000063F7 File Offset: 0x000045F7
		void ICollection<TElement>.Clear()
		{
			throw Error.NotSupported();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009FA2 File Offset: 0x000081A2
		bool ICollection<TElement>.Contains(TElement item)
		{
			return Array.IndexOf<TElement>(this._elements, item, 0, this._count) >= 0;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009FBD File Offset: 0x000081BD
		void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex)
		{
			Array.Copy(this._elements, 0, array, arrayIndex, this._count);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000063F7 File Offset: 0x000045F7
		bool ICollection<TElement>.Remove(TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009FD3 File Offset: 0x000081D3
		int IList<TElement>.IndexOf(TElement item)
		{
			return Array.IndexOf<TElement>(this._elements, item, 0, this._count);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000063F7 File Offset: 0x000045F7
		void IList<TElement>.Insert(int index, TElement item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000063F7 File Offset: 0x000045F7
		void IList<TElement>.RemoveAt(int index)
		{
			throw Error.NotSupported();
		}

		// Token: 0x17000026 RID: 38
		TElement IList<TElement>.this[int index]
		{
			get
			{
				if (index < 0 || index >= this._count)
				{
					throw Error.ArgumentOutOfRange("index");
				}
				return this._elements[index];
			}
			set
			{
				throw Error.NotSupported();
			}
		}

		// Token: 0x0400009E RID: 158
		internal TKey _key;

		// Token: 0x0400009F RID: 159
		internal int _hashCode;

		// Token: 0x040000A0 RID: 160
		internal TElement[] _elements;

		// Token: 0x040000A1 RID: 161
		internal int _count;

		// Token: 0x040000A2 RID: 162
		internal Grouping<TKey, TElement> _hashNext;

		// Token: 0x040000A3 RID: 163
		internal Grouping<TKey, TElement> _next;
	}
}
