using System;

namespace System.Data
{
	/// <summary>Describes the version of data in a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005A RID: 90
	[Flags]
	public enum DataViewRowState
	{
		/// <summary>None.</summary>
		// Token: 0x040001BB RID: 443
		None = 0,
		/// <summary>An unchanged row.</summary>
		// Token: 0x040001BC RID: 444
		Unchanged = 2,
		/// <summary>A new row.</summary>
		// Token: 0x040001BD RID: 445
		Added = 4,
		/// <summary>A deleted row.</summary>
		// Token: 0x040001BE RID: 446
		Deleted = 8,
		/// <summary>A current version of original data that has been modified (see ModifiedOriginal).</summary>
		// Token: 0x040001BF RID: 447
		ModifiedCurrent = 16,
		/// <summary>The original version of the data that was modified. (Although the data has since been modified, it is available as ModifiedCurrent).</summary>
		// Token: 0x040001C0 RID: 448
		ModifiedOriginal = 32,
		/// <summary>Original rows including unchanged and deleted rows.</summary>
		// Token: 0x040001C1 RID: 449
		OriginalRows = 42,
		/// <summary>Current rows including unchanged, new, and modified rows. By default, <see cref="T:System.Data.DataViewRowState" /> is set to CurrentRows.</summary>
		// Token: 0x040001C2 RID: 450
		CurrentRows = 22
	}
}
