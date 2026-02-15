using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for deserialization of types from SOAP-encoded, non-root XML data. </summary>
	/// <returns>The object returned by the callback.</returns>
	// Token: 0x020001CE RID: 462
	// (Invoke) Token: 0x060016CD RID: 5837
	public delegate object XmlSerializationReadCallback();
}
