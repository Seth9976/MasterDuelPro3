using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member (a field that returns an array of <see cref="T:System.Xml.XmlElement" /> or <see cref="T:System.Xml.XmlNode" /> objects) contains objects that represent any XML element that has no corresponding member in the object being serialized or deserialized.</summary>
	// Token: 0x020001A1 RID: 417
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlAnyElementAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class.</summary>
		// Token: 0x060013AC RID: 5036 RVA: 0x00060B86 File Offset: 0x0005ED86
		public XmlAnyElementAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class and specifies the XML element name generated in the XML document.</summary>
		/// <param name="name">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		// Token: 0x060013AD RID: 5037 RVA: 0x00060B95 File Offset: 0x0005ED95
		public XmlAnyElementAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> class and specifies the XML element name generated in the XML document and its XML namespace.</summary>
		/// <param name="name">The name of the XML element that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates. </param>
		/// <param name="ns">The XML namespace of the XML element. </param>
		// Token: 0x060013AE RID: 5038 RVA: 0x00060BAB File Offset: 0x0005EDAB
		public XmlAnyElementAttribute(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
			this.nsSpecified = true;
		}

		/// <summary>Gets or sets the XML element name.</summary>
		/// <returns>The name of the XML element.</returns>
		/// <exception cref="T:System.InvalidOperationException">The element name of an array member does not match the element name specified by the <see cref="P:System.Xml.Serialization.XmlAnyElementAttribute.Name" /> property. </exception>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00060BCF File Offset: 0x0005EDCF
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x00060BE5 File Offset: 0x0005EDE5
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the XML namespace generated in the XML document.</summary>
		/// <returns>An XML namespace.</returns>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00060BEE File Offset: 0x0005EDEE
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00060BF6 File Offset: 0x0005EDF6
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
				this.nsSpecified = true;
			}
		}

		/// <summary>Gets or sets the explicit order in which the elements are serialized or deserialized.</summary>
		/// <returns>The order of the code generation.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00060C06 File Offset: 0x0005EE06
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x00060C0E File Offset: 0x0005EE0E
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("Negative values are prohibited."), "Order");
				}
				this.order = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00060C30 File Offset: 0x0005EE30
		internal bool NamespaceSpecified
		{
			get
			{
				return this.nsSpecified;
			}
		}

		// Token: 0x04000932 RID: 2354
		private string name;

		// Token: 0x04000933 RID: 2355
		private string ns;

		// Token: 0x04000934 RID: 2356
		private int order = -1;

		// Token: 0x04000935 RID: 2357
		private bool nsSpecified;
	}
}
