using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.Clear" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004E RID: 78
	public sealed class DataTableClearEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableClearEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</param>
		// Token: 0x060004C2 RID: 1218 RVA: 0x000180E0 File Offset: 0x000162E0
		public DataTableClearEventArgs(DataTable dataTable)
		{
			this.<Table>k__BackingField = dataTable;
		}
	}
}
