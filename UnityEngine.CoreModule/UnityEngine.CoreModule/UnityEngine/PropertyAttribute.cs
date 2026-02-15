using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200015C RID: 348
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public abstract class PropertyAttribute : Attribute
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00020144 File Offset: 0x0001E344
		protected PropertyAttribute()
			: this(false)
		{
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002014F File Offset: 0x0001E34F
		protected PropertyAttribute(bool applyToCollection)
		{
			this.<applyToCollection>k__BackingField = applyToCollection;
		}
	}
}
