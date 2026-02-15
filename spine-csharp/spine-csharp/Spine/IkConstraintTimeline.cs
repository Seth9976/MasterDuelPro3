using System;

namespace Spine
{
	// Token: 0x02000025 RID: 37
	public class IkConstraintTimeline : CurveTimeline
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00005B88 File Offset: 0x00003D88
		public IkConstraintTimeline(int frameCount, int bezierCount, int ikConstraintIndex)
			: base(frameCount, bezierCount, new string[] { 15.ToString() + "|" + ikConstraintIndex.ToString() })
		{
			this.constraintIndex = ikConstraintIndex;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public override int FrameEntries
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00005BCB File Offset: 0x00003DCB
		public int IkConstraintIndex
		{
			get
			{
				return this.constraintIndex;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public void SetFrame(int frame, float time, float mix, float softness, int bendDirection, bool compress, bool stretch)
		{
			frame *= 6;
			this.frames[frame] = time;
			this.frames[frame + 1] = mix;
			this.frames[frame + 2] = softness;
			this.frames[frame + 3] = (float)bendDirection;
			this.frames[frame + 4] = (float)(compress ? 1 : 0);
			this.frames[frame + 5] = (float)(stretch ? 1 : 0);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005C3C File Offset: 0x00003E3C
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			IkConstraint constraint = skeleton.ikConstraints.Items[this.constraintIndex];
			if (!constraint.active)
			{
				return;
			}
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					constraint.mix = constraint.data.mix;
					constraint.softness = constraint.data.softness;
					constraint.bendDirection = constraint.data.bendDirection;
					constraint.compress = constraint.data.compress;
					constraint.stretch = constraint.data.stretch;
					return;
				}
				if (blend != MixBlend.First)
				{
					return;
				}
				constraint.mix += (constraint.data.mix - constraint.mix) * alpha;
				constraint.softness += (constraint.data.softness - constraint.softness) * alpha;
				constraint.bendDirection = constraint.data.bendDirection;
				constraint.compress = constraint.data.compress;
				constraint.stretch = constraint.data.stretch;
				return;
			}
			else
			{
				int i = Timeline.Search(frames, time, 6);
				int curveType = (int)this.curves[i / 6];
				float mix;
				float softness;
				if (curveType != 0)
				{
					if (curveType != 1)
					{
						mix = base.GetBezierValue(time, i, 1, curveType - 2);
						softness = base.GetBezierValue(time, i, 2, curveType + 18 - 2);
					}
					else
					{
						mix = frames[i + 1];
						softness = frames[i + 2];
					}
				}
				else
				{
					float before = frames[i];
					mix = frames[i + 1];
					softness = frames[i + 2];
					float t = (time - before) / (frames[i + 6] - before);
					mix += (frames[i + 6 + 1] - mix) * t;
					softness += (frames[i + 6 + 2] - softness) * t;
				}
				if (blend != MixBlend.Setup)
				{
					constraint.mix += (mix - constraint.mix) * alpha;
					constraint.softness += (softness - constraint.softness) * alpha;
					if (direction == MixDirection.In)
					{
						constraint.bendDirection = (int)frames[i + 3];
						constraint.compress = frames[i + 4] != 0f;
						constraint.stretch = frames[i + 5] != 0f;
					}
					return;
				}
				constraint.mix = constraint.data.mix + (mix - constraint.data.mix) * alpha;
				constraint.softness = constraint.data.softness + (softness - constraint.data.softness) * alpha;
				if (direction == MixDirection.Out)
				{
					constraint.bendDirection = constraint.data.bendDirection;
					constraint.compress = constraint.data.compress;
					constraint.stretch = constraint.data.stretch;
					return;
				}
				constraint.bendDirection = (int)frames[i + 3];
				constraint.compress = frames[i + 4] != 0f;
				constraint.stretch = frames[i + 5] != 0f;
				return;
			}
		}

		// Token: 0x04000085 RID: 133
		public const int ENTRIES = 6;

		// Token: 0x04000086 RID: 134
		private const int MIX = 1;

		// Token: 0x04000087 RID: 135
		private const int SOFTNESS = 2;

		// Token: 0x04000088 RID: 136
		private const int BEND_DIRECTION = 3;

		// Token: 0x04000089 RID: 137
		private const int COMPRESS = 4;

		// Token: 0x0400008A RID: 138
		private const int STRETCH = 5;

		// Token: 0x0400008B RID: 139
		private readonly int constraintIndex;
	}
}
