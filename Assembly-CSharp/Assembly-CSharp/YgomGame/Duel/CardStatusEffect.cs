using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D11 RID: 3345
	public class CardStatusEffect : DuelEffectHandle
	{
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06006060 RID: 24672 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06006061 RID: 24673 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06006062 RID: 24674 RVA: 0x0000216D File Offset: 0x0000036D
		public float fadeDulation
		{
			[CompilerGenerated]
			get
			{
				return 0f;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06006063 RID: 24675 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006064 RID: 24676 RVA: 0x0000216D File Offset: 0x0000036D
		public int order
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06006065 RID: 24677 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006066 RID: 24678 RVA: 0x0000216D File Offset: 0x0000036D
		public bool showOrder
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnInitialize()
		{
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTerminate()
		{
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPlay()
		{
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnStop()
		{
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetTarget(ICardStatusIconAnchor anchor)
		{
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(Action onFinished)
		{
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide(Action onFinished)
		{
		}

		// Token: 0x04009B84 RID: 39812
		private MeshAlphaFader alphaFader;

		// Token: 0x04009B85 RID: 39813
		private TextMesh textMesh;

		// Token: 0x04009B86 RID: 39814
		private bool m_isPlaying;

		// Token: 0x04009B87 RID: 39815
		private int m_order;

		// Token: 0x04009B88 RID: 39816
		private bool m_showOrder;
	}
}
