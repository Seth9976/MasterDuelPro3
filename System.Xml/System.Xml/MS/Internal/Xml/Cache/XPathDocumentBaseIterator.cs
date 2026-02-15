using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200037B RID: 891
	internal abstract class XPathDocumentBaseIterator : XPathNodeIterator
	{
		// Token: 0x06002745 RID: 10053 RVA: 0x000DA0CE File Offset: 0x000D82CE
		protected XPathDocumentBaseIterator(XPathDocumentNavigator ctxt)
		{
			this.ctxt = new XPathDocumentNavigator(ctxt);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000DA0E2 File Offset: 0x000D82E2
		protected XPathDocumentBaseIterator(XPathDocumentBaseIterator iter)
		{
			this.ctxt = new XPathDocumentNavigator(iter.ctxt);
			this.pos = iter.pos;
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x000DA107 File Offset: 0x000D8307
		public override XPathNavigator Current
		{
			get
			{
				return this.ctxt;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000DA10F File Offset: 0x000D830F
		public override int CurrentPosition
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x040012DC RID: 4828
		protected XPathDocumentNavigator ctxt;

		// Token: 0x040012DD RID: 4829
		protected int pos;
	}
}
