using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000EE4 RID: 3812
	public class ReplayControl : MonoBehaviour
	{
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06006F37 RID: 28471 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F38 RID: 28472 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<bool> OnChangeReplayPause
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

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06006F39 RID: 28473 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006F3A RID: 28474 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnFastReplay
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

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06006F3B RID: 28475 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006F3C RID: 28476 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsPause
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

		// Token: 0x06006F3D RID: 28477 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ElementObjectManager ui, DuelHUD duelHUD, GameObject replayMessage)
		{
		}

		// Token: 0x06006F3F RID: 28479 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006F40 RID: 28480 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnTapPause()
		{
		}

		// Token: 0x06006F41 RID: 28481 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnTapPlay()
		{
		}

		// Token: 0x06006F42 RID: 28482 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnTapFast()
		{
		}

		// Token: 0x06006F43 RID: 28483 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateFFIcon()
		{
		}

		// Token: 0x06006F44 RID: 28484 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x0400AA33 RID: 43571
		private DuelHUD duelHUD;

		// Token: 0x0400AA34 RID: 43572
		private ElementObjectManager ui;

		// Token: 0x0400AA35 RID: 43573
		private SelectionButton playButton;

		// Token: 0x0400AA36 RID: 43574
		private SelectionButton pauseButton;

		// Token: 0x0400AA37 RID: 43575
		private GameObject message;

		// Token: 0x0400AA38 RID: 43576
		private GameObject realtimeReplay;

		// Token: 0x0400AA39 RID: 43577
		private GameObject ffIconOn;

		// Token: 0x0400AA3A RID: 43578
		private GameObject ffIconOff;

		// Token: 0x0400AA3B RID: 43579
		private bool initialized;
	}
}
