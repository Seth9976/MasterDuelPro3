using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the documentation element from XML Schema as specified by the World Wide Web Consortium (W3C). This class specifies information to be read or used by humans within an annotation.</summary>
	// Token: 0x020002CD RID: 717
	public class XmlSchemaDocumentation : XmlSchemaObject
	{
		/// <summary>Gets or sets the Uniform Resource Identifier (URI) source of the information.</summary>
		/// <returns>A URI reference. The default is String.Empty.Optional.</returns>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x000BFA74 File Offset: 0x000BDC74
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x000BFA7C File Offset: 0x000BDC7C
		[XmlAttribute("source", DataType = "anyURI")]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		/// <summary>Gets or sets the xml:lang attribute. This serves as an indicator of the language used in the contents.</summary>
		/// <returns>The xml:lang attribute.Optional.</returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x000BFA85 File Offset: 0x000BDC85
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x000BFA8D File Offset: 0x000BDC8D
		[XmlAttribute("xml:lang")]
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = (string)XmlSchemaDocumentation.languageType.Datatype.ParseValue(value, null, null);
			}
		}

		/// <summary>Gets or sets an array of XmlNodes that represents the documentation child nodes.</summary>
		/// <returns>The array that represents the documentation child nodes.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x000BFAAC File Offset: 0x000BDCAC
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x000BFAB4 File Offset: 0x000BDCB4
		[XmlAnyElement]
		[XmlText]
		public XmlNode[] Markup
		{
			get
			{
				return this.markup;
			}
			set
			{
				this.markup = value;
			}
		}

		// Token: 0x04000F50 RID: 3920
		private string source;

		// Token: 0x04000F51 RID: 3921
		private string language;

		// Token: 0x04000F52 RID: 3922
		private XmlNode[] markup;

		// Token: 0x04000F53 RID: 3923
		private static XmlSchemaSimpleType languageType = DatatypeImplementation.GetSimpleTypeFromXsdType(new XmlQualifiedName("language", "http://www.w3.org/2001/XMLSchema"));
	}
}
