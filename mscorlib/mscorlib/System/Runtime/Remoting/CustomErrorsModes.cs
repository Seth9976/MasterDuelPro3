using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Specifies how custom errors are handled.</summary>
	// Token: 0x02000411 RID: 1041
	[ComVisible(true)]
	public enum CustomErrorsModes
	{
		/// <summary>All callers receive filtered exception information.</summary>
		// Token: 0x040010E1 RID: 4321
		On,
		/// <summary>All callers receive complete exception information.</summary>
		// Token: 0x040010E2 RID: 4322
		Off,
		/// <summary>Local callers receive complete exception information; remote callers receive filtered exception information.</summary>
		// Token: 0x040010E3 RID: 4323
		RemoteOnly
	}
}
