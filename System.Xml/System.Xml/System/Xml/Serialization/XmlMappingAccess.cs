using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies whether a mapping is read, write, or both.</summary>
	// Token: 0x020001B2 RID: 434
	[Flags]
	public enum XmlMappingAccess
	{
		/// <summary>Both read and write methods are generated.</summary>
		// Token: 0x04000974 RID: 2420
		None = 0,
		/// <summary>Read methods are generated.</summary>
		// Token: 0x04000975 RID: 2421
		Read = 1,
		/// <summary>Write methods are generated.</summary>
		// Token: 0x04000976 RID: 2422
		Write = 2
	}
}
