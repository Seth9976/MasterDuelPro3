using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200004E RID: 78
	internal struct TextBackingContainer
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00021468 File Offset: 0x0001F668
		public int Capacity
		{
			get
			{
				return this.m_Array.Length;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00021484 File Offset: 0x0001F684
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0002149C File Offset: 0x0001F69C
		public int Count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = value;
			}
		}

		// Token: 0x17000060 RID: 96
		public uint this[int index]
		{
			get
			{
				return this.m_Array[index];
			}
			set
			{
				bool flag = index >= this.m_Array.Length;
				if (flag)
				{
					this.Resize(index);
				}
				this.m_Array[index] = value;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000214F5 File Offset: 0x0001F6F5
		public TextBackingContainer(int size)
		{
			this.m_Array = new uint[size];
			this.m_Count = 0;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0002150B File Offset: 0x0001F70B
		public void Resize(int size)
		{
			size = Mathf.NextPowerOfTwo(size + 1);
			Array.Resize<uint>(ref this.m_Array, size);
		}

		// Token: 0x04000300 RID: 768
		private uint[] m_Array;

		// Token: 0x04000301 RID: 769
		private int m_Count;
	}
}
