using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004C2 RID: 1218
	internal class ObjectHolderListEnumerator
	{
		// Token: 0x060026E6 RID: 9958 RVA: 0x0009D122 File Offset: 0x0009B322
		internal ObjectHolderListEnumerator(ObjectHolderList list, bool isFixupEnumerator)
		{
			this.m_list = list;
			this.m_startingVersion = this.m_list.Version;
			this.m_currPos = -1;
			this.m_isFixupEnumerator = isFixupEnumerator;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x0009D150 File Offset: 0x0009B350
		internal bool MoveNext()
		{
			if (this.m_isFixupEnumerator)
			{
				int num;
				do
				{
					num = this.m_currPos + 1;
					this.m_currPos = num;
				}
				while (num < this.m_list.Count && this.m_list.m_values[this.m_currPos].CompletelyFixed);
				return this.m_currPos != this.m_list.Count;
			}
			this.m_currPos++;
			return this.m_currPos != this.m_list.Count;
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x0009D1D7 File Offset: 0x0009B3D7
		internal ObjectHolder Current
		{
			get
			{
				return this.m_list.m_values[this.m_currPos];
			}
		}

		// Token: 0x04001289 RID: 4745
		private bool m_isFixupEnumerator;

		// Token: 0x0400128A RID: 4746
		private ObjectHolderList m_list;

		// Token: 0x0400128B RID: 4747
		private int m_startingVersion;

		// Token: 0x0400128C RID: 4748
		private int m_currPos;
	}
}
