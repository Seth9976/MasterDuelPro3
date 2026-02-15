using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000358 RID: 856
	internal sealed class NumberFunctions : ValueQuery
	{
		// Token: 0x06002642 RID: 9794 RVA: 0x000D667B File Offset: 0x000D487B
		public NumberFunctions(Function.FunctionType ftype, Query arg)
		{
			this._arg = arg;
			this._ftype = ftype;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000D6691 File Offset: 0x000D4891
		private NumberFunctions(NumberFunctions other)
			: base(other)
		{
			this._arg = Query.Clone(other._arg);
			this._ftype = other._ftype;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000D66B7 File Offset: 0x000D48B7
		public override void SetXsltContext(XsltContext context)
		{
			if (this._arg != null)
			{
				this._arg.SetXsltContext(context);
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000D66CD File Offset: 0x000D48CD
		internal static double Number(bool arg)
		{
			if (!arg)
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000D66E5 File Offset: 0x000D48E5
		internal static double Number(string arg)
		{
			return XmlConvert.ToXPathDouble(arg);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000D66F0 File Offset: 0x000D48F0
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Function.FunctionType ftype = this._ftype;
			if (ftype == Function.FunctionType.FuncNumber)
			{
				return this.Number(nodeIterator);
			}
			switch (ftype)
			{
			case Function.FunctionType.FuncSum:
				return this.Sum(nodeIterator);
			case Function.FunctionType.FuncFloor:
				return this.Floor(nodeIterator);
			case Function.FunctionType.FuncCeiling:
				return this.Ceiling(nodeIterator);
			case Function.FunctionType.FuncRound:
				return this.Round(nodeIterator);
			default:
				return null;
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000D6768 File Offset: 0x000D4968
		private double Number(XPathNodeIterator nodeIterator)
		{
			if (this._arg == null)
			{
				return XmlConvert.ToXPathDouble(nodeIterator.Current.Value);
			}
			object obj = this._arg.Evaluate(nodeIterator);
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.Number:
				return (double)obj;
			case XPathResultType.String:
				return NumberFunctions.Number((string)obj);
			case XPathResultType.Boolean:
				return NumberFunctions.Number((bool)obj);
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator = this._arg.Advance();
				if (xpathNavigator != null)
				{
					return NumberFunctions.Number(xpathNavigator.Value);
				}
				break;
			}
			case (XPathResultType)4:
				return NumberFunctions.Number(((XPathNavigator)obj).Value);
			}
			return double.NaN;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000D6814 File Offset: 0x000D4A14
		private double Sum(XPathNodeIterator nodeIterator)
		{
			double num = 0.0;
			this._arg.Evaluate(nodeIterator);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this._arg.Advance()) != null)
			{
				num += NumberFunctions.Number(xpathNavigator.Value);
			}
			return num;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000D6858 File Offset: 0x000D4A58
		private double Floor(XPathNodeIterator nodeIterator)
		{
			return Math.Floor((double)this._arg.Evaluate(nodeIterator));
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000D6870 File Offset: 0x000D4A70
		private double Ceiling(XPathNodeIterator nodeIterator)
		{
			return Math.Ceiling((double)this._arg.Evaluate(nodeIterator));
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000D6888 File Offset: 0x000D4A88
		private double Round(XPathNodeIterator nodeIterator)
		{
			return XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this._arg.Evaluate(nodeIterator)));
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000D68A0 File Offset: 0x000D4AA0
		public override XPathNodeIterator Clone()
		{
			return new NumberFunctions(this);
		}

		// Token: 0x0400124D RID: 4685
		private Query _arg;

		// Token: 0x0400124E RID: 4686
		private Function.FunctionType _ftype;
	}
}
