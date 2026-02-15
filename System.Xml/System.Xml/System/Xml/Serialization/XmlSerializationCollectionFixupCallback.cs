using System;

namespace System.Xml.Serialization
{
	/// <summary>Delegate used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class for deserialization of SOAP-encoded XML data types that map to collections or enumerations. </summary>
	/// <param name="collection">The collection into which the collection items array is copied.</param>
	/// <param name="collectionItems">An array of items to be copied into the <paramref name="object collection" />.</param>
	// Token: 0x020001CD RID: 461
	// (Invoke) Token: 0x060016C9 RID: 5833
	public delegate void XmlSerializationCollectionFixupCallback(object collection, object collectionItems);
}
