using System;

namespace Spine
{
	// Token: 0x0200001B RID: 27
	public class InheritTimeline : Timeline, IBoneTimeline
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public InheritTimeline(int frameCount, int boneIndex)
			: base(frameCount, new string[] { 7.ToString() + "|" + boneIndex.ToString() })
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003E02 File Offset: 0x00002002
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002E78 File Offset: 0x00001078
		public override int FrameEntries
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003E0A File Offset: 0x0000200A
		public void SetFrame(int frame, float time, Inherit inherit)
		{
			frame *= 2;
			this.frames[frame] = time;
			this.frames[frame + 1] = (float)inherit;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003E28 File Offset: 0x00002028
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (!bone.active)
			{
				return;
			}
			if (direction == MixDirection.Out)
			{
				if (blend == MixBlend.Setup)
				{
					bone.inherit = bone.data.inherit;
				}
				return;
			}
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup || blend == MixBlend.First)
				{
					bone.inherit = bone.data.inherit;
				}
				return;
			}
			bone.inherit = InheritEnum.Values[(int)frames[Timeline.Search(frames, time, 2) + 1]];
		}

		// Token: 0x0400005C RID: 92
		public const int ENTRIES = 2;

		// Token: 0x0400005D RID: 93
		public const int INHERIT = 1;

		// Token: 0x0400005E RID: 94
		private readonly int boneIndex;
	}
}
