using System;

namespace Mono.Interop
{
	// Token: 0x02000052 RID: 82
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public MonoPInvokeCallbackAttribute(Type t)
		{
		}
	}
}
