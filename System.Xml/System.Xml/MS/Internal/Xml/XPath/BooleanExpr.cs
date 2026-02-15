using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000336 RID: 822
	internal sealed class BooleanExpr : ValueQuery
	{
		// Token: 0x0600254F RID: 9551 RVA: 0x000D40B4 File Offset: 0x000D22B4
		public BooleanExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			if (opnd1.StaticType != XPathResultType.Boolean)
			{
				opnd1 = new BooleanFunctions(Function.FunctionType.FuncBoolean, opnd1);
			}
			if (opnd2.StaticType != XPathResultType.Boolean)
			{
				opnd2 = new BooleanFunctions(Function.FunctionType.FuncBoolean, opnd2);
			}
			this._opnd1 = opnd1;
			this._opnd2 = opnd2;
			this._isOr = op == Operator.Op.OR;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000D4103 File Offset: 0x000D2303
		private BooleanExpr(BooleanExpr other)
			: base(other)
		{
			this._opnd1 = Query.Clone(other._opnd1);
			this._opnd2 = Query.Clone(other._opnd2);
			this._isOr = other._isOr;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000D413A File Offset: 0x000D233A
		public override void SetXsltContext(XsltContext context)
		{
			this._opnd1.SetXsltContext(context);
			this._opnd2.SetXsltContext(context);
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000D4154 File Offset: 0x000D2354
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			object obj = this._opnd1.Evaluate(nodeIterator);
			if ((bool)obj == this._isOr)
			{
				return obj;
			}
			return this._opnd2.Evaluate(nodeIterator);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000D418A File Offset: 0x000D238A
		public override XPathNodeIterator Clone()
		{
			return new BooleanExpr(this);
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x0003A73C File Offset: 0x0003893C
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x040011F6 RID: 4598
		private Query _opnd1;

		// Token: 0x040011F7 RID: 4599
		private Query _opnd2;

		// Token: 0x040011F8 RID: 4600
		private bool _isOr;
	}
}
