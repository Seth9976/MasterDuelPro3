using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000379 RID: 889
	internal sealed class XPathSelfQuery : BaseAxisQuery
	{
		// Token: 0x06002738 RID: 10040 RVA: 0x000D3D38 File Offset: 0x000D1F38
		public XPathSelfQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type)
			: base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000D5807 File Offset: 0x000D3A07
		private XPathSelfQuery(XPathSelfQuery other)
			: base(other)
		{
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000DA008 File Offset: 0x000D8208
		public override XPathNavigator Advance()
		{
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				if (this.matches(this.currentNode))
				{
					this.position = 1;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000DA04A File Offset: 0x000D824A
		public override XPathNodeIterator Clone()
		{
			return new XPathSelfQuery(this);
		}
	}
}
