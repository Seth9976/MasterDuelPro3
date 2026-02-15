using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Willow
{
	// Token: 0x02001552 RID: 5458
	public class TimelineController : MonoBehaviour
	{
		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06009E51 RID: 40529 RVA: 0x0000216A File Offset: 0x0000036A
		protected IEnumerable<PlayableBinding> bindingAll
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06009E52 RID: 40530 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerable<TrackAsset> trackAll
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06009E53 RID: 40531 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerable<TrackAsset> tracks
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06009E54 RID: 40532 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerable<TrackAsset> trackRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06009E55 RID: 40533 RVA: 0x0019B5BC File Offset: 0x001997BC
		// (set) Token: 0x06009E56 RID: 40534 RVA: 0x0019B5C4 File Offset: 0x001997C4
		public PlayableDirector currentDirector
		{
			get
			{
				return this.m_currentDirector;
			}
			set
			{
				this.m_currentDirector = value;
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06009E57 RID: 40535 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E58 RID: 40536 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPlay
		{
			get
			{
				return false;
			}
			private set
			{
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06009E59 RID: 40537 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E5A RID: 40538 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPause
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06009E5B RID: 40539 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLoop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06009E5C RID: 40540 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E5D RID: 40541 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isLoopIgnore
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06009E5E RID: 40542 RVA: 0x000F165E File Offset: 0x000EF85E
		// (set) Token: 0x06009E5F RID: 40543 RVA: 0x0000216D File Offset: 0x0000036D
		public double currentTime
		{
			[CompilerGenerated]
			get
			{
				return 0.0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06009E60 RID: 40544 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentFrame
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06009E61 RID: 40545 RVA: 0x000F165E File Offset: 0x000EF85E
		public double oneFrame
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06009E62 RID: 40546 RVA: 0x000F165E File Offset: 0x000EF85E
		// (set) Token: 0x06009E63 RID: 40547 RVA: 0x0000216D File Offset: 0x0000036D
		public double startTime
		{
			[CompilerGenerated]
			get
			{
				return 0.0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06009E64 RID: 40548 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06009E65 RID: 40549 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<bool> onChangeLoopState
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06009E66 RID: 40550 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Start()
		{
		}

		// Token: 0x06009E67 RID: 40551 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void LateUpdate()
		{
		}

		// Token: 0x06009E68 RID: 40552 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06009E69 RID: 40553 RVA: 0x0000216D File Offset: 0x0000036D
		private void Clear()
		{
		}

		// Token: 0x06009E6A RID: 40554 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Finish(bool isEndCall = true)
		{
		}

		// Token: 0x06009E6B RID: 40555 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void CheckDirector()
		{
		}

		// Token: 0x06009E6C RID: 40556 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Proceed(float deltaTime)
		{
		}

		// Token: 0x06009E6D RID: 40557 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06009E6E RID: 40558 RVA: 0x0000216D File Offset: 0x0000036D
		public void ChangeDirector(PlayableDirector director)
		{
		}

		// Token: 0x06009E6F RID: 40559 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Play(Action endCall, float speed = 1f, double startTimePosition = 0.0)
		{
		}

		// Token: 0x06009E70 RID: 40560 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Play(Action endCall, float speed, string startLabel)
		{
		}

		// Token: 0x06009E71 RID: 40561 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void ChangeSpeed(float speed)
		{
		}

		// Token: 0x06009E72 RID: 40562 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Stop(bool isEndTime = true, bool isEndCall = true)
		{
		}

		// Token: 0x06009E73 RID: 40563 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Pause(bool isPause, bool isForced = false)
		{
		}

		// Token: 0x06009E74 RID: 40564 RVA: 0x0000216D File Offset: 0x0000036D
		public void SkipLoop(bool isForce = true, Action onExitLoop = null, double timeAdjust = 0.0)
		{
		}

		// Token: 0x06009E75 RID: 40565 RVA: 0x0000216D File Offset: 0x0000036D
		public void Skip(double goalTime = 0.0)
		{
		}

		// Token: 0x06009E76 RID: 40566 RVA: 0x0000216A File Offset: 0x0000036A
		public TimelineController.ClipData GetLoopInfo(int index = 0)
		{
			return null;
		}

		// Token: 0x06009E77 RID: 40567 RVA: 0x000F165E File Offset: 0x000EF85E
		public double GetLabelTime(string labelName)
		{
			return 0.0;
		}

		// Token: 0x06009E78 RID: 40568 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool GoToLabel(string labelName, bool isForced = false)
		{
			return false;
		}

		// Token: 0x06009E79 RID: 40569 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] GetLabelNameList()
		{
			return null;
		}

		// Token: 0x06009E7A RID: 40570 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBind<T>(string trackName, T obj) where T : global::UnityEngine.Object
		{
		}

		// Token: 0x06009E7B RID: 40571 RVA: 0x0019B5D0 File Offset: 0x001997D0
		public T GetClip<T>(string trackName, string clipName) where T : PlayableAsset
		{
			return default(T);
		}

		// Token: 0x06009E7C RID: 40572 RVA: 0x0000216A File Offset: 0x0000036A
		public List<T> GetClips<T>(string trackName, string clipName) where T : PlayableAsset
		{
			return null;
		}

		// Token: 0x06009E7D RID: 40573 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBindCMCamera(CinemachineBrain brain, string trackName = "Cinemachine Track")
		{
		}

		// Token: 0x06009E7E RID: 40574 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMuteTrack(string trackName, bool isMute, bool isMuteBindObject = false)
		{
		}

		// Token: 0x06009E7F RID: 40575 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMuteTrackBindObject(TrackAsset track, bool isMute)
		{
		}

		// Token: 0x0400DDED RID: 56813
		[SerializeField]
		private PlayableDirector m_currentDirector;

		// Token: 0x0400DDEE RID: 56814
		private List<TimelineController.ClipData> m_listLoop;

		// Token: 0x0400DDEF RID: 56815
		private List<TimelineController.ClipData> m_listLabel;

		// Token: 0x0400DDF0 RID: 56816
		private bool m_isPlay;

		// Token: 0x0400DDF1 RID: 56817
		private bool m_isReady;

		// Token: 0x0400DDF2 RID: 56818
		private bool m_hasLoop;

		// Token: 0x0400DDF3 RID: 56819
		private int m_loopIndex;

		// Token: 0x0400DDF4 RID: 56820
		private double m_endTime;

		// Token: 0x0400DDF5 RID: 56821
		private double m_speedRate;

		// Token: 0x0400DDF6 RID: 56822
		protected bool m_isForceFinish;

		// Token: 0x0400DDF7 RID: 56823
		protected double m_oneFrame;

		// Token: 0x0400DDF8 RID: 56824
		private Action m_endCallHandler;

		// Token: 0x0400DDF9 RID: 56825
		private Action m_onExitLoopHandler;

		// Token: 0x0400DDFA RID: 56826
		private IEnumerable<PlayableBinding> m_bindingAll;

		// Token: 0x0400DDFB RID: 56827
		private IEnumerable<TrackAsset> m_trackAll;

		// Token: 0x0400DDFC RID: 56828
		private IEnumerable<TrackAsset> m_trackRoot;

		// Token: 0x0400DDFD RID: 56829
		private bool m_isJustBeforeEnd;

		// Token: 0x0400DDFE RID: 56830
		public const string kAutoCinemachineTrackName = "Cinemachine Track";

		// Token: 0x02001553 RID: 5459
		public class ClipData
		{
			// Token: 0x170014CD RID: 5325
			// (get) Token: 0x06009E81 RID: 40577 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06009E82 RID: 40578 RVA: 0x0000216D File Offset: 0x0000036D
			public string label
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170014CE RID: 5326
			// (get) Token: 0x06009E83 RID: 40579 RVA: 0x000F165E File Offset: 0x000EF85E
			// (set) Token: 0x06009E84 RID: 40580 RVA: 0x0000216D File Offset: 0x0000036D
			public double startTime
			{
				[CompilerGenerated]
				get
				{
					return 0.0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170014CF RID: 5327
			// (get) Token: 0x06009E85 RID: 40581 RVA: 0x000F165E File Offset: 0x000EF85E
			// (set) Token: 0x06009E86 RID: 40582 RVA: 0x0000216D File Offset: 0x0000036D
			public double endTime
			{
				[CompilerGenerated]
				get
				{
					return 0.0;
				}
				[CompilerGenerated]
				set
				{
				}
			}
		}
	}
}
