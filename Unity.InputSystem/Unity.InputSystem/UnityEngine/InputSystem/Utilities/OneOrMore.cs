using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000259 RID: 601
	internal struct OneOrMore<TValue, TList> : IReadOnlyList<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue> where TList : IReadOnlyList<TValue>
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0006294C File Offset: 0x00060B4C
		public int Count
		{
			get
			{
				if (!this.m_IsSingle)
				{
					TList multiple = this.m_Multiple;
					return multiple.Count;
				}
				return 1;
			}
		}

		// Token: 0x170005F0 RID: 1520
		public TValue this[int index]
		{
			get
			{
				if (!this.m_IsSingle)
				{
					TList multiple = this.m_Multiple;
					return multiple[index];
				}
				if (index < 0 || index > 1)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_Single;
			}
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000629BC File Offset: 0x00060BBC
		public OneOrMore(TValue single)
		{
			this.m_IsSingle = true;
			this.m_Single = single;
			this.m_Multiple = default(TList);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000629D8 File Offset: 0x00060BD8
		public OneOrMore(TList multiple)
		{
			this.m_IsSingle = false;
			this.m_Single = default(TValue);
			this.m_Multiple = multiple;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x000629F4 File Offset: 0x00060BF4
		public static implicit operator OneOrMore<TValue, TList>(TValue single)
		{
			return new OneOrMore<TValue, TList>(single);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000629FC File Offset: 0x00060BFC
		public static implicit operator OneOrMore<TValue, TList>(TList multiple)
		{
			return new OneOrMore<TValue, TList>(multiple);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00062A04 File Offset: 0x00060C04
		public IEnumerator<TValue> GetEnumerator()
		{
			return new OneOrMore<TValue, TList>.Enumerator
			{
				m_List = this
			};
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00062A17 File Offset: 0x00060C17
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000C92 RID: 3218
		private readonly bool m_IsSingle;

		// Token: 0x04000C93 RID: 3219
		private readonly TValue m_Single;

		// Token: 0x04000C94 RID: 3220
		private readonly TList m_Multiple;

		// Token: 0x0200025A RID: 602
		private class Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x060015BB RID: 5563 RVA: 0x00062A1F File Offset: 0x00060C1F
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Count;
			}

			// Token: 0x060015BC RID: 5564 RVA: 0x00062A45 File Offset: 0x00060C45
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x060015BD RID: 5565 RVA: 0x00062A4E File Offset: 0x00060C4E
			public TValue Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x060015BE RID: 5566 RVA: 0x00062A61 File Offset: 0x00060C61
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060015BF RID: 5567 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x04000C95 RID: 3221
			internal int m_Index = -1;

			// Token: 0x04000C96 RID: 3222
			internal OneOrMore<TValue, TList> m_List;
		}
	}
}
