using System;

namespace System.Data
{
	/// <summary>Specifies how a <see cref="T:System.Data.DataColumn" /> is mapped.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007F RID: 127
	public enum MappingType
	{
		/// <summary>The column is mapped to an XML element.</summary>
		// Token: 0x0400028C RID: 652
		Element = 1,
		/// <summary>The column is mapped to an XML attribute.</summary>
		// Token: 0x0400028D RID: 653
		Attribute,
		/// <summary>The column is mapped to an <see cref="T:System.Xml.XmlText" /> node.</summary>
		// Token: 0x0400028E RID: 654
		SimpleContent,
		/// <summary>The column is mapped to an internal structure.</summary>
		// Token: 0x0400028F RID: 655
		Hidden
	}
}
