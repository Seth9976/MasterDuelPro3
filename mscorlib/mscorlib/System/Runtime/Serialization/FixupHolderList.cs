using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004BF RID: 1215
	[Serializable]
	internal class FixupHolderList
	{
		// Token: 0x060026D2 RID: 9938 RVA: 0x0009CE1B File Offset: 0x0009B01B
		internal FixupHolderList()
			: this(2)
		{
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0009CE24 File Offset: 0x0009B024
		internal FixupHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new FixupHolder[startingSize];
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0009CE40 File Offset: 0x0009B040
		internal virtual void Add(FixupHolder fixup)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = fixup;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0009CE7C File Offset: 0x0009B07C
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
			FixupHolder[] array = new FixupHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x04001281 RID: 4737
		internal FixupHolder[] m_values;

		// Token: 0x04001282 RID: 4738
		internal int m_count;
	}
}
