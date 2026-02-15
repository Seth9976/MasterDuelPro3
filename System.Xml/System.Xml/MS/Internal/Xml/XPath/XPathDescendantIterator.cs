using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000372 RID: 882
	internal class XPathDescendantIterator : XPathAxisIterator
	{
		// Token: 0x060026EE RID: 9966 RVA: 0x000D8861 File Offset: 0x000D6A61
		public XPathDescendantIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf)
			: base(nav, type, matchSelf)
		{
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000D886C File Offset: 0x000D6A6C
		public XPathDescendantIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf)
			: base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000D8879 File Offset: 0x000D6A79
		public XPathDescendantIterator(XPathDescendantIterator it)
			: base(it)
		{
			this._level = it._level;
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000D888E File Offset: 0x000D6A8E
		public override XPathNodeIterator Clone()
		{
			return new XPathDescendantIterator(this);
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000D8898 File Offset: 0x000D6A98
		public override bool MoveNext()
		{
			if (this.first)
			{
				this.first = false;
				if (this.matchSelf && this.Matches)
				{
					this.position = 1;
					return true;
				}
			}
			for (;;)
			{
				if (!this.nav.MoveToFirstChild())
				{
					while (this._level != 0)
					{
						if (this.nav.MoveToNext())
						{
							goto IL_0078;
						}
						this.nav.MoveToParent();
						this._level--;
					}
					break;
				}
				this._level++;
				IL_0078:
				if (this.Matches)
				{
					goto Block_7;
				}
			}
			return false;
			Block_7:
			this.position++;
			return true;
		}

		// Token: 0x0400129A RID: 4762
		private int _level;
	}
}
