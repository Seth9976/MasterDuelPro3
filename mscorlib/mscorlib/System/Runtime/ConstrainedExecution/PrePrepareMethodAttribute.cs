using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Instructs the native image generation service to prepare a method for inclusion in a constrained execution region (CER).</summary>
	// Token: 0x02000575 RID: 1397
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	public sealed class PrePrepareMethodAttribute : Attribute
	{
	}
}
