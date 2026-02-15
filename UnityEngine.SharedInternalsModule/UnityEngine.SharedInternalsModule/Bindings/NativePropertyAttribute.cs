using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class NativePropertyAttribute : NativeMethodAttribute
	{
		// Token: 0x1700000D RID: 13
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000228A File Offset: 0x0000048A
		public TargetType TargetType
		{
			[CompilerGenerated]
			set
			{
				this.<TargetType>k__BackingField = value;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002293 File Offset: 0x00000493
		public NativePropertyAttribute()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000229D File Offset: 0x0000049D
		public NativePropertyAttribute(string name)
			: base(name)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022A8 File Offset: 0x000004A8
		public NativePropertyAttribute(string name, bool isFree, TargetType targetType)
			: base(name, isFree)
		{
			this.TargetType = targetType;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022BC File Offset: 0x000004BC
		public NativePropertyAttribute(string name, bool isFree, TargetType targetType, bool isThreadSafe)
			: base(name, isFree, isThreadSafe)
		{
			this.TargetType = targetType;
		}
	}
}
