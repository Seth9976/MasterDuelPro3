using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200035A RID: 858
	internal class Operand : AstNode
	{
		// Token: 0x06002656 RID: 9814 RVA: 0x000D69BF File Offset: 0x000D4BBF
		public Operand(string val)
		{
			this._type = XPathResultType.String;
			this._val = val;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000D69D5 File Offset: 0x000D4BD5
		public Operand(double val)
		{
			this._type = XPathResultType.Number;
			this._val = val;
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.ConstantOperand;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x000D69F0 File Offset: 0x000D4BF0
		public override XPathResultType ReturnType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x000D69F8 File Offset: 0x000D4BF8
		public object OperandValue
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001252 RID: 4690
		private XPathResultType _type;

		// Token: 0x04001253 RID: 4691
		private object _val;
	}
}
