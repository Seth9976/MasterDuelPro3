using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that data should be marshaled from the caller to the callee, but not back to the caller.</summary>
	// Token: 0x02000534 RID: 1332
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class InAttribute : Attribute
	{
	}
}
