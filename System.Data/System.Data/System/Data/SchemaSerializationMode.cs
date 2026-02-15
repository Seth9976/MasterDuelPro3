using System;

namespace System.Data
{
	/// <summary>Indicates the schema serialization mode for a typed <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x02000095 RID: 149
	public enum SchemaSerializationMode
	{
		/// <summary>Includes schema serialization for a typed <see cref="T:System.Data.DataSet" />. The default.</summary>
		// Token: 0x040002EF RID: 751
		IncludeSchema = 1,
		/// <summary>Skips schema serialization for a typed <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x040002F0 RID: 752
		ExcludeSchema
	}
}
