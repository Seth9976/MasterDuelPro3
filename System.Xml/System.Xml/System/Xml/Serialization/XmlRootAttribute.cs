using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls XML serialization of the attribute target as an XML root element.</summary>
	// Token: 0x020001BD RID: 445
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.ReturnValue)]
	public class XmlRootAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> class.</summary>
		// Token: 0x0600155C RID: 5468 RVA: 0x00069248 File Offset: 0x00067448
		public XmlRootAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> class and specifies the name of the XML root element.</summary>
		/// <param name="elementName">The name of the XML root element. </param>
		// Token: 0x0600155D RID: 5469 RVA: 0x00069257 File Offset: 0x00067457
		public XmlRootAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the name of the XML element that is generated and recognized by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class's <see cref="M:System.Xml.Serialization.XmlSerializer.Serialize(System.IO.TextWriter,System.Object)" /> and <see cref="M:System.Xml.Serialization.XmlSerializer.Deserialize(System.IO.Stream)" /> methods, respectively.</summary>
		/// <returns>The name of the XML root element that is generated and recognized in an XML-document instance. The default is the name of the serialized class.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0006926D File Offset: 0x0006746D
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x00069283 File Offset: 0x00067483
		public string ElementName
		{
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			set
			{
				this.elementName = value;
			}
		}

		/// <summary>Gets or sets the namespace for the XML root element.</summary>
		/// <returns>The namespace for the XML element.</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0006928C File Offset: 0x0006748C
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x00069294 File Offset: 0x00067494
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the XSD data type of the XML root element.</summary>
		/// <returns>An XSD (XML Schema Document) data type, as defined by the World Wide Web Consortium (www.w3.org) document named "XML Schema: DataTypes".</returns>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0006929D File Offset: 0x0006749D
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x000692B3 File Offset: 0x000674B3
		public string DataType
		{
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			set
			{
				this.dataType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member that is set to null into the xsi:nil attribute set to true.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:nil attribute; otherwise, false.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000692BC File Offset: 0x000674BC
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x000692C4 File Offset: 0x000674C4
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
				this.nullableSpecified = true;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000692D4 File Offset: 0x000674D4
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000692DC File Offset: 0x000674DC
		internal string Key
		{
			get
			{
				return string.Concat(new string[]
				{
					(this.ns == null) ? string.Empty : this.ns,
					":",
					this.ElementName,
					":",
					this.nullable.ToString()
				});
			}
		}

		// Token: 0x040009A1 RID: 2465
		private string elementName;

		// Token: 0x040009A2 RID: 2466
		private string ns;

		// Token: 0x040009A3 RID: 2467
		private string dataType;

		// Token: 0x040009A4 RID: 2468
		private bool nullable = true;

		// Token: 0x040009A5 RID: 2469
		private bool nullableSpecified;
	}
}
