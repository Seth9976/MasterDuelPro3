using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>The base class for any element that can contain annotation elements.</summary>
	// Token: 0x020002B6 RID: 694
	public class XmlSchemaAnnotated : XmlSchemaObject
	{
		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is String.Empty.Optional.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x000BE146 File Offset: 0x000BC346
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x000BE14E File Offset: 0x000BC34E
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

		/// <summary>Gets or sets the annotation property.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAnnotation" /> representing the annotation property.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x000BE157 File Offset: 0x000BC357
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x000BE15F File Offset: 0x000BC35F
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		/// <summary>Gets or sets the qualified attributes that do not belong to the current schema's target namespace.</summary>
		/// <returns>An array of qualified <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema's target namespace.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x000BE168 File Offset: 0x000BC368
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x000BE170 File Offset: 0x000BC370
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000BE179 File Offset: 0x000BC379
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x000BE181 File Offset: 0x000BC381
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

		// Token: 0x06001FEB RID: 8171 RVA: 0x000BE170 File Offset: 0x000BC370
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000BE15F File Offset: 0x000BC35F
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04000EF4 RID: 3828
		private string id;

		// Token: 0x04000EF5 RID: 3829
		private XmlSchemaAnnotation annotation;

		// Token: 0x04000EF6 RID: 3830
		private XmlAttribute[] moreAttributes;
	}
}
