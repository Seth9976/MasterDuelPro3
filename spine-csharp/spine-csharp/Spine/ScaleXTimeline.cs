using System;

namespace Spine
{
	// Token: 0x02000016 RID: 22
	public class ScaleXTimeline : CurveTimeline1, IBoneTimeline
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003948 File Offset: 0x00001B48
		public ScaleXTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 3.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000397E File Offset: 0x00001B7E
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003988 File Offset: 0x00001B88
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (bone.active)
			{
				bone.scaleX = base.GetScaleValue(time, alpha, blend, direction, bone.scaleX, bone.data.scaleX);
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly int boneIndex;
	}
}
