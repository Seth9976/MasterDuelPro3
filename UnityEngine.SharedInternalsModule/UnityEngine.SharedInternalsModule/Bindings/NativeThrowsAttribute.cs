using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class NativeThrowsAttribute : Attribute
	{
		// Token: 0x17000013 RID: 19
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000023F9 File Offset: 0x000005F9
		public bool ThrowsException
		{
			[CompilerGenerated]
			set
			{
				this.<ThrowsException>k__BackingField = value;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002402 File Offset: 0x00000602
		public NativeThrowsAttribute()
		{
			this.ThrowsException = true;
		}
	}
}
