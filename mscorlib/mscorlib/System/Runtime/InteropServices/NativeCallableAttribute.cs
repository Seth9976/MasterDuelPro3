using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000518 RID: 1304
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class NativeCallableAttribute : Attribute
	{
		// Token: 0x040014EB RID: 5355
		public CallingConvention CallingConvention;
	}
}
