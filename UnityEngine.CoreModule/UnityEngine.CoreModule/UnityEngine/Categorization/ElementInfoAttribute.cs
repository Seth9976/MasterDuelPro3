using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Categorization
{
	// Token: 0x0200023A RID: 570
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class ElementInfoAttribute : Attribute
	{
		// Token: 0x17000330 RID: 816
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0002B7A6 File Offset: 0x000299A6
		public int Order
		{
			[CompilerGenerated]
			set
			{
				this.<Order>k__BackingField = value;
			}
		} = int.MaxValue;

		// Token: 0x06001495 RID: 5269 RVA: 0x0002B7AF File Offset: 0x000299AF
		public ElementInfoAttribute()
		{
			this.<Name>k__BackingField = null;
			base..ctor();
		}
	}
}
