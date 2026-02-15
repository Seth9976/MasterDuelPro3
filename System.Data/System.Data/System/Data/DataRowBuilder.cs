using System;

namespace System.Data
{
	/// <summary>The DataRowBuilder type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000041 RID: 65
	public sealed class DataRowBuilder
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x00017BA7 File Offset: 0x00015DA7
		internal DataRowBuilder(DataTable table, int record)
		{
			this._table = table;
			this._record = record;
		}

		// Token: 0x0400015D RID: 349
		internal readonly DataTable _table;

		// Token: 0x0400015E RID: 350
		internal int _record;
	}
}
