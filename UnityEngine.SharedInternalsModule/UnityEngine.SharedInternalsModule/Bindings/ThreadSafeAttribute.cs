using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Method)]
	[VisibleToOtherModules]
	internal class ThreadSafeAttribute : NativeMethodAttribute
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000023A9 File Offset: 0x000005A9
		public ThreadSafeAttribute()
		{
			base.IsThreadSafe = true;
		}
	}
}
