using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI.InfinityScroll;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x02000696 RID: 1686
	public class SlidePagerWidget : ElementWidgetBehaviourBase<SlidePagerWidget>
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x0000216A File Offset: 0x0000036A
		public ScrollRectPageSnap pageSnap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton prevButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton nextButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06003510 RID: 13584 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06003511 RID: 13585 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onPageChanged
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

		// Token: 0x06003512 RID: 13586 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsValidIdx(int idx)
		{
			return false;
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsEnablePrev()
		{
			return false;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsEnableNext()
		{
			return false;
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x0000216A File Offset: 0x0000036A
		public static SlidePagerWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPageTotal(int pageTotal)
		{
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayIndicatorTween(bool immediate = false)
		{
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateButtons()
		{
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitMovePage(Selector seletor, InfinityScrollView isv)
		{
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool MovePage(int dstPage)
		{
			return false;
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickNext()
		{
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPrev()
		{
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPageChanged()
		{
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yMovePage(int dstPage, Action onComplete = null)
		{
			return null;
		}

		// Token: 0x0400304C RID: 12364
		private readonly string k_ELabelIndicatorTemplate;

		// Token: 0x0400304D RID: 12365
		private readonly string k_ELabelPrevButton;

		// Token: 0x0400304E RID: 12366
		private readonly string k_ELabelNextButton;

		// Token: 0x0400304F RID: 12367
		[SerializeField]
		private string m_TweenIndicatorOn;

		// Token: 0x04003050 RID: 12368
		[SerializeField]
		private string m_TweenIndicatorOff;

		// Token: 0x04003051 RID: 12369
		private ScrollRectPageSnap m_PageSnap;

		// Token: 0x04003052 RID: 12370
		private SelectionButton m_PrevButton;

		// Token: 0x04003053 RID: 12371
		private SelectionButton m_NextButton;

		// Token: 0x04003054 RID: 12372
		private List<GameObject> m_Indicators;

		// Token: 0x04003055 RID: 12373
		private Coroutine m_yMovePageRoutine;

		// Token: 0x04003056 RID: 12374
		private Selector m_Selector;

		// Token: 0x04003057 RID: 12375
		private InfinityScrollView m_Isv;

		// Token: 0x04003058 RID: 12376
		private List<SelectionItem> m_TmpOrderItems;

		// Token: 0x04003059 RID: 12377
		public int startPage;

		// Token: 0x0400305A RID: 12378
		public bool isLoop;
	}
}
