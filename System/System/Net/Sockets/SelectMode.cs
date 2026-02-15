using System;

namespace System.Net.Sockets
{
	/// <summary>Defines the polling modes for the <see cref="M:System.Net.Sockets.Socket.Poll(System.Int32,System.Net.Sockets.SelectMode)" /> method.</summary>
	// Token: 0x020004B7 RID: 1207
	public enum SelectMode
	{
		/// <summary>Read status mode.</summary>
		// Token: 0x040014A2 RID: 5282
		SelectRead,
		/// <summary>Write status mode.</summary>
		// Token: 0x040014A3 RID: 5283
		SelectWrite,
		/// <summary>Error status mode.</summary>
		// Token: 0x040014A4 RID: 5284
		SelectError
	}
}
