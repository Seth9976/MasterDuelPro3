using System;

namespace Spine
{
	// Token: 0x02000013 RID: 19
	public class TranslateXTimeline : CurveTimeline1, IBoneTimeline
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003434 File Offset: 0x00001634
		public TranslateXTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 1.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000346A File Offset: 0x0000166A
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003474 File Offset: 0x00001674
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (bone.active)
			{
				bone.x = base.GetRelativeValue(time, alpha, blend, bone.x, bone.data.x);
			}
		}

		// Token: 0x04000054 RID: 84
		private readonly int boneIndex;
	}
}
