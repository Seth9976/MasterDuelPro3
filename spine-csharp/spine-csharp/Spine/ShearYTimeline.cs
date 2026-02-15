using System;

namespace Spine
{
	// Token: 0x0200001A RID: 26
	public class ShearYTimeline : CurveTimeline1, IBoneTimeline
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003D38 File Offset: 0x00001F38
		public ShearYTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 6.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003D6E File Offset: 0x00001F6E
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003D78 File Offset: 0x00001F78
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (bone.active)
			{
				bone.shearY = base.GetRelativeValue(time, alpha, blend, bone.shearY, bone.data.shearY);
			}
		}

		// Token: 0x0400005B RID: 91
		private readonly int boneIndex;
	}
}
