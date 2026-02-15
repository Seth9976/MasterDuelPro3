using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate that is used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for serialization of types from SOAP-encoded, non-root XML data. </summary>
	/// <param name="o">The object being serialized.</param>
	// Token: 0x020001D7 RID: 471
	// (Invoke) Token: 0x06001808 RID: 6152
	public delegate void XmlSerializationWriteCallback(object o);
}
