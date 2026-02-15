using System;

namespace Spine
{
	// Token: 0x02000021 RID: 33
	public class AttachmentTimeline : Timeline, ISlotTimeline
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00005070 File Offset: 0x00003270
		public AttachmentTimeline(int frameCount, int slotIndex)
			: base(frameCount, new string[] { 11.ToString() + "|" + slotIndex.ToString() })
		{
			this.slotIndex = slotIndex;
			this.attachmentNames = new string[frameCount];
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000050BB File Offset: 0x000032BB
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000050C3 File Offset: 0x000032C3
		public string[] AttachmentNames
		{
			get
			{
				return this.attachmentNames;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000050CB File Offset: 0x000032CB
		public void SetFrame(int frame, float time, string attachmentName)
		{
			this.frames[frame] = time;
			this.attachmentNames[frame] = attachmentName;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000050E0 File Offset: 0x000032E0
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Slot slot = skeleton.slots.Items[this.slotIndex];
			if (!slot.bone.active)
			{
				return;
			}
			if (direction == MixDirection.Out)
			{
				if (blend == MixBlend.Setup)
				{
					this.SetAttachment(skeleton, slot, slot.data.attachmentName);
				}
				return;
			}
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup || blend == MixBlend.First)
				{
					this.SetAttachment(skeleton, slot, slot.data.attachmentName);
				}
				return;
			}
			this.SetAttachment(skeleton, slot, this.attachmentNames[Timeline.Search(frames, time)]);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000516B File Offset: 0x0000336B
		private void SetAttachment(Skeleton skeleton, Slot slot, string attachmentName)
		{
			slot.Attachment = ((attachmentName == null) ? null : skeleton.GetAttachment(this.slotIndex, attachmentName));
		}

		// Token: 0x0400007C RID: 124
		private readonly int slotIndex;

		// Token: 0x0400007D RID: 125
		private readonly string[] attachmentNames;
	}
}
