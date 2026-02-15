using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006C5 RID: 1733
	public class TimelineManager : MonoBehaviour
	{
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x0000216A File Offset: 0x0000036A
		private static TimelineManager instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x0000216A File Offset: 0x0000036A
		private static TimelineManager CreateTimelineManager()
		{
			return null;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DestroyAllTimelineObject()
		{
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetTImelineTableOfGroup(string group, bool includeEndEventTypeNone = true)
		{
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetTImelineTableOfGroupInpl(string group, bool includeEndEventTypeNone)
		{
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenTimelineAsync2D(string path, UnityAction<PlayableDirector> onLoaded, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenTimelineAsync2D(string group, string path, UnityAction<PlayableDirector> onLoaded, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenTimelineAsync3D(string path, UnityAction<PlayableDirector> onLoaded, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenTimelineAsync3D(string group, string path, UnityAction<PlayableDirector> onLoaded, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PreloadTimeline(string path, Action<bool> onFinish = null, int instancenum = 1, bool boostEnable = true)
		{
			return false;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PreloadTimeline(string group, string path, Action<bool> onFinish = null, int instancenum = 1, bool boostEnable = true)
		{
			return false;
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool PreloadTimelineImpl(string group, string path, Action<bool> onFinish = null, int instancenum = 1, bool boostEnable = true)
		{
			return false;
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenTimelineAsync(string path, UnityAction<PlayableDirector> onLoaded, Transform parent, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE, bool boostEnable = true)
		{
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenTimelineAsync(string group, string path, UnityAction<PlayableDirector> onLoaded, Transform parent, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE, bool boostEnable = true)
		{
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenTimelineAsyncImpl(string group, string path, UnityAction<PlayableDirector> onLoaded, Transform parent, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE, bool boostEnable = true)
		{
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineObject OpenTimeline(string path, Transform parent, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE, bool boostEnable = true)
		{
			return null;
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineObject OpenTimeline(string group, string path, Transform parent, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE, bool boostEnable = true)
		{
			return null;
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x0000216A File Offset: 0x0000036A
		private TimelineObject OpenTimelineImpl(string group, string path, Transform parent, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE, bool boostEnable = true)
		{
			return null;
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineObject OpenTimeline2D(string path, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
			return null;
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineObject OpenTimeline2D(string group, string path, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
			return null;
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineObject OpenTimeline3D(string path, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
			return null;
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineObject OpenTimeline3D(string group, string path, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
			return null;
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool RecycleTimeline(string path, TimelineObject timelineObject)
		{
			return false;
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool RecycleTimeline(string group, string path, TimelineObject timelineObject)
		{
			return false;
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool RecycleTimelineImpl(string group, string path, TimelineObject timelineObject)
		{
			return false;
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UpdateTimelineSpeed(string group, double speed)
		{
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UpdateTimelineSpeed(double speed)
		{
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateBoostModeImpl(string group, double speed)
		{
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x0000216D File Offset: 0x0000036D
		private void TimelineOnLoaded(TimelineObject to, UnityAction<PlayableDirector> onLoaded, Transform parent, Action onStop = null, bool autoPlay = true, TimelineManager.EndEventType endEvent = TimelineManager.EndEventType.AUTORECYCLE)
		{
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x0000216D File Offset: 0x0000036D
		private void RegisterTimelineObejct(TimelineObject to)
		{
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetEventCallback(string label, Action callback)
		{
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeEventCallback(string label)
		{
		}

		// Token: 0x0400310C RID: 12556
		public const string TM_LABEL_DUEL = "Duel";

		// Token: 0x0400310D RID: 12557
		public const string TM_LABEL_BASE = "Base";

		// Token: 0x0400310E RID: 12558
		public const string GROUP_LABEL_DEFAULT = "DefaultGroup";

		// Token: 0x0400310F RID: 12559
		public const string GROUP_LABEL_DUEL = "DuelGroup";

		// Token: 0x04003110 RID: 12560
		private static TimelineManager m_Instance;

		// Token: 0x04003111 RID: 12561
		private Dictionary<string, Dictionary<string, TimelineManager.TimelineObjectDesc>> m_CachedTimelineObjectGroupTable;

		// Token: 0x04003112 RID: 12562
		private Dictionary<int, TimelineObject> m_AllTimelineObjectInstanceTable;

		// Token: 0x04003113 RID: 12563
		private RectTransform m_TimeLineRoot2D;

		// Token: 0x04003114 RID: 12564
		private Transform m_TimeLineRoot3D;

		// Token: 0x04003115 RID: 12565
		private Transform m_HidePool;

		// Token: 0x04003116 RID: 12566
		private static Dictionary<string, Action> eventCallback;

		// Token: 0x020006C6 RID: 1734
		internal class TimelineObjectDesc
		{
			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x0600360B RID: 13835 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600360C RID: 13836 RVA: 0x0000216D File Offset: 0x0000036D
			public string label
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x0600360D RID: 13837 RVA: 0x00002739 File Offset: 0x00000939
			public TimelineObjectDesc(string label, bool boostEnable)
			{
			}

			// Token: 0x04003117 RID: 12567
			public Stack<int> toidList;

			// Token: 0x04003118 RID: 12568
			public bool boostEnable;
		}

		// Token: 0x020006C7 RID: 1735
		public enum EndEventType
		{
			// Token: 0x0400311A RID: 12570
			NONE,
			// Token: 0x0400311B RID: 12571
			AUTODESTROY,
			// Token: 0x0400311C RID: 12572
			AUTORECYCLE
		}
	}
}
