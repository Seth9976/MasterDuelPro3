using System;

namespace System.Data
{
	/// <summary>Describes an action performed on a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000042 RID: 66
	[Flags]
	public enum DataRowAction
	{
		/// <summary>The row has not changed.</summary>
		// Token: 0x04000160 RID: 352
		Nothing = 0,
		/// <summary>The row was deleted from the table.</summary>
		// Token: 0x04000161 RID: 353
		Delete = 1,
		/// <summary>The row has changed.</summary>
		// Token: 0x04000162 RID: 354
		Change = 2,
		/// <summary>The most recent change to the row has been rolled back.</summary>
		// Token: 0x04000163 RID: 355
		Rollback = 4,
		/// <summary>The changes to the row have been committed.</summary>
		// Token: 0x04000164 RID: 356
		Commit = 8,
		/// <summary>The row has been added to the table.</summary>
		// Token: 0x04000165 RID: 357
		Add = 16,
		/// <summary>The original version of the row has been changed.</summary>
		// Token: 0x04000166 RID: 358
		ChangeOriginal = 32,
		/// <summary>The original and the current versions of the row have been changed.</summary>
		// Token: 0x04000167 RID: 359
		ChangeCurrentAndOriginal = 64
	}
}
