using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200049C RID: 1180
	[Obsolete("BaseUxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public abstract class BaseUxmlTraits
	{
		// Token: 0x060021FD RID: 8701 RVA: 0x0007C7CA File Offset: 0x0007A9CA
		protected BaseUxmlTraits()
		{
			this.canHaveAnyAttribute = true;
		}

		// Token: 0x17000907 RID: 2311
		// (set) Token: 0x060021FE RID: 8702 RVA: 0x0007C7DC File Offset: 0x0007A9DC
		protected bool canHaveAnyAttribute
		{
			[CompilerGenerated]
			set
			{
				this.<canHaveAnyAttribute>k__BackingField = value;
			}
		}
	}
}
