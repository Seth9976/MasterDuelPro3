using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000359 RID: 857
	internal sealed class NumericExpr : ValueQuery
	{
		// Token: 0x0600264F RID: 9807 RVA: 0x000D68A8 File Offset: 0x000D4AA8
		public NumericExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			if (opnd1.StaticType != XPathResultType.Number)
			{
				opnd1 = new NumberFunctions(Function.FunctionType.FuncNumber, opnd1);
			}
			if (opnd2.StaticType != XPathResultType.Number)
			{
				opnd2 = new NumberFunctions(Function.FunctionType.FuncNumber, opnd2);
			}
			this._op = op;
			this._opnd1 = opnd1;
			this._opnd2 = opnd2;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000D68F4 File Offset: 0x000D4AF4
		private NumericExpr(NumericExpr other)
			: base(other)
		{
			this._op = other._op;
			this._opnd1 = Query.Clone(other._opnd1);
			this._opnd2 = Query.Clone(other._opnd2);
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000D692B File Offset: 0x000D4B2B
		public override void SetXsltContext(XsltContext context)
		{
			this._opnd1.SetXsltContext(context);
			this._opnd2.SetXsltContext(context);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000D6945 File Offset: 0x000D4B45
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return NumericExpr.GetValue(this._op, XmlConvert.ToXPathDouble(this._opnd1.Evaluate(nodeIterator)), XmlConvert.ToXPathDouble(this._opnd2.Evaluate(nodeIterator)));
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000D6979 File Offset: 0x000D4B79
		private static double GetValue(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.PLUS:
				return n1 + n2;
			case Operator.Op.MINUS:
				return n1 - n2;
			case Operator.Op.MUL:
				return n1 * n2;
			case Operator.Op.DIV:
				return n1 / n2;
			case Operator.Op.MOD:
				return n1 % n2;
			default:
				return 0.0;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000D69B7 File Offset: 0x000D4BB7
		public override XPathNodeIterator Clone()
		{
			return new NumericExpr(this);
		}

		// Token: 0x0400124F RID: 4687
		private Operator.Op _op;

		// Token: 0x04001250 RID: 4688
		private Query _opnd1;

		// Token: 0x04001251 RID: 4689
		private Query _opnd2;
	}
}
