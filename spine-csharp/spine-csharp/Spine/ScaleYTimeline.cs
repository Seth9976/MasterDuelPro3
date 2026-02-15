using System;

namespace Spine
{
	// Token: 0x02000017 RID: 23
	public class ScaleYTimeline : CurveTimeline1, IBoneTimeline
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000039D4 File Offset: 0x00001BD4
		public ScaleYTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 4.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003A0A File Offset: 0x00001C0A
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003A14 File Offset: 0x00001C14
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (bone.active)
			{
				bone.scaleY = base.GetScaleValue(time, alpha, blend, direction, bone.scaleY, bone.data.scaleY);
			}
		}

		// Token: 0x04000058 RID: 88
		private readonly int boneIndex;
	}
}
