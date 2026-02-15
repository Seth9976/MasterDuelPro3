using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200034E RID: 846
	internal class Group : AstNode
	{
		// Token: 0x060025EF RID: 9711 RVA: 0x000D57E7 File Offset: 0x000D39E7
		public Group(AstNode groupNode)
		{
			this._groupNode = groupNode;
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x0003D9A6 File Offset: 0x0003BBA6
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Group;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x000D57F6 File Offset: 0x000D39F6
		public AstNode GroupNode
		{
			get
			{
				return this._groupNode;
			}
		}

		// Token: 0x0400123D RID: 4669
		private AstNode _groupNode;
	}
}
