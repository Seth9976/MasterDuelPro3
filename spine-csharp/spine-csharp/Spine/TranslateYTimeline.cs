using System;

namespace Spine
{
	// Token: 0x02000014 RID: 20
	public class TranslateYTimeline : CurveTimeline1, IBoneTimeline
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000034C0 File Offset: 0x000016C0
		public TranslateYTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 2.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000034F6 File Offset: 0x000016F6
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003500 File Offset: 0x00001700
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (bone.active)
			{
				bone.y = base.GetRelativeValue(time, alpha, blend, bone.y, bone.data.y);
			}
		}

		// Token: 0x04000055 RID: 85
		private readonly int boneIndex;
	}
}
