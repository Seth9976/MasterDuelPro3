using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000CE RID: 206
	internal sealed class DocumentXPathNodeIterator_Empty : XPathNodeIterator
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00039EC8 File Offset: 0x000380C8
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNavigator nav)
		{
			this.nav = nav.Clone();
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00039EDC File Offset: 0x000380DC
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNodeIterator_Empty other)
		{
			this.nav = other.nav.Clone();
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00039EF5 File Offset: 0x000380F5
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_Empty(this);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00039EFD File Offset: 0x000380FD
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x040005E3 RID: 1507
		private XPathNavigator nav;
	}
}
