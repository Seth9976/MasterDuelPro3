using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.NewRow" /> method.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000051 RID: 81
	public sealed class DataTableNewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Data.DataTableNewRowEventArgs" />.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> being added.</param>
		// Token: 0x060004E6 RID: 1254 RVA: 0x00018CD7 File Offset: 0x00016ED7
		public DataTableNewRowEventArgs(DataRow dataRow)
		{
			this.<Row>k__BackingField = dataRow;
		}
	}
}
