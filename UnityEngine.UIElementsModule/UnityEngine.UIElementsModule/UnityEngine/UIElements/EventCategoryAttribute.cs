using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004DA RID: 1242
	[AttributeUsage(AttributeTargets.Class)]
	internal class EventCategoryAttribute : Attribute
	{
		// Token: 0x060022ED RID: 8941 RVA: 0x0008056B File Offset: 0x0007E76B
		public EventCategoryAttribute(EventCategory category)
		{
			this.category = category;
		}

		// Token: 0x04000FD4 RID: 4052
		internal EventCategory category;
	}
}
