using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x0200001C RID: 28
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, Inherited = false)]
	internal class UsedByNativeCodeAttribute : Attribute
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002068 File Offset: 0x00000268
		public UsedByNativeCodeAttribute()
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000241D File Offset: 0x0000061D
		public UsedByNativeCodeAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000015 RID: 21
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000242F File Offset: 0x0000062F
		public string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}
	}
}
