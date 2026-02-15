using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006EE RID: 1774
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class NotNullAttribute : Attribute
	{
	}
}
