using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200036C RID: 876
	internal class Variable : AstNode
	{
		// Token: 0x060026C8 RID: 9928 RVA: 0x000D8389 File Offset: 0x000D6589
		public Variable(string name, string prefix)
		{
			this._localname = name;
			this._prefix = prefix;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x00042E8D File Offset: 0x0004108D
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Variable;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0003D9A6 File Offset: 0x0003BBA6
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x000D839F File Offset: 0x000D659F
		public string Localname
		{
			get
			{
				return this._localname;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x000D83A7 File Offset: 0x000D65A7
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x0400128D RID: 4749
		private string _localname;

		// Token: 0x0400128E RID: 4750
		private string _prefix;
	}
}
