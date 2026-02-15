using System;

namespace Spine
{
	// Token: 0x02000026 RID: 38
	public class TransformConstraintTimeline : CurveTimeline
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00005F14 File Offset: 0x00004114
		public TransformConstraintTimeline(int frameCount, int bezierCount, int transformConstraintIndex)
			: base(frameCount, bezierCount, new string[] { 16.ToString() + "|" + transformConstraintIndex.ToString() })
		{
			this.constraintIndex = transformConstraintIndex;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004C01 File Offset: 0x00002E01
		public override int FrameEntries
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005F54 File Offset: 0x00004154
		public int TransformConstraintIndex
		{
			get
			{
				return this.constraintIndex;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005F5C File Offset: 0x0000415C
		public void SetFrame(int frame, float time, float mixRotate, float mixX, float mixY, float mixScaleX, float mixScaleY, float mixShearY)
		{
			frame *= 7;
			this.frames[frame] = time;
			this.frames[frame + 1] = mixRotate;
			this.frames[frame + 2] = mixX;
			this.frames[frame + 3] = mixY;
			this.frames[frame + 4] = mixScaleX;
			this.frames[frame + 5] = mixScaleY;
			this.frames[frame + 6] = mixShearY;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005FC0 File Offset: 0x000041C0
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			TransformConstraint constraint = skeleton.transformConstraints.Items[this.constraintIndex];
			if (!constraint.active)
			{
				return;
			}
			float[] frames = this.frames;
			if (time < frames[0])
			{
				TransformConstraintData data = constraint.data;
				if (blend == MixBlend.Setup)
				{
					constraint.mixRotate = data.mixRotate;
					constraint.mixX = data.mixX;
					constraint.mixY = data.mixY;
					constraint.mixScaleX = data.mixScaleX;
					constraint.mixScaleY = data.mixScaleY;
					constraint.mixShearY = data.mixShearY;
					return;
				}
				if (blend != MixBlend.First)
				{
					return;
				}
				constraint.mixRotate += (data.mixRotate - constraint.mixRotate) * alpha;
				constraint.mixX += (data.mixX - constraint.mixX) * alpha;
				constraint.mixY += (data.mixY - constraint.mixY) * alpha;
				constraint.mixScaleX += (data.mixScaleX - constraint.mixScaleX) * alpha;
				constraint.mixScaleY += (data.mixScaleY - constraint.mixScaleY) * alpha;
				constraint.mixShearY += (data.mixShearY - constraint.mixShearY) * alpha;
				return;
			}
			else
			{
				float rotate;
				float x;
				float y;
				float scaleX;
				float scaleY;
				float shearY;
				this.GetCurveValue(out rotate, out x, out y, out scaleX, out scaleY, out shearY, time);
				if (blend == MixBlend.Setup)
				{
					TransformConstraintData data2 = constraint.data;
					constraint.mixRotate = data2.mixRotate + (rotate - data2.mixRotate) * alpha;
					constraint.mixX = data2.mixX + (x - data2.mixX) * alpha;
					constraint.mixY = data2.mixY + (y - data2.mixY) * alpha;
					constraint.mixScaleX = data2.mixScaleX + (scaleX - data2.mixScaleX) * alpha;
					constraint.mixScaleY = data2.mixScaleY + (scaleY - data2.mixScaleY) * alpha;
					constraint.mixShearY = data2.mixShearY + (shearY - data2.mixShearY) * alpha;
					return;
				}
				constraint.mixRotate += (rotate - constraint.mixRotate) * alpha;
				constraint.mixX += (x - constraint.mixX) * alpha;
				constraint.mixY += (y - constraint.mixY) * alpha;
				constraint.mixScaleX += (scaleX - constraint.mixScaleX) * alpha;
				constraint.mixScaleY += (scaleY - constraint.mixScaleY) * alpha;
				constraint.mixShearY += (shearY - constraint.mixShearY) * alpha;
				return;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006268 File Offset: 0x00004468
		public void GetCurveValue(out float rotate, out float x, out float y, out float scaleX, out float scaleY, out float shearY, float time)
		{
			float[] frames = this.frames;
			int i = Timeline.Search(frames, time, 7);
			int curveType = (int)this.curves[i / 7];
			if (curveType == 0)
			{
				float before = frames[i];
				rotate = frames[i + 1];
				x = frames[i + 2];
				y = frames[i + 3];
				scaleX = frames[i + 4];
				scaleY = frames[i + 5];
				shearY = frames[i + 6];
				float t = (time - before) / (frames[i + 7] - before);
				rotate += (frames[i + 7 + 1] - rotate) * t;
				x += (frames[i + 7 + 2] - x) * t;
				y += (frames[i + 7 + 3] - y) * t;
				scaleX += (frames[i + 7 + 4] - scaleX) * t;
				scaleY += (frames[i + 7 + 5] - scaleY) * t;
				shearY += (frames[i + 7 + 6] - shearY) * t;
				return;
			}
			if (curveType != 1)
			{
				rotate = base.GetBezierValue(time, i, 1, curveType - 2);
				x = base.GetBezierValue(time, i, 2, curveType + 18 - 2);
				y = base.GetBezierValue(time, i, 3, curveType + 36 - 2);
				scaleX = base.GetBezierValue(time, i, 4, curveType + 54 - 2);
				scaleY = base.GetBezierValue(time, i, 5, curveType + 72 - 2);
				shearY = base.GetBezierValue(time, i, 6, curveType + 90 - 2);
				return;
			}
			rotate = frames[i + 1];
			x = frames[i + 2];
			y = frames[i + 3];
			scaleX = frames[i + 4];
			scaleY = frames[i + 5];
			shearY = frames[i + 6];
		}

		// Token: 0x0400008C RID: 140
		public const int ENTRIES = 7;

		// Token: 0x0400008D RID: 141
		private const int ROTATE = 1;

		// Token: 0x0400008E RID: 142
		private const int X = 2;

		// Token: 0x0400008F RID: 143
		private const int Y = 3;

		// Token: 0x04000090 RID: 144
		private const int SCALEX = 4;

		// Token: 0x04000091 RID: 145
		private const int SCALEY = 5;

		// Token: 0x04000092 RID: 146
		private const int SHEARY = 6;

		// Token: 0x04000093 RID: 147
		private readonly int constraintIndex;
	}
}
