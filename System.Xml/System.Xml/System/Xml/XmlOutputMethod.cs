using System;

namespace System.Xml
{
	/// <summary>Specifies the method used to serialize the <see cref="T:System.Xml.XmlWriter" /> output. </summary>
	// Token: 0x020000C2 RID: 194
	public enum XmlOutputMethod
	{
		/// <summary>Serialize according to the XML 1.0 rules.</summary>
		// Token: 0x0400056A RID: 1386
		Xml,
		/// <summary>Serialize according to the HTML rules specified by XSLT.</summary>
		// Token: 0x0400056B RID: 1387
		Html,
		/// <summary>Serialize text blocks only.</summary>
		// Token: 0x0400056C RID: 1388
		Text,
		/// <summary>Use the XSLT rules to choose between the <see cref="F:System.Xml.XmlOutputMethod.Xml" /> and <see cref="F:System.Xml.XmlOutputMethod.Html" /> output methods at runtime.</summary>
		// Token: 0x0400056D RID: 1389
		AutoDetect
	}
}
