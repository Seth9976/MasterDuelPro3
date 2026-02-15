using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents the number of 100-nanosecond intervals since January 1, 1601. This structure is a 64-bit value.</summary>
	// Token: 0x0200056A RID: 1386
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the FILETIME.</summary>
		// Token: 0x040015A7 RID: 5543
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the FILETIME.</summary>
		// Token: 0x040015A8 RID: 5544
		public int dwHighDateTime;
	}
}
