using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002D3 RID: 723
	internal enum CausalityRelation
	{
		// Token: 0x04000C45 RID: 3141
		AssignDelegate,
		// Token: 0x04000C46 RID: 3142
		Join,
		// Token: 0x04000C47 RID: 3143
		Choice,
		// Token: 0x04000C48 RID: 3144
		Cancel,
		// Token: 0x04000C49 RID: 3145
		Error
	}
}
