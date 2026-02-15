using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000239 RID: 569
	public struct StylePropertyNameCollection : IEnumerable<StylePropertyName>, IEnumerable
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x000431A2 File Offset: 0x000413A2
		internal StylePropertyNameCollection(List<StylePropertyName> list)
		{
			this.propertiesList = list;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000431AC File Offset: 0x000413AC
		public StylePropertyNameCollection.Enumerator GetEnumerator()
		{
			return new StylePropertyNameCollection.Enumerator(this.propertiesList.GetEnumerator());
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000431D0 File Offset: 0x000413D0
		IEnumerator<StylePropertyName> IEnumerable<StylePropertyName>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x000431F0 File Offset: 0x000413F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040008C5 RID: 2245
		internal List<StylePropertyName> propertiesList;

		// Token: 0x0200023A RID: 570
		public struct Enumerator : IEnumerator<StylePropertyName>, IEnumerator, IDisposable
		{
			// Token: 0x06000F72 RID: 3954 RVA: 0x0004320D File Offset: 0x0004140D
			internal Enumerator(List<StylePropertyName>.Enumerator enumerator)
			{
				this.m_Enumerator = enumerator;
			}

			// Token: 0x06000F73 RID: 3955 RVA: 0x00043217 File Offset: 0x00041417
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x170002DA RID: 730
			// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00043224 File Offset: 0x00041424
			public StylePropertyName Current
			{
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00043231 File Offset: 0x00041431
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000F76 RID: 3958 RVA: 0x000020EA File Offset: 0x000002EA
			public void Reset()
			{
			}

			// Token: 0x06000F77 RID: 3959 RVA: 0x0004323E File Offset: 0x0004143E
			public void Dispose()
			{
				this.m_Enumerator.Dispose();
			}

			// Token: 0x040008C6 RID: 2246
			private List<StylePropertyName>.Enumerator m_Enumerator;
		}
	}
}
