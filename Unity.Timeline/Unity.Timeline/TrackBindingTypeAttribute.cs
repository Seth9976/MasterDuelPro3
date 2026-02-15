using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000062 RID: 98
	[AttributeUsage(AttributeTargets.Class)]
	public class TrackBindingTypeAttribute : Attribute
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000A48A File Offset: 0x0000868A
		public TrackBindingTypeAttribute(Type type)
		{
			this.type = type;
			this.flags = TrackBindingFlags.AllowCreateComponent;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A4A0 File Offset: 0x000086A0
		public TrackBindingTypeAttribute(Type type, TrackBindingFlags flags)
		{
			this.type = type;
			this.flags = flags;
		}

		// Token: 0x04000163 RID: 355
		public readonly Type type;

		// Token: 0x04000164 RID: 356
		public readonly TrackBindingFlags flags;
	}
}
