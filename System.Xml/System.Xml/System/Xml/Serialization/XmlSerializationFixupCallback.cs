using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for deserialization of SOAP-encoded XML data. </summary>
	/// <param name="fixup">An instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.Fixup" /> class that contains the object to be fixed and the array of string identifiers for the items to fill in.</param>
	// Token: 0x020001CC RID: 460
	// (Invoke) Token: 0x060016C5 RID: 5829
	public delegate void XmlSerializationFixupCallback(object fixup);
}
