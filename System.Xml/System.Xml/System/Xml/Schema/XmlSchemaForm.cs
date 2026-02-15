using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Indicates if attributes or elements need to be qualified with a namespace prefix.</summary>
	// Token: 0x020002E0 RID: 736
	public enum XmlSchemaForm
	{
		/// <summary>Element and attribute form is not specified in the schema.</summary>
		// Token: 0x04000F88 RID: 3976
		[XmlIgnore]
		None,
		/// <summary>Elements and attributes must be qualified with a namespace prefix.</summary>
		// Token: 0x04000F89 RID: 3977
		[XmlEnum("qualified")]
		Qualified,
		/// <summary>Elements and attributes are not required to be qualified with a namespace prefix.</summary>
		// Token: 0x04000F8A RID: 3978
		[XmlEnum("unqualified")]
		Unqualified
	}
}
