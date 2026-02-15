using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the simpleContent element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is for simple and complex types with simple content model.</summary>
	// Token: 0x020002FC RID: 764
	public class XmlSchemaSimpleContent : XmlSchemaContentModel
	{
		/// <summary>Gets one of the <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentRestriction" /> or <see cref="T:System.Xml.Schema.XmlSchemaSimpleContentExtension" />.</summary>
		/// <returns>The content contained within the XmlSchemaSimpleContentRestriction or XmlSchemaSimpleContentExtension class.</returns>
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x000C2DFD File Offset: 0x000C0FFD
		// (set) Token: 0x0600222C RID: 8748 RVA: 0x000C2E05 File Offset: 0x000C1005
		[XmlElement("restriction", typeof(XmlSchemaSimpleContentRestriction))]
		[XmlElement("extension", typeof(XmlSchemaSimpleContentExtension))]
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

		// Token: 0x04000FE3 RID: 4067
		private XmlSchemaContent content;
	}
}
