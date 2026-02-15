using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Specifies the order and structure of the child elements of a type.</summary>
	// Token: 0x020002C8 RID: 712
	public abstract class XmlSchemaContentModel : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the content of the type.</summary>
		/// <returns>Provides the content of the type.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060020AC RID: 8364
		// (set) Token: 0x060020AD RID: 8365
		[XmlIgnore]
		public abstract XmlSchemaContent Content { get; set; }
	}
}
