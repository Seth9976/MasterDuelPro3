using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Provides information about the validation mode of any and anyAttribute element replacements.</summary>
	// Token: 0x020002C9 RID: 713
	public enum XmlSchemaContentProcessing
	{
		/// <summary>Document items are not validated.</summary>
		// Token: 0x04000F3E RID: 3902
		[XmlIgnore]
		None,
		/// <summary>Document items must consist of well-formed XML and are not validated by the schema.</summary>
		// Token: 0x04000F3F RID: 3903
		[XmlEnum("skip")]
		Skip,
		/// <summary>If the associated schema is found, the document items will be validated. No errors will be thrown otherwise.</summary>
		// Token: 0x04000F40 RID: 3904
		[XmlEnum("lax")]
		Lax,
		/// <summary>The schema processor must find a schema associated with the indicated namespace to validate the document items.</summary>
		// Token: 0x04000F41 RID: 3905
		[XmlEnum("strict")]
		Strict
	}
}
