using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000DAB RID: 3499
	public abstract class DuelUIBase : MonoBehaviour
	{
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060066B8 RID: 26296
		protected abstract UITransitionUtil.BlockType openCloseBlockType { get; }

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060066B9 RID: 26297 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060066BA RID: 26298 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isShowing
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

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060066BC RID: 26300 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isOpening
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

		// Token: 0x060066BD RID: 26301 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Initialize(RunEffectWorker effectWorker)
		{
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Open(Action openedCallback = null)
		{
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Close(Action closedCallback = null)
		{
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnOpened()
		{
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnClosed()
		{
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ShowUI()
		{
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void HideUI()
		{
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void StartUI()
		{
		}

		// Token: 0x060066C5 RID: 26309 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void UpdateUI()
		{
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void DestroyUI()
		{
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Update()
		{
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0400A10A RID: 41226
		protected RunEffectWorker effectWorker;

		// Token: 0x0400A10B RID: 41227
		protected UITransitionUtil uiTransition;

		// Token: 0x0400A10C RID: 41228
		private const string tweenLabelOpen = "Open";

		// Token: 0x0400A10D RID: 41229
		private const string tweenLabelClose = "Close";
	}
}
