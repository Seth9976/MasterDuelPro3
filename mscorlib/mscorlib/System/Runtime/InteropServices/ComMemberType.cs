using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the type of a COM member.</summary>
	// Token: 0x0200051C RID: 1308
	public enum ComMemberType
	{
		/// <summary>The member is a normal method.</summary>
		// Token: 0x040014F0 RID: 5360
		Method,
		/// <summary>The member gets properties.</summary>
		// Token: 0x040014F1 RID: 5361
		PropGet,
		/// <summary>The member sets properties.</summary>
		// Token: 0x040014F2 RID: 5362
		PropSet
	}
}
