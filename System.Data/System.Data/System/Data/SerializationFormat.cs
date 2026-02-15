using System;

namespace System.Data
{
	/// <summary>Determines the serialization format for a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004C RID: 76
	public enum SerializationFormat
	{
		/// <summary>Serialize as XML content. The default.</summary>
		// Token: 0x0400017E RID: 382
		Xml,
		/// <summary>Serialize as binary content. Available in ADO.NET 2.0 only.</summary>
		// Token: 0x0400017F RID: 383
		Binary
	}
}
