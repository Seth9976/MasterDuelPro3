using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200000C RID: 12
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	internal class NativeNameAttribute : Attribute
	{
		// Token: 0x17000007 RID: 7
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000218F File Offset: 0x0000038F
		public string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002198 File Offset: 0x00000398
		public NativeNameAttribute(string name)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			bool flag2 = name == "";
			if (flag2)
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			this.Name = name;
		}
	}
}
