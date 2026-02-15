using System;

namespace Spine
{
	// Token: 0x02000033 RID: 51
	public class SequenceTimeline : Timeline, ISlotTimeline
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00006B48 File Offset: 0x00004D48
		public SequenceTimeline(int frameCount, int slotIndex, Attachment attachment)
			: base(frameCount, new string[] { string.Concat(new string[]
			{
				28.ToString(),
				"|",
				slotIndex.ToString(),
				"|",
				((IHasTextureRegion)attachment).Sequence.Id.ToString()
			}) })
		{
			this.slotIndex = slotIndex;
			this.attachment = (IHasTextureRegion)attachment;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000030FA File Offset: 0x000012FA
		public override int FrameEntries
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006BC5 File Offset: 0x00004DC5
		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00006BCD File Offset: 0x00004DCD
		public Attachment Attachment
		{
			get
			{
				return (Attachment)this.attachment;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006BDA File Offset: 0x00004DDA
		public void SetFrame(int frame, float time, SequenceMode mode, int index, float delay)
		{
			frame *= 3;
			this.frames[frame] = time;
			this.frames[frame + 1] = (float)(mode | (SequenceMode)(index << 4));
			this.frames[frame + 2] = delay;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006C08 File Offset: 0x00004E08
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			Slot slot = skeleton.slots.Items[this.slotIndex];
			if (!slot.bone.active)
			{
				return;
			}
			Attachment slotAttachment = slot.attachment;
			if (slotAttachment != this.attachment)
			{
				VertexAttachment vertexAttachment = slotAttachment as VertexAttachment;
				if (vertexAttachment == null || vertexAttachment.TimelineAttachment != this.attachment)
				{
					return;
				}
			}
			Sequence sequence = ((IHasTextureRegion)slotAttachment).Sequence;
			if (sequence == null)
			{
				return;
			}
			float[] frames = this.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup || blend == MixBlend.First)
				{
					slot.SequenceIndex = -1;
				}
				return;
			}
			int i = Timeline.Search(frames, time, 3);
			float before = frames[i];
			int num = (int)frames[i + 1];
			float delay = frames[i + 2];
			int index = num >> 4;
			int count = sequence.Regions.Length;
			SequenceMode mode = (SequenceMode)(num & 15);
			if (mode != SequenceMode.Hold)
			{
				index += (int)((time - before) / delay + 0.0001f);
				switch (mode)
				{
				case SequenceMode.Once:
					index = Math.Min(count - 1, index);
					break;
				case SequenceMode.Loop:
					index %= count;
					break;
				case SequenceMode.Pingpong:
				{
					int j = (count << 1) - 2;
					index = ((j == 0) ? 0 : (index % j));
					if (index >= count)
					{
						index = j - index;
					}
					break;
				}
				case SequenceMode.OnceReverse:
					index = Math.Max(count - 1 - index, 0);
					break;
				case SequenceMode.LoopReverse:
					index = count - 1 - index % count;
					break;
				case SequenceMode.PingpongReverse:
				{
					int k = (count << 1) - 2;
					index = ((k == 0) ? 0 : ((index + count - 1) % k));
					if (index >= count)
					{
						index = k - index;
					}
					break;
				}
				}
			}
			slot.SequenceIndex = index;
		}

		// Token: 0x0400009E RID: 158
		public const int ENTRIES = 3;

		// Token: 0x0400009F RID: 159
		private const int MODE = 1;

		// Token: 0x040000A0 RID: 160
		private const int DELAY = 2;

		// Token: 0x040000A1 RID: 161
		private readonly int slotIndex;

		// Token: 0x040000A2 RID: 162
		private readonly IHasTextureRegion attachment;
	}
}
