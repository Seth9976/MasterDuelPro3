using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the complexContent element from XML Schema as specified by the World Wide Web Consortium (W3C). This class represents the complex content model for complex types. It contains extensions or restrictions on a complex type that has either only elements or mixed content.</summary>
	// Token: 0x020002C3 RID: 707
	public class XmlSchemaComplexContent : XmlSchemaContentModel
	{
		/// <summary>Gets or sets information that determines if the type has a mixed content model.</summary>
		/// <returns>If this property is true, character data is allowed to appear between the child elements of the complex type (mixed content model). The default is false.Optional.</returns>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x000BEB4A File Offset: 0x000BCD4A
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x000BEB52 File Offset: 0x000BCD52
		[XmlAttribute("mixed")]
		public bool IsMixed
		{
			get
			{
				return this.isMixed;
			}
			set
			{
				this.isMixed = value;
				this.hasMixedAttribute = true;
			}
		}

		/// <summary>Gets or sets the content.</summary>
		/// <returns>One of either the <see cref="T:System.Xml.Schema.XmlSchemaComplexContentRestriction" /> or <see cref="T:System.Xml.Schema.XmlSchemaComplexContentExtension" /> classes.</returns>
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x000BEB62 File Offset: 0x000BCD62
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x000BEB6A File Offset: 0x000BCD6A
		[XmlElement("restriction", typeof(XmlSchemaComplexContentRestriction))]
		[XmlElement("extension", typeof(XmlSchemaComplexContentExtension))]
		public override XmlSchemaContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x000BEB73 File Offset: 0x000BCD73
		[XmlIgnore]
		internal bool HasMixedAttribute
		{
			get
			{
				return this.hasMixedAttribute;
			}
		}

		// Token: 0x04000F24 RID: 3876
		private XmlSchemaContent content;

		// Token: 0x04000F25 RID: 3877
		private bool isMixed;

		// Token: 0x04000F26 RID: 3878
		private bool hasMixedAttribute;
	}
}
