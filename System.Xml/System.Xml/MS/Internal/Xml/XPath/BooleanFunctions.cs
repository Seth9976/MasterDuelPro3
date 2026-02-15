using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000337 RID: 823
	internal sealed class BooleanFunctions : ValueQuery
	{
		// Token: 0x06002555 RID: 9557 RVA: 0x000D4192 File Offset: 0x000D2392
		public BooleanFunctions(Function.FunctionType funcType, Query arg)
		{
			this._arg = arg;
			this._funcType = funcType;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000D41A8 File Offset: 0x000D23A8
		private BooleanFunctions(BooleanFunctions other)
			: base(other)
		{
			this._arg = Query.Clone(other._arg);
			this._funcType = other._funcType;
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000D41CE File Offset: 0x000D23CE
		public override void SetXsltContext(XsltContext context)
		{
			if (this._arg != null)
			{
				this._arg.SetXsltContext(context);
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000D41E4 File Offset: 0x000D23E4
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Function.FunctionType funcType = this._funcType;
			switch (funcType)
			{
			case Function.FunctionType.FuncBoolean:
				return this.toBoolean(nodeIterator);
			case Function.FunctionType.FuncNumber:
				break;
			case Function.FunctionType.FuncTrue:
				return true;
			case Function.FunctionType.FuncFalse:
				return false;
			case Function.FunctionType.FuncNot:
				return this.Not(nodeIterator);
			default:
				if (funcType == Function.FunctionType.FuncLang)
				{
					return this.Lang(nodeIterator);
				}
				break;
			}
			return false;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000D4256 File Offset: 0x000D2456
		internal static bool toBoolean(double number)
		{
			return number != 0.0 && !double.IsNaN(number);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000D426F File Offset: 0x000D246F
		internal static bool toBoolean(string str)
		{
			return str.Length > 0;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000D427C File Offset: 0x000D247C
		internal bool toBoolean(XPathNodeIterator nodeIterator)
		{
			object obj = this._arg.Evaluate(nodeIterator);
			if (obj is XPathNodeIterator)
			{
				return this._arg.Advance() != null;
			}
			string text = obj as string;
			if (text != null)
			{
				return BooleanFunctions.toBoolean(text);
			}
			if (obj is double)
			{
				return BooleanFunctions.toBoolean((double)obj);
			}
			return !(obj is bool) || (bool)obj;
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x0003A73C File Offset: 0x0003893C
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000D42E2 File Offset: 0x000D24E2
		private bool Not(XPathNodeIterator nodeIterator)
		{
			return !(bool)this._arg.Evaluate(nodeIterator);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000D42F8 File Offset: 0x000D24F8
		private bool Lang(XPathNodeIterator nodeIterator)
		{
			string text = this._arg.Evaluate(nodeIterator).ToString();
			string xmlLang = nodeIterator.Current.XmlLang;
			return xmlLang.StartsWith(text, StringComparison.OrdinalIgnoreCase) && (xmlLang.Length == text.Length || xmlLang[text.Length] == '-');
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000D434F File Offset: 0x000D254F
		public override XPathNodeIterator Clone()
		{
			return new BooleanFunctions(this);
		}

		// Token: 0x040011F9 RID: 4601
		private Query _arg;

		// Token: 0x040011FA RID: 4602
		private Function.FunctionType _funcType;
	}
}
