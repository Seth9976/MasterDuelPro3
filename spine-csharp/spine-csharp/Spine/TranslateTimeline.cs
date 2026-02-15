using System;

namespace Spine
{
	// Token: 0x02000012 RID: 18
	public class TranslateTimeline : CurveTimeline2, IBoneTimeline
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000031B0 File Offset: 0x000013B0
		public TranslateTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 1.ToString() + "|" + boneIndex.ToString(), 2.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003200 File Offset: 0x00001400
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003208 File Offset: 0x00001408
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (!bone.active)
			{
				return;
			}
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					bone.x = bone.data.x;
					bone.y = bone.data.y;
					return;
				}
				if (blend != MixBlend.First)
				{
					return;
				}
				bone.x += (bone.data.x - bone.x) * alpha;
				bone.y += (bone.data.y - bone.y) * alpha;
				return;
			}
			else
			{
				float x;
				float y;
				this.GetCurveValue(out x, out y, time);
				switch (blend)
				{
				case MixBlend.Setup:
					bone.x = bone.data.x + x * alpha;
					bone.y = bone.data.y + y * alpha;
					return;
				case MixBlend.First:
				case MixBlend.Replace:
					bone.x += (bone.data.x + x - bone.x) * alpha;
					bone.y += (bone.data.y + y - bone.y) * alpha;
					return;
				case MixBlend.Add:
					bone.x += x * alpha;
					bone.y += y * alpha;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000336C File Offset: 0x0000156C
		public void GetCurveValue(out float x, out float y, float time)
		{
			int i = Timeline.Search(this.frames, time, 3);
			int curveType = (int)this.curves[i / 3];
			if (curveType == 0)
			{
				float before = this.frames[i];
				x = this.frames[i + 1];
				y = this.frames[i + 2];
				float t = (time - before) / (this.frames[i + 3] - before);
				x += (this.frames[i + 3 + 1] - x) * t;
				y += (this.frames[i + 3 + 2] - y) * t;
				return;
			}
			if (curveType != 1)
			{
				x = base.GetBezierValue(time, i, 1, curveType - 2);
				y = base.GetBezierValue(time, i, 2, curveType + 18 - 2);
				return;
			}
			x = this.frames[i + 1];
			y = this.frames[i + 2];
		}

		// Token: 0x04000053 RID: 83
		private readonly int boneIndex;
	}
}
