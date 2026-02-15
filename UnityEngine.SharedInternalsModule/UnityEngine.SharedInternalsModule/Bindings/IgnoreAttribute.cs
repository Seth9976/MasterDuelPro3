using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Field)]
	[VisibleToOtherModules]
	internal class IgnoreAttribute : Attribute
	{
		// Token: 0x17000014 RID: 20
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002414 File Offset: 0x00000614
		public bool DoesNotContributeToSize
		{
			[CompilerGenerated]
			set
			{
				this.<DoesNotContributeToSize>k__BackingField = value;
			}
		}
	}
}
