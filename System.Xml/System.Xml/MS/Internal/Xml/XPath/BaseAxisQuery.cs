using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000335 RID: 821
	internal abstract class BaseAxisQuery : Query
	{
		// Token: 0x06002541 RID: 9537 RVA: 0x000D3EA9 File Offset: 0x000D20A9
		protected BaseAxisQuery(Query qyInput)
		{
			this._name = string.Empty;
			this._prefix = string.Empty;
			this._nsUri = string.Empty;
			this.qyInput = qyInput;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000D3EDC File Offset: 0x000D20DC
		protected BaseAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
		{
			this.qyInput = qyInput;
			this._name = name;
			this._prefix = prefix;
			this._typeTest = typeTest;
			this._nameTest = prefix.Length != 0 || name.Length != 0;
			this._nsUri = string.Empty;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000D3F34 File Offset: 0x000D2134
		protected BaseAxisQuery(BaseAxisQuery other)
			: base(other)
		{
			this.qyInput = Query.Clone(other.qyInput);
			this._name = other._name;
			this._prefix = other._prefix;
			this._nsUri = other._nsUri;
			this._typeTest = other._typeTest;
			this._nameTest = other._nameTest;
			this.position = other.position;
			this.currentNode = other.currentNode;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000D3FAD File Offset: 0x000D21AD
		public override void Reset()
		{
			this.position = 0;
			this.currentNode = null;
			this.qyInput.Reset();
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000D3FC8 File Offset: 0x000D21C8
		public override void SetXsltContext(XsltContext context)
		{
			this._nsUri = context.LookupNamespace(this._prefix);
			this.qyInput.SetXsltContext(context);
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x000D3FE8 File Offset: 0x000D21E8
		protected string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000D3FF0 File Offset: 0x000D21F0
		protected string Namespace
		{
			get
			{
				return this._nsUri;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x000D3FF8 File Offset: 0x000D21F8
		protected bool NameTest
		{
			get
			{
				return this._nameTest;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000D4000 File Offset: 0x000D2200
		protected XPathNodeType TypeTest
		{
			get
			{
				return this._typeTest;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x000D4008 File Offset: 0x000D2208
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x000D4010 File Offset: 0x000D2210
		public override XPathNavigator Current
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000D4018 File Offset: 0x000D2218
		public virtual bool matches(XPathNavigator e)
		{
			if (this.TypeTest == e.NodeType || this.TypeTest == XPathNodeType.All || (this.TypeTest == XPathNodeType.Text && (e.NodeType == XPathNodeType.Whitespace || e.NodeType == XPathNodeType.SignificantWhitespace)))
			{
				if (!this.NameTest)
				{
					return true;
				}
				if ((this._name.Equals(e.LocalName) || this._name.Length == 0) && this._nsUri.Equals(e.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000D4098 File Offset: 0x000D2298
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			base.ResetCount();
			this.Reset();
			this.qyInput.Evaluate(nodeIterator);
			return this;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x040011EE RID: 4590
		internal Query qyInput;

		// Token: 0x040011EF RID: 4591
		private bool _nameTest;

		// Token: 0x040011F0 RID: 4592
		private string _name;

		// Token: 0x040011F1 RID: 4593
		private string _prefix;

		// Token: 0x040011F2 RID: 4594
		private string _nsUri;

		// Token: 0x040011F3 RID: 4595
		private XPathNodeType _typeTest;

		// Token: 0x040011F4 RID: 4596
		protected XPathNavigator currentNode;

		// Token: 0x040011F5 RID: 4597
		protected int position;
	}
}
