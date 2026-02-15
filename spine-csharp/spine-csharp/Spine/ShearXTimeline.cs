using System;

namespace Spine
{
	// Token: 0x02000019 RID: 25
	public class ShearXTimeline : CurveTimeline1, IBoneTimeline
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003CAC File Offset: 0x00001EAC
		public ShearXTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 5.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003CE2 File Offset: 0x00001EE2
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003CEC File Offset: 0x00001EEC
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (bone.active)
			{
				bone.shearX = base.GetRelativeValue(time, alpha, blend, bone.shearX, bone.data.shearX);
			}
		}

		// Token: 0x0400005A RID: 90
		private readonly int boneIndex;
	}
}
