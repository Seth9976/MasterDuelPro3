using System;

namespace System.Data.Common
{
	/// <summary>Indicates the position of the catalog name in a qualified table name in a text command. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DC RID: 220
	public enum CatalogLocation
	{
		/// <summary>Indicates that the position of the catalog name occurs before the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x040004A9 RID: 1193
		Start = 1,
		/// <summary>Indicates that the position of the catalog name occurs after the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x040004AA RID: 1194
		End
	}
}
