using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000D8 RID: 216
	internal class XmlNodeListEnumerator : IEnumerator
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0003A57A File Offset: 0x0003877A
		public XmlNodeListEnumerator(XPathNodeList list)
		{
			this.list = list;
			this.index = -1;
			this.valid = false;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0003A597 File Offset: 0x00038797
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0003A5A0 File Offset: 0x000387A0
		public bool MoveNext()
		{
			this.index++;
			if (this.list.ReadUntil(this.index + 1) - 1 < this.index)
			{
				return false;
			}
			this.valid = this.list[this.index] != null;
			return this.valid;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0003A5FA File Offset: 0x000387FA
		public object Current
		{
			get
			{
				if (this.valid)
				{
					return this.list[this.index];
				}
				return null;
			}
		}

		// Token: 0x040005F3 RID: 1523
		private XPathNodeList list;

		// Token: 0x040005F4 RID: 1524
		private int index;

		// Token: 0x040005F5 RID: 1525
		private bool valid;
	}
}
