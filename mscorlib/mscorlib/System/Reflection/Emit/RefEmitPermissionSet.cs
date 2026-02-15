using System;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000653 RID: 1619
	internal struct RefEmitPermissionSet
	{
		// Token: 0x060030D9 RID: 12505 RVA: 0x000B815A File Offset: 0x000B635A
		public RefEmitPermissionSet(SecurityAction action, string pset)
		{
			this.action = action;
			this.pset = pset;
		}

		// Token: 0x040018E0 RID: 6368
		public SecurityAction action;

		// Token: 0x040018E1 RID: 6369
		public string pset;
	}
}
