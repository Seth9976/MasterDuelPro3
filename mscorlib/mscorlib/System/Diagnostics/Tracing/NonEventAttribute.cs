using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Identifies a method that is not generating an event.</summary>
	// Token: 0x020006EC RID: 1772
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class NonEventAttribute : Attribute
	{
	}
}
