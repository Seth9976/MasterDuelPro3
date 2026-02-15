using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x0200001D RID: 29
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, Inherited = false)]
	internal class RequiredByNativeCodeAttribute : Attribute
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002068 File Offset: 0x00000268
		public RequiredByNativeCodeAttribute()
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002438 File Offset: 0x00000638
		public RequiredByNativeCodeAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000244A File Offset: 0x0000064A
		public RequiredByNativeCodeAttribute(bool optional)
		{
			this.Optional = optional;
		}

		// Token: 0x17000016 RID: 22
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000245C File Offset: 0x0000065C
		public string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002465 File Offset: 0x00000665
		public bool Optional
		{
			[CompilerGenerated]
			set
			{
				this.<Optional>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000246E File Offset: 0x0000066E
		public bool GenerateProxy
		{
			[CompilerGenerated]
			set
			{
				this.<GenerateProxy>k__BackingField = value;
			}
		}
	}
}
