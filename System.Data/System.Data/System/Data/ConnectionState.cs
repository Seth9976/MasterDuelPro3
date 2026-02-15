using System;

namespace System.Data
{
	/// <summary>Describes the current state of the connection to a data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002C RID: 44
	[Flags]
	public enum ConnectionState
	{
		/// <summary>The connection is closed.</summary>
		// Token: 0x04000100 RID: 256
		Closed = 0,
		/// <summary>The connection is open.</summary>
		// Token: 0x04000101 RID: 257
		Open = 1,
		/// <summary>The connection object is connecting to the data source.</summary>
		// Token: 0x04000102 RID: 258
		Connecting = 2,
		/// <summary>The connection object is executing a command. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000103 RID: 259
		Executing = 4,
		/// <summary>The connection object is retrieving data. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000104 RID: 260
		Fetching = 8,
		/// <summary>The connection to the data source is broken. This can occur only after the connection has been opened. A connection in this state may be closed and then re-opened. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000105 RID: 261
		Broken = 16
	}
}
