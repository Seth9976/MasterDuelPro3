using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006ED RID: 1773
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	public sealed class DisallowNullAttribute : Attribute
	{
	}
}
