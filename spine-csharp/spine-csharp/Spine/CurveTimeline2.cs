using System;

namespace Spine
{
	// Token: 0x02000010 RID: 16
	public abstract class CurveTimeline2 : CurveTimeline
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000030E1 File Offset: 0x000012E1
		public CurveTimeline2(int frameCount, int bezierCount, string propertyId1, string propertyId2)
			: base(frameCount, bezierCount, new string[] { propertyId1, propertyId2 })
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000030FA File Offset: 0x000012FA
		public override int FrameEntries
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000030FD File Offset: 0x000012FD
		public void SetFrame(int frame, float time, float value1, float value2)
		{
			frame *= 3;
			this.frames[frame] = time;
			this.frames[frame + 1] = value1;
			this.frames[frame + 2] = value2;
		}

		// Token: 0x0400004F RID: 79
		public const int ENTRIES = 3;

		// Token: 0x04000050 RID: 80
		internal const int VALUE1 = 1;

		// Token: 0x04000051 RID: 81
		internal const int VALUE2 = 2;
	}
}
