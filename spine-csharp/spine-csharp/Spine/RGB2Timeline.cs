using System;

namespace Spine
{
	// Token: 0x02000020 RID: 32
	public class RGB2Timeline : CurveTimeline, ISlotTimeline
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public RGB2Timeline(int frameCount, int bezierCount, int slotIndex)
			: base(frameCount, bezierCount, new string[]
			{
				8.ToString() + "|" + slotIndex.ToString(),
				10.ToString() + "|" + slotIndex.ToString()
			})
		{
			this.slotIndex = slotIndex;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004C01 File Offset: 0x00002E01
		public override int FrameEntries
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004C04 File Offset: 0x00002E04
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004C0C File Offset: 0x00002E0C
		public void SetFrame(int frame, float time, float r, float g, float b, float r2, float g2, float b2)
		{
			frame *= 7;
			this.frames[frame] = time;
			this.frames[frame + 1] = r;
			this.frames[frame + 2] = g;
			this.frames[frame + 3] = b;
			this.frames[frame + 4] = r2;
			this.frames[frame + 5] = g2;
			this.frames[frame + 6] = b2;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004C70 File Offset: 0x00002E70
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
				int i = Timeline.Search(frames, time, 7);
				int curveType = (int)this.curves[i / 7];
				float r;
				float g;
				float b;
				float r2;
				float g2;
				float b2;
				if (curveType != 0)
				{
					if (curveType != 1)
					{
						r = base.GetBezierValue(time, i, 1, curveType - 2);
						g = base.GetBezierValue(time, i, 2, curveType + 18 - 2);
						b = base.GetBezierValue(time, i, 3, curveType + 36 - 2);
						r2 = base.GetBezierValue(time, i, 4, curveType + 54 - 2);
						g2 = base.GetBezierValue(time, i, 5, curveType + 72 - 2);
						b2 = base.GetBezierValue(time, i, 6, curveType + 90 - 2);
					}
					else
					{
						r = frames[i + 1];
						g = frames[i + 2];
						b = frames[i + 3];
						r2 = frames[i + 4];
						g2 = frames[i + 5];
						b2 = frames[i + 6];
					}
				}
				else
				{
					float before = frames[i];
					r = frames[i + 1];
					g = frames[i + 2];
					b = frames[i + 3];
					r2 = frames[i + 4];
					g2 = frames[i + 5];
					b2 = frames[i + 6];
					float t = (time - before) / (frames[i + 7] - before);
					r += (frames[i + 7 + 1] - r) * t;
					g += (frames[i + 7 + 2] - g) * t;
					b += (frames[i + 7 + 3] - b) * t;
					r2 += (frames[i + 7 + 4] - r2) * t;
					g2 += (frames[i + 7 + 5] - g2) * t;
					b2 += (frames[i + 7 + 6] - b2) * t;
				}
				if (alpha == 1f)
				{
					slot.r = r;
					slot.g = g;
					slot.b = b;
					slot.r2 = r2;
					slot.g2 = g2;
					slot.b2 = b2;
				}
				else
				{
					float br;
					float bg;
					float bb;
					float br2;
					float bg2;
					float bb2;
					if (blend == MixBlend.Setup)
					{
						SlotData data = slot.data;
						br = data.r;
						bg = data.g;
						bb = data.b;
						br2 = data.r2;
						bg2 = data.g2;
						bb2 = data.b2;
					}
					else
					{
						br = slot.r;
						bg = slot.g;
						bb = slot.b;
						br2 = slot.r2;
						bg2 = slot.g2;
						bb2 = slot.b2;
					}
					slot.r = br + (r - br) * alpha;
					slot.g = bg + (g - bg) * alpha;
					slot.b = bb + (b - bb) * alpha;
					slot.r2 = br2 + (r2 - br2) * alpha;
					slot.g2 = bg2 + (g2 - bg2) * alpha;
					slot.b2 = bb2 + (b2 - bb2) * alpha;
				}
				slot.ClampColor();
				slot.ClampSecondColor();
				return;
			}
			SlotData setup = slot.data;
			if (blend == MixBlend.Setup)
			{
				slot.r = setup.r;
				slot.g = setup.g;
				slot.b = setup.b;
				slot.ClampColor();
				slot.r2 = setup.r2;
				slot.g2 = setup.g2;
				slot.b2 = setup.b2;
				slot.ClampSecondColor();
				return;
			}
			if (blend != MixBlend.First)
			{
				return;
			}
			slot.r += (slot.r - setup.r) * alpha;
			slot.g += (slot.g - setup.g) * alpha;
			slot.b += (slot.b - setup.b) * alpha;
			slot.ClampColor();
			slot.r2 += (slot.r2 - setup.r2) * alpha;
			slot.g2 += (slot.g2 - setup.g2) * alpha;
			slot.b2 += (slot.b2 - setup.b2) * alpha;
			slot.ClampSecondColor();
		}

		// Token: 0x04000074 RID: 116
		public const int ENTRIES = 7;

		// Token: 0x04000075 RID: 117
		protected const int R = 1;

		// Token: 0x04000076 RID: 118
		protected const int G = 2;

		// Token: 0x04000077 RID: 119
		protected const int B = 3;

		// Token: 0x04000078 RID: 120
		protected const int R2 = 4;

		// Token: 0x04000079 RID: 121
		protected const int G2 = 5;

		// Token: 0x0400007A RID: 122
		protected const int B2 = 6;

		// Token: 0x0400007B RID: 123
		private readonly int slotIndex;
	}
}
