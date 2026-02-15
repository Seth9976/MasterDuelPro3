using System;

namespace Spine
{
	// Token: 0x02000023 RID: 35
	public class EventTimeline : Timeline
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000593D File Offset: 0x00003B3D
		public EventTimeline(int frameCount)
			: base(frameCount, EventTimeline.propertyIds)
		{
			this.events = new Event[frameCount];
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00005957 File Offset: 0x00003B57
		public Event[] Events
		{
			get
			{
				return this.events;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000595F File Offset: 0x00003B5F
		public void SetFrame(int frame, Event e)
		{
			this.frames[frame] = e.time;
			this.events[frame] = e;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005978 File Offset: 0x00003B78
		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixBlend blend, MixDirection direction)
		{
			if (firedEvents == null)
			{
				return;
			}
			float[] frames = this.frames;
			int frameCount = frames.Length;
			if (lastTime > time)
			{
				this.Apply(skeleton, lastTime, 2.1474836E+09f, firedEvents, alpha, blend, direction);
				lastTime = -1f;
			}
			else if (lastTime >= frames[frameCount - 1])
			{
				return;
			}
			if (time < frames[0])
			{
				return;
			}
			int i;
			if (lastTime < frames[0])
			{
				i = 0;
			}
			else
			{
				i = Timeline.Search(frames, lastTime) + 1;
				float frameTime = frames[i];
				while (i > 0)
				{
					if (frames[i - 1] != frameTime)
					{
						break;
					}
					i--;
				}
			}
			while (i < frameCount && time >= frames[i])
			{
				firedEvents.Add(this.events[i]);
				i++;
			}
		}

		// Token: 0x04000081 RID: 129
		private static readonly string[] propertyIds = new string[] { 13.ToString() };

		// Token: 0x04000082 RID: 130
		private readonly Event[] events;
	}
}
