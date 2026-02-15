using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D9 RID: 1241
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class EventInterestAttribute : Attribute
	{
		// Token: 0x060022EA RID: 8938 RVA: 0x0008053B File Offset: 0x0007E73B
		public EventInterestAttribute(params Type[] eventTypes)
		{
			this.eventTypes = eventTypes;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x00080553 File Offset: 0x0007E753
		public EventInterestAttribute(EventInterestOptions interests)
		{
			this.categoryFlags = (EventCategoryFlags)interests;
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00080553 File Offset: 0x0007E753
		internal EventInterestAttribute(EventInterestOptionsInternal interests)
		{
			this.categoryFlags = (EventCategoryFlags)interests;
		}

		// Token: 0x04000FD2 RID: 4050
		internal Type[] eventTypes;

		// Token: 0x04000FD3 RID: 4051
		internal EventCategoryFlags categoryFlags = EventCategoryFlags.None;
	}
}
