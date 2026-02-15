using System;
using System.Collections.Generic;
using System.Text;

namespace Spine
{
	// Token: 0x02000034 RID: 52
	public class AnimationState
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00006D8F File Offset: 0x00004F8F
		internal void OnStart(TrackEntry entry)
		{
			if (this.Start != null)
			{
				this.Start(entry);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006DA5 File Offset: 0x00004FA5
		internal void OnInterrupt(TrackEntry entry)
		{
			if (this.Interrupt != null)
			{
				this.Interrupt(entry);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006DBB File Offset: 0x00004FBB
		internal void OnEnd(TrackEntry entry)
		{
			if (this.End != null)
			{
				this.End(entry);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006DD1 File Offset: 0x00004FD1
		internal void OnDispose(TrackEntry entry)
		{
			if (this.Dispose != null)
			{
				this.Dispose(entry);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006DE7 File Offset: 0x00004FE7
		internal void OnComplete(TrackEntry entry)
		{
			if (this.Complete != null)
			{
				this.Complete(entry);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006DFD File Offset: 0x00004FFD
		internal void OnEvent(TrackEntry entry, Event e)
		{
			if (this.Event != null)
			{
				this.Event(entry, e);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000EB RID: 235 RVA: 0x00006E14 File Offset: 0x00005014
		// (remove) Token: 0x060000EC RID: 236 RVA: 0x00006E4C File Offset: 0x0000504C
		public event AnimationState.TrackEntryDelegate Start;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000ED RID: 237 RVA: 0x00006E84 File Offset: 0x00005084
		// (remove) Token: 0x060000EE RID: 238 RVA: 0x00006EBC File Offset: 0x000050BC
		public event AnimationState.TrackEntryDelegate Interrupt;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000EF RID: 239 RVA: 0x00006EF4 File Offset: 0x000050F4
		// (remove) Token: 0x060000F0 RID: 240 RVA: 0x00006F2C File Offset: 0x0000512C
		public event AnimationState.TrackEntryDelegate End;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000F1 RID: 241 RVA: 0x00006F64 File Offset: 0x00005164
		// (remove) Token: 0x060000F2 RID: 242 RVA: 0x00006F9C File Offset: 0x0000519C
		public event AnimationState.TrackEntryDelegate Dispose;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000F3 RID: 243 RVA: 0x00006FD4 File Offset: 0x000051D4
		// (remove) Token: 0x060000F4 RID: 244 RVA: 0x0000700C File Offset: 0x0000520C
		public event AnimationState.TrackEntryDelegate Complete;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000F5 RID: 245 RVA: 0x00007044 File Offset: 0x00005244
		// (remove) Token: 0x060000F6 RID: 246 RVA: 0x0000707C File Offset: 0x0000527C
		public event AnimationState.TrackEntryEventDelegate Event;

		// Token: 0x060000F7 RID: 247 RVA: 0x000070B4 File Offset: 0x000052B4
		public void AssignEventSubscribersFrom(AnimationState src)
		{
			this.Event = src.Event;
			this.Start = src.Start;
			this.Interrupt = src.Interrupt;
			this.End = src.End;
			this.Dispose = src.Dispose;
			this.Complete = src.Complete;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000710C File Offset: 0x0000530C
		public void AddEventSubscribersFrom(AnimationState src)
		{
			this.Event += src.Event;
			this.Start += src.Start;
			this.Interrupt += src.Interrupt;
			this.End += src.End;
			this.Dispose += src.Dispose;
			this.Complete += src.Complete;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007164 File Offset: 0x00005364
		public AnimationState(AnimationStateData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			this.data = data;
			this.queue = new EventQueue(this, delegate
			{
				this.animationsChanged = true;
			}, this.trackEntryPool);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000071F0 File Offset: 0x000053F0
		public void Update(float delta)
		{
			delta *= this.timeScale;
			TrackEntry[] tracksItems = this.tracks.Items;
			int i = 0;
			int j = this.tracks.Count;
			while (i < j)
			{
				TrackEntry current = tracksItems[i];
				if (current != null)
				{
					current.animationLast = current.nextAnimationLast;
					current.trackLast = current.nextTrackLast;
					float currentDelta = delta * current.timeScale;
					if (current.delay > 0f)
					{
						current.delay -= currentDelta;
						if (current.delay > 0f)
						{
							goto IL_01C9;
						}
						currentDelta = -current.delay;
						current.delay = 0f;
					}
					TrackEntry next = current.next;
					if (next != null)
					{
						float nextTime = current.trackLast - next.delay;
						if (nextTime >= 0f)
						{
							next.delay = 0f;
							next.trackTime += ((current.timeScale == 0f) ? 0f : ((nextTime / current.timeScale + delta) * next.timeScale));
							current.trackTime += currentDelta;
							this.SetCurrent(i, next, true);
							while (next.mixingFrom != null)
							{
								next.mixTime += delta;
								next = next.mixingFrom;
							}
							goto IL_01C9;
						}
					}
					else if (current.trackLast >= current.trackEnd && current.mixingFrom == null)
					{
						tracksItems[i] = null;
						this.queue.End(current);
						this.ClearNext(current);
						goto IL_01C9;
					}
					if (current.mixingFrom != null && this.UpdateMixingFrom(current, delta))
					{
						TrackEntry from = current.mixingFrom;
						current.mixingFrom = null;
						if (from != null)
						{
							from.mixingTo = null;
						}
						while (from != null)
						{
							this.queue.End(from);
							from = from.mixingFrom;
						}
					}
					current.trackTime += currentDelta;
				}
				IL_01C9:
				i++;
			}
			this.queue.Drain();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000073DC File Offset: 0x000055DC
		private bool UpdateMixingFrom(TrackEntry to, float delta)
		{
			TrackEntry from = to.mixingFrom;
			if (from == null)
			{
				return true;
			}
			bool finished = this.UpdateMixingFrom(from, delta);
			from.animationLast = from.nextAnimationLast;
			from.trackLast = from.nextTrackLast;
			if (to.mixTime > 0f && to.mixTime >= to.mixDuration)
			{
				if (from.totalAlpha == 0f || to.mixDuration == 0f)
				{
					to.mixingFrom = from.mixingFrom;
					if (from.mixingFrom != null)
					{
						from.mixingFrom.mixingTo = to;
					}
					to.interruptAlpha = from.interruptAlpha;
					this.queue.End(from);
				}
				return finished;
			}
			from.trackTime += delta * from.timeScale;
			to.mixTime += delta;
			return false;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000074AC File Offset: 0x000056AC
		public bool Apply(Skeleton skeleton)
		{
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			if (this.animationsChanged)
			{
				this.AnimationsChanged();
			}
			ExposedList<Event> events = this.events;
			bool applied = false;
			TrackEntry[] tracksItems = this.tracks.Items;
			int i = 0;
			int j = this.tracks.Count;
			while (i < j)
			{
				TrackEntry current = tracksItems[i];
				if (current != null && current.delay <= 0f)
				{
					applied = true;
					MixBlend blend = ((i == 0) ? MixBlend.First : current.mixBlend);
					float alpha = current.alpha;
					if (current.mixingFrom != null)
					{
						alpha *= this.ApplyMixingFrom(current, skeleton, blend);
					}
					else if (current.trackTime >= current.trackEnd && current.next == null)
					{
						alpha = 0f;
					}
					bool attachments = alpha >= current.alphaAttachmentThreshold;
					float animationLast = current.animationLast;
					float animationTime = current.AnimationTime;
					float applyTime = animationTime;
					ExposedList<Event> applyEvents = events;
					if (current.reverse)
					{
						applyTime = current.animation.duration - applyTime;
						applyEvents = null;
					}
					int timelineCount = current.animation.timelines.Count;
					Timeline[] timelines = current.animation.timelines.Items;
					if ((i == 0 && alpha == 1f) || blend == MixBlend.Add)
					{
						if (i == 0)
						{
							attachments = true;
						}
						for (int ii = 0; ii < timelineCount; ii++)
						{
							Timeline timeline = timelines[ii];
							if (timeline is AttachmentTimeline)
							{
								this.ApplyAttachmentTimeline((AttachmentTimeline)timeline, skeleton, applyTime, blend, attachments);
							}
							else
							{
								timeline.Apply(skeleton, animationLast, applyTime, applyEvents, alpha, blend, MixDirection.In);
							}
						}
					}
					else
					{
						int[] timelineMode = current.timelineMode.Items;
						bool shortestRotation = current.shortestRotation;
						bool firstFrame = !shortestRotation && current.timelinesRotation.Count != timelineCount << 1;
						if (firstFrame)
						{
							current.timelinesRotation.Resize(timelineCount << 1);
						}
						float[] timelinesRotation = current.timelinesRotation.Items;
						for (int ii2 = 0; ii2 < timelineCount; ii2++)
						{
							Timeline timeline2 = timelines[ii2];
							MixBlend timelineBlend = ((timelineMode[ii2] == 0) ? blend : MixBlend.Setup);
							RotateTimeline rotateTimeline = timeline2 as RotateTimeline;
							if (!shortestRotation && rotateTimeline != null)
							{
								AnimationState.ApplyRotateTimeline(rotateTimeline, skeleton, applyTime, alpha, timelineBlend, timelinesRotation, ii2 << 1, firstFrame);
							}
							else if (timeline2 is AttachmentTimeline)
							{
								this.ApplyAttachmentTimeline((AttachmentTimeline)timeline2, skeleton, applyTime, blend, attachments);
							}
							else
							{
								timeline2.Apply(skeleton, animationLast, applyTime, applyEvents, alpha, timelineBlend, MixDirection.In);
							}
						}
					}
					this.QueueEvents(current, animationTime);
					events.Clear(false);
					current.nextAnimationLast = animationTime;
					current.nextTrackLast = current.trackTime;
				}
				i++;
			}
			int setupState = this.unkeyedState + 1;
			Slot[] slots = skeleton.slots.Items;
			int k = 0;
			int l = skeleton.slots.Count;
			while (k < l)
			{
				Slot slot = slots[k];
				if (slot.attachmentState == setupState)
				{
					string attachmentName = slot.data.attachmentName;
					slot.Attachment = ((attachmentName == null) ? null : skeleton.GetAttachment(slot.data.index, attachmentName));
				}
				k++;
			}
			this.unkeyedState += 2;
			this.queue.Drain();
			return applied;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000077EC File Offset: 0x000059EC
		public bool ApplyEventTimelinesOnly(Skeleton skeleton, bool issueEvents = true)
		{
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			ExposedList<Event> events = this.events;
			bool applied = false;
			TrackEntry[] tracksItems = this.tracks.Items;
			int i = 0;
			int j = this.tracks.Count;
			while (i < j)
			{
				TrackEntry current = tracksItems[i];
				if (current != null && current.delay <= 0f)
				{
					applied = true;
					if (current.mixingFrom != null)
					{
						this.ApplyMixingFromEventTimelinesOnly(current, skeleton, issueEvents);
					}
					float animationLast = current.animationLast;
					float animationTime = current.AnimationTime;
					if (issueEvents)
					{
						int timelineCount = current.animation.timelines.Count;
						Timeline[] timelines = current.animation.timelines.Items;
						for (int ii = 0; ii < timelineCount; ii++)
						{
							Timeline timeline = timelines[ii];
							if (timeline is EventTimeline)
							{
								timeline.Apply(skeleton, animationLast, animationTime, events, 1f, MixBlend.Setup, MixDirection.In);
							}
						}
						this.QueueEvents(current, animationTime);
						events.Clear(false);
					}
					current.nextAnimationLast = animationTime;
					current.nextTrackLast = current.trackTime;
				}
				i++;
			}
			if (issueEvents)
			{
				this.queue.Drain();
			}
			return applied;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000791C File Offset: 0x00005B1C
		private float ApplyMixingFrom(TrackEntry to, Skeleton skeleton, MixBlend blend)
		{
			TrackEntry from = to.mixingFrom;
			if (from.mixingFrom != null)
			{
				this.ApplyMixingFrom(from, skeleton, blend);
			}
			float mix;
			if (to.mixDuration == 0f)
			{
				mix = 1f;
				if (blend == MixBlend.First)
				{
					blend = MixBlend.Setup;
				}
			}
			else
			{
				mix = to.mixTime / to.mixDuration;
				if (mix > 1f)
				{
					mix = 1f;
				}
				if (blend != MixBlend.First)
				{
					blend = from.mixBlend;
				}
			}
			bool attachments = mix < from.mixAttachmentThreshold;
			bool drawOrder = mix < from.mixDrawOrderThreshold;
			int timelineCount = from.animation.timelines.Count;
			Timeline[] timelines = from.animation.timelines.Items;
			float alphaHold = from.alpha * to.interruptAlpha;
			float alphaMix = alphaHold * (1f - mix);
			float animationLast = from.animationLast;
			float animationTime = from.AnimationTime;
			float applyTime = animationTime;
			ExposedList<Event> events = null;
			if (from.reverse)
			{
				applyTime = from.animation.duration - applyTime;
			}
			else if (mix < from.eventThreshold)
			{
				events = this.events;
			}
			if (blend == MixBlend.Add)
			{
				for (int i = 0; i < timelineCount; i++)
				{
					timelines[i].Apply(skeleton, animationLast, applyTime, events, alphaMix, blend, MixDirection.Out);
				}
			}
			else
			{
				int[] timelineMode = from.timelineMode.Items;
				TrackEntry[] timelineHoldMix = from.timelineHoldMix.Items;
				bool shortestRotation = from.shortestRotation;
				bool firstFrame = !shortestRotation && from.timelinesRotation.Count != timelineCount << 1;
				if (firstFrame)
				{
					from.timelinesRotation.Resize(timelineCount << 1);
				}
				float[] timelinesRotation = from.timelinesRotation.Items;
				from.totalAlpha = 0f;
				int j = 0;
				while (j < timelineCount)
				{
					Timeline timeline = timelines[j];
					MixDirection direction = MixDirection.Out;
					MixBlend timelineBlend;
					float alpha;
					switch (timelineMode[j])
					{
					case 0:
						if (drawOrder || !(timeline is DrawOrderTimeline))
						{
							timelineBlend = blend;
							alpha = alphaMix;
							goto IL_021F;
						}
						break;
					case 1:
						timelineBlend = MixBlend.Setup;
						alpha = alphaMix;
						goto IL_021F;
					case 2:
						timelineBlend = blend;
						alpha = alphaHold;
						goto IL_021F;
					case 3:
						timelineBlend = MixBlend.Setup;
						alpha = alphaHold;
						goto IL_021F;
					default:
					{
						timelineBlend = MixBlend.Setup;
						TrackEntry holdMix = timelineHoldMix[j];
						alpha = alphaHold * Math.Max(0f, 1f - holdMix.mixTime / holdMix.mixDuration);
						goto IL_021F;
					}
					}
					IL_02AE:
					j++;
					continue;
					IL_021F:
					from.totalAlpha += alpha;
					RotateTimeline rotateTimeline = timeline as RotateTimeline;
					if (!shortestRotation && rotateTimeline != null)
					{
						AnimationState.ApplyRotateTimeline(rotateTimeline, skeleton, applyTime, alpha, timelineBlend, timelinesRotation, j << 1, firstFrame);
						goto IL_02AE;
					}
					if (timeline is AttachmentTimeline)
					{
						this.ApplyAttachmentTimeline((AttachmentTimeline)timeline, skeleton, applyTime, timelineBlend, attachments && alpha >= from.alphaAttachmentThreshold);
						goto IL_02AE;
					}
					if (drawOrder && timeline is DrawOrderTimeline && timelineBlend == MixBlend.Setup)
					{
						direction = MixDirection.In;
					}
					timeline.Apply(skeleton, animationLast, applyTime, events, alpha, timelineBlend, direction);
					goto IL_02AE;
				}
			}
			if (to.mixDuration > 0f)
			{
				this.QueueEvents(from, animationTime);
			}
			this.events.Clear(false);
			from.nextAnimationLast = animationTime;
			from.nextTrackLast = from.trackTime;
			return mix;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007C20 File Offset: 0x00005E20
		private float ApplyMixingFromEventTimelinesOnly(TrackEntry to, Skeleton skeleton, bool issueEvents)
		{
			TrackEntry from = to.mixingFrom;
			if (from.mixingFrom != null)
			{
				this.ApplyMixingFromEventTimelinesOnly(from, skeleton, issueEvents);
			}
			float mix;
			if (to.mixDuration == 0f)
			{
				mix = 1f;
			}
			else
			{
				mix = to.mixTime / to.mixDuration;
				if (mix > 1f)
				{
					mix = 1f;
				}
			}
			ExposedList<Event> eventBuffer = ((mix < from.eventThreshold) ? this.events : null);
			if (eventBuffer == null)
			{
				return mix;
			}
			float animationLast = from.animationLast;
			float animationTime = from.AnimationTime;
			if (issueEvents)
			{
				int timelineCount = from.animation.timelines.Count;
				Timeline[] timelines = from.animation.timelines.Items;
				for (int i = 0; i < timelineCount; i++)
				{
					Timeline timeline = timelines[i];
					if (timeline is EventTimeline)
					{
						timeline.Apply(skeleton, animationLast, animationTime, eventBuffer, 0f, MixBlend.Setup, MixDirection.Out);
					}
				}
				if (to.mixDuration > 0f)
				{
					this.QueueEvents(from, animationTime);
				}
				this.events.Clear(false);
			}
			from.nextAnimationLast = animationTime;
			from.nextTrackLast = from.trackTime;
			return mix;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00007D30 File Offset: 0x00005F30
		private void ApplyAttachmentTimeline(AttachmentTimeline timeline, Skeleton skeleton, float time, MixBlend blend, bool attachments)
		{
			Slot slot = skeleton.slots.Items[timeline.SlotIndex];
			if (!slot.bone.active)
			{
				return;
			}
			float[] frames = timeline.frames;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup || blend == MixBlend.First)
				{
					this.SetAttachment(skeleton, slot, slot.data.attachmentName, attachments);
				}
			}
			else
			{
				this.SetAttachment(skeleton, slot, timeline.AttachmentNames[Timeline.Search(frames, time)], attachments);
			}
			if (slot.attachmentState <= this.unkeyedState)
			{
				slot.attachmentState = this.unkeyedState + 1;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007DBF File Offset: 0x00005FBF
		private void SetAttachment(Skeleton skeleton, Slot slot, string attachmentName, bool attachments)
		{
			slot.Attachment = ((attachmentName == null) ? null : skeleton.GetAttachment(slot.data.index, attachmentName));
			if (attachments)
			{
				slot.attachmentState = this.unkeyedState + 2;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007DF4 File Offset: 0x00005FF4
		private static void ApplyRotateTimeline(RotateTimeline timeline, Skeleton skeleton, float time, float alpha, MixBlend blend, float[] timelinesRotation, int i, bool firstFrame)
		{
			if (firstFrame)
			{
				timelinesRotation[i] = 0f;
			}
			if (alpha == 1f)
			{
				timeline.Apply(skeleton, 0f, time, null, 1f, blend, MixDirection.In);
				return;
			}
			Bone bone = skeleton.bones.Items[timeline.BoneIndex];
			if (!bone.active)
			{
				return;
			}
			float[] frames = timeline.frames;
			float r;
			float r2;
			if (time < frames[0])
			{
				if (blend == MixBlend.Setup)
				{
					bone.rotation = bone.data.rotation;
					return;
				}
				if (blend != MixBlend.First)
				{
					return;
				}
				r = bone.rotation;
				r2 = bone.data.rotation;
			}
			else
			{
				r = ((blend == MixBlend.Setup) ? bone.data.rotation : bone.rotation);
				r2 = bone.data.rotation + timeline.GetCurveValue(time);
			}
			float diff = r2 - r;
			diff -= (float)Math.Ceiling((double)(diff / 360f - 0.5f)) * 360f;
			float total;
			if (diff == 0f)
			{
				total = timelinesRotation[i];
			}
			else
			{
				float lastTotal;
				float lastDiff;
				if (firstFrame)
				{
					lastTotal = 0f;
					lastDiff = diff;
				}
				else
				{
					lastTotal = timelinesRotation[i];
					lastDiff = timelinesRotation[i + 1];
				}
				float loops = lastTotal - lastTotal % 360f;
				total = diff + loops;
				bool current = diff >= 0f;
				bool dir = lastTotal >= 0f;
				if (Math.Abs(lastDiff) <= 90f && Math.Sign(lastDiff) != Math.Sign(diff))
				{
					if (Math.Abs(lastTotal - loops) > 180f)
					{
						total += (float)(360 * Math.Sign(lastTotal));
						dir = current;
					}
					else if (loops != 0f)
					{
						total -= (float)(360 * Math.Sign(lastTotal));
					}
					else
					{
						dir = current;
					}
				}
				if (dir != current)
				{
					total += (float)(360 * Math.Sign(lastTotal));
				}
				timelinesRotation[i] = total;
			}
			timelinesRotation[i + 1] = diff;
			bone.rotation = r + total * alpha;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007FE0 File Offset: 0x000061E0
		private void QueueEvents(TrackEntry entry, float animationTime)
		{
			float animationStart = entry.animationStart;
			float animationEnd = entry.animationEnd;
			float duration = animationEnd - animationStart;
			float trackLastWrapped = entry.trackLast % duration;
			Event[] eventsItems = this.events.Items;
			int i = 0;
			int j = this.events.Count;
			while (i < j)
			{
				Event e = eventsItems[i];
				if (e.time < trackLastWrapped)
				{
					break;
				}
				if (e.time <= animationEnd)
				{
					this.queue.Event(entry, e);
				}
				i++;
			}
			bool complete;
			if (entry.loop)
			{
				if (duration == 0f)
				{
					complete = true;
				}
				else
				{
					int cycles = (int)(entry.trackTime / duration);
					complete = cycles > 0 && cycles > (int)(entry.trackLast / duration);
				}
			}
			else
			{
				complete = animationTime >= animationEnd && entry.animationLast < animationEnd;
			}
			if (complete)
			{
				this.queue.Complete(entry);
			}
			while (i < j)
			{
				if (eventsItems[i].time >= animationStart)
				{
					this.queue.Event(entry, eventsItems[i]);
				}
				i++;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000080E8 File Offset: 0x000062E8
		public void ClearTracks()
		{
			bool oldDrainDisabled = this.queue.drainDisabled;
			this.queue.drainDisabled = true;
			int i = 0;
			int j = this.tracks.Count;
			while (i < j)
			{
				this.ClearTrack(i);
				i++;
			}
			this.tracks.Clear(true);
			this.queue.drainDisabled = oldDrainDisabled;
			this.queue.Drain();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008150 File Offset: 0x00006350
		public void ClearTrack(int trackIndex)
		{
			if (trackIndex >= this.tracks.Count)
			{
				return;
			}
			TrackEntry current = this.tracks.Items[trackIndex];
			if (current == null)
			{
				return;
			}
			this.queue.End(current);
			this.ClearNext(current);
			TrackEntry entry = current;
			for (;;)
			{
				TrackEntry from = entry.mixingFrom;
				if (from == null)
				{
					break;
				}
				this.queue.End(from);
				entry.mixingFrom = null;
				entry.mixingTo = null;
				entry = from;
			}
			this.tracks.Items[current.trackIndex] = null;
			this.queue.Drain();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000081DC File Offset: 0x000063DC
		private void SetCurrent(int index, TrackEntry current, bool interrupt)
		{
			TrackEntry from = this.ExpandToIndex(index);
			this.tracks.Items[index] = current;
			current.previous = null;
			if (from != null)
			{
				if (interrupt)
				{
					this.queue.Interrupt(from);
				}
				current.mixingFrom = from;
				from.mixingTo = current;
				current.mixTime = 0f;
				if (from.mixingFrom != null && from.mixDuration > 0f)
				{
					current.interruptAlpha *= Math.Min(1f, from.mixTime / from.mixDuration);
				}
				from.timelinesRotation.Clear(true);
			}
			this.queue.Start(current);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008284 File Offset: 0x00006484
		public TrackEntry SetAnimation(int trackIndex, string animationName, bool loop)
		{
			Animation animation = this.data.skeletonData.FindAnimation(animationName);
			if (animation == null)
			{
				throw new ArgumentException("Animation not found: " + animationName, "animationName");
			}
			return this.SetAnimation(trackIndex, animation, loop);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000082C8 File Offset: 0x000064C8
		public TrackEntry SetAnimation(int trackIndex, Animation animation, bool loop)
		{
			if (animation == null)
			{
				throw new ArgumentNullException("animation", "animation cannot be null.");
			}
			bool interrupt = true;
			TrackEntry current = this.ExpandToIndex(trackIndex);
			if (current != null)
			{
				if (current.nextTrackLast == -1f)
				{
					this.tracks.Items[trackIndex] = current.mixingFrom;
					this.queue.Interrupt(current);
					this.queue.End(current);
					this.ClearNext(current);
					current = current.mixingFrom;
					interrupt = false;
				}
				else
				{
					this.ClearNext(current);
				}
			}
			TrackEntry entry = this.NewTrackEntry(trackIndex, animation, loop, current);
			this.SetCurrent(trackIndex, entry, interrupt);
			this.queue.Drain();
			return entry;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008368 File Offset: 0x00006568
		public TrackEntry AddAnimation(int trackIndex, string animationName, bool loop, float delay)
		{
			Animation animation = this.data.skeletonData.FindAnimation(animationName);
			if (animation == null)
			{
				throw new ArgumentException("Animation not found: " + animationName, "animationName");
			}
			return this.AddAnimation(trackIndex, animation, loop, delay);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000083AC File Offset: 0x000065AC
		public TrackEntry AddAnimation(int trackIndex, Animation animation, bool loop, float delay)
		{
			if (animation == null)
			{
				throw new ArgumentNullException("animation", "animation cannot be null.");
			}
			TrackEntry last = this.ExpandToIndex(trackIndex);
			if (last != null)
			{
				while (last.next != null)
				{
					last = last.next;
				}
			}
			TrackEntry entry = this.NewTrackEntry(trackIndex, animation, loop, last);
			if (last == null)
			{
				this.SetCurrent(trackIndex, entry, true);
				this.queue.Drain();
			}
			else
			{
				last.next = entry;
				entry.previous = last;
				if (delay <= 0f)
				{
					delay += last.TrackComplete - entry.mixDuration;
				}
			}
			entry.delay = delay;
			return entry;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000843E File Offset: 0x0000663E
		public TrackEntry SetEmptyAnimation(int trackIndex, float mixDuration)
		{
			TrackEntry trackEntry = this.SetAnimation(trackIndex, AnimationState.EmptyAnimation, false);
			trackEntry.mixDuration = mixDuration;
			trackEntry.trackEnd = mixDuration;
			return trackEntry;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000845C File Offset: 0x0000665C
		public TrackEntry AddEmptyAnimation(int trackIndex, float mixDuration, float delay)
		{
			TrackEntry entry = this.AddAnimation(trackIndex, AnimationState.EmptyAnimation, false, delay);
			if (delay <= 0f)
			{
				entry.delay += entry.mixDuration - mixDuration;
			}
			entry.mixDuration = mixDuration;
			entry.trackEnd = mixDuration;
			return entry;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000084A4 File Offset: 0x000066A4
		public void SetEmptyAnimations(float mixDuration)
		{
			bool oldDrainDisabled = this.queue.drainDisabled;
			this.queue.drainDisabled = true;
			TrackEntry[] tracksItems = this.tracks.Items;
			int i = 0;
			int j = this.tracks.Count;
			while (i < j)
			{
				TrackEntry current = tracksItems[i];
				if (current != null)
				{
					this.SetEmptyAnimation(current.trackIndex, mixDuration);
				}
				i++;
			}
			this.queue.drainDisabled = oldDrainDisabled;
			this.queue.Drain();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000851C File Offset: 0x0000671C
		private TrackEntry ExpandToIndex(int index)
		{
			if (index < this.tracks.Count)
			{
				return this.tracks.Items[index];
			}
			this.tracks.Resize(index + 1);
			return null;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000854C File Offset: 0x0000674C
		private TrackEntry NewTrackEntry(int trackIndex, Animation animation, bool loop, TrackEntry last)
		{
			TrackEntry trackEntry = this.trackEntryPool.Obtain();
			trackEntry.trackIndex = trackIndex;
			trackEntry.animation = animation;
			trackEntry.loop = loop;
			trackEntry.holdPrevious = false;
			trackEntry.eventThreshold = 0f;
			trackEntry.alphaAttachmentThreshold = 0f;
			trackEntry.mixAttachmentThreshold = 0f;
			trackEntry.mixDrawOrderThreshold = 0f;
			trackEntry.animationStart = 0f;
			trackEntry.animationEnd = animation.Duration;
			trackEntry.animationLast = -1f;
			trackEntry.nextAnimationLast = -1f;
			trackEntry.delay = 0f;
			trackEntry.trackTime = 0f;
			trackEntry.trackLast = -1f;
			trackEntry.nextTrackLast = -1f;
			trackEntry.trackEnd = float.MaxValue;
			trackEntry.timeScale = 1f;
			trackEntry.alpha = 1f;
			trackEntry.interruptAlpha = 1f;
			trackEntry.mixTime = 0f;
			trackEntry.mixDuration = ((last == null) ? 0f : this.data.GetMix(last.animation, animation));
			trackEntry.mixBlend = MixBlend.Replace;
			return trackEntry;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008668 File Offset: 0x00006868
		public void ClearNext(TrackEntry entry)
		{
			for (TrackEntry next = entry.next; next != null; next = next.next)
			{
				this.queue.Dispose(next);
			}
			entry.next = null;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000869C File Offset: 0x0000689C
		private void AnimationsChanged()
		{
			this.animationsChanged = false;
			this.propertyIds.Clear();
			int i = this.tracks.Count;
			TrackEntry[] tracksItems = this.tracks.Items;
			for (int j = 0; j < i; j++)
			{
				TrackEntry entry = tracksItems[j];
				if (entry != null)
				{
					while (entry.mixingFrom != null)
					{
						entry = entry.mixingFrom;
					}
					do
					{
						if (entry.mixingTo == null || entry.mixBlend != MixBlend.Add)
						{
							this.ComputeHold(entry);
						}
						entry = entry.mixingTo;
					}
					while (entry != null);
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000871C File Offset: 0x0000691C
		private void ComputeHold(TrackEntry entry)
		{
			TrackEntry to = entry.mixingTo;
			Timeline[] timelines = entry.animation.timelines.Items;
			int timelinesCount = entry.animation.timelines.Count;
			int[] timelineMode = entry.timelineMode.Resize(timelinesCount).Items;
			entry.timelineHoldMix.Clear(true);
			TrackEntry[] timelineHoldMix = entry.timelineHoldMix.Resize(timelinesCount).Items;
			HashSet<string> propertyIds = this.propertyIds;
			if (to != null && to.holdPrevious)
			{
				for (int i = 0; i < timelinesCount; i++)
				{
					timelineMode[i] = (propertyIds.AddAll(timelines[i].PropertyIds) ? 3 : 2);
				}
				return;
			}
			for (int j = 0; j < timelinesCount; j++)
			{
				Timeline timeline = timelines[j];
				string[] ids = timeline.PropertyIds;
				if (!propertyIds.AddAll(ids))
				{
					timelineMode[j] = 0;
				}
				else if (to == null || timeline is AttachmentTimeline || timeline is DrawOrderTimeline || timeline is EventTimeline || !to.animation.HasTimeline(ids))
				{
					timelineMode[j] = 1;
				}
				else
				{
					TrackEntry next = to.mixingTo;
					while (next != null)
					{
						if (!next.animation.HasTimeline(ids))
						{
							if (next.mixDuration > 0f)
							{
								timelineMode[j] = 4;
								timelineHoldMix[j] = next;
								goto IL_013D;
							}
							break;
						}
						else
						{
							next = next.mixingTo;
						}
					}
					timelineMode[j] = 3;
				}
				IL_013D:;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008874 File Offset: 0x00006A74
		public TrackEntry GetCurrent(int trackIndex)
		{
			if (trackIndex >= this.tracks.Count)
			{
				return null;
			}
			return this.tracks.Items[trackIndex];
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008893 File Offset: 0x00006A93
		public void ClearListenerNotifications()
		{
			this.queue.Clear();
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000088A0 File Offset: 0x00006AA0
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000088A8 File Offset: 0x00006AA8
		public float TimeScale
		{
			get
			{
				return this.timeScale;
			}
			set
			{
				this.timeScale = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000088B1 File Offset: 0x00006AB1
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000088B9 File Offset: 0x00006AB9
		public AnimationStateData Data
		{
			get
			{
				return this.data;
			}
			set
			{
				if (this.data == null)
				{
					throw new ArgumentNullException("data", "data cannot be null.");
				}
				this.data = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000088DA File Offset: 0x00006ADA
		public ExposedList<TrackEntry> Tracks
		{
			get
			{
				return this.tracks;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000088E4 File Offset: 0x00006AE4
		public override string ToString()
		{
			StringBuilder buffer = new StringBuilder();
			TrackEntry[] tracksItems = this.tracks.Items;
			int i = 0;
			int j = this.tracks.Count;
			while (i < j)
			{
				TrackEntry entry = tracksItems[i];
				if (entry != null)
				{
					if (buffer.Length > 0)
					{
						buffer.Append(", ");
					}
					buffer.Append(entry.ToString());
				}
				i++;
			}
			if (buffer.Length == 0)
			{
				return "<none>";
			}
			return buffer.ToString();
		}

		// Token: 0x040000A3 RID: 163
		internal static readonly Animation EmptyAnimation = new Animation("<empty>", new ExposedList<Timeline>(), 0f);

		// Token: 0x040000A4 RID: 164
		internal const int Subsequent = 0;

		// Token: 0x040000A5 RID: 165
		internal const int First = 1;

		// Token: 0x040000A6 RID: 166
		internal const int HoldSubsequent = 2;

		// Token: 0x040000A7 RID: 167
		internal const int HoldFirst = 3;

		// Token: 0x040000A8 RID: 168
		internal const int HoldMix = 4;

		// Token: 0x040000A9 RID: 169
		internal const int Setup = 1;

		// Token: 0x040000AA RID: 170
		internal const int Current = 2;

		// Token: 0x040000AB RID: 171
		protected AnimationStateData data;

		// Token: 0x040000AC RID: 172
		private readonly ExposedList<TrackEntry> tracks = new ExposedList<TrackEntry>();

		// Token: 0x040000AD RID: 173
		private readonly ExposedList<Event> events = new ExposedList<Event>();

		// Token: 0x040000B4 RID: 180
		private readonly EventQueue queue;

		// Token: 0x040000B5 RID: 181
		private readonly HashSet<string> propertyIds = new HashSet<string>();

		// Token: 0x040000B6 RID: 182
		private bool animationsChanged;

		// Token: 0x040000B7 RID: 183
		private float timeScale = 1f;

		// Token: 0x040000B8 RID: 184
		private int unkeyedState;

		// Token: 0x040000B9 RID: 185
		private readonly Pool<TrackEntry> trackEntryPool = new Pool<TrackEntry>(16, int.MaxValue);

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x0600011E RID: 286
		public delegate void TrackEntryDelegate(TrackEntry trackEntry);

		// Token: 0x02000036 RID: 54
		// (Invoke) Token: 0x06000122 RID: 290
		public delegate void TrackEntryEventDelegate(TrackEntry trackEntry, Event e);
	}
}
