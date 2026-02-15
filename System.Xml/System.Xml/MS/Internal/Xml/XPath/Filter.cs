using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000346 RID: 838
	internal class Filter : AstNode
	{
		// Token: 0x060025C1 RID: 9665 RVA: 0x000D4F5B File Offset: 0x000D315B
		public Filter(AstNode input, AstNode condition)
		{
			this._input = input;
			this._condition = condition;
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x0003A73C File Offset: 0x0003893C
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Filter;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000D4F71 File Offset: 0x000D3171
		public AstNode Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000D4F79 File Offset: 0x000D3179
		public AstNode Condition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x04001210 RID: 4624
		private AstNode _input;

		// Token: 0x04001211 RID: 4625
		private AstNode _condition;
	}
}
