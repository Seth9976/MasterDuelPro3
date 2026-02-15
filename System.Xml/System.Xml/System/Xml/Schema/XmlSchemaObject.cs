using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the root class for the Xml schema object model hierarchy and serves as a base class for classes such as the <see cref="T:System.Xml.Schema.XmlSchema" /> class.</summary>
	// Token: 0x020002ED RID: 749
	public abstract class XmlSchemaObject
	{
		/// <summary>Gets or sets the line number in the file to which the schema element refers.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000C06BD File Offset: 0x000BE8BD
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x000C06C5 File Offset: 0x000BE8C5
		[XmlIgnore]
		public int LineNumber
		{
			get
			{
				return this.lineNum;
			}
			set
			{
				this.lineNum = value;
			}
		}

		/// <summary>Gets or sets the line position in the file to which the schema element refers.</summary>
		/// <returns>The line position.</returns>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000C06CE File Offset: 0x000BE8CE
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x000C06D6 File Offset: 0x000BE8D6
		[XmlIgnore]
		public int LinePosition
		{
			get
			{
				return this.linePos;
			}
			set
			{
				this.linePos = value;
			}
		}

		/// <summary>Gets or sets the source location for the file that loaded the schema.</summary>
		/// <returns>The source location (URI) for the file.</returns>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x000C06DF File Offset: 0x000BE8DF
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x000C06E7 File Offset: 0x000BE8E7
		[XmlIgnore]
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
			}
		}

		/// <summary>Gets or sets the parent of this <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <returns>The parent <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of this <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x000C06F0 File Offset: 0x000BE8F0
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x000C06F8 File Offset: 0x000BE8F8
		[XmlIgnore]
		public XmlSchemaObject Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> to use with this schema object.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> property for the schema object.</returns>
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x000C0701 File Offset: 0x000BE901
		// (set) Token: 0x0600219E RID: 8606 RVA: 0x000C071C File Offset: 0x000BE91C
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new XmlSerializerNamespaces();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void OnAdd(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void OnRemove(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void OnClear(XmlSchemaObjectCollection container)
		{
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x00014C6C File Offset: 0x00012E6C
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x0000A558 File Offset: 0x00008758
		[XmlIgnore]
		internal virtual string IdAttribute
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void AddAnnotation(XmlSchemaAnnotation annotation)
		{
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x00014C6C File Offset: 0x00012E6C
		// (set) Token: 0x060021A7 RID: 8615 RVA: 0x0000A558 File Offset: 0x00008758
		[XmlIgnore]
		internal virtual string NameAttribute
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x000C0725 File Offset: 0x000BE925
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x000C072D File Offset: 0x000BE92D
		[XmlIgnore]
		internal bool IsProcessing
		{
			get
			{
				return this.isProcessing;
			}
			set
			{
				this.isProcessing = value;
			}
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000C0736 File Offset: 0x000BE936
		internal virtual XmlSchemaObject Clone()
		{
			return (XmlSchemaObject)base.MemberwiseClone();
		}

		// Token: 0x04000FAA RID: 4010
		private int lineNum;

		// Token: 0x04000FAB RID: 4011
		private int linePos;

		// Token: 0x04000FAC RID: 4012
		private string sourceUri;

		// Token: 0x04000FAD RID: 4013
		private XmlSerializerNamespaces namespaces;

		// Token: 0x04000FAE RID: 4014
		private XmlSchemaObject parent;

		// Token: 0x04000FAF RID: 4015
		private bool isProcessing;
	}
}
