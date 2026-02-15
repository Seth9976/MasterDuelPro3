using System;

namespace System.Security.Policy
{
	/// <summary>Defines special attribute flags for security policy on code groups.</summary>
	// Token: 0x02000335 RID: 821
	[Flags]
	public enum PolicyStatementAttribute
	{
		/// <summary>All attribute flags are set.</summary>
		// Token: 0x04000D75 RID: 3445
		All = 3,
		/// <summary>The exclusive code group flag. When a code group has this flag set, only the permissions associated with that code group are granted to code belonging to the code group. At most, one code group matching a given piece of code can be set as exclusive.</summary>
		// Token: 0x04000D76 RID: 3446
		Exclusive = 1,
		/// <summary>The flag representing a policy statement that causes lower policy levels to not be evaluated as part of the resolve operation, effectively allowing the policy level to override lower levels.</summary>
		// Token: 0x04000D77 RID: 3447
		LevelFinal = 2,
		/// <summary>No flags are set.</summary>
		// Token: 0x04000D78 RID: 3448
		Nothing = 0
	}
}
