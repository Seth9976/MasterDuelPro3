using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200035F RID: 863
	internal class PreSiblingQuery : CacheAxisQuery
	{
		// Token: 0x0600266B RID: 9835 RVA: 0x000D6A96 File Offset: 0x000D4C96
		public PreSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
			: base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000D6AA3 File Offset: 0x000D4CA3
		protected PreSiblingQuery(PreSiblingQuery other)
			: base(other)
		{
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000D6B00 File Offset: 0x000D4D00
		private static bool NotVisited(XPathNavigator nav, List<XPathNavigator> parentStk)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToParent();
			for (int i = 0; i < parentStk.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(parentStk[i]))
				{
					return false;
				}
			}
			parentStk.Add(xpathNavigator);
			return true;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000D6B48 File Offset: 0x000D4D48
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			List<XPathNavigator> list = new List<XPathNavigator>();
			Stack<XPathNavigator> stack = new Stack<XPathNavigator>();
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				stack.Push(this.currentNode.Clone());
			}
			while (stack.Count != 0)
			{
				XPathNavigator xpathNavigator = stack.Pop();
				if (xpathNavigator.NodeType != XPathNodeType.Attribute && xpathNavigator.NodeType != XPathNodeType.Namespace && PreSiblingQuery.NotVisited(xpathNavigator, list))
				{
					XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
					if (xpathNavigator2.MoveToParent())
					{
						xpathNavigator2.MoveToFirstChild();
						while (!xpathNavigator2.IsSamePosition(xpathNavigator))
						{
							if (this.matches(xpathNavigator2))
							{
								Query.Insert(this.outputBuffer, xpathNavigator2);
							}
							if (!xpathNavigator2.MoveToNext())
							{
								break;
							}
						}
					}
				}
			}
			return this;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000D6C05 File Offset: 0x000D4E05
		public override XPathNodeIterator Clone()
		{
			return new PreSiblingQuery(this);
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x000D6C0D File Offset: 0x000D4E0D
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
