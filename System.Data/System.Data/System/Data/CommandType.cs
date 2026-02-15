using System;

namespace System.Data
{
	/// <summary>Specifies how a command string is interpreted.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000029 RID: 41
	public enum CommandType
	{
		/// <summary>An SQL text command. (Default.) </summary>
		// Token: 0x040000F6 RID: 246
		Text = 1,
		/// <summary>The name of a stored procedure.</summary>
		// Token: 0x040000F7 RID: 247
		StoredProcedure = 4,
		/// <summary>The name of a table.</summary>
		// Token: 0x040000F8 RID: 248
		TableDirect = 512
	}
}
