using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class TrackClipTypeAttribute : Attribute
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000A462 File Offset: 0x00008662
		public TrackClipTypeAttribute(Type clipClass)
		{
			this.inspectedType = clipClass;
			this.allowAutoCreate = true;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A478 File Offset: 0x00008678
		public TrackClipTypeAttribute(Type clipClass, bool allowAutoCreate)
		{
			this.inspectedType = clipClass;
		}

		// Token: 0x0400015D RID: 349
		public readonly Type inspectedType;

		// Token: 0x0400015E RID: 350
		public readonly bool allowAutoCreate;
	}
}
