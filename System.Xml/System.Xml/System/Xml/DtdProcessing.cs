using System;

namespace System.Xml
{
	/// <summary>Specifies the options for processing DTDs. The <see cref="T:System.Xml.DtdProcessing" /> enumeration is used by <see cref="T:System.Xml.XmlReaderSettings" />.</summary>
	// Token: 0x02000023 RID: 35
	public enum DtdProcessing
	{
		/// <summary>Specifies that when a DTD is encountered, an <see cref="T:System.Xml.XmlException" /> is thrown with a message that states that DTDs are prohibited. This is the default behavior.</summary>
		// Token: 0x04000102 RID: 258
		Prohibit,
		/// <summary>Causes the DOCTYPE element to be ignored. No DTD processing occurs. </summary>
		// Token: 0x04000103 RID: 259
		Ignore,
		/// <summary>Used for parsing DTDs.</summary>
		// Token: 0x04000104 RID: 260
		Parse
	}
}
