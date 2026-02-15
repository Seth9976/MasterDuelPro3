using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000357 RID: 855
	internal sealed class NodeFunctions : ValueQuery
	{
		// Token: 0x0600263C RID: 9788 RVA: 0x000D64EE File Offset: 0x000D46EE
		public NodeFunctions(Function.FunctionType funcType, Query arg)
		{
			this._funcType = funcType;
			this._arg = arg;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000D6504 File Offset: 0x000D4704
		public override void SetXsltContext(XsltContext context)
		{
			this._xsltContext = (context.Whitespace ? context : null);
			if (this._arg != null)
			{
				this._arg.SetXsltContext(context);
			}
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000D652C File Offset: 0x000D472C
		private XPathNavigator EvaluateArg(XPathNodeIterator context)
		{
			if (this._arg == null)
			{
				return context.Current;
			}
			this._arg.Evaluate(context);
			return this._arg.Advance();
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000D6558 File Offset: 0x000D4758
		public override object Evaluate(XPathNodeIterator context)
		{
			switch (this._funcType)
			{
			case Function.FunctionType.FuncLast:
				return (double)context.Count;
			case Function.FunctionType.FuncPosition:
				return (double)context.CurrentPosition;
			case Function.FunctionType.FuncCount:
			{
				this._arg.Evaluate(context);
				int num = 0;
				if (this._xsltContext != null)
				{
					XPathNavigator xpathNavigator;
					while ((xpathNavigator = this._arg.Advance()) != null)
					{
						if (xpathNavigator.NodeType != XPathNodeType.Whitespace || this._xsltContext.PreserveWhitespace(xpathNavigator))
						{
							num++;
						}
					}
				}
				else
				{
					while (this._arg.Advance() != null)
					{
						num++;
					}
				}
				return (double)num;
			}
			case Function.FunctionType.FuncLocalName:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.LocalName;
				}
				break;
			}
			case Function.FunctionType.FuncNameSpaceUri:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.NamespaceURI;
				}
				break;
			}
			case Function.FunctionType.FuncName:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.Name;
				}
				break;
			}
			}
			return string.Empty;
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x000D6649 File Offset: 0x000D4849
		public override XPathResultType StaticType
		{
			get
			{
				return Function.ReturnTypes[(int)this._funcType];
			}
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000D6657 File Offset: 0x000D4857
		public override XPathNodeIterator Clone()
		{
			return new NodeFunctions(this._funcType, Query.Clone(this._arg))
			{
				_xsltContext = this._xsltContext
			};
		}

		// Token: 0x0400124A RID: 4682
		private Query _arg;

		// Token: 0x0400124B RID: 4683
		private Function.FunctionType _funcType;

		// Token: 0x0400124C RID: 4684
		private XsltContext _xsltContext;
	}
}
