using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000015 RID: 21
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method)]
	internal class FreeFunctionAttribute : NativeMethodAttribute
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000237E File Offset: 0x0000057E
		public FreeFunctionAttribute()
		{
			base.IsFreeFunction = true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002390 File Offset: 0x00000590
		public FreeFunctionAttribute(string name)
			: base(name, true)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000239C File Offset: 0x0000059C
		public FreeFunctionAttribute(string name, bool isThreadSafe)
			: base(name, true, isThreadSafe)
		{
		}
	}
}
