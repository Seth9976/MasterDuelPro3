using System;

namespace Spine
{
	// Token: 0x0200001F RID: 31
	public class RGBA2Timeline : CurveTimeline, ISlotTimeline
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00004600 File Offset: 0x00002800
		public RGBA2Timeline(int frameCount, int bezierCount, int slotIndex)
			: base(frameCount, bezierCount, new string[]
			{
				8.ToString() + "|" + slotIndex.ToString(),
				9.ToString() + "|" + slotIndex.ToString(),
				10.ToString() + "|" + slotIndex.ToString()
			})
		{
			this.slotIndex = slotIndex;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000467B File Offset: 0x0000287B
		public override int FrameEntries
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000467E File Offset: 0x0000287E
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004688 File Offset: 0x00002888
		public void SetFrame(int frame, float time, float r, float g, float b, float a, float r2, float g2, float b2)
		{
			frame <<= 3;
			this.frames[frame] = time;
			this.frames[frame + 1] = r;
			this.frames[frame + 2] = g;
			this.frames[frame + 3] = b;
			this.frames[frame + 4] = a;
			this.frames[frame + 5] = r2;
			this.frames[frame + 6] = g2;
			this.frames[frame + 7] = b2;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000046F8 File Offset: 0x000028F8
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
				int i = Timeline.Search(frames, time, 8);
				int curveType = (int)this.curves[i >> 3];
				float r;
				float g;
				float b;
				float a;
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
						a = base.GetBezierValue(time, i, 4, curveType + 54 - 2);
						r2 = base.GetBezierValue(time, i, 5, curveType + 72 - 2);
						g2 = base.GetBezierValue(time, i, 6, curveType + 90 - 2);
						b2 = base.GetBezierValue(time, i, 7, curveType + 108 - 2);
					}
					else
					{
						r = frames[i + 1];
						g = frames[i + 2];
						b = frames[i + 3];
						a = frames[i + 4];
						r2 = frames[i + 5];
						g2 = frames[i + 6];
						b2 = frames[i + 7];
					}
				}
				else
				{
					float before = frames[i];
					r = frames[i + 1];
					g = frames[i + 2];
					b = frames[i + 3];
					a = frames[i + 4];
					r2 = frames[i + 5];
					g2 = frames[i + 6];
					b2 = frames[i + 7];
					float t = (time - before) / (frames[i + 8] - before);
					r += (frames[i + 8 + 1] - r) * t;
					g += (frames[i + 8 + 2] - g) * t;
					b += (frames[i + 8 + 3] - b) * t;
					a += (frames[i + 8 + 4] - a) * t;
					r2 += (frames[i + 8 + 5] - r2) * t;
					g2 += (frames[i + 8 + 6] - g2) * t;
					b2 += (frames[i + 8 + 7] - b2) * t;
				}
				if (alpha == 1f)
				{
					slot.r = r;
					slot.g = g;
					slot.b = b;
					slot.a = a;
					slot.r2 = r2;
					slot.g2 = g2;
					slot.b2 = b2;
				}
				else
				{
					float br;
					float bg;
					float bb;
					float ba;
					float br2;
					float bg2;
					float bb2;
					if (blend == MixBlend.Setup)
					{
						br = slot.data.r;
						bg = slot.data.g;
						bb = slot.data.b;
						ba = slot.data.a;
						br2 = slot.data.r2;
						bg2 = slot.data.g2;
						bb2 = slot.data.b2;
					}
					else
					{
						br = slot.r;
						bg = slot.g;
						bb = slot.b;
						ba = slot.a;
						br2 = slot.r2;
						bg2 = slot.g2;
						bb2 = slot.b2;
					}
					slot.r = br + (r - br) * alpha;
					slot.g = bg + (g - bg) * alpha;
					slot.b = bb + (b - bb) * alpha;
					slot.a = ba + (a - ba) * alpha;
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
				slot.a = setup.a;
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
			slot.a += (slot.a - setup.a) * alpha;
			slot.ClampColor();
			slot.r2 += (slot.r2 - setup.r2) * alpha;
			slot.g2 += (slot.g2 - setup.g2) * alpha;
			slot.b2 += (slot.b2 - setup.b2) * alpha;
			slot.ClampSecondColor();
		}

		// Token: 0x0400006B RID: 107
		public const int ENTRIES = 8;

		// Token: 0x0400006C RID: 108
		protected const int R = 1;

		// Token: 0x0400006D RID: 109
		protected const int G = 2;

		// Token: 0x0400006E RID: 110
		protected const int B = 3;

		// Token: 0x0400006F RID: 111
		protected const int A = 4;

		// Token: 0x04000070 RID: 112
		protected const int R2 = 5;

		// Token: 0x04000071 RID: 113
		protected const int G2 = 6;

		// Token: 0x04000072 RID: 114
		protected const int B2 = 7;

		// Token: 0x04000073 RID: 115
		private readonly int slotIndex;
	}
}
