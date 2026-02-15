using System;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000CD RID: 205
	internal sealed class DocumentXPathNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x06000A46 RID: 2630 RVA: 0x000389D4 File Offset: 0x00036BD4
		public DocumentXPathNavigator(XmlDocument document, XmlNode node)
		{
			this.document = document;
			this.ResetPosition(node);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000389EA File Offset: 0x00036BEA
		public DocumentXPathNavigator(DocumentXPathNavigator other)
		{
			this.document = other.document;
			this.source = other.source;
			this.attributeIndex = other.attributeIndex;
			this.namespaceParent = other.namespaceParent;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00038A22 File Offset: 0x00036C22
		public override XPathNavigator Clone()
		{
			return new DocumentXPathNavigator(this);
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00038A2A File Offset: 0x00036C2A
		public override XmlNameTable NameTable
		{
			get
			{
				return this.document.NameTable;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00038A37 File Offset: 0x00036C37
		public override XPathNodeType NodeType
		{
			get
			{
				this.CalibrateText();
				return this.source.XPNodeType;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00038A4A File Offset: 0x00036C4A
		public override string LocalName
		{
			get
			{
				return this.source.XPLocalName;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00038A58 File Offset: 0x00036C58
		public override string NamespaceURI
		{
			get
			{
				XmlAttribute xmlAttribute = this.source as XmlAttribute;
				if (xmlAttribute != null && xmlAttribute.IsNamespace)
				{
					return string.Empty;
				}
				return this.source.NamespaceURI;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00038A90 File Offset: 0x00036C90
		public override string Name
		{
			get
			{
				XmlNodeType nodeType = this.source.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.ProcessingInstruction)
						{
							return string.Empty;
						}
					}
					else
					{
						if (!((XmlAttribute)this.source).IsNamespace)
						{
							return this.source.Name;
						}
						string localName = this.source.LocalName;
						if (Ref.Equal(localName, this.document.strXmlns))
						{
							return string.Empty;
						}
						return localName;
					}
				}
				return this.source.Name;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00038B0C File Offset: 0x00036D0C
		public override string Prefix
		{
			get
			{
				XmlAttribute xmlAttribute = this.source as XmlAttribute;
				if (xmlAttribute != null && xmlAttribute.IsNamespace)
				{
					return string.Empty;
				}
				return this.source.Prefix;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00038B44 File Offset: 0x00036D44
		public override string Value
		{
			get
			{
				XmlNodeType nodeType = this.source.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.Text > 1)
					{
						switch (nodeType)
						{
						case XmlNodeType.Document:
							return this.ValueDocument;
						case XmlNodeType.DocumentFragment:
							goto IL_0039;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							goto IL_004C;
						}
						return this.source.Value;
					}
					IL_004C:
					return this.ValueText;
				}
				IL_0039:
				return this.source.InnerText;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00038BB0 File Offset: 0x00036DB0
		private string ValueDocument
		{
			get
			{
				XmlElement documentElement = this.document.DocumentElement;
				if (documentElement != null)
				{
					return documentElement.InnerText;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00038BD8 File Offset: 0x00036DD8
		private string ValueText
		{
			get
			{
				this.CalibrateText();
				string text = this.source.Value;
				XmlNode xmlNode = this.NextSibling(this.source);
				if (xmlNode != null && xmlNode.IsText)
				{
					StringBuilder stringBuilder = new StringBuilder(text);
					do
					{
						stringBuilder.Append(xmlNode.Value);
						xmlNode = this.NextSibling(xmlNode);
					}
					while (xmlNode != null && xmlNode.IsText);
					text = stringBuilder.ToString();
				}
				return text;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00038C3E File Offset: 0x00036E3E
		public override string BaseURI
		{
			get
			{
				return this.source.BaseURI;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00038C4C File Offset: 0x00036E4C
		public override bool IsEmptyElement
		{
			get
			{
				XmlElement xmlElement = this.source as XmlElement;
				return xmlElement != null && xmlElement.IsEmpty;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00038C70 File Offset: 0x00036E70
		public override string XmlLang
		{
			get
			{
				return this.source.XmlLang;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00038C7D File Offset: 0x00036E7D
		public override object UnderlyingObject
		{
			get
			{
				this.CalibrateText();
				return this.source;
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00038C8C File Offset: 0x00036E8C
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null && xmlElement.HasAttributes)
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				int i = 0;
				while (i < attributes.Count)
				{
					XmlAttribute xmlAttribute = attributes[i];
					if (xmlAttribute.LocalName == localName && xmlAttribute.NamespaceURI == namespaceURI)
					{
						if (!xmlAttribute.IsNamespace)
						{
							this.source = xmlAttribute;
							this.attributeIndex = i;
							return true;
						}
						return false;
					}
					else
					{
						i++;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00038D08 File Offset: 0x00036F08
		public override bool MoveToFirstAttribute()
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null && xmlElement.HasAttributes)
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				for (int i = 0; i < attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = attributes[i];
					if (!xmlAttribute.IsNamespace)
					{
						this.source = xmlAttribute;
						this.attributeIndex = i;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00038D68 File Offset: 0x00036F68
		public override bool MoveToNextAttribute()
		{
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute == null || xmlAttribute.IsNamespace)
			{
				return false;
			}
			XmlAttributeCollection xmlAttributeCollection;
			if (!DocumentXPathNavigator.CheckAttributePosition(xmlAttribute, out xmlAttributeCollection, this.attributeIndex) && !DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, xmlAttributeCollection, out this.attributeIndex))
			{
				return false;
			}
			for (int i = this.attributeIndex + 1; i < xmlAttributeCollection.Count; i++)
			{
				xmlAttribute = xmlAttributeCollection[i];
				if (!xmlAttribute.IsNamespace)
				{
					this.source = xmlAttribute;
					this.attributeIndex = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00038DE8 File Offset: 0x00036FE8
		public override bool MoveToNamespace(string name)
		{
			if (name == this.document.strXmlns)
			{
				return false;
			}
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement != null)
			{
				string text;
				if (name != null && name.Length != 0)
				{
					text = name;
				}
				else
				{
					text = this.document.strXmlns;
				}
				string strReservedXmlns = this.document.strReservedXmlns;
				XmlAttribute attributeNode;
				for (;;)
				{
					attributeNode = xmlElement.GetAttributeNode(text, strReservedXmlns);
					if (attributeNode != null)
					{
						break;
					}
					xmlElement = xmlElement.ParentNode as XmlElement;
					if (xmlElement == null)
					{
						goto Block_6;
					}
				}
				this.namespaceParent = (XmlElement)this.source;
				this.source = attributeNode;
				return true;
				Block_6:
				if (name == this.document.strXml)
				{
					this.namespaceParent = (XmlElement)this.source;
					this.source = this.document.NamespaceXml;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00038EB0 File Offset: 0x000370B0
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			XmlElement xmlElement = this.source as XmlElement;
			if (xmlElement == null)
			{
				return false;
			}
			int maxValue = int.MaxValue;
			switch (scope)
			{
			case XPathNamespaceScope.All:
			{
				XmlAttributeCollection xmlAttributeCollection = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceGlobal(ref xmlAttributeCollection, ref maxValue))
				{
					this.source = this.document.NamespaceXml;
				}
				else
				{
					this.source = xmlAttributeCollection[maxValue];
					this.attributeIndex = maxValue;
				}
				this.namespaceParent = xmlElement;
				break;
			}
			case XPathNamespaceScope.ExcludeXml:
			{
				XmlAttributeCollection xmlAttributeCollection = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceGlobal(ref xmlAttributeCollection, ref maxValue))
				{
					return false;
				}
				XmlAttribute xmlAttribute = xmlAttributeCollection[maxValue];
				while (Ref.Equal(xmlAttribute.LocalName, this.document.strXml))
				{
					if (!DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref xmlAttributeCollection, ref maxValue))
					{
						return false;
					}
					xmlAttribute = xmlAttributeCollection[maxValue];
				}
				this.source = xmlAttribute;
				this.attributeIndex = maxValue;
				this.namespaceParent = xmlElement;
				break;
			}
			case XPathNamespaceScope.Local:
			{
				if (!xmlElement.HasAttributes)
				{
					return false;
				}
				XmlAttributeCollection xmlAttributeCollection = xmlElement.Attributes;
				if (!DocumentXPathNavigator.MoveToFirstNamespaceLocal(xmlAttributeCollection, ref maxValue))
				{
					return false;
				}
				this.source = xmlAttributeCollection[maxValue];
				this.attributeIndex = maxValue;
				this.namespaceParent = xmlElement;
				break;
			}
			default:
				return false;
			}
			return true;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00038FD0 File Offset: 0x000371D0
		private static bool MoveToFirstNamespaceLocal(XmlAttributeCollection attributes, ref int index)
		{
			for (int i = attributes.Count - 1; i >= 0; i--)
			{
				if (attributes[i].IsNamespace)
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00039004 File Offset: 0x00037204
		private static bool MoveToFirstNamespaceGlobal(ref XmlAttributeCollection attributes, ref int index)
		{
			if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
			{
				return true;
			}
			for (XmlElement xmlElement = attributes.parent.ParentNode as XmlElement; xmlElement != null; xmlElement = xmlElement.ParentNode as XmlElement)
			{
				if (xmlElement.HasAttributes)
				{
					attributes = xmlElement.Attributes;
					if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00039060 File Offset: 0x00037260
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute == null || !xmlAttribute.IsNamespace)
			{
				return false;
			}
			int num = this.attributeIndex;
			XmlAttributeCollection xmlAttributeCollection;
			if (!DocumentXPathNavigator.CheckAttributePosition(xmlAttribute, out xmlAttributeCollection, num) && !DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, xmlAttributeCollection, out num))
			{
				return false;
			}
			switch (scope)
			{
			case XPathNamespaceScope.All:
				while (DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref xmlAttributeCollection, ref num))
				{
					xmlAttribute = xmlAttributeCollection[num];
					if (!this.PathHasDuplicateNamespace(xmlAttribute.OwnerElement, this.namespaceParent, xmlAttribute.LocalName))
					{
						this.source = xmlAttribute;
						this.attributeIndex = num;
						return true;
					}
				}
				if (this.PathHasDuplicateNamespace(null, this.namespaceParent, this.document.strXml))
				{
					return false;
				}
				this.source = this.document.NamespaceXml;
				return true;
			case XPathNamespaceScope.ExcludeXml:
				while (DocumentXPathNavigator.MoveToNextNamespaceGlobal(ref xmlAttributeCollection, ref num))
				{
					xmlAttribute = xmlAttributeCollection[num];
					string localName = xmlAttribute.LocalName;
					if (!this.PathHasDuplicateNamespace(xmlAttribute.OwnerElement, this.namespaceParent, localName) && !Ref.Equal(localName, this.document.strXml))
					{
						this.source = xmlAttribute;
						this.attributeIndex = num;
						return true;
					}
				}
				return false;
			case XPathNamespaceScope.Local:
				if (xmlAttribute.OwnerElement != this.namespaceParent)
				{
					return false;
				}
				if (!DocumentXPathNavigator.MoveToNextNamespaceLocal(xmlAttributeCollection, ref num))
				{
					return false;
				}
				this.source = xmlAttributeCollection[num];
				this.attributeIndex = num;
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000391B4 File Offset: 0x000373B4
		private static bool MoveToNextNamespaceLocal(XmlAttributeCollection attributes, ref int index)
		{
			for (int i = index - 1; i >= 0; i--)
			{
				if (attributes[i].IsNamespace)
				{
					index = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000391E4 File Offset: 0x000373E4
		private static bool MoveToNextNamespaceGlobal(ref XmlAttributeCollection attributes, ref int index)
		{
			if (DocumentXPathNavigator.MoveToNextNamespaceLocal(attributes, ref index))
			{
				return true;
			}
			for (XmlElement xmlElement = attributes.parent.ParentNode as XmlElement; xmlElement != null; xmlElement = xmlElement.ParentNode as XmlElement)
			{
				if (xmlElement.HasAttributes)
				{
					attributes = xmlElement.Attributes;
					if (DocumentXPathNavigator.MoveToFirstNamespaceLocal(attributes, ref index))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00039240 File Offset: 0x00037440
		private bool PathHasDuplicateNamespace(XmlElement top, XmlElement bottom, string localName)
		{
			string strReservedXmlns = this.document.strReservedXmlns;
			while (bottom != null && bottom != top)
			{
				if (bottom.GetAttributeNode(localName, strReservedXmlns) != null)
				{
					return true;
				}
				bottom = bottom.ParentNode as XmlElement;
			}
			return false;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0003927C File Offset: 0x0003747C
		public override string LookupNamespace(string prefix)
		{
			string text = base.LookupNamespace(prefix);
			if (text != null)
			{
				text = this.NameTable.Add(text);
			}
			return text;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000392A4 File Offset: 0x000374A4
		public override bool MoveToNext()
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			if (xmlNode.IsText && this.source.IsText)
			{
				xmlNode = this.NextSibling(this.TextEnd(xmlNode));
				if (xmlNode == null)
				{
					return false;
				}
			}
			XmlNode xmlNode2 = this.ParentNode(xmlNode);
			while (!DocumentXPathNavigator.IsValidChild(xmlNode2, xmlNode))
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00039314 File Offset: 0x00037514
		public override bool MoveToFirstChild()
		{
			XmlNodeType nodeType = this.source.NodeType;
			XmlNode xmlNode;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Document && nodeType != XmlNodeType.DocumentFragment)
				{
					return false;
				}
				xmlNode = this.FirstChild(this.source);
				if (xmlNode == null)
				{
					return false;
				}
				while (!DocumentXPathNavigator.IsValidChild(this.source, xmlNode))
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
			}
			else
			{
				xmlNode = this.FirstChild(this.source);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00039388 File Offset: 0x00037588
		public override bool MoveToParent()
		{
			XmlNode xmlNode = this.ParentNode(this.source);
			if (xmlNode != null)
			{
				this.source = xmlNode;
				return true;
			}
			XmlAttribute xmlAttribute = this.source as XmlAttribute;
			if (xmlAttribute != null)
			{
				xmlNode = (xmlAttribute.IsNamespace ? this.namespaceParent : xmlAttribute.OwnerElement);
				if (xmlNode != null)
				{
					this.source = xmlNode;
					this.namespaceParent = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000393E8 File Offset: 0x000375E8
		public override void MoveToRoot()
		{
			for (;;)
			{
				XmlNode xmlNode = this.source.ParentNode;
				if (xmlNode == null)
				{
					XmlAttribute xmlAttribute = this.source as XmlAttribute;
					if (xmlAttribute == null)
					{
						break;
					}
					xmlNode = (xmlAttribute.IsNamespace ? this.namespaceParent : xmlAttribute.OwnerElement);
					if (xmlNode == null)
					{
						break;
					}
				}
				this.source = xmlNode;
			}
			this.namespaceParent = null;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00039440 File Offset: 0x00037640
		public override bool MoveTo(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator != null && this.document == documentXPathNavigator.document)
			{
				this.source = documentXPathNavigator.source;
				this.attributeIndex = documentXPathNavigator.attributeIndex;
				this.namespaceParent = documentXPathNavigator.namespaceParent;
				return true;
			}
			return false;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0003948C File Offset: 0x0003768C
		public override bool MoveToId(string id)
		{
			XmlElement elementById = this.document.GetElementById(id);
			if (elementById != null)
			{
				this.source = elementById;
				this.namespaceParent = null;
				return true;
			}
			return false;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000394BC File Offset: 0x000376BC
		public override bool MoveToChild(string localName, string namespaceUri)
		{
			if (this.source.NodeType == XmlNodeType.Attribute)
			{
				return false;
			}
			XmlNode xmlNode = this.FirstChild(this.source);
			if (xmlNode != null)
			{
				while (xmlNode.NodeType != XmlNodeType.Element || !(xmlNode.LocalName == localName) || !(xmlNode.NamespaceURI == namespaceUri))
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
				this.source = xmlNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00039524 File Offset: 0x00037724
		public override bool MoveToChild(XPathNodeType type)
		{
			if (this.source.NodeType == XmlNodeType.Attribute)
			{
				return false;
			}
			XmlNode xmlNode = this.FirstChild(this.source);
			if (xmlNode != null)
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(type);
				if (contentKindMask == 0)
				{
					return false;
				}
				while (((1 << (int)xmlNode.XPNodeType) & contentKindMask) == 0)
				{
					xmlNode = this.NextSibling(xmlNode);
					if (xmlNode == null)
					{
						return false;
					}
				}
				this.source = xmlNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00039584 File Offset: 0x00037784
		public override bool MoveToFollowing(string localName, string namespaceUri, XPathNavigator end)
		{
			XmlNode xmlNode = null;
			DocumentXPathNavigator documentXPathNavigator = end as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				if (this.document != documentXPathNavigator.document)
				{
					return false;
				}
				if (documentXPathNavigator.source.NodeType == XmlNodeType.Attribute)
				{
					documentXPathNavigator = (DocumentXPathNavigator)documentXPathNavigator.Clone();
					if (!documentXPathNavigator.MoveToNonDescendant())
					{
						return false;
					}
				}
				xmlNode = documentXPathNavigator.source;
			}
			XmlNode xmlNode2 = this.source;
			if (xmlNode2.NodeType == XmlNodeType.Attribute)
			{
				xmlNode2 = ((XmlAttribute)xmlNode2).OwnerElement;
				if (xmlNode2 == null)
				{
					return false;
				}
			}
			for (;;)
			{
				XmlNode firstChild = xmlNode2.FirstChild;
				if (firstChild != null)
				{
					xmlNode2 = firstChild;
				}
				else
				{
					XmlNode nextSibling;
					for (;;)
					{
						nextSibling = xmlNode2.NextSibling;
						if (nextSibling != null)
						{
							break;
						}
						XmlNode parentNode = xmlNode2.ParentNode;
						if (parentNode == null)
						{
							return false;
						}
						xmlNode2 = parentNode;
					}
					xmlNode2 = nextSibling;
				}
				if (xmlNode2 == xmlNode)
				{
					return false;
				}
				if (xmlNode2.NodeType == XmlNodeType.Element && !(xmlNode2.LocalName != localName) && !(xmlNode2.NamespaceURI != namespaceUri))
				{
					goto Block_13;
				}
			}
			return false;
			Block_13:
			this.source = xmlNode2;
			return true;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00039660 File Offset: 0x00037860
		public override bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XmlNode xmlNode = null;
			DocumentXPathNavigator documentXPathNavigator = end as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				if (this.document != documentXPathNavigator.document)
				{
					return false;
				}
				if (documentXPathNavigator.source.NodeType == XmlNodeType.Attribute)
				{
					documentXPathNavigator = (DocumentXPathNavigator)documentXPathNavigator.Clone();
					if (!documentXPathNavigator.MoveToNonDescendant())
					{
						return false;
					}
				}
				xmlNode = documentXPathNavigator.source;
			}
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			if (contentKindMask == 0)
			{
				return false;
			}
			XmlNode xmlNode2 = this.source;
			XmlNodeType nodeType = xmlNode2.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType - XmlNodeType.Text <= 1 || nodeType - XmlNodeType.Whitespace <= 1)
				{
					xmlNode2 = this.TextEnd(xmlNode2);
				}
			}
			else
			{
				xmlNode2 = ((XmlAttribute)xmlNode2).OwnerElement;
				if (xmlNode2 == null)
				{
					return false;
				}
			}
			for (;;)
			{
				XmlNode firstChild = xmlNode2.FirstChild;
				if (firstChild != null)
				{
					xmlNode2 = firstChild;
				}
				else
				{
					XmlNode nextSibling;
					for (;;)
					{
						nextSibling = xmlNode2.NextSibling;
						if (nextSibling != null)
						{
							break;
						}
						XmlNode parentNode = xmlNode2.ParentNode;
						if (parentNode == null)
						{
							return false;
						}
						xmlNode2 = parentNode;
					}
					xmlNode2 = nextSibling;
				}
				if (xmlNode2 == xmlNode)
				{
					return false;
				}
				if (((1 << (int)xmlNode2.XPNodeType) & contentKindMask) != 0)
				{
					goto Block_14;
				}
			}
			return false;
			Block_14:
			this.source = xmlNode2;
			return true;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00039750 File Offset: 0x00037950
		public override bool MoveToNext(string localName, string namespaceUri)
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			while (xmlNode.NodeType != XmlNodeType.Element || !(xmlNode.LocalName == localName) || !(xmlNode.NamespaceURI == namespaceUri))
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000397AC File Offset: 0x000379AC
		public override bool MoveToNext(XPathNodeType type)
		{
			XmlNode xmlNode = this.NextSibling(this.source);
			if (xmlNode == null)
			{
				return false;
			}
			if (xmlNode.IsText && this.source.IsText)
			{
				xmlNode = this.NextSibling(this.TextEnd(xmlNode));
				if (xmlNode == null)
				{
					return false;
				}
			}
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			if (contentKindMask == 0)
			{
				return false;
			}
			while (((1 << (int)xmlNode.XPNodeType) & contentKindMask) == 0)
			{
				xmlNode = this.NextSibling(xmlNode);
				if (xmlNode == null)
				{
					return false;
				}
			}
			this.source = xmlNode;
			return true;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00039824 File Offset: 0x00037A24
		public override bool IsSamePosition(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator != null)
			{
				this.CalibrateText();
				documentXPathNavigator.CalibrateText();
				return this.source == documentXPathNavigator.source && this.namespaceParent == documentXPathNavigator.namespaceParent;
			}
			return false;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00039868 File Offset: 0x00037A68
		public override bool IsDescendant(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			return documentXPathNavigator != null && DocumentXPathNavigator.IsDescendant(this.source, documentXPathNavigator.source);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00039892 File Offset: 0x00037A92
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.source.SchemaInfo;
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000398A0 File Offset: 0x00037AA0
		private static XmlNode OwnerNode(XmlNode node)
		{
			XmlNode parentNode = node.ParentNode;
			if (parentNode != null)
			{
				return parentNode;
			}
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute != null)
			{
				return xmlAttribute.OwnerElement;
			}
			return null;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000398CC File Offset: 0x00037ACC
		private static int GetDepth(XmlNode node)
		{
			int num = 0;
			for (XmlNode xmlNode = DocumentXPathNavigator.OwnerNode(node); xmlNode != null; xmlNode = DocumentXPathNavigator.OwnerNode(xmlNode))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000398F4 File Offset: 0x00037AF4
		private XmlNodeOrder Compare(XmlNode node1, XmlNode node2)
		{
			if (node1.XPNodeType == XPathNodeType.Attribute)
			{
				if (node2.XPNodeType == XPathNodeType.Attribute)
				{
					XmlElement ownerElement = ((XmlAttribute)node1).OwnerElement;
					if (ownerElement.HasAttributes)
					{
						XmlAttributeCollection attributes = ownerElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute == node1)
							{
								return XmlNodeOrder.Before;
							}
							if (xmlAttribute == node2)
							{
								return XmlNodeOrder.After;
							}
						}
					}
					return XmlNodeOrder.Unknown;
				}
				return XmlNodeOrder.Before;
			}
			else
			{
				if (node2.XPNodeType == XPathNodeType.Attribute)
				{
					return XmlNodeOrder.After;
				}
				XmlNode xmlNode = node1.NextSibling;
				while (xmlNode != null && xmlNode != node2)
				{
					xmlNode = xmlNode.NextSibling;
				}
				if (xmlNode == null)
				{
					return XmlNodeOrder.After;
				}
				return XmlNodeOrder.Before;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00039984 File Offset: 0x00037B84
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			DocumentXPathNavigator documentXPathNavigator = other as DocumentXPathNavigator;
			if (documentXPathNavigator == null)
			{
				return XmlNodeOrder.Unknown;
			}
			this.CalibrateText();
			documentXPathNavigator.CalibrateText();
			if (this.source == documentXPathNavigator.source && this.namespaceParent == documentXPathNavigator.namespaceParent)
			{
				return XmlNodeOrder.Same;
			}
			if (this.namespaceParent != null || documentXPathNavigator.namespaceParent != null)
			{
				return base.ComparePosition(other);
			}
			XmlNode xmlNode = this.source;
			XmlNode xmlNode2 = documentXPathNavigator.source;
			XmlNode xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
			XmlNode xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
			if (xmlNode3 != xmlNode4)
			{
				int num = DocumentXPathNavigator.GetDepth(xmlNode);
				int num2 = DocumentXPathNavigator.GetDepth(xmlNode2);
				if (num2 > num)
				{
					while (xmlNode2 != null && num2 > num)
					{
						xmlNode2 = DocumentXPathNavigator.OwnerNode(xmlNode2);
						num2--;
					}
					if (xmlNode == xmlNode2)
					{
						return XmlNodeOrder.Before;
					}
					xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
				}
				else if (num > num2)
				{
					while (xmlNode != null && num > num2)
					{
						xmlNode = DocumentXPathNavigator.OwnerNode(xmlNode);
						num--;
					}
					if (xmlNode == xmlNode2)
					{
						return XmlNodeOrder.After;
					}
					xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
				}
				while (xmlNode3 != null && xmlNode4 != null)
				{
					if (xmlNode3 == xmlNode4)
					{
						return this.Compare(xmlNode, xmlNode2);
					}
					xmlNode = xmlNode3;
					xmlNode2 = xmlNode4;
					xmlNode3 = DocumentXPathNavigator.OwnerNode(xmlNode);
					xmlNode4 = DocumentXPathNavigator.OwnerNode(xmlNode2);
				}
				return XmlNodeOrder.Unknown;
			}
			if (xmlNode3 == null)
			{
				return XmlNodeOrder.Unknown;
			}
			return this.Compare(xmlNode, xmlNode2);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00039AA4 File Offset: 0x00037CA4
		XmlNode IHasXmlNode.GetNode()
		{
			return this.source;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00039AAC File Offset: 0x00037CAC
		public override XPathNodeIterator SelectDescendants(string localName, string namespaceURI, bool matchSelf)
		{
			string text = this.document.NameTable.Get(namespaceURI);
			if (text == null || this.source.NodeType == XmlNodeType.Attribute)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			string text2 = this.document.NameTable.Get(localName);
			if (text2 == null)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			if (text2.Length == 0)
			{
				if (matchSelf)
				{
					return new DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(this, text);
				}
				return new DocumentXPathNodeIterator_ElemChildren_NoLocalName(this, text);
			}
			else
			{
				if (matchSelf)
				{
					return new DocumentXPathNodeIterator_ElemChildren_AndSelf(this, text2, text);
				}
				return new DocumentXPathNodeIterator_ElemChildren(this, text2, text);
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00039B30 File Offset: 0x00037D30
		public override XPathNodeIterator SelectDescendants(XPathNodeType nt, bool includeSelf)
		{
			if (nt != XPathNodeType.Element)
			{
				return base.SelectDescendants(nt, includeSelf);
			}
			XmlNodeType nodeType = this.source.NodeType;
			if (nodeType != XmlNodeType.Document && nodeType != XmlNodeType.Element)
			{
				return new DocumentXPathNodeIterator_Empty(this);
			}
			if (includeSelf)
			{
				return new DocumentXPathNodeIterator_AllElemChildren_AndSelf(this);
			}
			return new DocumentXPathNodeIterator_AllElemChildren(this);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00039B78 File Offset: 0x00037D78
		internal void ResetPosition(XmlNode node)
		{
			this.source = node;
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute != null)
			{
				XmlElement ownerElement = xmlAttribute.OwnerElement;
				if (ownerElement != null)
				{
					DocumentXPathNavigator.ResetAttributePosition(xmlAttribute, ownerElement.Attributes, out this.attributeIndex);
					if (xmlAttribute.IsNamespace)
					{
						this.namespaceParent = ownerElement;
					}
				}
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00039BC4 File Offset: 0x00037DC4
		private static bool ResetAttributePosition(XmlAttribute attribute, XmlAttributeCollection attributes, out int index)
		{
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Count; i++)
				{
					if (attribute == attributes[i])
					{
						index = i;
						return true;
					}
				}
			}
			index = 0;
			return false;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00039BF8 File Offset: 0x00037DF8
		private static bool CheckAttributePosition(XmlAttribute attribute, out XmlAttributeCollection attributes, int index)
		{
			XmlElement ownerElement = attribute.OwnerElement;
			if (ownerElement != null)
			{
				attributes = ownerElement.Attributes;
				if (index >= 0 && index < attributes.Count && attribute == attributes[index])
				{
					return true;
				}
			}
			else
			{
				attributes = null;
			}
			return false;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00039C38 File Offset: 0x00037E38
		private void CalibrateText()
		{
			for (XmlNode xmlNode = this.PreviousText(this.source); xmlNode != null; xmlNode = this.PreviousText(xmlNode))
			{
				this.ResetPosition(xmlNode);
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00039C68 File Offset: 0x00037E68
		private XmlNode ParentNode(XmlNode node)
		{
			XmlNode parentNode = node.ParentNode;
			if (!this.document.HasEntityReferences)
			{
				return parentNode;
			}
			return this.ParentNodeTail(parentNode);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00039C92 File Offset: 0x00037E92
		private XmlNode ParentNodeTail(XmlNode parent)
		{
			while (parent != null && parent.NodeType == XmlNodeType.EntityReference)
			{
				parent = parent.ParentNode;
			}
			return parent;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00039CAC File Offset: 0x00037EAC
		private XmlNode FirstChild(XmlNode node)
		{
			XmlNode firstChild = node.FirstChild;
			if (!this.document.HasEntityReferences)
			{
				return firstChild;
			}
			return this.FirstChildTail(firstChild);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00039CD6 File Offset: 0x00037ED6
		private XmlNode FirstChildTail(XmlNode child)
		{
			while (child != null && child.NodeType == XmlNodeType.EntityReference)
			{
				child = child.FirstChild;
			}
			return child;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00039CF0 File Offset: 0x00037EF0
		private XmlNode NextSibling(XmlNode node)
		{
			XmlNode nextSibling = node.NextSibling;
			if (!this.document.HasEntityReferences)
			{
				return nextSibling;
			}
			return this.NextSiblingTail(node, nextSibling);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00039D1B File Offset: 0x00037F1B
		private XmlNode NextSiblingTail(XmlNode node, XmlNode sibling)
		{
			while (sibling == null)
			{
				node = node.ParentNode;
				if (node == null || node.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
				sibling = node.NextSibling;
			}
			while (sibling != null && sibling.NodeType == XmlNodeType.EntityReference)
			{
				sibling = sibling.FirstChild;
			}
			return sibling;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00039D58 File Offset: 0x00037F58
		private XmlNode PreviousText(XmlNode node)
		{
			XmlNode previousText = node.PreviousText;
			if (!this.document.HasEntityReferences)
			{
				return previousText;
			}
			return this.PreviousTextTail(node, previousText);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00039D84 File Offset: 0x00037F84
		private XmlNode PreviousTextTail(XmlNode node, XmlNode text)
		{
			if (text != null)
			{
				return text;
			}
			if (!node.IsText)
			{
				return null;
			}
			XmlNode xmlNode;
			for (xmlNode = node.PreviousSibling; xmlNode == null; xmlNode = node.PreviousSibling)
			{
				node = node.ParentNode;
				if (node == null || node.NodeType != XmlNodeType.EntityReference)
				{
					return null;
				}
			}
			while (xmlNode != null)
			{
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.EntityReference)
					{
						xmlNode = xmlNode.LastChild;
						continue;
					}
					if (nodeType - XmlNodeType.Whitespace > 1)
					{
						return null;
					}
				}
				return xmlNode;
			}
			return null;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00039DF8 File Offset: 0x00037FF8
		private static bool IsDescendant(XmlNode top, XmlNode bottom)
		{
			do
			{
				XmlNode xmlNode = bottom.ParentNode;
				if (xmlNode == null)
				{
					XmlAttribute xmlAttribute = bottom as XmlAttribute;
					if (xmlAttribute == null)
					{
						return false;
					}
					xmlNode = xmlAttribute.OwnerElement;
					if (xmlNode == null)
					{
						return false;
					}
				}
				bottom = xmlNode;
			}
			while (top != bottom);
			return true;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00039E30 File Offset: 0x00038030
		private static bool IsValidChild(XmlNode parent, XmlNode child)
		{
			XmlNodeType nodeType = parent.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Document)
				{
					if (nodeType == XmlNodeType.DocumentFragment)
					{
						XmlNodeType xmlNodeType = child.NodeType;
						switch (xmlNodeType)
						{
						case XmlNodeType.Element:
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
						case XmlNodeType.ProcessingInstruction:
						case XmlNodeType.Comment:
							break;
						case XmlNodeType.Attribute:
						case XmlNodeType.EntityReference:
						case XmlNodeType.Entity:
							return false;
						default:
							if (xmlNodeType - XmlNodeType.Whitespace > 1)
							{
								return false;
							}
							break;
						}
						return true;
					}
				}
				else
				{
					XmlNodeType xmlNodeType = child.NodeType;
					if (xmlNodeType == XmlNodeType.Element || xmlNodeType - XmlNodeType.ProcessingInstruction <= 1)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00039EA4 File Offset: 0x000380A4
		private XmlNode TextEnd(XmlNode node)
		{
			XmlNode xmlNode;
			do
			{
				xmlNode = node;
				node = this.NextSibling(node);
			}
			while (node != null && node.IsText);
			return xmlNode;
		}

		// Token: 0x040005DF RID: 1503
		private XmlDocument document;

		// Token: 0x040005E0 RID: 1504
		private XmlNode source;

		// Token: 0x040005E1 RID: 1505
		private int attributeIndex;

		// Token: 0x040005E2 RID: 1506
		private XmlElement namespaceParent;
	}
}
