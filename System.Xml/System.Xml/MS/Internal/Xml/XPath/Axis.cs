using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000333 RID: 819
	internal class Axis : AstNode
	{
		// Token: 0x06002534 RID: 9524 RVA: 0x000D3E0A File Offset: 0x000D200A
		public Axis(Axis.AxisType axisType, AstNode input, string prefix, string name, XPathNodeType nodetype)
		{
			this._axisType = axisType;
			this._input = input;
			this._prefix = prefix;
			this._name = name;
			this._nodeType = nodetype;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000D3E42 File Offset: 0x000D2042
		public Axis(Axis.AxisType axisType, AstNode input)
			: this(axisType, input, string.Empty, string.Empty, XPathNodeType.All)
		{
			this.abbrAxis = true;
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Axis;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000D3E5F File Offset: 0x000D205F
		// (set) Token: 0x06002539 RID: 9529 RVA: 0x000D3E67 File Offset: 0x000D2067
		public AstNode Input
		{
			get
			{
				return this._input;
			}
			set
			{
				this._input = value;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000D3E70 File Offset: 0x000D2070
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000D3E78 File Offset: 0x000D2078
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000D3E80 File Offset: 0x000D2080
		public XPathNodeType NodeType
		{
			get
			{
				return this._nodeType;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000D3E88 File Offset: 0x000D2088
		public Axis.AxisType TypeOfAxis
		{
			get
			{
				return this._axisType;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x000D3E90 File Offset: 0x000D2090
		public bool AbbrAxis
		{
			get
			{
				return this.abbrAxis;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x000D3E98 File Offset: 0x000D2098
		// (set) Token: 0x06002540 RID: 9536 RVA: 0x000D3EA0 File Offset: 0x000D20A0
		public string Urn
		{
			get
			{
				return this._urn;
			}
			set
			{
				this._urn = value;
			}
		}

		// Token: 0x040011D8 RID: 4568
		private Axis.AxisType _axisType;

		// Token: 0x040011D9 RID: 4569
		private AstNode _input;

		// Token: 0x040011DA RID: 4570
		private string _prefix;

		// Token: 0x040011DB RID: 4571
		private string _name;

		// Token: 0x040011DC RID: 4572
		private XPathNodeType _nodeType;

		// Token: 0x040011DD RID: 4573
		protected bool abbrAxis;

		// Token: 0x040011DE RID: 4574
		private string _urn = string.Empty;

		// Token: 0x02000334 RID: 820
		public enum AxisType
		{
			// Token: 0x040011E0 RID: 4576
			Ancestor,
			// Token: 0x040011E1 RID: 4577
			AncestorOrSelf,
			// Token: 0x040011E2 RID: 4578
			Attribute,
			// Token: 0x040011E3 RID: 4579
			Child,
			// Token: 0x040011E4 RID: 4580
			Descendant,
			// Token: 0x040011E5 RID: 4581
			DescendantOrSelf,
			// Token: 0x040011E6 RID: 4582
			Following,
			// Token: 0x040011E7 RID: 4583
			FollowingSibling,
			// Token: 0x040011E8 RID: 4584
			Namespace,
			// Token: 0x040011E9 RID: 4585
			Parent,
			// Token: 0x040011EA RID: 4586
			Preceding,
			// Token: 0x040011EB RID: 4587
			PrecedingSibling,
			// Token: 0x040011EC RID: 4588
			Self,
			// Token: 0x040011ED RID: 4589
			None
		}
	}
}
