using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000362 RID: 866
	[DebuggerDisplay("{ToString()}")]
	internal abstract class Query : ResetableIterator
	{
		// Token: 0x06002677 RID: 9847 RVA: 0x000D6D96 File Offset: 0x000D4F96
		public Query()
		{
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000D6D9E File Offset: 0x000D4F9E
		protected Query(Query other)
			: base(other)
		{
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000D6DA7 File Offset: 0x000D4FA7
		public override bool MoveNext()
		{
			return this.Advance() != null;
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000D6DB4 File Offset: 0x000D4FB4
		public override int Count
		{
			get
			{
				if (this.count == -1)
				{
					Query query = (Query)this.Clone();
					query.Reset();
					this.count = 0;
					while (query.MoveNext())
					{
						this.count++;
					}
				}
				return this.count;
			}
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0000A558 File Offset: 0x00008758
		public virtual void SetXsltContext(XsltContext context)
		{
		}

		// Token: 0x0600267C RID: 9852
		public abstract object Evaluate(XPathNodeIterator nodeIterator);

		// Token: 0x0600267D RID: 9853
		public abstract XPathNavigator Advance();

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600267E RID: 9854
		public abstract XPathResultType StaticType { get; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x0009E556 File Offset: 0x0009C756
		public virtual QueryProps Properties
		{
			get
			{
				return QueryProps.Merge;
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000D6E01 File Offset: 0x000D5001
		public static Query Clone(Query input)
		{
			if (input != null)
			{
				return (Query)input.Clone();
			}
			return null;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000D6E13 File Offset: 0x000D5013
		protected static XPathNodeIterator Clone(XPathNodeIterator input)
		{
			if (input != null)
			{
				return input.Clone();
			}
			return null;
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000D6E20 File Offset: 0x000D5020
		protected static XPathNavigator Clone(XPathNavigator input)
		{
			if (input != null)
			{
				return input.Clone();
			}
			return null;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000D6E30 File Offset: 0x000D5030
		public static bool Insert(List<XPathNavigator> buffer, XPathNavigator nav)
		{
			int i = 0;
			int num = buffer.Count;
			if (num != 0)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(buffer[num - 1], nav);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					buffer.Add(nav.Clone());
					return true;
				}
				if (xmlNodeOrder == XmlNodeOrder.Same)
				{
					return false;
				}
				num--;
			}
			while (i < num)
			{
				int median = Query.GetMedian(i, num);
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(buffer[median], nav);
				if (xmlNodeOrder != XmlNodeOrder.Before)
				{
					if (xmlNodeOrder == XmlNodeOrder.Same)
					{
						return false;
					}
					num = median;
				}
				else
				{
					i = median + 1;
				}
			}
			buffer.Insert(i, nav.Clone());
			return true;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000D6EAF File Offset: 0x000D50AF
		private static int GetMedian(int l, int r)
		{
			return (int)((uint)(l + r) >> 1);
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000D6EB8 File Offset: 0x000D50B8
		public static XmlNodeOrder CompareNodes(XPathNavigator l, XPathNavigator r)
		{
			XmlNodeOrder xmlNodeOrder = l.ComparePosition(r);
			if (xmlNodeOrder == XmlNodeOrder.Unknown)
			{
				XPathNavigator xpathNavigator = l.Clone();
				xpathNavigator.MoveToRoot();
				string baseURI = xpathNavigator.BaseURI;
				if (!xpathNavigator.MoveTo(r))
				{
					xpathNavigator = r.Clone();
				}
				xpathNavigator.MoveToRoot();
				string baseURI2 = xpathNavigator.BaseURI;
				int num = string.CompareOrdinal(baseURI, baseURI2);
				xmlNodeOrder = ((num < 0) ? XmlNodeOrder.Before : ((num > 0) ? XmlNodeOrder.After : XmlNodeOrder.Unknown));
			}
			return xmlNodeOrder;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000D6F1E File Offset: 0x000D511E
		protected XPathResultType GetXPathType(object value)
		{
			if (value is XPathNodeIterator)
			{
				return XPathResultType.NodeSet;
			}
			if (value is string)
			{
				return XPathResultType.String;
			}
			if (value is double)
			{
				return XPathResultType.Number;
			}
			if (value is bool)
			{
				return XPathResultType.Boolean;
			}
			return (XPathResultType)4;
		}
	}
}
