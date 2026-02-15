using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Categorization
{
	// Token: 0x0200023B RID: 571
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class CategoryInfoAttribute : Attribute
	{
		// Token: 0x17000331 RID: 817
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0002B7CA File Offset: 0x000299CA
		public int Order
		{
			[CompilerGenerated]
			set
			{
				this.<Order>k__BackingField = value;
			}
		} = int.MaxValue;

		// Token: 0x17000332 RID: 818
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x0002B7D3 File Offset: 0x000299D3
		public string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		} = null;
	}
}
