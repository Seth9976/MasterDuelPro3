using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) appinfo element.</summary>
	// Token: 0x020002BA RID: 698
	public class XmlSchemaAppInfo : XmlSchemaObject
	{
		/// <summary>Gets or sets the source of the application information.</summary>
		/// <returns>A Uniform Resource Identifier (URI) reference. The default is String.Empty.Optional.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x000BE4C9 File Offset: 0x000BC6C9
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x000BE4D1 File Offset: 0x000BC6D1
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

		/// <summary>Gets or sets an array of <see cref="T:System.Xml.XmlNode" /> objects that represents the appinfo child nodes.</summary>
		/// <returns>An array of <see cref="T:System.Xml.XmlNode" /> objects that represents the appinfo child nodes.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x000BE4DA File Offset: 0x000BC6DA
		// (set) Token: 0x06002011 RID: 8209 RVA: 0x000BE4E2 File Offset: 0x000BC6E2
		[XmlText]
		[XmlAnyElement]
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

		// Token: 0x04000F00 RID: 3840
		private string source;

		// Token: 0x04000F01 RID: 3841
		private XmlNode[] markup;
	}
}
