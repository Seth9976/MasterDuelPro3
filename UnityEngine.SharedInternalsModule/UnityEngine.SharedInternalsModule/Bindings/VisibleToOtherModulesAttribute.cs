using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[VisibleToOtherModules]
	internal class VisibleToOtherModulesAttribute : Attribute
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002068 File Offset: 0x00000268
		public VisibleToOtherModulesAttribute()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002068 File Offset: 0x00000268
		public VisibleToOtherModulesAttribute(params string[] modules)
		{
		}
	}
}
