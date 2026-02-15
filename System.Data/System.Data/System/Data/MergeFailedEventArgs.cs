using System;

namespace System.Data
{
	/// <summary>Occurs when a target and source DataRow have the same primary key value, and the <see cref="P:System.Data.DataSet.EnforceConstraints" /> property is set to true.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000080 RID: 128
	public class MergeFailedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.MergeFailedEventArgs" /> class with the <see cref="T:System.Data.DataTable" /> and a description of the merge conflict.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> object. </param>
		/// <param name="conflict">A description of the merge conflict. </param>
		// Token: 0x060006D7 RID: 1751 RVA: 0x00021683 File Offset: 0x0001F883
		public MergeFailedEventArgs(DataTable table, string conflict)
		{
			this.<Table>k__BackingField = table;
			this.Conflict = conflict;
		}

		/// <summary>Returns a description of the merge conflict.</summary>
		/// <returns>A description of the merge conflict.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00021699 File Offset: 0x0001F899
		public string Conflict { get; }
	}
}
