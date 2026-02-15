using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An abstract class. Provides information about the included schema.</summary>
	// Token: 0x020002D0 RID: 720
	public abstract class XmlSchemaExternal : XmlSchemaObject
	{
		/// <summary>Gets or sets the Uniform Resource Identifier (URI) location for the schema, which tells the schema processor where the schema physically resides.</summary>
		/// <returns>The URI location for the schema.Optional for imported schemas.</returns>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x000C0138 File Offset: 0x000BE338
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x000C0140 File Offset: 0x000BE340
		[XmlAttribute("schemaLocation", DataType = "anyURI")]
		public string SchemaLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		/// <summary>Gets or sets the XmlSchema for the referenced schema.</summary>
		/// <returns>The XmlSchema for the referenced schema.</returns>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x000C0149 File Offset: 0x000BE349
		// (set) Token: 0x06002121 RID: 8481 RVA: 0x000C0151 File Offset: 0x000BE351
		[XmlIgnore]
		public XmlSchema Schema
		{
			get
			{
				return this.schema;
			}
			set
			{
				this.schema = value;
			}
		}

		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x000C015A File Offset: 0x000BE35A
		// (set) Token: 0x06002123 RID: 8483 RVA: 0x000C0162 File Offset: 0x000BE362
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x000C016B File Offset: 0x000BE36B
		// (set) Token: 0x06002125 RID: 8485 RVA: 0x000C0173 File Offset: 0x000BE373
		[XmlIgnore]
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x000C017C File Offset: 0x000BE37C
		// (set) Token: 0x06002127 RID: 8487 RVA: 0x000C0184 File Offset: 0x000BE384
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000C018D File Offset: 0x000BE38D
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x000C0196 File Offset: 0x000BE396
		// (set) Token: 0x0600212A RID: 8490 RVA: 0x000C019E File Offset: 0x000BE39E
		internal Compositor Compositor
		{
			get
			{
				return this.compositor;
			}
			set
			{
				this.compositor = value;
			}
		}

		// Token: 0x04000F70 RID: 3952
		private string location;

		// Token: 0x04000F71 RID: 3953
		private Uri baseUri;

		// Token: 0x04000F72 RID: 3954
		private XmlSchema schema;

		// Token: 0x04000F73 RID: 3955
		private string id;

		// Token: 0x04000F74 RID: 3956
		private XmlAttribute[] moreAttributes;

		// Token: 0x04000F75 RID: 3957
		private Compositor compositor;
	}
}
