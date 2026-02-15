using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the import element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is used to import schema components from other schemas.</summary>
	// Token: 0x020002E9 RID: 745
	public class XmlSchemaImport : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaImport" /> class.</summary>
		// Token: 0x0600216F RID: 8559 RVA: 0x000C04A7 File Offset: 0x000BE6A7
		public XmlSchemaImport()
		{
			base.Compositor = Compositor.Import;
		}

		/// <summary>Gets or sets the target namespace for the imported schema as a Uniform Resource Identifier (URI) reference.</summary>
		/// <returns>The target namespace for the imported schema as a URI reference.Optional.</returns>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x000C04B6 File Offset: 0x000BE6B6
		// (set) Token: 0x06002171 RID: 8561 RVA: 0x000C04BE File Offset: 0x000BE6BE
		[XmlAttribute("namespace", DataType = "anyURI")]
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

		// Token: 0x06002172 RID: 8562 RVA: 0x000C04C7 File Offset: 0x000BE6C7
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04000F9B RID: 3995
		private string ns;

		// Token: 0x04000F9C RID: 3996
		private XmlSchemaAnnotation annotation;
	}
}
