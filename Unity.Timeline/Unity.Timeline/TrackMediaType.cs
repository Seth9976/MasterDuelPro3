using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200005E RID: 94
	[AttributeUsage(AttributeTargets.Class)]
	[Obsolete("TrackMediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
	public class TrackMediaType : Attribute
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000A453 File Offset: 0x00008653
		public TrackMediaType(TimelineAsset.MediaType mt)
		{
			this.m_MediaType = mt;
		}

		// Token: 0x0400015C RID: 348
		public readonly TimelineAsset.MediaType m_MediaType;
	}
}
