using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000CF RID: 207
	internal abstract class DocumentXPathNodeIterator_ElemDescendants : XPathNodeIterator
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x00039F05 File Offset: 0x00038105
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNavigator nav)
		{
			this.nav = (DocumentXPathNavigator)nav.Clone();
			this.level = 0;
			this.position = 0;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00039F2C File Offset: 0x0003812C
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNodeIterator_ElemDescendants other)
		{
			this.nav = (DocumentXPathNavigator)other.nav.Clone();
			this.level = other.level;
			this.position = other.position;
		}

		// Token: 0x06000A90 RID: 2704
		protected abstract bool Match(XmlNode node);

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00039F62 File Offset: 0x00038162
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00039F6A File Offset: 0x0003816A
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00039F72 File Offset: 0x00038172
		protected void SetPosition(int pos)
		{
			this.position = pos;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00039F7C File Offset: 0x0003817C
		public override bool MoveNext()
		{
			for (;;)
			{
				if (this.nav.MoveToFirstChild())
				{
					this.level++;
				}
				else
				{
					if (this.level == 0)
					{
						break;
					}
					while (!this.nav.MoveToNext())
					{
						this.level--;
						if (this.level == 0)
						{
							return false;
						}
						if (!this.nav.MoveToParent())
						{
							return false;
						}
					}
				}
				XmlNode xmlNode = (XmlNode)this.nav.UnderlyingObject;
				if (xmlNode.NodeType == XmlNodeType.Element && this.Match(xmlNode))
				{
					goto Block_5;
				}
			}
			return false;
			Block_5:
			this.position++;
			return true;
		}

		// Token: 0x040005E4 RID: 1508
		private DocumentXPathNavigator nav;

		// Token: 0x040005E5 RID: 1509
		private int level;

		// Token: 0x040005E6 RID: 1510
		private int position;
	}
}
