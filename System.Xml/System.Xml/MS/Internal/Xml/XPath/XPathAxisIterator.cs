using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000370 RID: 880
	internal abstract class XPathAxisIterator : XPathNodeIterator
	{
		// Token: 0x060026E2 RID: 9954 RVA: 0x000D8653 File Offset: 0x000D6853
		public XPathAxisIterator(XPathNavigator nav, bool matchSelf)
		{
			this.nav = nav;
			this.matchSelf = matchSelf;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000D8670 File Offset: 0x000D6870
		public XPathAxisIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf)
			: this(nav, matchSelf)
		{
			this.type = type;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000D8681 File Offset: 0x000D6881
		public XPathAxisIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf)
			: this(nav, matchSelf)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.name = name;
			this.uri = namespaceURI;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000D86B8 File Offset: 0x000D68B8
		public XPathAxisIterator(XPathAxisIterator it)
		{
			this.nav = it.nav.Clone();
			this.type = it.type;
			this.name = it.name;
			this.uri = it.uri;
			this.position = it.position;
			this.matchSelf = it.matchSelf;
			this.first = it.first;
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000D872B File Offset: 0x000D692B
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x000D8733 File Offset: 0x000D6933
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x000D873C File Offset: 0x000D693C
		protected virtual bool Matches
		{
			get
			{
				if (this.name == null)
				{
					return this.type == this.nav.NodeType || this.type == XPathNodeType.All || (this.type == XPathNodeType.Text && (this.nav.NodeType == XPathNodeType.Whitespace || this.nav.NodeType == XPathNodeType.SignificantWhitespace));
				}
				return this.nav.NodeType == XPathNodeType.Element && (this.name.Length == 0 || this.name == this.nav.LocalName) && this.uri == this.nav.NamespaceURI;
			}
		}

		// Token: 0x04001293 RID: 4755
		internal XPathNavigator nav;

		// Token: 0x04001294 RID: 4756
		internal XPathNodeType type;

		// Token: 0x04001295 RID: 4757
		internal string name;

		// Token: 0x04001296 RID: 4758
		internal string uri;

		// Token: 0x04001297 RID: 4759
		internal int position;

		// Token: 0x04001298 RID: 4760
		internal bool matchSelf;

		// Token: 0x04001299 RID: 4761
		internal bool first = true;
	}
}
