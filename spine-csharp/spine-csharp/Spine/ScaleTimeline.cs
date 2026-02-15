using System;

namespace Spine
{
	// Token: 0x02000015 RID: 21
	public class ScaleTimeline : CurveTimeline2, IBoneTimeline
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000354C File Offset: 0x0000174C
		public ScaleTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 3.ToString() + "|" + boneIndex.ToString(), 4.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000359C File Offset: 0x0000179C
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000035A4 File Offset: 0x000017A4
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
					bone.scaleX = bone.data.scaleX;
					bone.scaleY = bone.data.scaleY;
					return;
				}
				if (blend != MixBlend.First)
				{
					return;
				}
				bone.scaleX += (bone.data.scaleX - bone.scaleX) * alpha;
				bone.scaleY += (bone.data.scaleY - bone.scaleY) * alpha;
				return;
			}
			else
			{
				int i = Timeline.Search(frames, time, 3);
				int curveType = (int)this.curves[i / 3];
				float x;
				float y;
				if (curveType != 0)
				{
					if (curveType != 1)
					{
						x = base.GetBezierValue(time, i, 1, curveType - 2);
						y = base.GetBezierValue(time, i, 2, curveType + 18 - 2);
					}
					else
					{
						x = frames[i + 1];
						y = frames[i + 2];
					}
				}
				else
				{
					float before = frames[i];
					x = frames[i + 1];
					y = frames[i + 2];
					float t = (time - before) / (frames[i + 3] - before);
					x += (frames[i + 3 + 1] - x) * t;
					y += (frames[i + 3 + 2] - y) * t;
				}
				x *= bone.data.scaleX;
				y *= bone.data.scaleY;
				if (alpha == 1f)
				{
					if (blend == MixBlend.Add)
					{
						bone.scaleX += x - bone.data.scaleX;
						bone.scaleY += y - bone.data.scaleY;
						return;
					}
					bone.scaleX = x;
					bone.scaleY = y;
					return;
				}
				else if (direction == MixDirection.Out)
				{
					switch (blend)
					{
					case MixBlend.Setup:
					{
						float bx = bone.data.scaleX;
						float by = bone.data.scaleY;
						bone.scaleX = bx + (Math.Abs(x) * (float)Math.Sign(bx) - bx) * alpha;
						bone.scaleY = by + (Math.Abs(y) * (float)Math.Sign(by) - by) * alpha;
						return;
					}
					case MixBlend.First:
					case MixBlend.Replace:
					{
						float bx = bone.scaleX;
						float by = bone.scaleY;
						bone.scaleX = bx + (Math.Abs(x) * (float)Math.Sign(bx) - bx) * alpha;
						bone.scaleY = by + (Math.Abs(y) * (float)Math.Sign(by) - by) * alpha;
						return;
					}
					case MixBlend.Add:
						bone.scaleX += (x - bone.data.scaleX) * alpha;
						bone.scaleY += (y - bone.data.scaleY) * alpha;
						return;
					default:
						return;
					}
				}
				else
				{
					switch (blend)
					{
					case MixBlend.Setup:
					{
						float bx = Math.Abs(bone.data.scaleX) * (float)Math.Sign(x);
						float by = Math.Abs(bone.data.scaleY) * (float)Math.Sign(y);
						bone.scaleX = bx + (x - bx) * alpha;
						bone.scaleY = by + (y - by) * alpha;
						return;
					}
					case MixBlend.First:
					case MixBlend.Replace:
					{
						float bx = Math.Abs(bone.scaleX) * (float)Math.Sign(x);
						float by = Math.Abs(bone.scaleY) * (float)Math.Sign(y);
						bone.scaleX = bx + (x - bx) * alpha;
						bone.scaleY = by + (y - by) * alpha;
						return;
					}
					case MixBlend.Add:
						bone.scaleX += (x - bone.data.scaleX) * alpha;
						bone.scaleY += (y - bone.data.scaleY) * alpha;
						return;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x04000056 RID: 86
		private readonly int boneIndex;
	}
}
