using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004C0 RID: 1216
	[Serializable]
	internal class LongList
	{
		// Token: 0x060026D6 RID: 9942 RVA: 0x0009CED6 File Offset: 0x0009B0D6
		internal LongList()
			: this(2)
		{
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0009CEDF File Offset: 0x0009B0DF
		internal LongList(int startingSize)
		{
			this.m_count = 0;
			this.m_totalItems = 0;
			this.m_values = new long[startingSize];
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0009CF04 File Offset: 0x0009B104
		internal void Add(long value)
		{
			if (this.m_totalItems == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			long[] values = this.m_values;
			int totalItems = this.m_totalItems;
			this.m_totalItems = totalItems + 1;
			values[totalItems] = value;
			this.m_count++;
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x0009CF4E File Offset: 0x0009B14E
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0009CF56 File Offset: 0x0009B156
		internal void StartEnumeration()
		{
			this.m_currentItem = -1;
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x0009CF60 File Offset: 0x0009B160
		internal bool MoveNext()
		{
			int num;
			do
			{
				num = this.m_currentItem + 1;
				this.m_currentItem = num;
			}
			while (num < this.m_totalItems && this.m_values[this.m_currentItem] == -1L);
			return this.m_currentItem != this.m_totalItems;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x0009CFA8 File Offset: 0x0009B1A8
		internal long Current
		{
			get
			{
				return this.m_values[this.m_currentItem];
			}
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0009CFB8 File Offset: 0x0009B1B8
		internal bool RemoveElement(long value)
		{
			int num = 0;
			while (num < this.m_totalItems && this.m_values[num] != value)
			{
				num++;
			}
			if (num == this.m_totalItems)
			{
				return false;
			}
			this.m_values[num] = -1L;
			return true;
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0009CFF8 File Offset: 0x0009B1F8
		private void EnlargeArray()
		{
			int num = this.m_values.Length * 2;
			if (num < 0)
			{
				if (num == 2147483647)
				{
					throw new SerializationException(Environment.GetResourceString("The internal array cannot expand to greater than Int32.MaxValue elements."));
				}
				num = int.MaxValue;
			}
			long[] array = new long[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x04001283 RID: 4739
		private long[] m_values;

		// Token: 0x04001284 RID: 4740
		private int m_count;

		// Token: 0x04001285 RID: 4741
		private int m_totalItems;

		// Token: 0x04001286 RID: 4742
		private int m_currentItem;
	}
}
