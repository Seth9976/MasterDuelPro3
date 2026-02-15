using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000350 RID: 848
	internal sealed class IDQuery : CacheOutputQuery
	{
		// Token: 0x060025FA RID: 9722 RVA: 0x000D4C87 File Offset: 0x000D2E87
		public IDQuery(Query arg)
			: base(arg)
		{
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000D4C90 File Offset: 0x000D2E90
		private IDQuery(IDQuery other)
			: base(other)
		{
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000D5864 File Offset: 0x000D3A64
		public override object Evaluate(XPathNodeIterator context)
		{
			object obj = base.Evaluate(context);
			XPathNavigator xpathNavigator = context.Current.Clone();
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.Number:
				this.ProcessIds(xpathNavigator, StringFunctions.toString((double)obj));
				break;
			case XPathResultType.String:
				this.ProcessIds(xpathNavigator, (string)obj);
				break;
			case XPathResultType.Boolean:
				this.ProcessIds(xpathNavigator, StringFunctions.toString((bool)obj));
				break;
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator2;
				while ((xpathNavigator2 = this.input.Advance()) != null)
				{
					this.ProcessIds(xpathNavigator, xpathNavigator2.Value);
				}
				break;
			}
			case (XPathResultType)4:
				this.ProcessIds(xpathNavigator, ((XPathNavigator)obj).Value);
				break;
			}
			return this;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000D5914 File Offset: 0x000D3B14
		private void ProcessIds(XPathNavigator contextNode, string val)
		{
			string[] array = XmlConvert.SplitString(val);
			for (int i = 0; i < array.Length; i++)
			{
				if (contextNode.MoveToId(array[i]))
				{
					Query.Insert(this.outputBuffer, contextNode);
				}
			}
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000D594E File Offset: 0x000D3B4E
		public override XPathNodeIterator Clone()
		{
			return new IDQuery(this);
		}
	}
}
