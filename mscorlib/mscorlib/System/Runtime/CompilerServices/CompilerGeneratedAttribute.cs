using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Distinguishes a compiler-generated element from a user-generated element. This class cannot be inherited.</summary>
	// Token: 0x0200057E RID: 1406
	[AttributeUsage(AttributeTargets.All, Inherited = true)]
	[Serializable]
	public sealed class CompilerGeneratedAttribute : Attribute
	{
	}
}
