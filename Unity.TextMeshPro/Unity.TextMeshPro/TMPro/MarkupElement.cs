using System;

namespace TMPro
{
	// Token: 0x0200009B RID: 155
	internal struct MarkupElement
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0002BA81 File Offset: 0x00029C81
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0002BA9E File Offset: 0x00029C9E
		public int NameHashCode
		{
			get
			{
				if (this.m_Attributes != null)
				{
					return this.m_Attributes[0].NameHashCode;
				}
				return 0;
			}
			set
			{
				if (this.m_Attributes == null)
				{
					this.m_Attributes = new MarkupAttribute[8];
				}
				this.m_Attributes[0].NameHashCode = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0002BAC6 File Offset: 0x00029CC6
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0002BAE3 File Offset: 0x00029CE3
		public int ValueHashCode
		{
			get
			{
				if (this.m_Attributes != null)
				{
					return this.m_Attributes[0].ValueHashCode;
				}
				return 0;
			}
			set
			{
				this.m_Attributes[0].ValueHashCode = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0002BAF7 File Offset: 0x00029CF7
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0002BB14 File Offset: 0x00029D14
		public int ValueStartIndex
		{
			get
			{
				if (this.m_Attributes != null)
				{
					return this.m_Attributes[0].ValueStartIndex;
				}
				return 0;
			}
			set
			{
				this.m_Attributes[0].ValueStartIndex = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0002BB28 File Offset: 0x00029D28
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0002BB45 File Offset: 0x00029D45
		public int ValueLength
		{
			get
			{
				if (this.m_Attributes != null)
				{
					return this.m_Attributes[0].ValueLength;
				}
				return 0;
			}
			set
			{
				this.m_Attributes[0].ValueLength = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0002BB59 File Offset: 0x00029D59
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0002BB61 File Offset: 0x00029D61
		public MarkupAttribute[] Attributes
		{
			get
			{
				return this.m_Attributes;
			}
			set
			{
				this.m_Attributes = value;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0002BB6C File Offset: 0x00029D6C
		public MarkupElement(int nameHashCode, int startIndex, int length)
		{
			this.m_Attributes = new MarkupAttribute[8];
			this.m_Attributes[0].NameHashCode = nameHashCode;
			this.m_Attributes[0].ValueStartIndex = startIndex;
			this.m_Attributes[0].ValueLength = length;
		}

		// Token: 0x04000567 RID: 1383
		private MarkupAttribute[] m_Attributes;
	}
}
