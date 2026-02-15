using System;

namespace System.Security
{
	/// <summary>Defines the integer values corresponding to security zones used by security policy.</summary>
	// Token: 0x02000315 RID: 789
	public enum SecurityZone
	{
		/// <summary>The Internet zone is used for the Web sites on the Internet that do not belong to another zone.</summary>
		// Token: 0x04000D06 RID: 3334
		Internet = 3,
		/// <summary>The local intranet zone is used for content located on a company's intranet. Because the servers and information would be within a company's firewall, a user or company could assign a higher trust level to the content on the intranet.</summary>
		// Token: 0x04000D07 RID: 3335
		Intranet = 1,
		/// <summary>The local computer zone is an implicit zone used for content that exists on the user's computer.</summary>
		// Token: 0x04000D08 RID: 3336
		MyComputer = 0,
		/// <summary>No zone is specified.</summary>
		// Token: 0x04000D09 RID: 3337
		NoZone = -1,
		/// <summary>The trusted sites zone is used for content located on Web sites considered more reputable or trustworthy than other sites on the Internet. Users can use this zone to assign a higher trust level to these sites to minimize the number of authentication requests. The URLs of these trusted Web sites need to be mapped into this zone by the user.</summary>
		// Token: 0x04000D0A RID: 3338
		Trusted = 2,
		/// <summary>The restricted sites zone is used for Web sites with content that could cause, or could have caused, problems when downloaded. The URLs of these untrusted Web sites need to be mapped into this zone by the user.</summary>
		// Token: 0x04000D0B RID: 3339
		Untrusted = 4
	}
}
