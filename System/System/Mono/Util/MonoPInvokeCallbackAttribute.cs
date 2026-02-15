using System;
using System.Diagnostics;

namespace Mono.Util
{
	// Token: 0x02000014 RID: 20
	[Conditional("MONOTOUCH")]
	[Conditional("FULL_AOT_RUNTIME")]
	[Conditional("UNITY")]
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000029F9 File Offset: 0x00000BF9
		public MonoPInvokeCallbackAttribute(Type t)
		{
		}
	}
}
