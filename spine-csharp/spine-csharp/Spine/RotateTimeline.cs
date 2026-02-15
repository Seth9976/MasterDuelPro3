using System;

namespace Spine
{
	// Token: 0x02000011 RID: 17
	public class RotateTimeline : CurveTimeline1, IBoneTimeline
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003124 File Offset: 0x00001324
		public RotateTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 0.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000315A File Offset: 0x0000135A
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003164 File Offset: 0x00001364
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (bone.active)
			{
				bone.rotation = base.GetRelativeValue(time, alpha, blend, bone.rotation, bone.data.rotation);
			}
		}

		// Token: 0x04000052 RID: 82
		private readonly int boneIndex;
	}
}
