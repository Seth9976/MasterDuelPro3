using System;
using System.Collections;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B8D RID: 2957
	public class MDMarkupBoardPagerContainerWidget : ElementWidgetBase, IMDMarkupContainerWidget
	{
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060054D5 RID: 21717 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton prevButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060054D6 RID: 21718 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton nextButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTLabelPagingOut(int direction)
		{
			return null;
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTLabelPagingIn(int direction)
		{
			return null;
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ValidIdx(int idx)
		{
			return false;
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ContainCache(int idx)
		{
			return false;
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x0000216A File Offset: 0x0000036A
		private MDMarkupBoardContainerWidget TryGetCache(int idx)
		{
			return null;
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupBoardPagerContainerWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(IMDMarkupContainer containerData)
		{
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x0000216D File Offset: 0x0000036D
		public void Output(MDMarkupGraphFactory graphFactory, Action onComplete)
		{
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnStart(Dictionary<string, object> args)
		{
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x0000216D File Offset: 0x0000036D
		private void OutputGraph(int idx, Action onComplete = null)
		{
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yOutputGraph(int idx, Action onComplete = null)
		{
			return null;
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yCreateMMA(int idx)
		{
			return null;
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayOut(int prevIdx, int dstIdx)
		{
			return null;
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayIn(int prevIdx, int dstIdx)
		{
			return null;
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshButtons()
		{
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickNext()
		{
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPrev()
		{
		}

		// Token: 0x04009218 RID: 37400
		private readonly string k_ELabel_MMATemplate;

		// Token: 0x04009219 RID: 37401
		private readonly string k_ELabel_PagerGroup;

		// Token: 0x0400921A RID: 37402
		private readonly string k_ELabel_NextButton;

		// Token: 0x0400921B RID: 37403
		private readonly string k_ELabel_PrevButton;

		// Token: 0x0400921C RID: 37404
		private readonly string k_TLabelPagingNextOut;

		// Token: 0x0400921D RID: 37405
		private readonly string k_TLabelPagingBackOut;

		// Token: 0x0400921E RID: 37406
		private readonly string k_TLabelPagingNextIn;

		// Token: 0x0400921F RID: 37407
		private readonly string k_TLabelPagingBackIn;

		// Token: 0x04009220 RID: 37408
		private int m_CacheLimit;

		// Token: 0x04009221 RID: 37409
		private ElementObjectManager m_MMATemplate;

		// Token: 0x04009222 RID: 37410
		private MDMarkupContentCustomBoardPageHandler m_Handler;

		// Token: 0x04009223 RID: 37411
		private MDMarkupBoardPagerContainerWidget.Context m_Context;

		// Token: 0x04009224 RID: 37412
		private MDMarkupGraphFactory m_GraphFactory;

		// Token: 0x04009225 RID: 37413
		private MDMarkupBoardContainer[] m_BoardContainers;

		// Token: 0x04009226 RID: 37414
		private int m_Idx;

		// Token: 0x04009227 RID: 37415
		private Queue<ValueTuple<int, MDMarkupBoardContainerWidget>> m_MMACaches;

		// Token: 0x02000B8E RID: 2958
		public class Context
		{
			// Token: 0x04009228 RID: 37416
			public bool badge;

			// Token: 0x04009229 RID: 37417
			public string dateStr;
		}
	}
}
