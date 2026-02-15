using System;

namespace System.Xml
{
	/// <summary>Represents the XML type for the string. This allows the string to be read as a particular XML type, for example a CDATA section type.</summary>
	// Token: 0x0200010F RID: 271
	public enum XmlTokenizedType
	{
		/// <summary>CDATA type.</summary>
		// Token: 0x04000714 RID: 1812
		CDATA,
		/// <summary>ID type.</summary>
		// Token: 0x04000715 RID: 1813
		ID,
		/// <summary>IDREF type.</summary>
		// Token: 0x04000716 RID: 1814
		IDREF,
		/// <summary>IDREFS type.</summary>
		// Token: 0x04000717 RID: 1815
		IDREFS,
		/// <summary>ENTITY type.</summary>
		// Token: 0x04000718 RID: 1816
		ENTITY,
		/// <summary>ENTITIES type.</summary>
		// Token: 0x04000719 RID: 1817
		ENTITIES,
		/// <summary>NMTOKEN type.</summary>
		// Token: 0x0400071A RID: 1818
		NMTOKEN,
		/// <summary>NMTOKENS type.</summary>
		// Token: 0x0400071B RID: 1819
		NMTOKENS,
		/// <summary>NOTATION type.</summary>
		// Token: 0x0400071C RID: 1820
		NOTATION,
		/// <summary>ENUMERATION type.</summary>
		// Token: 0x0400071D RID: 1821
		ENUMERATION,
		/// <summary>QName type.</summary>
		// Token: 0x0400071E RID: 1822
		QName,
		/// <summary>NCName type.</summary>
		// Token: 0x0400071F RID: 1823
		NCName,
		/// <summary>No type.</summary>
		// Token: 0x04000720 RID: 1824
		None
	}
}
