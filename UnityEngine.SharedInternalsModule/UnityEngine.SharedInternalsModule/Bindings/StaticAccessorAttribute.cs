using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000018 RID: 24
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class StaticAccessorAttribute : Attribute
	{
		// Token: 0x17000011 RID: 17
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000023BB File Offset: 0x000005BB
		public string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000023C4 File Offset: 0x000005C4
		public StaticAccessorType Type
		{
			[CompilerGenerated]
			set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000023CD File Offset: 0x000005CD
		[VisibleToOtherModules]
		internal StaticAccessorAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000023DF File Offset: 0x000005DF
		public StaticAccessorAttribute(string name, StaticAccessorType type)
		{
			this.Name = name;
			this.Type = type;
		}
	}
}
