using System;

namespace Unity.Properties
{
	// Token: 0x02000029 RID: 41
	internal readonly struct IndexedCollectionPropertyBagEnumerable<TContainer>
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00004581 File Offset: 0x00002781
		public IndexedCollectionPropertyBagEnumerable(IIndexedCollectionPropertyBagEnumerator<TContainer> impl, TContainer container)
		{
			this.m_Impl = impl;
			this.m_Container = container;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004592 File Offset: 0x00002792
		public IndexedCollectionPropertyBagEnumerator<TContainer> GetEnumerator()
		{
			return new IndexedCollectionPropertyBagEnumerator<TContainer>(this.m_Impl, this.m_Container);
		}

		// Token: 0x04000045 RID: 69
		private readonly IIndexedCollectionPropertyBagEnumerator<TContainer> m_Impl;

		// Token: 0x04000046 RID: 70
		private readonly TContainer m_Container;
	}
}
