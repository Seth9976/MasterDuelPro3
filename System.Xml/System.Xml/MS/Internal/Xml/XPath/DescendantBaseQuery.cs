using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000340 RID: 832
	internal abstract class DescendantBaseQuery : BaseAxisQuery
	{
		// Token: 0x0600259B RID: 9627 RVA: 0x000D49E6 File Offset: 0x000D2BE6
		public DescendantBaseQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis)
			: base(qyParent, Name, Prefix, Type)
		{
			this.matchSelf = matchSelf;
			this.abbrAxis = abbrAxis;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000D4A03 File Offset: 0x000D2C03
		public DescendantBaseQuery(DescendantBaseQuery other)
			: base(other)
		{
			this.matchSelf = other.matchSelf;
			this.abbrAxis = other.abbrAxis;
		}

		// Token: 0x04001208 RID: 4616
		protected bool matchSelf;

		// Token: 0x04001209 RID: 4617
		protected bool abbrAxis;
	}
}
