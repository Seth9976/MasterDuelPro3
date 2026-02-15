using System;

namespace Spine
{
	// Token: 0x0200001E RID: 30
	public class AlphaTimeline : CurveTimeline1, ISlotTimeline
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000044F4 File Offset: 0x000026F4
		public AlphaTimeline(int frameCount, int bezierCount, int slotIndex)
			: base(frameCount, bezierCount, 9.ToString() + "|" + slotIndex.ToString())
		{
			this.slotIndex = slotIndex;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000452B File Offset: 0x0000272B
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004534 File Offset: 0x00002734
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
				float a = base.GetCurveValue(time);
				if (alpha == 1f)
				{
					slot.a = a;
				}
				else
				{
					if (blend == MixBlend.Setup)
					{
						slot.a = slot.data.a;
					}
					slot.a += (a - slot.a) * alpha;
				}
				slot.ClampColor();
				return;
			}
			SlotData setup = slot.data;
			if (blend == MixBlend.Setup)
			{
				slot.a = setup.a;
				return;
			}
			if (blend != MixBlend.First)
			{
				return;
			}
			slot.a += (setup.a - slot.a) * alpha;
			slot.ClampColor();
		}

		// Token: 0x0400006A RID: 106
		private readonly int slotIndex;
	}
}
