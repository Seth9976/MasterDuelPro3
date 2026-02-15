using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006C8 RID: 1736
	public class TimelineObject : MonoBehaviour
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x000029CC File Offset: 0x00000BCC
		public PlayState state
		{
			get
			{
				return PlayState.Paused;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000F165E File Offset: 0x000EF85E
		public double time
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06003610 RID: 13840 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003611 RID: 13841 RVA: 0x0000216D File Offset: 0x0000036D
		public string path
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

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003613 RID: 13843 RVA: 0x0000216D File Offset: 0x0000036D
		public string group
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x0000216A File Offset: 0x0000036A
		public PlayableDirector playableDirector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineObject CreateTimelineObject(GameObject gob, string path, string group)
		{
			return null;
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddOnStopCallBack(UnityAction<PlayableDirector> onstop)
		{
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddOnPlayCallback(UnityAction<PlayableDirector> onplay)
		{
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddOnPauseCallback(UnityAction<PlayableDirector> onpause)
		{
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayableDirectorStop(PlayableDirector pd)
		{
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play()
		{
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop()
		{
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x0000216D File Offset: 0x0000036D
		public void Pause()
		{
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0000216D File Offset: 0x0000036D
		public void Resume()
		{
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x0000216D File Offset: 0x0000036D
		public void Evaluate()
		{
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x0000216D File Offset: 0x0000036D
		public void SkipTo(float target, float start = 0f)
		{
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x0000216A File Offset: 0x0000036A
		public LabeledPlayableController GetLabeledPlayableController()
		{
			return null;
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x0000216D File Offset: 0x0000036D
		public void Recycle()
		{
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCached()
		{
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayableDirectorPlay(PlayableDirector pd)
		{
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayableDirectorPause(PlayableDirector pd)
		{
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetEventQueue(Queue<UnityAction<PlayableDirector>> eventQueue)
		{
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0400311D RID: 12573
		public TimelineManager.EndEventType endEventType;

		// Token: 0x0400311E RID: 12574
		public UnityAction onDestroy;

		// Token: 0x0400311F RID: 12575
		protected PlayableDirector m_PlayableDirector;

		// Token: 0x04003120 RID: 12576
		protected Queue<UnityAction<PlayableDirector>> onStopQueue;

		// Token: 0x04003121 RID: 12577
		protected Queue<UnityAction<PlayableDirector>> onPlayQueue;

		// Token: 0x04003122 RID: 12578
		protected Queue<UnityAction<PlayableDirector>> onPauseQueue;
	}
}
