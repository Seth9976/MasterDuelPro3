using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200036A RID: 874
	internal sealed class UnionExpr : Query
	{
		// Token: 0x060026B4 RID: 9908 RVA: 0x000D8134 File Offset: 0x000D6334
		public UnionExpr(Query query1, Query query2)
		{
			this.qy1 = query1;
			this.qy2 = query2;
			this._advance1 = true;
			this._advance2 = true;
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000D8158 File Offset: 0x000D6358
		private UnionExpr(UnionExpr other)
			: base(other)
		{
			this.qy1 = Query.Clone(other.qy1);
			this.qy2 = Query.Clone(other.qy2);
			this._advance1 = other._advance1;
			this._advance2 = other._advance2;
			this._currentNode = Query.Clone(other._currentNode);
			this._nextNode = Query.Clone(other._nextNode);
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000D81C8 File Offset: 0x000D63C8
		public override void Reset()
		{
			this.qy1.Reset();
			this.qy2.Reset();
			this._advance1 = true;
			this._advance2 = true;
			this._nextNode = null;
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000D81F5 File Offset: 0x000D63F5
		public override void SetXsltContext(XsltContext xsltContext)
		{
			this.qy1.SetXsltContext(xsltContext);
			this.qy2.SetXsltContext(xsltContext);
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000D820F File Offset: 0x000D640F
		public override object Evaluate(XPathNodeIterator context)
		{
			this.qy1.Evaluate(context);
			this.qy2.Evaluate(context);
			this._advance1 = true;
			this._advance2 = true;
			this._nextNode = null;
			base.ResetCount();
			return this;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000D8248 File Offset: 0x000D6448
		private XPathNavigator ProcessSamePosition(XPathNavigator result)
		{
			this._currentNode = result;
			this._advance1 = (this._advance2 = true);
			return result;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000D826D File Offset: 0x000D646D
		private XPathNavigator ProcessBeforePosition(XPathNavigator res1, XPathNavigator res2)
		{
			this._nextNode = res2;
			this._advance2 = false;
			this._advance1 = true;
			this._currentNode = res1;
			return res1;
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000D828C File Offset: 0x000D648C
		private XPathNavigator ProcessAfterPosition(XPathNavigator res1, XPathNavigator res2)
		{
			this._nextNode = res1;
			this._advance1 = false;
			this._advance2 = true;
			this._currentNode = res2;
			return res2;
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000D82AC File Offset: 0x000D64AC
		public override XPathNavigator Advance()
		{
			XPathNavigator xpathNavigator;
			if (this._advance1)
			{
				xpathNavigator = this.qy1.Advance();
			}
			else
			{
				xpathNavigator = this._nextNode;
			}
			XPathNavigator xpathNavigator2;
			if (this._advance2)
			{
				xpathNavigator2 = this.qy2.Advance();
			}
			else
			{
				xpathNavigator2 = this._nextNode;
			}
			if (xpathNavigator != null && xpathNavigator2 != null)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(xpathNavigator, xpathNavigator2);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					return this.ProcessBeforePosition(xpathNavigator, xpathNavigator2);
				}
				if (xmlNodeOrder == XmlNodeOrder.After)
				{
					return this.ProcessAfterPosition(xpathNavigator, xpathNavigator2);
				}
				return this.ProcessSamePosition(xpathNavigator);
			}
			else
			{
				if (xpathNavigator2 == null)
				{
					this._advance1 = true;
					this._advance2 = false;
					this._currentNode = xpathNavigator;
					this._nextNode = null;
					return xpathNavigator;
				}
				this._advance1 = false;
				this._advance2 = true;
				this._currentNode = xpathNavigator2;
				this._nextNode = null;
				return xpathNavigator2;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000D8364 File Offset: 0x000D6564
		public override XPathNodeIterator Clone()
		{
			return new UnionExpr(this);
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000D836C File Offset: 0x000D656C
		public override XPathNavigator Current
		{
			get
			{
				return this._currentNode;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000356D5 File Offset: 0x000338D5
		public override int CurrentPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04001287 RID: 4743
		internal Query qy1;

		// Token: 0x04001288 RID: 4744
		internal Query qy2;

		// Token: 0x04001289 RID: 4745
		private bool _advance1;

		// Token: 0x0400128A RID: 4746
		private bool _advance2;

		// Token: 0x0400128B RID: 4747
		private XPathNavigator _currentNode;

		// Token: 0x0400128C RID: 4748
		private XPathNavigator _nextNode;
	}
}
