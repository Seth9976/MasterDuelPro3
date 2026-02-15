using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the post-schema-validation infoset of a validated XML node.</summary>
	// Token: 0x020002EB RID: 747
	public class XmlSchemaInfo : IXmlSchemaInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> class.</summary>
		// Token: 0x06002175 RID: 8565 RVA: 0x000C04E8 File Offset: 0x000BE6E8
		public XmlSchemaInfo()
		{
			this.Clear();
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000C04F6 File Offset: 0x000BE6F6
		internal XmlSchemaInfo(XmlSchemaValidity validity)
			: this()
		{
			this.validity = validity;
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value.</returns>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000C0505 File Offset: 0x000BE705
		// (set) Token: 0x06002178 RID: 8568 RVA: 0x000C050D File Offset: 0x000BE70D
		public XmlSchemaValidity Validity
		{
			get
			{
				return this.validity;
			}
			set
			{
				this.validity = value;
			}
		}

		/// <summary>Gets or sets a value indicating if this validated XML node was set as the result of a default being applied during XML Schema Definition Language (XSD) schema validation.</summary>
		/// <returns>A bool value.</returns>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x000C0516 File Offset: 0x000BE716
		// (set) Token: 0x0600217A RID: 8570 RVA: 0x000C051E File Offset: 0x000BE71E
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				this.isDefault = value;
			}
		}

		/// <summary>Gets or sets a value indicating if the value for this validated XML node is nil.</summary>
		/// <returns>A bool value.</returns>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x000C0527 File Offset: 0x000BE727
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x000C052F File Offset: 0x000BE72F
		public bool IsNil
		{
			get
			{
				return this.isNil;
			}
			set
			{
				this.isNil = value;
			}
		}

		/// <summary>Gets or sets the dynamic schema type for this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object.</returns>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x000C0538 File Offset: 0x000BE738
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x000C0540 File Offset: 0x000BE740
		public XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
			set
			{
				this.memberType = value;
			}
		}

		/// <summary>Gets or sets the static XML Schema Definition Language (XSD) schema type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</returns>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000C0549 File Offset: 0x000BE749
		// (set) Token: 0x06002180 RID: 8576 RVA: 0x000C0551 File Offset: 0x000BE751
		public XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
				if (this.schemaType != null)
				{
					this.contentType = this.schemaType.SchemaContentType;
					return;
				}
				this.contentType = XmlSchemaContentType.Empty;
			}
		}

		/// <summary>Gets or sets the compiled <see cref="T:System.Xml.Schema.XmlSchemaElement" /> object that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> object.</returns>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x000C057B File Offset: 0x000BE77B
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x000C0583 File Offset: 0x000BE783
		public XmlSchemaElement SchemaElement
		{
			get
			{
				return this.schemaElement;
			}
			set
			{
				this.schemaElement = value;
				if (value != null)
				{
					this.schemaAttribute = null;
				}
			}
		}

		/// <summary>Gets or sets the compiled <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> object that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> object.</returns>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x000C0596 File Offset: 0x000BE796
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x000C059E File Offset: 0x000BE79E
		public XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.schemaAttribute;
			}
			set
			{
				this.schemaAttribute = value;
				if (value != null)
				{
					this.schemaElement = null;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaContentType" /> object that corresponds to the content type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaContentType" /> object.</returns>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x000C05B1 File Offset: 0x000BE7B1
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000C05B9 File Offset: 0x000BE7B9
		internal XmlSchemaType XmlType
		{
			get
			{
				if (this.memberType != null)
				{
					return this.memberType;
				}
				return this.schemaType;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x000C05D0 File Offset: 0x000BE7D0
		internal bool HasDefaultValue
		{
			get
			{
				return this.schemaElement != null && this.schemaElement.ElementDecl.DefaultValueTyped != null;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000C05EF File Offset: 0x000BE7EF
		internal bool IsUnionType
		{
			get
			{
				return this.schemaType != null && this.schemaType.Datatype != null && this.schemaType.Datatype.Variety == XmlSchemaDatatypeVariety.Union;
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000C061B File Offset: 0x000BE81B
		internal void Clear()
		{
			this.isNil = false;
			this.isDefault = false;
			this.schemaType = null;
			this.schemaElement = null;
			this.schemaAttribute = null;
			this.memberType = null;
			this.validity = XmlSchemaValidity.NotKnown;
			this.contentType = XmlSchemaContentType.Empty;
		}

		// Token: 0x04000F9E RID: 3998
		private bool isDefault;

		// Token: 0x04000F9F RID: 3999
		private bool isNil;

		// Token: 0x04000FA0 RID: 4000
		private XmlSchemaElement schemaElement;

		// Token: 0x04000FA1 RID: 4001
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x04000FA2 RID: 4002
		private XmlSchemaType schemaType;

		// Token: 0x04000FA3 RID: 4003
		private XmlSchemaSimpleType memberType;

		// Token: 0x04000FA4 RID: 4004
		private XmlSchemaValidity validity;

		// Token: 0x04000FA5 RID: 4005
		private XmlSchemaContentType contentType;
	}
}
