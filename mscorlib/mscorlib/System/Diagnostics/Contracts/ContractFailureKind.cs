using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Specifies the type of contract that failed. </summary>
	// Token: 0x020006E1 RID: 1761
	public enum ContractFailureKind
	{
		/// <summary>A <see cref="Overload:System.Diagnostics.Contracts.Contract.Requires" /> contract failed.</summary>
		// Token: 0x04001E05 RID: 7685
		Precondition,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Ensures" /> contract failed. </summary>
		// Token: 0x04001E06 RID: 7686
		Postcondition,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.EnsuresOnThrow" /> contract failed.</summary>
		// Token: 0x04001E07 RID: 7687
		PostconditionOnException,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Invariant" /> contract failed.</summary>
		// Token: 0x04001E08 RID: 7688
		Invariant,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Assert" /> contract failed.</summary>
		// Token: 0x04001E09 RID: 7689
		Assert,
		/// <summary>An <see cref="Overload:System.Diagnostics.Contracts.Contract.Assume" /> contract failed.</summary>
		// Token: 0x04001E0A RID: 7690
		Assume
	}
}
