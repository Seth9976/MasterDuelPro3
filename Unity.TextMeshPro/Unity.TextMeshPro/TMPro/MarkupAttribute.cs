using System;

namespace TMPro
{
	// Token: 0x0200009A RID: 154
	internal struct MarkupAttribute
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0002BA3D File Offset: 0x00029C3D
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0002BA45 File Offset: 0x00029C45
		public int NameHashCode
		{
			get
			{
				return this.m_NameHashCode;
			}
			set
			{
				this.m_NameHashCode = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0002BA4E File Offset: 0x00029C4E
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0002BA56 File Offset: 0x00029C56
		public int ValueHashCode
		{
			get
			{
				return this.m_ValueHashCode;
			}
			set
			{
				this.m_ValueHashCode = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0002BA5F File Offset: 0x00029C5F
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0002BA67 File Offset: 0x00029C67
		public int ValueStartIndex
		{
			get
			{
				return this.m_ValueStartIndex;
			}
			set
			{
				this.m_ValueStartIndex = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0002BA70 File Offset: 0x00029C70
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0002BA78 File Offset: 0x00029C78
		public int ValueLength
		{
			get
			{
				return this.m_ValueLength;
			}
			set
			{
				this.m_ValueLength = value;
			}
		}

		// Token: 0x04000563 RID: 1379
		private int m_NameHashCode;

		// Token: 0x04000564 RID: 1380
		private int m_ValueHashCode;

		// Token: 0x04000565 RID: 1381
		private int m_ValueStartIndex;

		// Token: 0x04000566 RID: 1382
		private int m_ValueLength;
	}
}
