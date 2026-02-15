using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000F5 RID: 245
	internal class XmlNodeReaderNavigator
	{
		// Token: 0x06000CBF RID: 3263 RVA: 0x00040C74 File Offset: 0x0003EE74
		public XmlNodeReaderNavigator(XmlNode node)
		{
			this.curNode = node;
			this.logNode = node;
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType == XmlNodeType.Attribute)
			{
				this.elemNode = null;
				this.attrIndex = -1;
				this.bCreatedOnAttribute = true;
			}
			else
			{
				this.elemNode = node;
				this.attrIndex = -1;
				this.bCreatedOnAttribute = false;
			}
			if (nodeType == XmlNodeType.Document)
			{
				this.doc = (XmlDocument)this.curNode;
			}
			else
			{
				this.doc = node.OwnerDocument;
			}
			this.nameTable = this.doc.NameTable;
			this.nAttrInd = -1;
			this.nDeclarationAttrCount = -1;
			this.nDocTypeAttrCount = -1;
			this.bOnAttrVal = false;
			this.bLogOnAttrVal = false;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00040D88 File Offset: 0x0003EF88
		public XmlNodeType NodeType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				if (this.nAttrInd == -1)
				{
					return nodeType;
				}
				if (this.bOnAttrVal)
				{
					return XmlNodeType.Text;
				}
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00040DB7 File Offset: 0x0003EFB7
		public string NamespaceURI
		{
			get
			{
				return this.curNode.NamespaceURI;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00040DC4 File Offset: 0x0003EFC4
		public string Name
		{
			get
			{
				if (this.nAttrInd != -1)
				{
					if (this.bOnAttrVal)
					{
						return string.Empty;
					}
					if (this.curNode.NodeType == XmlNodeType.XmlDeclaration)
					{
						return this.decNodeAttributes[this.nAttrInd].name;
					}
					return this.docTypeNodeAttributes[this.nAttrInd].name;
				}
				else
				{
					if (this.IsLocalNameEmpty(this.curNode.NodeType))
					{
						return string.Empty;
					}
					return this.curNode.Name;
				}
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00040E49 File Offset: 0x0003F049
		public string LocalName
		{
			get
			{
				if (this.nAttrInd != -1)
				{
					return this.Name;
				}
				if (this.IsLocalNameEmpty(this.curNode.NodeType))
				{
					return string.Empty;
				}
				return this.curNode.LocalName;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00040E7F File Offset: 0x0003F07F
		internal bool CreatedOnAttribute
		{
			get
			{
				return this.bCreatedOnAttribute;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00040E88 File Offset: 0x0003F088
		private bool IsLocalNameEmpty(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.None:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				return true;
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00040EEA File Offset: 0x0003F0EA
		public string Prefix
		{
			get
			{
				return this.curNode.Prefix;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00040EF7 File Offset: 0x0003F0F7
		public bool HasValue
		{
			get
			{
				return this.nAttrInd != -1 || (this.curNode.Value != null || this.curNode.NodeType == XmlNodeType.DocumentType);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00040F24 File Offset: 0x0003F124
		public string Value
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				if (this.nAttrInd != -1)
				{
					if (this.curNode.NodeType == XmlNodeType.XmlDeclaration)
					{
						return this.decNodeAttributes[this.nAttrInd].value;
					}
					return this.docTypeNodeAttributes[this.nAttrInd].value;
				}
				else
				{
					string text;
					if (nodeType == XmlNodeType.DocumentType)
					{
						text = ((XmlDocumentType)this.curNode).InternalSubset;
					}
					else if (nodeType == XmlNodeType.XmlDeclaration)
					{
						StringBuilder stringBuilder = new StringBuilder(string.Empty);
						if (this.nDeclarationAttrCount == -1)
						{
							this.InitDecAttr();
						}
						for (int i = 0; i < this.nDeclarationAttrCount; i++)
						{
							stringBuilder.Append(this.decNodeAttributes[i].name + "=\"" + this.decNodeAttributes[i].value + "\"");
							if (i != this.nDeclarationAttrCount - 1)
							{
								stringBuilder.Append(" ");
							}
						}
						text = stringBuilder.ToString();
					}
					else
					{
						text = this.curNode.Value;
					}
					if (text != null)
					{
						return text;
					}
					return string.Empty;
				}
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00041041 File Offset: 0x0003F241
		public string BaseURI
		{
			get
			{
				return this.curNode.BaseURI;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0004104E File Offset: 0x0003F24E
		public XmlSpace XmlSpace
		{
			get
			{
				return this.curNode.XmlSpace;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0004105B File Offset: 0x0003F25B
		public string XmlLang
		{
			get
			{
				return this.curNode.XmlLang;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00041068 File Offset: 0x0003F268
		public bool IsEmptyElement
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Element && ((XmlElement)this.curNode).IsEmpty;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0004108A File Offset: 0x0003F28A
		public bool IsDefault
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this.curNode).Specified;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x000410AF File Offset: 0x0003F2AF
		public IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.curNode.SchemaInfo;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x000410BC File Offset: 0x0003F2BC
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x000410C4 File Offset: 0x0003F2C4
		public int AttributeCount
		{
			get
			{
				if (this.bCreatedOnAttribute)
				{
					return 0;
				}
				XmlNodeType nodeType = this.curNode.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					return ((XmlElement)this.curNode).Attributes.Count;
				}
				if (nodeType == XmlNodeType.Attribute || (this.bOnAttrVal && nodeType != XmlNodeType.XmlDeclaration && nodeType != XmlNodeType.DocumentType))
				{
					return this.elemNode.Attributes.Count;
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nDeclarationAttrCount != -1)
					{
						return this.nDeclarationAttrCount;
					}
					this.InitDecAttr();
					return this.nDeclarationAttrCount;
				}
				else
				{
					if (nodeType != XmlNodeType.DocumentType)
					{
						return 0;
					}
					if (this.nDocTypeAttrCount != -1)
					{
						return this.nDocTypeAttrCount;
					}
					this.InitDocTypeAttr();
					return this.nDocTypeAttrCount;
				}
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0004116D File Offset: 0x0003F36D
		private void CheckIndexCondition(int attributeIndex)
		{
			if (attributeIndex < 0 || attributeIndex >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00041188 File Offset: 0x0003F388
		private void InitDecAttr()
		{
			int num = 0;
			string text = this.doc.Version;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "version";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			text = this.doc.Encoding;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "encoding";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			text = this.doc.Standalone;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "standalone";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			this.nDeclarationAttrCount = num;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00041267 File Offset: 0x0003F467
		public string GetDeclarationAttr(XmlDeclaration decl, string name)
		{
			if (name == "version")
			{
				return decl.Version;
			}
			if (name == "encoding")
			{
				return decl.Encoding;
			}
			if (name == "standalone")
			{
				return decl.Standalone;
			}
			return null;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000412A6 File Offset: 0x0003F4A6
		public string GetDeclarationAttr(int i)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			return this.decNodeAttributes[i].value;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000412C8 File Offset: 0x0003F4C8
		public int GetDecAttrInd(string name)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			for (int i = 0; i < this.nDeclarationAttrCount; i++)
			{
				if (this.decNodeAttributes[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00041314 File Offset: 0x0003F514
		private void InitDocTypeAttr()
		{
			int num = 0;
			XmlDocumentType documentType = this.doc.DocumentType;
			if (documentType == null)
			{
				this.nDocTypeAttrCount = 0;
				return;
			}
			string text = documentType.PublicId;
			if (text != null)
			{
				this.docTypeNodeAttributes[num].name = "PUBLIC";
				this.docTypeNodeAttributes[num].value = text;
				num++;
			}
			text = documentType.SystemId;
			if (text != null)
			{
				this.docTypeNodeAttributes[num].name = "SYSTEM";
				this.docTypeNodeAttributes[num].value = text;
				num++;
			}
			this.nDocTypeAttrCount = num;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000413AD File Offset: 0x0003F5AD
		public string GetDocumentTypeAttr(XmlDocumentType docType, string name)
		{
			if (name == "PUBLIC")
			{
				return docType.PublicId;
			}
			if (name == "SYSTEM")
			{
				return docType.SystemId;
			}
			return null;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000413D8 File Offset: 0x0003F5D8
		public string GetDocumentTypeAttr(int i)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			return this.docTypeNodeAttributes[i].value;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000413FC File Offset: 0x0003F5FC
		public int GetDocTypeAttrInd(string name)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			for (int i = 0; i < this.nDocTypeAttrCount; i++)
			{
				if (this.docTypeNodeAttributes[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00041448 File Offset: 0x0003F648
		private string GetAttributeFromElement(XmlElement elem, string name)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00041468 File Offset: 0x0003F668
		public string GetAttribute(string name)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					return this.GetAttributeFromElement((XmlElement)this.curNode, name);
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.GetAttributeFromElement((XmlElement)this.elemNode, name);
				}
			}
			else
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
			}
			return null;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000414F4 File Offset: 0x0003F6F4
		private string GetAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name, ns);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00041518 File Offset: 0x0003F718
		public string GetAttribute(string name, string ns)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					return this.GetAttributeFromElement((XmlElement)this.curNode, name, ns);
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.GetAttributeFromElement((XmlElement)this.elemNode, name, ns);
				}
			}
			else if (nodeType != XmlNodeType.DocumentType)
			{
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (ns.Length != 0)
					{
						return null;
					}
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
			}
			else
			{
				if (ns.Length != 0)
				{
					return null;
				}
				return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
			}
			return null;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000415B8 File Offset: 0x0003F7B8
		public string GetAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					this.CheckIndexCondition(attributeIndex);
					return ((XmlElement)this.curNode).Attributes[attributeIndex].Value;
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					this.CheckIndexCondition(attributeIndex);
					return ((XmlElement)this.elemNode).Attributes[attributeIndex].Value;
				}
			}
			else
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDocumentTypeAttr(attributeIndex);
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDeclarationAttr(attributeIndex);
				}
			}
			throw new ArgumentOutOfRangeException("attributeIndex");
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00041663 File Offset: 0x0003F863
		public void LogMove(int level)
		{
			this.logNode = this.curNode;
			this.nLogLevel = level;
			this.nLogAttrInd = this.nAttrInd;
			this.logAttrIndex = this.attrIndex;
			this.bLogOnAttrVal = this.bOnAttrVal;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0004169C File Offset: 0x0003F89C
		public void RollBackMove(ref int level)
		{
			this.curNode = this.logNode;
			level = this.nLogLevel;
			this.nAttrInd = this.nLogAttrInd;
			this.attrIndex = this.logAttrIndex;
			this.bOnAttrVal = this.bLogOnAttrVal;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x000416D8 File Offset: 0x0003F8D8
		private bool IsOnDeclOrDocType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				return nodeType == XmlNodeType.XmlDeclaration || nodeType == XmlNodeType.DocumentType;
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00041700 File Offset: 0x0003F900
		public void ResetToAttribute(ref int level)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			if (this.bOnAttrVal)
			{
				if (this.IsOnDeclOrDocType)
				{
					level -= 2;
				}
				else
				{
					while (this.curNode.NodeType != XmlNodeType.Attribute && (this.curNode = this.curNode.ParentNode) != null)
					{
						level--;
					}
				}
				this.bOnAttrVal = false;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00041760 File Offset: 0x0003F960
		public void ResetMove(ref int level, ref XmlNodeType nt)
		{
			this.LogMove(level);
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			if (this.nAttrInd != -1)
			{
				if (this.bOnAttrVal)
				{
					level--;
					this.bOnAttrVal = false;
				}
				this.nLogAttrInd = this.nAttrInd;
				level--;
				this.nAttrInd = -1;
				nt = this.curNode.NodeType;
				return;
			}
			if (this.bOnAttrVal && this.curNode.NodeType != XmlNodeType.Attribute)
			{
				this.ResetToAttribute(ref level);
			}
			if (this.curNode.NodeType == XmlNodeType.Attribute)
			{
				this.curNode = ((XmlAttribute)this.curNode).OwnerElement;
				this.attrIndex = -1;
				level--;
				nt = XmlNodeType.Element;
			}
			if (this.curNode.NodeType == XmlNodeType.Element)
			{
				this.elemNode = this.curNode;
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0004182E File Offset: 0x0003FA2E
		public bool MoveToAttribute(string name)
		{
			return this.MoveToAttribute(name, string.Empty);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0004183C File Offset: 0x0003FA3C
		private bool MoveToAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute xmlAttribute;
			if (ns.Length == 0)
			{
				xmlAttribute = elem.GetAttributeNode(name);
			}
			else
			{
				xmlAttribute = elem.GetAttributeNode(name, ns);
			}
			if (xmlAttribute != null)
			{
				this.bOnAttrVal = false;
				this.elemNode = elem;
				this.curNode = xmlAttribute;
				this.attrIndex = elem.Attributes.FindNodeOffsetNS(xmlAttribute);
				if (this.attrIndex != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0004189C File Offset: 0x0003FA9C
		public bool MoveToAttribute(string name, string namespaceURI)
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return this.MoveToAttributeFromElement((XmlElement)this.curNode, name, namespaceURI);
			}
			if (nodeType == XmlNodeType.Attribute)
			{
				return this.MoveToAttributeFromElement((XmlElement)this.elemNode, name, namespaceURI);
			}
			if (nodeType == XmlNodeType.XmlDeclaration && namespaceURI.Length == 0)
			{
				if ((this.nAttrInd = this.GetDecAttrInd(name)) != -1)
				{
					this.bOnAttrVal = false;
					return true;
				}
			}
			else if (nodeType == XmlNodeType.DocumentType && namespaceURI.Length == 0 && (this.nAttrInd = this.GetDocTypeAttrInd(name)) != -1)
			{
				this.bOnAttrVal = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00041944 File Offset: 0x0003FB44
		public void MoveToAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						return;
					}
					this.CheckIndexCondition(attributeIndex);
					XmlAttribute xmlAttribute = ((XmlElement)this.elemNode).Attributes[attributeIndex];
					if (xmlAttribute != null)
					{
						this.curNode = xmlAttribute;
						this.attrIndex = attributeIndex;
						return;
					}
				}
				else
				{
					this.CheckIndexCondition(attributeIndex);
					XmlAttribute xmlAttribute = ((XmlElement)this.curNode).Attributes[attributeIndex];
					if (xmlAttribute != null)
					{
						this.elemNode = this.curNode;
						this.curNode = xmlAttribute;
						this.attrIndex = attributeIndex;
						return;
					}
				}
			}
			else
			{
				if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
				{
					return;
				}
				this.CheckIndexCondition(attributeIndex);
				this.nAttrInd = attributeIndex;
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000419FC File Offset: 0x0003FBFC
		public bool MoveToNextAttribute(ref int level)
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					if (this.curNode.Attributes.Count > 0)
					{
						level++;
						this.elemNode = this.curNode;
						this.curNode = this.curNode.Attributes[0];
						this.attrIndex = 0;
						return true;
					}
				}
				else if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nDeclarationAttrCount == -1)
					{
						this.InitDecAttr();
					}
					this.nAttrInd++;
					if (this.nAttrInd < this.nDeclarationAttrCount)
					{
						if (this.nAttrInd == 0)
						{
							level++;
						}
						this.bOnAttrVal = false;
						return true;
					}
					this.nAttrInd--;
				}
				else if (nodeType == XmlNodeType.DocumentType)
				{
					if (this.nDocTypeAttrCount == -1)
					{
						this.InitDocTypeAttr();
					}
					this.nAttrInd++;
					if (this.nAttrInd < this.nDocTypeAttrCount)
					{
						if (this.nAttrInd == 0)
						{
							level++;
						}
						this.bOnAttrVal = false;
						return true;
					}
					this.nAttrInd--;
				}
				return false;
			}
			if (this.attrIndex >= this.elemNode.Attributes.Count - 1)
			{
				return false;
			}
			XmlAttributeCollection attributes = this.elemNode.Attributes;
			int num = this.attrIndex + 1;
			this.attrIndex = num;
			this.curNode = attributes[num];
			return true;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00041B64 File Offset: 0x0003FD64
		public bool MoveToParent()
		{
			XmlNode parentNode = this.curNode.ParentNode;
			if (parentNode != null)
			{
				this.curNode = parentNode;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = 0;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00041B9C File Offset: 0x0003FD9C
		public bool MoveToFirstChild()
		{
			XmlNode firstChild = this.curNode.FirstChild;
			if (firstChild != null)
			{
				this.curNode = firstChild;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00041BD4 File Offset: 0x0003FDD4
		private bool MoveToNextSibling(XmlNode node)
		{
			XmlNode nextSibling = node.NextSibling;
			if (nextSibling != null)
			{
				this.curNode = nextSibling;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00041C04 File Offset: 0x0003FE04
		public bool MoveToNext()
		{
			if (this.curNode.NodeType != XmlNodeType.Attribute)
			{
				return this.MoveToNextSibling(this.curNode);
			}
			return this.MoveToNextSibling(this.elemNode);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00041C30 File Offset: 0x0003FE30
		public bool MoveToElement()
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nAttrInd != -1)
					{
						this.nAttrInd = -1;
						return true;
					}
				}
			}
			else if (this.elemNode != null)
			{
				this.curNode = this.elemNode;
				this.attrIndex = -1;
				return true;
			}
			return false;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00041C94 File Offset: 0x0003FE94
		public string LookupNamespace(string prefix)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			if (prefix == "xmlns")
			{
				return this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			}
			if (prefix == "xml")
			{
				return this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			string text;
			if (prefix.Length == 0)
			{
				text = "xmlns";
			}
			else
			{
				text = "xmlns:" + prefix;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttribute attributeNode = xmlElement.GetAttributeNode(text);
						if (attributeNode != null)
						{
							return attributeNode.Value;
						}
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			return null;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00041D70 File Offset: 0x0003FF70
		internal string DefaultLookupNamespace(string prefix)
		{
			if (!this.bCreatedOnAttribute)
			{
				if (prefix == "xmlns")
				{
					return this.nameTable.Add("http://www.w3.org/2000/xmlns/");
				}
				if (prefix == "xml")
				{
					return this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
				}
				if (prefix == string.Empty)
				{
					return this.nameTable.Add(string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00041DE0 File Offset: 0x0003FFE0
		internal string LookupPrefix(string namespaceName)
		{
			if (this.bCreatedOnAttribute || namespaceName == null)
			{
				return null;
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return this.nameTable.Add("xmlns");
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return this.nameTable.Add("xml");
			}
			if (namespaceName == string.Empty)
			{
				return string.Empty;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute.Value == namespaceName)
							{
								if (xmlAttribute.Prefix.Length == 0 && xmlAttribute.LocalName == "xmlns")
								{
									if (this.LookupNamespace(string.Empty) == namespaceName)
									{
										return string.Empty;
									}
								}
								else if (xmlAttribute.Prefix == "xmlns")
								{
									string localName = xmlAttribute.LocalName;
									if (this.LookupNamespace(localName) == namespaceName)
									{
										return this.nameTable.Add(localName);
									}
								}
							}
						}
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			return null;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00041F44 File Offset: 0x00040144
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (this.bCreatedOnAttribute)
			{
				return dictionary;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute.LocalName == "xmlns" && xmlAttribute.Prefix.Length == 0)
							{
								if (!dictionary.ContainsKey(string.Empty))
								{
									dictionary.Add(this.nameTable.Add(string.Empty), this.nameTable.Add(xmlAttribute.Value));
								}
							}
							else if (xmlAttribute.Prefix == "xmlns")
							{
								string localName = xmlAttribute.LocalName;
								if (!dictionary.ContainsKey(localName))
								{
									dictionary.Add(this.nameTable.Add(localName), this.nameTable.Add(xmlAttribute.Value));
								}
							}
						}
					}
					if (scope == XmlNamespaceScope.Local)
					{
						break;
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			if (scope != XmlNamespaceScope.Local)
			{
				if (dictionary.ContainsKey(string.Empty) && dictionary[string.Empty] == string.Empty)
				{
					dictionary.Remove(string.Empty);
				}
				if (scope == XmlNamespaceScope.All)
				{
					dictionary.Add(this.nameTable.Add("xml"), this.nameTable.Add("http://www.w3.org/XML/1998/namespace"));
				}
			}
			return dictionary;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000420E4 File Offset: 0x000402E4
		public bool ReadAttributeValue(ref int level, ref bool bResolveEntity, ref XmlNodeType nt)
		{
			if (this.nAttrInd == -1)
			{
				if (this.curNode.NodeType == XmlNodeType.Attribute)
				{
					XmlNode firstChild = this.curNode.FirstChild;
					if (firstChild != null)
					{
						this.curNode = firstChild;
						nt = this.curNode.NodeType;
						level++;
						this.bOnAttrVal = true;
						return true;
					}
				}
				else if (this.bOnAttrVal)
				{
					if ((this.curNode.NodeType == XmlNodeType.EntityReference) & bResolveEntity)
					{
						this.curNode = this.curNode.FirstChild;
						nt = this.curNode.NodeType;
						level++;
						bResolveEntity = false;
						return true;
					}
					XmlNode nextSibling = this.curNode.NextSibling;
					if (nextSibling == null)
					{
						XmlNode parentNode = this.curNode.ParentNode;
						if (parentNode != null && parentNode.NodeType == XmlNodeType.EntityReference)
						{
							this.curNode = parentNode;
							nt = XmlNodeType.EndEntity;
							level--;
							return true;
						}
					}
					if (nextSibling != null)
					{
						this.curNode = nextSibling;
						nt = this.curNode.NodeType;
						return true;
					}
					return false;
				}
				return false;
			}
			if (!this.bOnAttrVal)
			{
				this.bOnAttrVal = true;
				level++;
				nt = XmlNodeType.Text;
				return true;
			}
			return false;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000421F8 File Offset: 0x000403F8
		public XmlDocument Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x04000663 RID: 1635
		private XmlNode curNode;

		// Token: 0x04000664 RID: 1636
		private XmlNode elemNode;

		// Token: 0x04000665 RID: 1637
		private XmlNode logNode;

		// Token: 0x04000666 RID: 1638
		private int attrIndex;

		// Token: 0x04000667 RID: 1639
		private int logAttrIndex;

		// Token: 0x04000668 RID: 1640
		private XmlNameTable nameTable;

		// Token: 0x04000669 RID: 1641
		private XmlDocument doc;

		// Token: 0x0400066A RID: 1642
		private int nAttrInd;

		// Token: 0x0400066B RID: 1643
		private int nDeclarationAttrCount;

		// Token: 0x0400066C RID: 1644
		private int nDocTypeAttrCount;

		// Token: 0x0400066D RID: 1645
		private int nLogLevel;

		// Token: 0x0400066E RID: 1646
		private int nLogAttrInd;

		// Token: 0x0400066F RID: 1647
		private bool bLogOnAttrVal;

		// Token: 0x04000670 RID: 1648
		private bool bCreatedOnAttribute;

		// Token: 0x04000671 RID: 1649
		internal XmlNodeReaderNavigator.VirtualAttribute[] decNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x04000672 RID: 1650
		internal XmlNodeReaderNavigator.VirtualAttribute[] docTypeNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x04000673 RID: 1651
		private bool bOnAttrVal;

		// Token: 0x020000F6 RID: 246
		internal struct VirtualAttribute
		{
			// Token: 0x06000CF4 RID: 3316 RVA: 0x00042200 File Offset: 0x00040400
			internal VirtualAttribute(string name, string value)
			{
				this.name = name;
				this.value = value;
			}

			// Token: 0x04000674 RID: 1652
			internal string name;

			// Token: 0x04000675 RID: 1653
			internal string value;
		}
	}
}
