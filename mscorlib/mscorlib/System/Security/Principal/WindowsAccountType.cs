using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies the type of Windows account used.</summary>
	// Token: 0x020003DB RID: 987
	[ComVisible(true)]
	[Serializable]
	public enum WindowsAccountType
	{
		/// <summary>A standard user account.</summary>
		// Token: 0x0400100E RID: 4110
		Normal,
		/// <summary>A Windows guest account.</summary>
		// Token: 0x0400100F RID: 4111
		Guest,
		/// <summary>A Windows system account.</summary>
		// Token: 0x04001010 RID: 4112
		System,
		/// <summary>An anonymous account.</summary>
		// Token: 0x04001011 RID: 4113
		Anonymous
	}
}
