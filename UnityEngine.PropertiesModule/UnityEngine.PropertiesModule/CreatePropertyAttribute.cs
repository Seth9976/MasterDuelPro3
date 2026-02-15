using System;
using UnityEngine.Scripting;

namespace Unity.Properties
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class CreatePropertyAttribute : RequiredMemberAttribute
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024DC File Offset: 0x000006DC
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024E4 File Offset: 0x000006E4
		public bool ReadOnly { get; set; } = false;
	}
}
