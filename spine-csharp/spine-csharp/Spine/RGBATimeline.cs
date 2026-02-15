using System;

namespace Spine
{
	// Token: 0x0200001C RID: 28
	public class RGBATimeline : CurveTimeline, ISlotTimeline
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003EB0 File Offset: 0x000020B0
		public RGBATimeline(int frameCount, int bezierCount, int slotIndex)
			: base(frameCount, bezierCount, new string[]
			{
				8.ToString() + "|" + slotIndex.ToString(),
				9.ToString() + "|" + slotIndex.ToString()
			})
		{
			this.slotIndex = slotIndex;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003F0D File Offset: 0x0000210D
		public override int FrameEntries
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003F10 File Offset: 0x00002110
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003F18 File Offset: 0x00002118
		public void SetFrame(int frame, float time, float r, float g, float b, float a)
		{
			frame *= 5;
			this.frames[frame] = time;
			this.frames[frame + 1] = r;
			this.frames[frame + 2] = g;
			this.frames[frame + 3] = b;
			this.frames[frame + 4] = a;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F58 File Offset: 0x00002158
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Slot slot = skeleton.slots.Items[this.slotIndex];
			if (!slot.bone.active)
			{
				return;
			}
			float[] frames = this.frames;
			if (time >= frames[0])
			{
				int i = Timeline.Search(frames, time, 5);
				int curveType = (int)this.curves[i / 5];
				float r;
				float g;
				float b;
				float a;
				if (curveType != 0)
				{
					if (curveType != 1)
					{
						r = base.GetBezierValue(time, i, 1, curveType - 2);
						g = base.GetBezierValue(time, i, 2, curveType + 18 - 2);
						b = base.GetBezierValue(time, i, 3, curveType + 36 - 2);
						a = base.GetBezierValue(time, i, 4, curveType + 54 - 2);
					}
					else
					{
						r = frames[i + 1];
						g = frames[i + 2];
						b = frames[i + 3];
						a = frames[i + 4];
					}
				}
				else
				{
					float before = frames[i];
					r = frames[i + 1];
					g = frames[i + 2];
					b = frames[i + 3];
					a = frames[i + 4];
					float t = (time - before) / (frames[i + 5] - before);
					r += (frames[i + 5 + 1] - r) * t;
					g += (frames[i + 5 + 2] - g) * t;
					b += (frames[i + 5 + 3] - b) * t;
					a += (frames[i + 5 + 4] - a) * t;
				}
				if (alpha == 1f)
				{
					slot.r = r;
					slot.g = g;
					slot.b = b;
					slot.a = a;
				}
				else
				{
					float br;
					float bg;
					float bb;
					float ba;
					if (blend == MixBlend.Setup)
					{
						br = slot.data.r;
						bg = slot.data.g;
						bb = slot.data.b;
						ba = slot.data.a;
					}
					else
					{
						br = slot.r;
						bg = slot.g;
						bb = slot.b;
						ba = slot.a;
					}
					slot.r = br + (r - br) * alpha;
					slot.g = bg + (g - bg) * alpha;
					slot.b = bb + (b - bb) * alpha;
					slot.a = ba + (a - ba) * alpha;
				}
				slot.ClampColor();
				return;
			}
			SlotData setup = slot.data;
			if (blend == MixBlend.Setup)
			{
				slot.r = setup.r;
				slot.g = setup.g;
				slot.b = setup.b;
				slot.a = setup.a;
				return;
			}
			if (blend != MixBlend.First)
			{
				return;
			}
			slot.r += (setup.r - slot.r) * alpha;
			slot.g += (setup.g - slot.g) * alpha;
			slot.b += (setup.b - slot.b) * alpha;
			slot.a += (setup.a - slot.a) * alpha;
			slot.ClampColor();
		}

		// Token: 0x0400005F RID: 95
		public const int ENTRIES = 5;

		// Token: 0x04000060 RID: 96
		protected const int R = 1;

		// Token: 0x04000061 RID: 97
		protected const int G = 2;

		// Token: 0x04000062 RID: 98
		protected const int B = 3;

		// Token: 0x04000063 RID: 99
		protected const int A = 4;

		// Token: 0x04000064 RID: 100
		private readonly int slotIndex;
	}
}
