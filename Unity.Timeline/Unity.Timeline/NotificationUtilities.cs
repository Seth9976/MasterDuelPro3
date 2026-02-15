using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000073 RID: 115
	internal static class NotificationUtilities
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000AD87 File Offset: 0x00008F87
		public static ScriptPlayable<TimeNotificationBehaviour> CreateNotificationsPlayable(PlayableGraph graph, IEnumerable<IMarker> markers, PlayableDirector director)
		{
			return NotificationUtilities.CreateNotificationsPlayable(graph, markers, null, director);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000AD92 File Offset: 0x00008F92
		public static ScriptPlayable<TimeNotificationBehaviour> CreateNotificationsPlayable(PlayableGraph graph, IEnumerable<IMarker> markers, TimelineAsset timelineAsset)
		{
			return NotificationUtilities.CreateNotificationsPlayable(graph, markers, timelineAsset, null);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		private static ScriptPlayable<TimeNotificationBehaviour> CreateNotificationsPlayable(PlayableGraph graph, IEnumerable<IMarker> markers, IPlayableAsset asset, PlayableDirector director)
		{
			ScriptPlayable<TimeNotificationBehaviour> notificationPlayable = ScriptPlayable<TimeNotificationBehaviour>.Null;
			DirectorWrapMode extrapolationMode = ((director != null) ? director.extrapolationMode : DirectorWrapMode.None);
			bool didCalculateDuration = false;
			double duration = 0.0;
			foreach (IMarker e in markers)
			{
				INotification notification = e as INotification;
				if (notification != null)
				{
					if (!didCalculateDuration)
					{
						duration = ((director != null) ? director.playableAsset.duration : asset.duration);
						didCalculateDuration = true;
					}
					if (notificationPlayable.Equals(ScriptPlayable<TimeNotificationBehaviour>.Null))
					{
						notificationPlayable = TimeNotificationBehaviour.Create(graph, duration, extrapolationMode);
					}
					DiscreteTime time = (DiscreteTime)e.time;
					DiscreteTime tlDuration = (DiscreteTime)duration;
					if (time >= tlDuration && time <= tlDuration.OneTickAfter() && tlDuration != 0)
					{
						time = tlDuration.OneTickBefore();
					}
					INotificationOptionProvider notificationOptionProvider = e as INotificationOptionProvider;
					if (notificationOptionProvider != null)
					{
						notificationPlayable.GetBehaviour().AddNotification((double)time, notification, notificationOptionProvider.flags);
					}
					else
					{
						notificationPlayable.GetBehaviour().AddNotification((double)time, notification, NotificationFlags.Retroactive);
					}
				}
			}
			return notificationPlayable;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000AEE8 File Offset: 0x000090E8
		public static bool TrackTypeSupportsNotifications(Type type)
		{
			TrackBindingTypeAttribute binding = (TrackBindingTypeAttribute)Attribute.GetCustomAttribute(type, typeof(TrackBindingTypeAttribute));
			return binding != null && (typeof(Component).IsAssignableFrom(binding.type) || typeof(GameObject).IsAssignableFrom(binding.type));
		}
	}
}
