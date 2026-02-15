using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the attributeGroup element with the ref attribute from the XML Schema as specified by the World Wide Web Consortium (W3C). AttributesGroupRef is the reference for an attributeGroup, name property contains the attribute group being referenced. </summary>
	// Token: 0x020002BD RID: 701
	public class XmlSchemaAttributeGroupRef : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the referenced attributeGroup element.</summary>
		/// <returns>The name of the referenced attribute group. The value must be a QName.</returns>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x000BE759 File Offset: 0x000BC959
		// (set) Token: 0x06002043 RID: 8259 RVA: 0x000BE761 File Offset: 0x000BC961
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x04000F15 RID: 3861
		private XmlQualifiedName refName = XmlQualifiedName.Empty;
	}
}
