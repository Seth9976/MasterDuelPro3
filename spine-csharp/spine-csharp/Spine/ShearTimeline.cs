using System;

namespace Spine
{
	// Token: 0x02000018 RID: 24
	public class ShearTimeline : CurveTimeline2, IBoneTimeline
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003A60 File Offset: 0x00001C60
		public ShearTimeline(int frameCount, int bezierCount, int boneIndex)
			: base(frameCount, bezierCount, 5.ToString() + "|" + boneIndex.ToString(), 6.ToString() + "|" + boneIndex.ToString())
		{
			this.boneIndex = boneIndex;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003AB8 File Offset: 0x00001CB8
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
					bone.shearX = bone.data.shearX;
					bone.shearY = bone.data.shearY;
					return;
				}
				if (blend != MixBlend.First)
				{
					return;
				}
				bone.shearX += (bone.data.shearX - bone.shearX) * alpha;
				bone.shearY += (bone.data.shearY - bone.shearY) * alpha;
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
				switch (blend)
				{
				case MixBlend.Setup:
					bone.shearX = bone.data.shearX + x * alpha;
					bone.shearY = bone.data.shearY + y * alpha;
					return;
				case MixBlend.First:
				case MixBlend.Replace:
					bone.shearX += (bone.data.shearX + x - bone.shearX) * alpha;
					bone.shearY += (bone.data.shearY + y - bone.shearY) * alpha;
					return;
				case MixBlend.Add:
					bone.shearX += x * alpha;
					bone.shearY += y * alpha;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x04000059 RID: 89
		private readonly int boneIndex;
	}
}
