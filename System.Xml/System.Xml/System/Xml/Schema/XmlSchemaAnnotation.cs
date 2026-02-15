using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) annotation element.</summary>
	// Token: 0x020002B7 RID: 695
	public class XmlSchemaAnnotation : XmlSchemaObject
	{
		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x000BE192 File Offset: 0x000BC392
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x000BE19A File Offset: 0x000BC39A
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

		/// <summary>Gets the Items collection that is used to store the appinfo and documentation child elements.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of appinfo and documentation child elements.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x000BE1A3 File Offset: 0x000BC3A3
		[XmlElement("documentation", typeof(XmlSchemaDocumentation))]
		[XmlElement("appinfo", typeof(XmlSchemaAppInfo))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the qualified attributes that do not belong to the schema's target namespace.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema's target namespace.</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x000BE1AB File Offset: 0x000BC3AB
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x000BE1B3 File Offset: 0x000BC3B3
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x000BE1BB File Offset: 0x000BC3BB
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

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000BE1C4 File Offset: 0x000BC3C4
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x04000EF7 RID: 3831
		private string id;

		// Token: 0x04000EF8 RID: 3832
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04000EF9 RID: 3833
		private XmlAttribute[] moreAttributes;
	}
}
