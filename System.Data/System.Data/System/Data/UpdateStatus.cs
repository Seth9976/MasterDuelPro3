using System;

namespace System.Data
{
	/// <summary>Specifies the action to take with regard to the current and remaining rows during an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B5 RID: 181
	public enum UpdateStatus
	{
		/// <summary>The <see cref="T:System.Data.Common.DataAdapter" /> is to continue proccessing rows.</summary>
		// Token: 0x0400038C RID: 908
		Continue,
		/// <summary>The event handler reports that the update should be treated as an error.</summary>
		// Token: 0x0400038D RID: 909
		ErrorsOccurred,
		/// <summary>The current row is not to be updated.</summary>
		// Token: 0x0400038E RID: 910
		SkipCurrentRow,
		/// <summary>The current row and all remaining rows are not to be updated.</summary>
		// Token: 0x0400038F RID: 911
		SkipAllRemainingRows
	}
}
