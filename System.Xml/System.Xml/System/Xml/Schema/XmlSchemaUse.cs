using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Indicator of how the attribute is used.</summary>
	// Token: 0x02000307 RID: 775
	public enum XmlSchemaUse
	{
		/// <summary>Attribute use not specified.</summary>
		// Token: 0x04001004 RID: 4100
		[XmlIgnore]
		None,
		/// <summary>Attribute is optional.</summary>
		// Token: 0x04001005 RID: 4101
		[XmlEnum("optional")]
		Optional,
		/// <summary>Attribute cannot be used.</summary>
		// Token: 0x04001006 RID: 4102
		[XmlEnum("prohibited")]
		Prohibited,
		/// <summary>Attribute must appear once.</summary>
		// Token: 0x04001007 RID: 4103
		[XmlEnum("required")]
		Required
	}
}
