using System;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents the document type declaration.</summary>
	// Token: 0x020000E3 RID: 227
	public class XmlDocumentType : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocumentType" /> class.</summary>
		/// <param name="name">The qualified name; see the <see cref="P:System.Xml.XmlDocumentType.Name" /> property.</param>
		/// <param name="publicId">The public identifier; see the <see cref="P:System.Xml.XmlDocumentType.PublicId" /> property.</param>
		/// <param name="systemId">The system identifier; see the <see cref="P:System.Xml.XmlDocumentType.SystemId" /> property.</param>
		/// <param name="internalSubset">The DTD internal subset; see the <see cref="P:System.Xml.XmlDocumentType.InternalSubset" /> property.</param>
		/// <param name="doc">The parent document.</param>
		// Token: 0x06000BB0 RID: 2992 RVA: 0x0003CF64 File Offset: 0x0003B164
		protected internal XmlDocumentType(string name, string publicId, string systemId, string internalSubset, XmlDocument doc)
			: base(doc)
		{
			this.name = name;
			this.publicId = publicId;
			this.systemId = systemId;
			this.namespaces = true;
			this.internalSubset = internalSubset;
			if (!doc.IsLoading)
			{
				doc.IsLoading = true;
				new XmlLoader().ParseDocumentType(this);
				doc.IsLoading = false;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For DocumentType nodes, this property returns the name of the document type.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0003CFC1 File Offset: 0x0003B1C1
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For DocumentType nodes, this property returns the name of the document type.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0003CFC1 File Offset: 0x0003B1C1
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For DocumentType nodes, this value is XmlNodeType.DocumentType.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For document type nodes, the cloned node always includes the subtree, regardless of the parameter setting. </param>
		// Token: 0x06000BB4 RID: 2996 RVA: 0x0003CFCD File Offset: 0x0003B1CD
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateDocumentType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because DocumentType nodes are read-only, this property always returns true.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.XmlEntity" /> nodes declared in the document type declaration.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNamedNodeMap" /> containing the XmlEntity nodes. The returned XmlNamedNodeMap is read-only.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0003CFF2 File Offset: 0x0003B1F2
		public XmlNamedNodeMap Entities
		{
			get
			{
				if (this.entities == null)
				{
					this.entities = new XmlNamedNodeMap(this);
				}
				return this.entities;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.XmlNotation" /> nodes present in the document type declaration.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNamedNodeMap" /> containing the XmlNotation nodes. The returned XmlNamedNodeMap is read-only.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0003D00E File Offset: 0x0003B20E
		public XmlNamedNodeMap Notations
		{
			get
			{
				if (this.notations == null)
				{
					this.notations = new XmlNamedNodeMap(this);
				}
				return this.notations;
			}
		}

		/// <summary>Gets the value of the public identifier on the DOCTYPE declaration.</summary>
		/// <returns>The public identifier on the DOCTYPE. If there is no public identifier, null is returned.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0003D02A File Offset: 0x0003B22A
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		/// <summary>Gets the value of the system identifier on the DOCTYPE declaration.</summary>
		/// <returns>The system identifier on the DOCTYPE. If there is no system identifier, null is returned.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0003D032 File Offset: 0x0003B232
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		/// <summary>Gets the value of the document type definition (DTD) internal subset on the DOCTYPE declaration.</summary>
		/// <returns>The DTD internal subset on the DOCTYPE. If there is no DTD internal subset, String.Empty is returned.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0003D03A File Offset: 0x0003B23A
		public string InternalSubset
		{
			get
			{
				return this.internalSubset;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0003D042 File Offset: 0x0003B242
		internal bool ParseWithNamespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000BBC RID: 3004 RVA: 0x0003D04A File Offset: 0x0003B24A
		public override void WriteTo(XmlWriter w)
		{
			w.WriteDocType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlDocumentType nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000BBD RID: 3005 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0003D06A File Offset: 0x0003B26A
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0003D072 File Offset: 0x0003B272
		internal SchemaInfo DtdSchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x0400062E RID: 1582
		private string name;

		// Token: 0x0400062F RID: 1583
		private string publicId;

		// Token: 0x04000630 RID: 1584
		private string systemId;

		// Token: 0x04000631 RID: 1585
		private string internalSubset;

		// Token: 0x04000632 RID: 1586
		private bool namespaces;

		// Token: 0x04000633 RID: 1587
		private XmlNamedNodeMap entities;

		// Token: 0x04000634 RID: 1588
		private XmlNamedNodeMap notations;

		// Token: 0x04000635 RID: 1589
		private SchemaInfo schemaInfo;
	}
}
