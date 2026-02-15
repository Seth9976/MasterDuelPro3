using System;

namespace TMPro
{
	// Token: 0x02000099 RID: 153
	internal struct CharacterElement
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0002BA17 File Offset: 0x00029C17
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0002BA1F File Offset: 0x00029C1F
		public uint Unicode
		{
			get
			{
				return this.m_Unicode;
			}
			set
			{
				this.m_Unicode = value;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002BA28 File Offset: 0x00029C28
		public CharacterElement(TMP_TextElement textElement)
		{
			this.m_Unicode = textElement.unicode;
			this.m_TextElement = textElement;
		}

		// Token: 0x04000561 RID: 1377
		private uint m_Unicode;

		// Token: 0x04000562 RID: 1378
		private TMP_TextElement m_TextElement;
	}
}
