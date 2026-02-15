using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the section of a security descriptor to be queried or set.</summary>
	// Token: 0x02000409 RID: 1033
	[Flags]
	public enum SecurityInfos
	{
		/// <summary>Specifies the owner identifier.</summary>
		// Token: 0x040010D6 RID: 4310
		Owner = 1,
		/// <summary>Specifies the primary group identifier.</summary>
		// Token: 0x040010D7 RID: 4311
		Group = 2,
		/// <summary>Specifies the discretionary access control list (DACL).</summary>
		// Token: 0x040010D8 RID: 4312
		DiscretionaryAcl = 4,
		/// <summary>Specifies the system access control list (SACL).</summary>
		// Token: 0x040010D9 RID: 4313
		SystemAcl = 8
	}
}
