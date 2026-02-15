using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200002D RID: 45
	public class IndexedCollectionPropertyBag<TList, TElement> : PropertyBag<TList>, IListPropertyBag<TList, TElement>, ICollectionPropertyBag<TList, TElement>, IPropertyBag<TList>, IPropertyBag, ICollectionPropertyBagAccept<TList>, IListPropertyBagAccept<TList>, IListPropertyAccept<TList>, IIndexedProperties<TList>, IConstructorWithCount<TList>, IConstructor, IIndexedCollectionPropertyBagEnumerator<TList> where TList : IList<TElement>
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004680 File Offset: 0x00002880
		public override PropertyCollection<TList> GetProperties()
		{
			return PropertyCollection<TList>.Empty;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004698 File Offset: 0x00002898
		public override PropertyCollection<TList> GetProperties(ref TList container)
		{
			return new PropertyCollection<TList>(new IndexedCollectionPropertyBagEnumerable<TList>(this, container));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000046BC File Offset: 0x000028BC
		public bool TryGetProperty(ref TList container, int index, out IProperty<TList> property)
		{
			bool flag = index >= container.Count;
			bool flag2;
			if (flag)
			{
				property = null;
				flag2 = false;
			}
			else
			{
				property = new IndexedCollectionPropertyBag<TList, TElement>.ListElementProperty
				{
					m_Index = index,
					m_IsReadOnly = false
				};
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004702 File Offset: 0x00002902
		void ICollectionPropertyBagAccept<TList>.Accept(ICollectionPropertyBagVisitor visitor, ref TList container)
		{
			visitor.Visit<TList, TElement>(this, ref container);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000470E File Offset: 0x0000290E
		void IListPropertyBagAccept<TList>.Accept(IListPropertyBagVisitor visitor, ref TList list)
		{
			visitor.Visit<TList, TElement>(this, ref list);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000471C File Offset: 0x0000291C
		void IListPropertyAccept<TList>.Accept<TContainer>(IListPropertyVisitor visitor, Property<TContainer, TList> property, ref TContainer container, ref TList list)
		{
			using (new AttributesScope(this.m_Property, property))
			{
				visitor.Visit<TContainer, TList, TElement>(property, ref container, ref list);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004768 File Offset: 0x00002968
		TList IConstructorWithCount<TList>.InstantiateWithCount(int count)
		{
			return this.InstantiateWithCount(count);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004784 File Offset: 0x00002984
		protected virtual TList InstantiateWithCount(int count)
		{
			return default(TList);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000047A0 File Offset: 0x000029A0
		int IIndexedCollectionPropertyBagEnumerator<TList>.GetCount(ref TList container)
		{
			return container.Count;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000047C0 File Offset: 0x000029C0
		IProperty<TList> IIndexedCollectionPropertyBagEnumerator<TList>.GetSharedProperty()
		{
			return this.m_Property;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000047D8 File Offset: 0x000029D8
		IndexedCollectionSharedPropertyState IIndexedCollectionPropertyBagEnumerator<TList>.GetSharedPropertyState()
		{
			return new IndexedCollectionSharedPropertyState
			{
				Index = this.m_Property.m_Index,
				IsReadOnly = this.m_Property.IsReadOnly
			};
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004817 File Offset: 0x00002A17
		void IIndexedCollectionPropertyBagEnumerator<TList>.SetSharedPropertyState(IndexedCollectionSharedPropertyState state)
		{
			this.m_Property.m_Index = state.Index;
			this.m_Property.m_IsReadOnly = state.IsReadOnly;
		}

		// Token: 0x0400004D RID: 77
		private readonly IndexedCollectionPropertyBag<TList, TElement>.ListElementProperty m_Property = new IndexedCollectionPropertyBag<TList, TElement>.ListElementProperty();

		// Token: 0x0200002E RID: 46
		private class ListElementProperty : Property<TList, TElement>, IListElementProperty
		{
			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004850 File Offset: 0x00002A50
			public int Index
			{
				get
				{
					return this.m_Index;
				}
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004858 File Offset: 0x00002A58
			public override string Name
			{
				get
				{
					return this.Index.ToString();
				}
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004873 File Offset: 0x00002A73
			public override bool IsReadOnly
			{
				get
				{
					return this.m_IsReadOnly;
				}
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x0000487B File Offset: 0x00002A7B
			public override TElement GetValue(ref TList container)
			{
				return container[this.m_Index];
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x0000488F File Offset: 0x00002A8F
			public override void SetValue(ref TList container, TElement value)
			{
				container[this.m_Index] = value;
			}

			// Token: 0x0400004E RID: 78
			internal int m_Index;

			// Token: 0x0400004F RID: 79
			internal bool m_IsReadOnly;
		}
	}
}
