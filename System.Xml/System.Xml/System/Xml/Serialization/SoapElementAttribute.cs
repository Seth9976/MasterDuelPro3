using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the public member value be serialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> as an encoded SOAP XML element.</summary>
	// Token: 0x0200018C RID: 396
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapElementAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> class.</summary>
		// Token: 0x060012B3 RID: 4787 RVA: 0x0005995F File Offset: 0x00057B5F
		public SoapElementAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> class and specifies the name of the XML element.</summary>
		/// <param name="elementName">The XML element name of the serialized member. </param>
		// Token: 0x060012B4 RID: 4788 RVA: 0x0005A401 File Offset: 0x00058601
		public SoapElementAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the name of the generated XML element.</summary>
		/// <returns>The name of the generated XML element. The default is the member identifier.</returns>
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0005A410 File Offset: 0x00058610
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x0005A426 File Offset: 0x00058626
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

		/// <summary>Gets or sets the XML Schema definition language (XSD) data type of the generated XML element.</summary>
		/// <returns>One of the XML Schema data types.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0005A42F File Offset: 0x0005862F
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x0005A445 File Offset: 0x00058645
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member that has the xsi:null attribute set to "1".</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the xsi:null attribute; otherwise, false.</returns>
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0005A44E File Offset: 0x0005864E
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x0005A456 File Offset: 0x00058656
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x040008CB RID: 2251
		private string elementName;

		// Token: 0x040008CC RID: 2252
		private string dataType;

		// Token: 0x040008CD RID: 2253
		private bool nullable;
	}
}
