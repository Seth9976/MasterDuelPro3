using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Allows an assembly to be called by partially trusted code. Without this declaration, only fully trusted callers are able to use the assembly. This class cannot be inherited.</summary>
	// Token: 0x02000317 RID: 791
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class AllowPartiallyTrustedCallersAttribute : Attribute
	{
	}
}
