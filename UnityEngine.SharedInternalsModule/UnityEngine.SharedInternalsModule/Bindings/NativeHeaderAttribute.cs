using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200000B RID: 11
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	internal class NativeHeaderAttribute : Attribute
	{
		// Token: 0x17000006 RID: 6
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002137 File Offset: 0x00000337
		public string Header
		{
			[CompilerGenerated]
			set
			{
				this.<Header>k__BackingField = value;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002068 File Offset: 0x00000268
		public NativeHeaderAttribute()
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002140 File Offset: 0x00000340
		public NativeHeaderAttribute(string header)
		{
			bool flag = header == null;
			if (flag)
			{
				throw new ArgumentNullException("header");
			}
			bool flag2 = header == "";
			if (flag2)
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			this.Header = header;
		}
	}
}
