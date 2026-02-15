using System;

namespace System.Runtime.Versioning
{
	// Token: 0x020004A1 RID: 1185
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class NonVersionableAttribute : Attribute
	{
	}
}
