using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Scenario
{
	// Token: 0x020009CB RID: 2507
	public class ScenarioCardActor : ElementWidgetBase
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060048DF RID: 18655 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ready
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060048E1 RID: 18657 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPlaying
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

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060048E2 RID: 18658 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060048E3 RID: 18659 RVA: 0x0000216D File Offset: 0x0000036D
		public int mrk
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060048E5 RID: 18661 RVA: 0x0000216D File Offset: 0x0000036D
		public int subMrk
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060048E6 RID: 18662 RVA: 0x0000216A File Offset: 0x0000036A
		public ScenarioCardPopActor popActor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isPlayingFadeOut
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ScenarioCardActor(ElementObjectManager eom, ElementObjectManager cardEom, ElementObjectManager popEom, ScenarioCardActor.TimelineAssets timelineAssets)
			: base(null)
		{
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Clear()
		{
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearSub()
		{
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x0000216D File Offset: 0x0000036D
		public void CaptureSub()
		{
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(int mrk)
		{
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideFront()
		{
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFadeIn()
		{
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFadeOut()
		{
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlaySwap()
		{
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayed(PlayableDirector director)
		{
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnStopped(PlayableDirector director)
		{
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToBlurTarget()
		{
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToIgnoreBlurTarget()
		{
		}

		// Token: 0x040086D5 RID: 34517
		internal const string k_ActorProtectorPath = "Protector/<_CARD_ILLUST_>/0001/PMat";

		// Token: 0x040086D6 RID: 34518
		private readonly string k_ELabelBackModel;

		// Token: 0x040086D7 RID: 34519
		private readonly string k_ELabelFrontModel;

		// Token: 0x040086D8 RID: 34520
		private readonly string k_ELabelSubFrontModel;

		// Token: 0x040086D9 RID: 34521
		private readonly string k_ELabelSideModel;

		// Token: 0x040086DA RID: 34522
		private PlayableDirector m_PlayableDirector;

		// Token: 0x040086DB RID: 34523
		private ScenarioCardActor.TimelineAssets m_TimelineAssets;

		// Token: 0x040086DC RID: 34524
		private readonly ScenarioCardPopActor m_CardPopActor;

		// Token: 0x040086DD RID: 34525
		public readonly MeshRenderer frontRenderer;

		// Token: 0x040086DE RID: 34526
		public readonly MeshRenderer subFrontRenderer;

		// Token: 0x040086DF RID: 34527
		public readonly MeshRenderer backRenderer;

		// Token: 0x040086E0 RID: 34528
		public readonly MeshRenderer sideRenderer;

		// Token: 0x040086E1 RID: 34529
		private int m_LoadingCnt;

		// Token: 0x020009CC RID: 2508
		public class TimelineAssets
		{
			// Token: 0x040086E2 RID: 34530
			public TimelineAsset timelineFadeIn;

			// Token: 0x040086E3 RID: 34531
			public TimelineAsset timelineFadeOut;

			// Token: 0x040086E4 RID: 34532
			public TimelineAsset timelineFadeSwap;
		}
	}
}
