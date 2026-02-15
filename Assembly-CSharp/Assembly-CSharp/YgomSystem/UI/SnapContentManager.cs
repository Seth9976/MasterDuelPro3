using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x020005FE RID: 1534
	public class SnapContentManager : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x0000216A File Offset: 0x0000036A
		public ScrollRect.ScrollRectEvent onValueChanged
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeContent(int maxPage, Action onCompleteCallBack = null, bool isLoop = false)
		{
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFirstPage(int firstPage = 0)
		{
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ToNextPage()
		{
			return false;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ToPrevPage()
		{
			return false;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetScrollDisabled()
		{
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsContainViewport(GameObject entity)
		{
			return false;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnIdxChanged()
		{
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnReleaseDrag(int idx)
		{
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangePage(int diff)
		{
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x0000216D File Offset: 0x0000036D
		private void RearrangeTemplate(int idxDiff)
		{
		}

		// Token: 0x04002D78 RID: 11640
		private const int TEMPLATE_NUM = 3;

		// Token: 0x04002D79 RID: 11641
		private int templateNum;

		// Token: 0x04002D7A RID: 11642
		private ScrollRectPageSnap m_pageSnap;

		// Token: 0x04002D7B RID: 11643
		private ScrollRect m_scrollRect;

		// Token: 0x04002D7C RID: 11644
		private int m_pastIdx;

		// Token: 0x04002D7D RID: 11645
		public int currentPage;

		// Token: 0x04002D7E RID: 11646
		public int m_maxPage;

		// Token: 0x04002D7F RID: 11647
		public string templateLabel;

		// Token: 0x04002D80 RID: 11648
		private List<GameObject> templateGOList;

		// Token: 0x04002D81 RID: 11649
		public Action<GameObject> onCreatedEntityCallback;

		// Token: 0x04002D82 RID: 11650
		public Action<GameObject, int> onSetEntityCallback;

		// Token: 0x04002D83 RID: 11651
		public Action onPageChangedCallBack;

		// Token: 0x04002D84 RID: 11652
		private int m_pageBuff;

		// Token: 0x04002D85 RID: 11653
		private bool m_isTouching;

		// Token: 0x04002D86 RID: 11654
		private bool m_isLoop;
	}
}
