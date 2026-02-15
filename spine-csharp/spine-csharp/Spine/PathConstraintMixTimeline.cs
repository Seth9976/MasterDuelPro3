using System;

namespace Spine
{
	// Token: 0x02000029 RID: 41
	public class PathConstraintMixTimeline : CurveTimeline
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00006508 File Offset: 0x00004708
		public PathConstraintMixTimeline(int frameCount, int bezierCount, int pathConstraintIndex)
			: base(frameCount, bezierCount, new string[] { 19.ToString() + "|" + pathConstraintIndex.ToString() })
		{
			this.constraintIndex = pathConstraintIndex;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004277 File Offset: 0x00002477
		public override int FrameEntries
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00006548 File Offset: 0x00004748
		public int PathConstraintIndex
		{
			get
			{
				return this.constraintIndex;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004282 File Offset: 0x00002482
		public void SetFrame(int frame, float time, float mixRotate, float mixX, float mixY)
		{
			frame <<= 2;
			this.frames[frame] = time;
			this.frames[frame + 1] = mixRotate;
			this.frames[frame + 2] = mixX;
			this.frames[frame + 3] = mixY;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006550 File Offset: 0x00004750
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			PathConstraint constraint = skeleton.pathConstraints.Items[this.constraintIndex];
			if (!constraint.active)
			{
				return;
			}
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					constraint.mixRotate = constraint.data.mixRotate;
					constraint.mixX = constraint.data.mixX;
					constraint.mixY = constraint.data.mixY;
					return;
				}
				if (blend != MixBlend.First)
				{
					return;
				}
				constraint.mixRotate += (constraint.data.mixRotate - constraint.mixRotate) * alpha;
				constraint.mixX += (constraint.data.mixX - constraint.mixX) * alpha;
				constraint.mixY += (constraint.data.mixY - constraint.mixY) * alpha;
				return;
			}
			else
			{
				int i = Timeline.Search(frames, time, 4);
				int curveType = (int)this.curves[i >> 2];
				float rotate;
				float x;
				float y;
				if (curveType != 0)
				{
					if (curveType != 1)
					{
						rotate = base.GetBezierValue(time, i, 1, curveType - 2);
						x = base.GetBezierValue(time, i, 2, curveType + 18 - 2);
						y = base.GetBezierValue(time, i, 3, curveType + 36 - 2);
					}
					else
					{
						rotate = frames[i + 1];
						x = frames[i + 2];
						y = frames[i + 3];
					}
				}
				else
				{
					float before = frames[i];
					rotate = frames[i + 1];
					x = frames[i + 2];
					y = frames[i + 3];
					float t = (time - before) / (frames[i + 4] - before);
					rotate += (frames[i + 4 + 1] - rotate) * t;
					x += (frames[i + 4 + 2] - x) * t;
					y += (frames[i + 4 + 3] - y) * t;
				}
				if (blend == MixBlend.Setup)
				{
					PathConstraintData data = constraint.data;
					constraint.mixRotate = data.mixRotate + (rotate - data.mixRotate) * alpha;
					constraint.mixX = data.mixX + (x - data.mixX) * alpha;
					constraint.mixY = data.mixY + (y - data.mixY) * alpha;
					return;
				}
				constraint.mixRotate += (rotate - constraint.mixRotate) * alpha;
				constraint.mixX += (x - constraint.mixX) * alpha;
				constraint.mixY += (y - constraint.mixY) * alpha;
				return;
			}
		}

		// Token: 0x04000096 RID: 150
		public const int ENTRIES = 4;

		// Token: 0x04000097 RID: 151
		private const int ROTATE = 1;

		// Token: 0x04000098 RID: 152
		private const int X = 2;

		// Token: 0x04000099 RID: 153
		private const int Y = 3;

		// Token: 0x0400009A RID: 154
		private readonly int constraintIndex;
	}
}
