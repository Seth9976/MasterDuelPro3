using System;

namespace Spine
{
	// Token: 0x02000024 RID: 36
	public class DrawOrderTimeline : Timeline
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00005A35 File Offset: 0x00003C35
		public DrawOrderTimeline(int frameCount)
			: base(frameCount, DrawOrderTimeline.propertyIds)
		{
			this.drawOrders = new int[frameCount][];
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005A4F File Offset: 0x00003C4F
		public int[][] DrawOrders
		{
			get
			{
				return this.drawOrders;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005A57 File Offset: 0x00003C57
		public void SetFrame(int frame, float time, int[] drawOrder)
		{
			this.frames[frame] = time;
			this.drawOrders[frame] = drawOrder;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005A6C File Offset: 0x00003C6C
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			if (direction == MixDirection.Out)
			{
				if (blend == MixBlend.Setup)
				{
					Array.Copy(skeleton.slots.Items, 0, skeleton.drawOrder.Items, 0, skeleton.slots.Count);
				}
				return;
			}
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup || blend == MixBlend.First)
				{
					Array.Copy(skeleton.slots.Items, 0, skeleton.drawOrder.Items, 0, skeleton.slots.Count);
				}
				return;
			}
			int[] drawOrderToSetupIndex = this.drawOrders[Timeline.Search(frames, time)];
			if (drawOrderToSetupIndex == null)
			{
				Array.Copy(skeleton.slots.Items, 0, skeleton.drawOrder.Items, 0, skeleton.slots.Count);
				return;
			}
			Slot[] slots = skeleton.slots.Items;
			Slot[] drawOrder = skeleton.drawOrder.Items;
			int i = 0;
			int j = drawOrderToSetupIndex.Length;
			while (i < j)
			{
				drawOrder[i] = slots[drawOrderToSetupIndex[i]];
				i++;
			}
		}

		// Token: 0x04000083 RID: 131
		private static readonly string[] propertyIds = new string[] { 14.ToString() };

		// Token: 0x04000084 RID: 132
		private readonly int[][] drawOrders;
	}
}
