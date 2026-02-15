using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000368 RID: 872
	internal class Root : AstNode
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x0003D904 File Offset: 0x0003BB04
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Root;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}
	}
}
