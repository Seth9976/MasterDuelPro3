using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200005A RID: 90
	public class TimeNotificationBehaviour : PlayableBehaviour
	{
		// Token: 0x170000CB RID: 203
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00009FB2 File Offset: 0x000081B2
		public Playable timeSource
		{
			set
			{
				this.m_TimeSource = value;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00009FBB File Offset: 0x000081BB
		public static ScriptPlayable<TimeNotificationBehaviour> Create(PlayableGraph graph, double duration, DirectorWrapMode loopMode)
		{
			ScriptPlayable<TimeNotificationBehaviour> scriptPlayable = ScriptPlayable<TimeNotificationBehaviour>.Create(graph, 0);
			scriptPlayable.SetDuration(duration);
			scriptPlayable.SetTimeWrapMode(loopMode);
			scriptPlayable.SetPropagateSetTime(true);
			return scriptPlayable;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009FDC File Offset: 0x000081DC
		public void AddNotification(double time, INotification payload, NotificationFlags flags = NotificationFlags.Retroactive)
		{
			this.m_Notifications.Add(new TimeNotificationBehaviour.NotificationEntry
			{
				time = time,
				payload = payload,
				flags = flags
			});
			this.m_NeedSortNotifications = true;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000A01C File Offset: 0x0000821C
		public override void OnGraphStart(Playable playable)
		{
			this.SortNotifications();
			double currentTime = playable.GetTime<Playable>();
			for (int i = 0; i < this.m_Notifications.Count; i++)
			{
				if (this.m_Notifications[i].time > currentTime && !this.m_Notifications[i].triggerOnce)
				{
					TimeNotificationBehaviour.NotificationEntry notification = this.m_Notifications[i];
					notification.notificationFired = false;
					this.m_Notifications[i] = notification;
				}
			}
			this.m_PreviousTime = playable.GetTime<Playable>();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (playable.IsDone<Playable>())
			{
				this.SortNotifications();
				for (int i = 0; i < this.m_Notifications.Count; i++)
				{
					TimeNotificationBehaviour.NotificationEntry e = this.m_Notifications[i];
					if (!e.notificationFired)
					{
						double duration = playable.GetDuration<Playable>();
						if (this.m_PreviousTime <= e.time && e.time <= duration)
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref e);
							this.m_Notifications[i] = e;
						}
					}
				}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000A12C File Offset: 0x0000832C
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (info.evaluationType == FrameData.EvaluationType.Evaluate)
			{
				return;
			}
			this.SyncDurationWithExternalSource(playable);
			this.SortNotifications();
			double currentTime = playable.GetTime<Playable>();
			if (info.timeLooped)
			{
				double duration = playable.GetDuration<Playable>();
				this.TriggerNotificationsInRange(this.m_PreviousTime, duration, info, playable, true);
				double dx = playable.GetDuration<Playable>() - this.m_PreviousTime;
				int nFullTimelines = (int)(((double)(info.deltaTime * info.effectiveSpeed) - dx) / playable.GetDuration<Playable>());
				for (int i = 0; i < nFullTimelines; i++)
				{
					this.TriggerNotificationsInRange(0.0, duration, info, playable, false);
				}
				this.TriggerNotificationsInRange(0.0, currentTime, info, playable, false);
			}
			else
			{
				double pt = playable.GetTime<Playable>();
				this.TriggerNotificationsInRange(this.m_PreviousTime, pt, info, playable, true);
			}
			for (int j = 0; j < this.m_Notifications.Count; j++)
			{
				TimeNotificationBehaviour.NotificationEntry e = this.m_Notifications[j];
				if (e.notificationFired && TimeNotificationBehaviour.CanRestoreNotification(e, info, currentTime, this.m_PreviousTime))
				{
					TimeNotificationBehaviour.Restore_internal(ref e);
					this.m_Notifications[j] = e;
				}
			}
			this.m_PreviousTime = playable.GetTime<Playable>();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000A256 File Offset: 0x00008456
		private void SortNotifications()
		{
			if (this.m_NeedSortNotifications)
			{
				this.m_Notifications.Sort((TimeNotificationBehaviour.NotificationEntry x, TimeNotificationBehaviour.NotificationEntry y) => x.time.CompareTo(y.time));
				this.m_NeedSortNotifications = false;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A291 File Offset: 0x00008491
		private static bool CanRestoreNotification(TimeNotificationBehaviour.NotificationEntry e, FrameData info, double currentTime, double previousTime)
		{
			return !e.triggerOnce && (info.timeLooped || (previousTime > currentTime && currentTime <= e.time));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000A2BC File Offset: 0x000084BC
		private void TriggerNotificationsInRange(double start, double end, FrameData info, Playable playable, bool checkState)
		{
			if (start <= end)
			{
				bool playMode = Application.isPlaying;
				for (int i = 0; i < this.m_Notifications.Count; i++)
				{
					TimeNotificationBehaviour.NotificationEntry e = this.m_Notifications[i];
					if (!e.notificationFired || (!checkState && !e.triggerOnce))
					{
						double notificationTime = e.time;
						if (e.prewarm && notificationTime < end && (e.triggerInEditor || playMode))
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref e);
							this.m_Notifications[i] = e;
						}
						else if (notificationTime >= start && notificationTime <= end && (e.triggerInEditor || playMode))
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref e);
							this.m_Notifications[i] = e;
						}
					}
				}
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000A382 File Offset: 0x00008582
		private void SyncDurationWithExternalSource(Playable playable)
		{
			if (this.m_TimeSource.IsValid<Playable>())
			{
				playable.SetDuration(this.m_TimeSource.GetDuration<Playable>());
				playable.SetTimeWrapMode(this.m_TimeSource.GetTimeWrapMode<Playable>());
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000A3B3 File Offset: 0x000085B3
		private static void Trigger_internal(Playable playable, PlayableOutput output, ref TimeNotificationBehaviour.NotificationEntry e)
		{
			output.PushNotification(playable, e.payload, null);
			e.notificationFired = true;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000A3CA File Offset: 0x000085CA
		private static void Restore_internal(ref TimeNotificationBehaviour.NotificationEntry e)
		{
			e.notificationFired = false;
		}

		// Token: 0x04000152 RID: 338
		private readonly List<TimeNotificationBehaviour.NotificationEntry> m_Notifications = new List<TimeNotificationBehaviour.NotificationEntry>();

		// Token: 0x04000153 RID: 339
		private double m_PreviousTime;

		// Token: 0x04000154 RID: 340
		private bool m_NeedSortNotifications;

		// Token: 0x04000155 RID: 341
		private Playable m_TimeSource;

		// Token: 0x0200005B RID: 91
		private struct NotificationEntry
		{
			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000303 RID: 771 RVA: 0x0000A3E6 File Offset: 0x000085E6
			public bool triggerInEditor
			{
				get
				{
					return (this.flags & NotificationFlags.TriggerInEditMode) > (NotificationFlags)0;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000304 RID: 772 RVA: 0x0000A3F3 File Offset: 0x000085F3
			public bool prewarm
			{
				get
				{
					return (this.flags & NotificationFlags.Retroactive) > (NotificationFlags)0;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000305 RID: 773 RVA: 0x0000A400 File Offset: 0x00008600
			public bool triggerOnce
			{
				get
				{
					return (this.flags & NotificationFlags.TriggerOnce) > (NotificationFlags)0;
				}
			}

			// Token: 0x04000156 RID: 342
			public double time;

			// Token: 0x04000157 RID: 343
			public INotification payload;

			// Token: 0x04000158 RID: 344
			public bool notificationFired;

			// Token: 0x04000159 RID: 345
			public NotificationFlags flags;
		}
	}
}
