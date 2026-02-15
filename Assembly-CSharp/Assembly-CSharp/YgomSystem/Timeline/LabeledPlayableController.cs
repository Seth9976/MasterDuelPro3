using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006AE RID: 1710
	public class LabeledPlayableController : MonoBehaviour
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x0000216D File Offset: 0x0000036D
		public DirectorWrapMode wrapMode
		{
			get
			{
				return DirectorWrapMode.Hold;
			}
			set
			{
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x0000216D File Offset: 0x0000036D
		public LabelDirectorWrapMode labelWrapMode
		{
			get
			{
				return LabelDirectorWrapMode.Hold;
			}
			set
			{
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x000029CC File Offset: 0x00000BCC
		public PlayState state
		{
			get
			{
				return PlayState.Paused;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x0000216A File Offset: 0x0000036A
		public string playLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600357B RID: 13691 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600357C RID: 13692 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<string, LabeledPlayableController> stopped
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600357D RID: 13693 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600357E RID: 13694 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<string, LabeledPlayableController> played
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600357F RID: 13695 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06003580 RID: 13696 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<string, LabeledPlayableController> paused
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000F3048 File Offset: 0x000F1248
		public static LabeledPlayableController Create(PlayableDirector target)
		{
			LabeledPlayableController labeledPlayableController = target.gameObject.AddComponent<LabeledPlayableController>();
			labeledPlayableController.m_Director = target;
			return labeledPlayableController;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000F305C File Offset: 0x000F125C
		private void Awake()
		{
			this.initialized = true;
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCreatedLabelMixer(LabelMixerBehaviour labelMixer)
		{
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x0000216D File Offset: 0x0000036D
		public void Pause()
		{
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetLabelClipExWrapMode(string label, LabelDirectorWrapMode mode)
		{
			return false;
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000F3065 File Offset: 0x000F1265
		public void PlayLabel(string label, TimelineClip loopClip)
		{
			this.loopMixerBehaviour.PlayClip(label, loopClip);
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLabel(string label)
		{
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLabel(string label, PlayableAsset asset)
		{
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLabel(string label, LabelDirectorWrapMode mode)
		{
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLabel(string label, PlayableAsset asset, LabelDirectorWrapMode mode)
		{
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool PlayNextLabel()
		{
			return false;
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x0000216A File Offset: 0x0000036A
		public string SearchNextLabel()
		{
			return null;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x0000216D File Offset: 0x0000036D
		public void Resume()
		{
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop()
		{
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingLabel(string label)
		{
			return false;
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnStopped(PlayableDirector director)
		{
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayed(PlayableDirector director)
		{
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPaused(PlayableDirector director)
		{
		}

		// Token: 0x040030D2 RID: 12498
		private PlayableDirector m_Director;

		// Token: 0x040030D3 RID: 12499
		private Dictionary<string, TimelineClip> m_TrackClips;

		// Token: 0x040030D4 RID: 12500
		private Dictionary<string, LabelDirectorWrapMode> m_LabelModeTable;

		// Token: 0x040030D5 RID: 12501
		private string[] m_ClipLabels;

		// Token: 0x040030D6 RID: 12502
		private DirectorWrapMode m_WrapMode;

		// Token: 0x040030D7 RID: 12503
		[SerializeField]
		private LabelDirectorWrapMode m_LabelWrapMode;

		// Token: 0x040030D8 RID: 12504
		[SerializeField]
		private string m_PlayLabel;

		// Token: 0x040030D9 RID: 12505
		private double m_LastCheckedTime;

		// Token: 0x040030DA RID: 12506
		public bool initialized;

		// Token: 0x040030DB RID: 12507
		public LoopMixerBehaviour loopMixerBehaviour;
	}
}
