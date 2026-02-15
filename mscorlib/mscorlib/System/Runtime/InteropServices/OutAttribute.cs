using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that data should be marshaled from callee back to caller.</summary>
	// Token: 0x02000535 RID: 1333
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class OutAttribute : Attribute
	{
	}
}
