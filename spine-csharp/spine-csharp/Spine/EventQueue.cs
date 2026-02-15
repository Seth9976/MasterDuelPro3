using System;
using System.Collections.Generic;

namespace Spine
{
	// Token: 0x02000038 RID: 56
	internal class EventQueue
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000170 RID: 368 RVA: 0x00009078 File Offset: 0x00007278
		// (remove) Token: 0x06000171 RID: 369 RVA: 0x000090B0 File Offset: 0x000072B0
		internal event Action AnimationsChanged;

		// Token: 0x06000172 RID: 370 RVA: 0x000090E5 File Offset: 0x000072E5
		internal EventQueue(AnimationState state, Action HandleAnimationsChanged, Pool<TrackEntry> trackEntryPool)
		{
			this.state = state;
			this.AnimationsChanged += HandleAnimationsChanged;
			this.trackEntryPool = trackEntryPool;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000910D File Offset: 0x0000730D
		internal void Start(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Start, entry, null));
			if (this.AnimationsChanged != null)
			{
				this.AnimationsChanged();
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00009135 File Offset: 0x00007335
		internal void Interrupt(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Interrupt, entry, null));
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000914A File Offset: 0x0000734A
		internal void End(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.End, entry, null));
			if (this.AnimationsChanged != null)
			{
				this.AnimationsChanged();
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009172 File Offset: 0x00007372
		internal void Dispose(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Dispose, entry, null));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00009187 File Offset: 0x00007387
		internal void Complete(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Complete, entry, null));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000919C File Offset: 0x0000739C
		internal void Event(TrackEntry entry, Event e)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Event, entry, e));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000091B4 File Offset: 0x000073B4
		internal void Drain()
		{
			if (this.drainDisabled)
			{
				return;
			}
			this.drainDisabled = true;
			List<EventQueue.EventQueueEntry> eventQueueEntries = this.eventQueueEntries;
			AnimationState state = this.state;
			int i = 0;
			while (i < eventQueueEntries.Count)
			{
				EventQueue.EventQueueEntry queueEntry = eventQueueEntries[i];
				TrackEntry trackEntry = queueEntry.entry;
				switch (queueEntry.type)
				{
				case EventQueue.EventType.Start:
					trackEntry.OnStart();
					state.OnStart(trackEntry);
					break;
				case EventQueue.EventType.Interrupt:
					trackEntry.OnInterrupt();
					state.OnInterrupt(trackEntry);
					break;
				case EventQueue.EventType.End:
					trackEntry.OnEnd();
					state.OnEnd(trackEntry);
					goto IL_008F;
				case EventQueue.EventType.Dispose:
					goto IL_008F;
				case EventQueue.EventType.Complete:
					trackEntry.OnComplete();
					state.OnComplete(trackEntry);
					break;
				case EventQueue.EventType.Event:
					trackEntry.OnEvent(queueEntry.e);
					state.OnEvent(trackEntry, queueEntry.e);
					break;
				}
				IL_00D9:
				i++;
				continue;
				IL_008F:
				trackEntry.OnDispose();
				state.OnDispose(trackEntry);
				this.trackEntryPool.Free(trackEntry);
				goto IL_00D9;
			}
			eventQueueEntries.Clear();
			this.drainDisabled = false;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000092B7 File Offset: 0x000074B7
		internal void Clear()
		{
			this.eventQueueEntries.Clear();
		}

		// Token: 0x040000E1 RID: 225
		private readonly List<EventQueue.EventQueueEntry> eventQueueEntries = new List<EventQueue.EventQueueEntry>();

		// Token: 0x040000E2 RID: 226
		internal bool drainDisabled;

		// Token: 0x040000E3 RID: 227
		private readonly AnimationState state;

		// Token: 0x040000E4 RID: 228
		private readonly Pool<TrackEntry> trackEntryPool;

		// Token: 0x02000039 RID: 57
		private struct EventQueueEntry
		{
			// Token: 0x0600017B RID: 379 RVA: 0x000092C4 File Offset: 0x000074C4
			public EventQueueEntry(EventQueue.EventType eventType, TrackEntry trackEntry, Event e = null)
			{
				this.type = eventType;
				this.entry = trackEntry;
				this.e = e;
			}

			// Token: 0x040000E6 RID: 230
			public EventQueue.EventType type;

			// Token: 0x040000E7 RID: 231
			public TrackEntry entry;

			// Token: 0x040000E8 RID: 232
			public Event e;
		}

		// Token: 0x0200003A RID: 58
		private enum EventType
		{
			// Token: 0x040000EA RID: 234
			Start,
			// Token: 0x040000EB RID: 235
			Interrupt,
			// Token: 0x040000EC RID: 236
			End,
			// Token: 0x040000ED RID: 237
			Dispose,
			// Token: 0x040000EE RID: 238
			Complete,
			// Token: 0x040000EF RID: 239
			Event
		}
	}
}
