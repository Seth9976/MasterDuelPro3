using System;

namespace System.IO
{
	/// <summary>Specifies the position in a stream to use for seeking.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020007A1 RID: 1953
	public enum SeekOrigin
	{
		/// <summary>Specifies the beginning of a stream.</summary>
		// Token: 0x04001FA3 RID: 8099
		Begin,
		/// <summary>Specifies the current position within a stream.</summary>
		// Token: 0x04001FA4 RID: 8100
		Current,
		/// <summary>Specifies the end of a stream.</summary>
		// Token: 0x04001FA5 RID: 8101
		End
	}
}
