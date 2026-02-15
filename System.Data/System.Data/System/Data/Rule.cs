using System;

namespace System.Data
{
	/// <summary>Indicates the action that occurs when a <see cref="T:System.Data.ForeignKeyConstraint" /> is enforced.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000094 RID: 148
	public enum Rule
	{
		/// <summary>No action taken on related rows.</summary>
		// Token: 0x040002EA RID: 746
		None,
		/// <summary>Delete or update related rows. This is the default.</summary>
		// Token: 0x040002EB RID: 747
		Cascade,
		/// <summary>Set values in related rows to DBNull.</summary>
		// Token: 0x040002EC RID: 748
		SetNull,
		/// <summary>Set values in related rows to the value contained in the <see cref="P:System.Data.DataColumn.DefaultValue" /> property.</summary>
		// Token: 0x040002ED RID: 749
		SetDefault
	}
}
