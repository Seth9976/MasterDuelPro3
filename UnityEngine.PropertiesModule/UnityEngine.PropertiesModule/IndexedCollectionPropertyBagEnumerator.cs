using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200002A RID: 42
	internal struct IndexedCollectionPropertyBagEnumerator<TContainer> : IEnumerator<IProperty<TContainer>>, IEnumerator, IDisposable
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000045A5 File Offset: 0x000027A5
		internal IndexedCollectionPropertyBagEnumerator(IIndexedCollectionPropertyBagEnumerator<TContainer> impl, TContainer container)
		{
			this.m_Impl = impl;
			this.m_Container = container;
			this.m_Previous = impl.GetSharedPropertyState();
			this.m_Position = -1;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000045C9 File Offset: 0x000027C9
		public IProperty<TContainer> Current
		{
			get
			{
				return this.m_Impl.GetSharedProperty();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000045D6 File Offset: 0x000027D6
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000045E0 File Offset: 0x000027E0
		public bool MoveNext()
		{
			this.m_Position++;
			bool flag = this.m_Position < this.m_Impl.GetCount(ref this.m_Container);
			bool flag2;
			if (flag)
			{
				this.m_Impl.SetSharedPropertyState(new IndexedCollectionSharedPropertyState
				{
					Index = this.m_Position,
					IsReadOnly = false
				});
				flag2 = true;
			}
			else
			{
				this.m_Impl.SetSharedPropertyState(this.m_Previous);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000465F File Offset: 0x0000285F
		public void Reset()
		{
			this.m_Position = -1;
			this.m_Impl.SetSharedPropertyState(this.m_Previous);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000467B File Offset: 0x0000287B
		public void Dispose()
		{
		}

		// Token: 0x04000047 RID: 71
		private readonly IIndexedCollectionPropertyBagEnumerator<TContainer> m_Impl;

		// Token: 0x04000048 RID: 72
		private readonly IndexedCollectionSharedPropertyState m_Previous;

		// Token: 0x04000049 RID: 73
		private TContainer m_Container;

		// Token: 0x0400004A RID: 74
		private int m_Position;
	}
}
