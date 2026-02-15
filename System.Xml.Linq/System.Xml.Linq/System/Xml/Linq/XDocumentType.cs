using System;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML Document Type Definition (DTD). </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200000E RID: 14
	public class XDocumentType : XNode
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Linq.XDocumentType" /> class. </summary>
		/// <param name="name">A <see cref="T:System.String" /> that contains the qualified name of the DTD, which is the same as the qualified name of the root element of the XML document.</param>
		/// <param name="publicId">A <see cref="T:System.String" /> that contains the public identifier of an external public DTD.</param>
		/// <param name="systemId">A <see cref="T:System.String" /> that contains the system identifier of an external private DTD.</param>
		/// <param name="internalSubset">A <see cref="T:System.String" /> that contains the internal subset for an internal DTD.</param>
		// Token: 0x06000058 RID: 88 RVA: 0x000038B4 File Offset: 0x00001AB4
		public XDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			this._name = XmlConvert.VerifyName(name);
			this._publicId = publicId;
			this._systemId = systemId;
			this._internalSubset = internalSubset;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Linq.XDocumentType" /> class from another <see cref="T:System.Xml.Linq.XDocumentType" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XDocumentType" /> object to copy from.</param>
		// Token: 0x06000059 RID: 89 RVA: 0x000038E0 File Offset: 0x00001AE0
		public XDocumentType(XDocumentType other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this._name = other._name;
			this._publicId = other._publicId;
			this._systemId = other._systemId;
			this._internalSubset = other._internalSubset;
		}

		/// <summary>Gets or sets the internal subset for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the internal subset for this Document Type Definition (DTD).</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003931 File Offset: 0x00001B31
		public string InternalSubset
		{
			get
			{
				return this._internalSubset;
			}
		}

		/// <summary>Gets or sets the name for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name for this Document Type Definition (DTD).</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003939 File Offset: 0x00001B39
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XDocumentType" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.DocumentType" />.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003941 File Offset: 0x00001B41
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		/// <summary>Gets or sets the public identifier for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the public identifier for this Document Type Definition (DTD).</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003945 File Offset: 0x00001B45
		public string PublicId
		{
			get
			{
				return this._publicId;
			}
		}

		/// <summary>Gets or sets the system identifier for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the system identifier for this Document Type Definition (DTD).</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000394D File Offset: 0x00001B4D
		public string SystemId
		{
			get
			{
				return this._systemId;
			}
		}

		/// <summary>Write this <see cref="T:System.Xml.Linq.XDocumentType" /> to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600005F RID: 95 RVA: 0x00003955 File Offset: 0x00001B55
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteDocType(this._name, this._publicId, this._systemId, this._internalSubset);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003983 File Offset: 0x00001B83
		internal override XNode CloneNode()
		{
			return new XDocumentType(this);
		}

		// Token: 0x04000017 RID: 23
		private string _name;

		// Token: 0x04000018 RID: 24
		private string _publicId;

		// Token: 0x04000019 RID: 25
		private string _systemId;

		// Token: 0x0400001A RID: 26
		private string _internalSubset;
	}
}
