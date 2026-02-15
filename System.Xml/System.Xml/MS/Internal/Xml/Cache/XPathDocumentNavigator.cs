using System;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000380 RID: 896
	internal sealed class XPathDocumentNavigator : XPathNavigator, IXmlLineInfo
	{
		// Token: 0x06002759 RID: 10073 RVA: 0x000DA439 File Offset: 0x000D8639
		public XPathDocumentNavigator(XPathNode[] pageCurrent, int idxCurrent, XPathNode[] pageParent, int idxParent)
		{
			this._pageCurrent = pageCurrent;
			this._pageParent = pageParent;
			this._idxCurrent = idxCurrent;
			this._idxParent = idxParent;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000DA45E File Offset: 0x000D865E
		public XPathDocumentNavigator(XPathDocumentNavigator nav)
			: this(nav._pageCurrent, nav._idxCurrent, nav._pageParent, nav._idxParent)
		{
			this._atomizedLocalName = nav._atomizedLocalName;
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000DA48C File Offset: 0x000D868C
		public override string Value
		{
			get
			{
				string value = this._pageCurrent[this._idxCurrent].Value;
				if (value != null)
				{
					return value;
				}
				if (this._idxParent != 0)
				{
					return this._pageParent[this._idxParent].Value;
				}
				string text = string.Empty;
				StringBuilder stringBuilder = null;
				XPathNode[] pageCurrent;
				XPathNode[] array = (pageCurrent = this._pageCurrent);
				int idxCurrent;
				int num = (idxCurrent = this._idxCurrent);
				if (!XPathNodeHelper.GetNonDescendant(ref array, ref num))
				{
					array = null;
					num = 0;
				}
				while (XPathNodeHelper.GetTextFollowing(ref pageCurrent, ref idxCurrent, array, num))
				{
					if (text.Length == 0)
					{
						text = pageCurrent[idxCurrent].Value;
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(text);
						}
						stringBuilder.Append(pageCurrent[idxCurrent].Value);
					}
				}
				if (stringBuilder == null)
				{
					return text;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000DA561 File Offset: 0x000D8761
		public override XPathNavigator Clone()
		{
			return new XPathDocumentNavigator(this._pageCurrent, this._idxCurrent, this._pageParent, this._idxParent);
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000DA580 File Offset: 0x000D8780
		public override XPathNodeType NodeType
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].NodeType;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x000DA598 File Offset: 0x000D8798
		public override string LocalName
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].LocalName;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x000DA5B0 File Offset: 0x000D87B0
		public override string NamespaceURI
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].NamespaceUri;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x000DA5C8 File Offset: 0x000D87C8
		public override string Name
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].Name;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x000DA5E0 File Offset: 0x000D87E0
		public override string Prefix
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].Prefix;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x000DA5F8 File Offset: 0x000D87F8
		public override string BaseURI
		{
			get
			{
				XPathNode[] array;
				int num;
				if (this._idxParent != 0)
				{
					array = this._pageParent;
					num = this._idxParent;
				}
				else
				{
					array = this._pageCurrent;
					num = this._idxCurrent;
				}
				for (;;)
				{
					XPathNodeType nodeType = array[num].NodeType;
					if (nodeType <= XPathNodeType.Element || nodeType == XPathNodeType.ProcessingInstruction)
					{
						break;
					}
					num = array[num].GetParent(out array);
					if (num == 0)
					{
						goto Block_3;
					}
				}
				return array[num].BaseUri;
				Block_3:
				return string.Empty;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x000DA664 File Offset: 0x000D8864
		public override bool IsEmptyElement
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].AllowShortcutTag;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x000DA67C File Offset: 0x000D887C
		public override XmlNameTable NameTable
		{
			get
			{
				return this._pageCurrent[this._idxCurrent].Document.NameTable;
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000DA69C File Offset: 0x000D889C
		public override bool MoveToFirstAttribute()
		{
			XPathNode[] pageCurrent = this._pageCurrent;
			int idxCurrent = this._idxCurrent;
			if (XPathNodeHelper.GetFirstAttribute(ref this._pageCurrent, ref this._idxCurrent))
			{
				this._pageParent = pageCurrent;
				this._idxParent = idxCurrent;
				return true;
			}
			return false;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000DA6DB File Offset: 0x000D88DB
		public override bool MoveToNextAttribute()
		{
			return XPathNodeHelper.GetNextAttribute(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000DA6F0 File Offset: 0x000D88F0
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			XPathNode[] pageCurrent = this._pageCurrent;
			int idxCurrent = this._idxCurrent;
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			if (XPathNodeHelper.GetAttribute(ref this._pageCurrent, ref this._idxCurrent, this._atomizedLocalName, namespaceURI))
			{
				this._pageParent = pageCurrent;
				this._idxParent = idxCurrent;
				return true;
			}
			return false;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000DA758 File Offset: 0x000D8958
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			XPathNode[] array;
			int num;
			if (namespaceScope == XPathNamespaceScope.Local)
			{
				num = XPathNodeHelper.GetLocalNamespaces(this._pageCurrent, this._idxCurrent, out array);
			}
			else
			{
				num = XPathNodeHelper.GetInScopeNamespaces(this._pageCurrent, this._idxCurrent, out array);
			}
			while (num != 0)
			{
				if (namespaceScope != XPathNamespaceScope.ExcludeXml || !array[num].IsXmlNamespaceNode)
				{
					this._pageParent = this._pageCurrent;
					this._idxParent = this._idxCurrent;
					this._pageCurrent = array;
					this._idxCurrent = num;
					return true;
				}
				num = array[num].GetSibling(out array);
			}
			return false;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000DA7E4 File Offset: 0x000D89E4
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XPathNode[] pageCurrent = this._pageCurrent;
			int num = this._idxCurrent;
			if (pageCurrent[num].NodeType != XPathNodeType.Namespace)
			{
				return false;
			}
			for (;;)
			{
				num = pageCurrent[num].GetSibling(out pageCurrent);
				if (num == 0)
				{
					break;
				}
				if (scope != XPathNamespaceScope.ExcludeXml)
				{
					goto Block_3;
				}
				if (!pageCurrent[num].IsXmlNamespaceNode)
				{
					goto IL_006A;
				}
			}
			return false;
			Block_3:
			XPathNode[] array;
			if (scope == XPathNamespaceScope.Local && (pageCurrent[num].GetParent(out array) != this._idxParent || array != this._pageParent))
			{
				return false;
			}
			IL_006A:
			this._pageCurrent = pageCurrent;
			this._idxCurrent = num;
			return true;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000DA86A File Offset: 0x000D8A6A
		public override bool MoveToNext()
		{
			return XPathNodeHelper.GetContentSibling(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000DA880 File Offset: 0x000D8A80
		public override bool MoveToFirstChild()
		{
			if (this._pageCurrent[this._idxCurrent].HasCollapsedText)
			{
				this._pageParent = this._pageCurrent;
				this._idxParent = this._idxCurrent;
				this._idxCurrent = this._pageCurrent[this._idxCurrent].Document.GetCollapsedTextNode(out this._pageCurrent);
				return true;
			}
			return XPathNodeHelper.GetContentChild(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000DA8F8 File Offset: 0x000D8AF8
		public override bool MoveToParent()
		{
			if (this._idxParent != 0)
			{
				this._pageCurrent = this._pageParent;
				this._idxCurrent = this._idxParent;
				this._pageParent = null;
				this._idxParent = 0;
				return true;
			}
			return XPathNodeHelper.GetParent(ref this._pageCurrent, ref this._idxCurrent);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000DA948 File Offset: 0x000D8B48
		public override bool MoveTo(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				this._pageCurrent = xpathDocumentNavigator._pageCurrent;
				this._idxCurrent = xpathDocumentNavigator._idxCurrent;
				this._pageParent = xpathDocumentNavigator._pageParent;
				this._idxParent = xpathDocumentNavigator._idxParent;
				return true;
			}
			return false;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000DA994 File Offset: 0x000D8B94
		public override bool MoveToId(string id)
		{
			XPathNode[] array;
			int num = this._pageCurrent[this._idxCurrent].Document.LookupIdElement(id, out array);
			if (num != 0)
			{
				this._pageCurrent = array;
				this._idxCurrent = num;
				this._pageParent = null;
				this._idxParent = 0;
				return true;
			}
			return false;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000DA9E4 File Offset: 0x000D8BE4
		public override bool IsSamePosition(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			return xpathDocumentNavigator != null && (this._idxCurrent == xpathDocumentNavigator._idxCurrent && this._pageCurrent == xpathDocumentNavigator._pageCurrent && this._idxParent == xpathDocumentNavigator._idxParent) && this._pageParent == xpathDocumentNavigator._pageParent;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000DAA37 File Offset: 0x000D8C37
		public override void MoveToRoot()
		{
			if (this._idxParent != 0)
			{
				this._pageParent = null;
				this._idxParent = 0;
			}
			this._idxCurrent = this._pageCurrent[this._idxCurrent].GetRoot(out this._pageCurrent);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000DAA71 File Offset: 0x000D8C71
		public override bool MoveToChild(string localName, string namespaceURI)
		{
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return XPathNodeHelper.GetElementChild(ref this._pageCurrent, ref this._idxCurrent, this._atomizedLocalName, namespaceURI);
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000DAAAC File Offset: 0x000D8CAC
		public override bool MoveToNext(string localName, string namespaceURI)
		{
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return XPathNodeHelper.GetElementSibling(ref this._pageCurrent, ref this._idxCurrent, this._atomizedLocalName, namespaceURI);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000DAAE8 File Offset: 0x000D8CE8
		public override bool MoveToChild(XPathNodeType type)
		{
			if (!this._pageCurrent[this._idxCurrent].HasCollapsedText)
			{
				return XPathNodeHelper.GetContentChild(ref this._pageCurrent, ref this._idxCurrent, type);
			}
			if (type != XPathNodeType.Text && type != XPathNodeType.All)
			{
				return false;
			}
			this._pageParent = this._pageCurrent;
			this._idxParent = this._idxCurrent;
			this._idxCurrent = this._pageCurrent[this._idxCurrent].Document.GetCollapsedTextNode(out this._pageCurrent);
			return true;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000DAB6B File Offset: 0x000D8D6B
		public override bool MoveToNext(XPathNodeType type)
		{
			return XPathNodeHelper.GetContentSibling(ref this._pageCurrent, ref this._idxCurrent, type);
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000DAB80 File Offset: 0x000D8D80
		public override bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			XPathNode[] array;
			int followingEnd = this.GetFollowingEnd(end as XPathDocumentNavigator, false, out array);
			if (this._idxParent == 0)
			{
				return XPathNodeHelper.GetElementFollowing(ref this._pageCurrent, ref this._idxCurrent, array, followingEnd, this._atomizedLocalName, namespaceURI);
			}
			if (!XPathNodeHelper.GetElementFollowing(ref this._pageParent, ref this._idxParent, array, followingEnd, this._atomizedLocalName, namespaceURI))
			{
				return false;
			}
			this._pageCurrent = this._pageParent;
			this._idxCurrent = this._idxParent;
			this._pageParent = null;
			this._idxParent = 0;
			return true;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000DAC28 File Offset: 0x000D8E28
		public override bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathDocumentNavigator xpathDocumentNavigator = end as XPathDocumentNavigator;
			XPathNode[] array;
			int num;
			if (type == XPathNodeType.Text || type == XPathNodeType.All)
			{
				if (this._pageCurrent[this._idxCurrent].HasCollapsedText)
				{
					if (xpathDocumentNavigator != null && this._idxCurrent == xpathDocumentNavigator._idxParent && this._pageCurrent == xpathDocumentNavigator._pageParent)
					{
						return false;
					}
					this._pageParent = this._pageCurrent;
					this._idxParent = this._idxCurrent;
					this._idxCurrent = this._pageCurrent[this._idxCurrent].Document.GetCollapsedTextNode(out this._pageCurrent);
					return true;
				}
				else if (type == XPathNodeType.Text)
				{
					num = this.GetFollowingEnd(xpathDocumentNavigator, true, out array);
					XPathNode[] array2;
					int num2;
					if (this._idxParent != 0)
					{
						array2 = this._pageParent;
						num2 = this._idxParent;
					}
					else
					{
						array2 = this._pageCurrent;
						num2 = this._idxCurrent;
					}
					if (xpathDocumentNavigator != null && xpathDocumentNavigator._idxParent != 0 && num2 == num && array2 == array)
					{
						return false;
					}
					if (!XPathNodeHelper.GetTextFollowing(ref array2, ref num2, array, num))
					{
						return false;
					}
					if (array2[num2].NodeType == XPathNodeType.Element)
					{
						this._idxCurrent = array2[num2].Document.GetCollapsedTextNode(out this._pageCurrent);
						this._pageParent = array2;
						this._idxParent = num2;
					}
					else
					{
						this._pageCurrent = array2;
						this._idxCurrent = num2;
						this._pageParent = null;
						this._idxParent = 0;
					}
					return true;
				}
			}
			num = this.GetFollowingEnd(xpathDocumentNavigator, false, out array);
			if (this._idxParent == 0)
			{
				return XPathNodeHelper.GetContentFollowing(ref this._pageCurrent, ref this._idxCurrent, array, num, type);
			}
			if (!XPathNodeHelper.GetContentFollowing(ref this._pageParent, ref this._idxParent, array, num, type))
			{
				return false;
			}
			this._pageCurrent = this._pageParent;
			this._idxCurrent = this._idxParent;
			this._pageParent = null;
			this._idxParent = 0;
			return true;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000DADE5 File Offset: 0x000D8FE5
		public override XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathDocumentKindChildIterator(this, type);
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000DADEE File Offset: 0x000D8FEE
		public override XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			if (name == null || name.Length == 0)
			{
				return base.SelectChildren(name, namespaceURI);
			}
			return new XPathDocumentElementChildIterator(this, name, namespaceURI);
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000DAE0C File Offset: 0x000D900C
		public override XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDocumentKindDescendantIterator(this, type, matchSelf);
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000DAE16 File Offset: 0x000D9016
		public override XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			if (name == null || name.Length == 0)
			{
				return base.SelectDescendants(name, namespaceURI, matchSelf);
			}
			return new XPathDocumentElementDescendantIterator(this, name, namespaceURI, matchSelf);
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000DAE38 File Offset: 0x000D9038
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				XPathDocument document = this._pageCurrent[this._idxCurrent].Document;
				XPathDocument document2 = xpathDocumentNavigator._pageCurrent[xpathDocumentNavigator._idxCurrent].Document;
				if (document == document2)
				{
					int num = this.GetPrimaryLocation();
					int num2 = xpathDocumentNavigator.GetPrimaryLocation();
					if (num == num2)
					{
						num = this.GetSecondaryLocation();
						num2 = xpathDocumentNavigator.GetSecondaryLocation();
						if (num == num2)
						{
							return XmlNodeOrder.Same;
						}
					}
					if (num >= num2)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000DAEB0 File Offset: 0x000D90B0
		public override bool IsDescendant(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				XPathNode[] pageParent;
				int num;
				if (xpathDocumentNavigator._idxParent != 0)
				{
					pageParent = xpathDocumentNavigator._pageParent;
					num = xpathDocumentNavigator._idxParent;
				}
				else
				{
					num = xpathDocumentNavigator._pageCurrent[xpathDocumentNavigator._idxCurrent].GetParent(out pageParent);
				}
				while (num != 0)
				{
					if (num == this._idxCurrent && pageParent == this._pageCurrent)
					{
						return true;
					}
					num = pageParent[num].GetParent(out pageParent);
				}
			}
			return false;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000DAF21 File Offset: 0x000D9121
		private int GetPrimaryLocation()
		{
			if (this._idxParent == 0)
			{
				return XPathNodeHelper.GetLocation(this._pageCurrent, this._idxCurrent);
			}
			return XPathNodeHelper.GetLocation(this._pageParent, this._idxParent);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000DAF50 File Offset: 0x000D9150
		private int GetSecondaryLocation()
		{
			if (this._idxParent == 0)
			{
				return int.MinValue;
			}
			XPathNodeType nodeType = this._pageCurrent[this._idxCurrent].NodeType;
			if (nodeType == XPathNodeType.Attribute)
			{
				return XPathNodeHelper.GetLocation(this._pageCurrent, this._idxCurrent);
			}
			if (nodeType == XPathNodeType.Namespace)
			{
				return -2147483647 + XPathNodeHelper.GetLocation(this._pageCurrent, this._idxCurrent);
			}
			return int.MaxValue;
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x0004D475 File Offset: 0x0004B675
		public override object UnderlyingObject
		{
			get
			{
				return this.Clone();
			}
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000DAFB9 File Offset: 0x000D91B9
		public bool HasLineInfo()
		{
			return this._pageCurrent[this._idxCurrent].Document.HasLineInfo;
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x000DAFD6 File Offset: 0x000D91D6
		public int LineNumber
		{
			get
			{
				if (this._idxParent != 0 && this.NodeType == XPathNodeType.Text)
				{
					return this._pageParent[this._idxParent].LineNumber;
				}
				return this._pageCurrent[this._idxCurrent].LineNumber;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x000DB016 File Offset: 0x000D9216
		public int LinePosition
		{
			get
			{
				if (this._idxParent != 0 && this.NodeType == XPathNodeType.Text)
				{
					return this._pageParent[this._idxParent].CollapsedLinePosition;
				}
				return this._pageCurrent[this._idxCurrent].LinePosition;
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000DB056 File Offset: 0x000D9256
		public int GetPositionHashCode()
		{
			return this._idxCurrent ^ this._idxParent;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000DB068 File Offset: 0x000D9268
		public bool IsElementMatch(string localName, string namespaceURI)
		{
			if (localName != this._atomizedLocalName)
			{
				this._atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return this._idxParent == 0 && this._pageCurrent[this._idxCurrent].ElementMatch(this._atomizedLocalName, namespaceURI);
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000DB0BD File Offset: 0x000D92BD
		public bool IsKindMatch(XPathNodeType typ)
		{
			return ((1 << (int)this._pageCurrent[this._idxCurrent].NodeType) & XPathNavigator.GetKindMask(typ)) != 0;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000DB0E4 File Offset: 0x000D92E4
		private int GetFollowingEnd(XPathDocumentNavigator end, bool useParentOfVirtual, out XPathNode[] pageEnd)
		{
			if (end == null || this._pageCurrent[this._idxCurrent].Document != end._pageCurrent[end._idxCurrent].Document)
			{
				pageEnd = null;
				return 0;
			}
			if (end._idxParent == 0)
			{
				pageEnd = end._pageCurrent;
				return end._idxCurrent;
			}
			pageEnd = end._pageParent;
			if (!useParentOfVirtual)
			{
				return end._idxParent + 1;
			}
			return end._idxParent;
		}

		// Token: 0x040012E8 RID: 4840
		private XPathNode[] _pageCurrent;

		// Token: 0x040012E9 RID: 4841
		private XPathNode[] _pageParent;

		// Token: 0x040012EA RID: 4842
		private int _idxCurrent;

		// Token: 0x040012EB RID: 4843
		private int _idxParent;

		// Token: 0x040012EC RID: 4844
		private string _atomizedLocalName;
	}
}
