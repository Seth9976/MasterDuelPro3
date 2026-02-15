using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the include element from XML Schema as specified by the World Wide Web Consortium (W3C). This class is used to include declarations and definitions from an external schema. The included declarations and definitions are then available for processing in the containing schema.</summary>
	// Token: 0x020002EA RID: 746
	public class XmlSchemaInclude : XmlSchemaExternal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInclude" /> class.</summary>
		// Token: 0x06002173 RID: 8563 RVA: 0x000C04D0 File Offset: 0x000BE6D0
		public XmlSchemaInclude()
		{
			base.Compositor = Compositor.Include;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000C04DF File Offset: 0x000BE6DF
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04000F9D RID: 3997
		private XmlSchemaAnnotation annotation;
	}
}
