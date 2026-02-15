using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000344 RID: 836
	internal sealed class EmptyQuery : Query
	{
		// Token: 0x060025AD RID: 9645 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override XPathNavigator Advance()
		{
			return null;
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x00035F33 File Offset: 0x00034133
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x00035F33 File Offset: 0x00034133
		public override object Evaluate(XPathNodeIterator context)
		{
			return this;
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x0009EA15 File Offset: 0x0009CC15
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x0000A558 File Offset: 0x00008758
		public override void Reset()
		{
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}
	}
}
