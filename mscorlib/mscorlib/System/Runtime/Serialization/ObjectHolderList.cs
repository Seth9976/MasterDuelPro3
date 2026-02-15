using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004C1 RID: 1217
	internal class ObjectHolderList
	{
		// Token: 0x060026DF RID: 9951 RVA: 0x0009D052 File Offset: 0x0009B252
		internal ObjectHolderList()
			: this(8)
		{
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0009D05B File Offset: 0x0009B25B
		internal ObjectHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new ObjectHolder[startingSize];
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x0009D078 File Offset: 0x0009B278
		internal virtual void Add(ObjectHolder value)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			ObjectHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = value;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0009D0B4 File Offset: 0x0009B2B4
		internal ObjectHolderListEnumerator GetFixupEnumerator()
		{
			return new ObjectHolderListEnumerator(this, true);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0009D0C0 File Offset: 0x0009B2C0
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
			ObjectHolder[] array = new ObjectHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x0009D11A File Offset: 0x0009B31A
		internal int Version
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x0009D11A File Offset: 0x0009B31A
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x04001287 RID: 4743
		internal ObjectHolder[] m_values;

		// Token: 0x04001288 RID: 4744
		internal int m_count;
	}
}
