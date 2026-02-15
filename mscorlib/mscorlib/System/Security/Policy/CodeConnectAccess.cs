using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies the network resource access that is granted to code.</summary>
	// Token: 0x02000339 RID: 825
	[ComVisible(true)]
	[Serializable]
	public class CodeConnectAccess
	{
		/// <summary>Contains the string value that represents the scheme wildcard.</summary>
		// Token: 0x04000D80 RID: 3456
		public static readonly string AnyScheme = "*";

		/// <summary>Contains the value used to represent the default port.</summary>
		// Token: 0x04000D81 RID: 3457
		public static readonly int DefaultPort = -3;

		/// <summary>Contains the value used to represent the port value in the URI where code originated.</summary>
		// Token: 0x04000D82 RID: 3458
		public static readonly int OriginPort = -4;

		/// <summary>Contains the value used to represent the scheme in the URL where the code originated.</summary>
		// Token: 0x04000D83 RID: 3459
		public static readonly string OriginScheme = "$origin";
	}
}
