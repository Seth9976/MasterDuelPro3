using System;

namespace Spine
{
	// Token: 0x0200001D RID: 29
	public class RGBTimeline : CurveTimeline, ISlotTimeline
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00004238 File Offset: 0x00002438
		public RGBTimeline(int frameCount, int bezierCount, int slotIndex)
			: base(frameCount, bezierCount, new string[] { 8.ToString() + "|" + slotIndex.ToString() })
		{
			this.slotIndex = slotIndex;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004277 File Offset: 0x00002477
		public override int FrameEntries
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000427A File Offset: 0x0000247A
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004282 File Offset: 0x00002482
		public void SetFrame(int frame, float time, float r, float g, float b)
		{
			frame <<= 2;
			this.frames[frame] = time;
			this.frames[frame + 1] = r;
			this.frames[frame + 2] = g;
			this.frames[frame + 3] = b;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000042B8 File Offset: 0x000024B8
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
				int i = Timeline.Search(frames, time, 4);
				int curveType = (int)this.curves[i >> 2];
				float r;
				float g;
				float b;
				if (curveType != 0)
				{
					if (curveType != 1)
					{
						r = base.GetBezierValue(time, i, 1, curveType - 2);
						g = base.GetBezierValue(time, i, 2, curveType + 18 - 2);
						b = base.GetBezierValue(time, i, 3, curveType + 36 - 2);
					}
					else
					{
						r = frames[i + 1];
						g = frames[i + 2];
						b = frames[i + 3];
					}
				}
				else
				{
					float before = frames[i];
					r = frames[i + 1];
					g = frames[i + 2];
					b = frames[i + 3];
					float t = (time - before) / (frames[i + 4] - before);
					r += (frames[i + 4 + 1] - r) * t;
					g += (frames[i + 4 + 2] - g) * t;
					b += (frames[i + 4 + 3] - b) * t;
				}
				if (alpha == 1f)
				{
					slot.r = r;
					slot.g = g;
					slot.b = b;
				}
				else
				{
					float br;
					float bg;
					float bb;
					if (blend == MixBlend.Setup)
					{
						SlotData data = slot.data;
						br = data.r;
						bg = data.g;
						bb = data.b;
					}
					else
					{
						br = slot.r;
						bg = slot.g;
						bb = slot.b;
					}
					slot.r = br + (r - br) * alpha;
					slot.g = bg + (g - bg) * alpha;
					slot.b = bb + (b - bb) * alpha;
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
				return;
			}
			if (blend != MixBlend.First)
			{
				return;
			}
			slot.r += (setup.r - slot.r) * alpha;
			slot.g += (setup.g - slot.g) * alpha;
			slot.b += (setup.b - slot.b) * alpha;
			slot.ClampColor();
		}

		// Token: 0x04000065 RID: 101
		public const int ENTRIES = 4;

		// Token: 0x04000066 RID: 102
		protected const int R = 1;

		// Token: 0x04000067 RID: 103
		protected const int G = 2;

		// Token: 0x04000068 RID: 104
		protected const int B = 3;

		// Token: 0x04000069 RID: 105
		private readonly int slotIndex;
	}
}
