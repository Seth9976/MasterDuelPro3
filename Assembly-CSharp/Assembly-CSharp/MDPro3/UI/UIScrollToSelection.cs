using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001413 RID: 5139
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("UI/Extensions/UIScrollToSelection")]
	public class UIScrollToSelection : MonoBehaviour
	{
		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x0600948E RID: 38030 RVA: 0x00153183 File Offset: 0x00151383
		protected RectTransform LayoutListGroup
		{
			get
			{
				if (!(this.TargetScrollRect != null))
				{
					return null;
				}
				return this.TargetScrollRect.content;
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x0600948F RID: 38031 RVA: 0x001531A0 File Offset: 0x001513A0
		protected UIScrollToSelection.ScrollType ScrollDirection
		{
			get
			{
				return this.scrollDirection;
			}
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06009490 RID: 38032 RVA: 0x001531A8 File Offset: 0x001513A8
		// (set) Token: 0x06009491 RID: 38033 RVA: 0x001531B0 File Offset: 0x001513B0
		protected RectTransform ScrollWindow { get; set; }

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06009492 RID: 38034 RVA: 0x001531B9 File Offset: 0x001513B9
		// (set) Token: 0x06009493 RID: 38035 RVA: 0x001531C1 File Offset: 0x001513C1
		protected ScrollRect TargetScrollRect { get; set; }

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06009494 RID: 38036 RVA: 0x001531CA File Offset: 0x001513CA
		protected EventSystem CurrentEventSystem
		{
			get
			{
				return EventSystem.current;
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06009495 RID: 38037 RVA: 0x001531D1 File Offset: 0x001513D1
		// (set) Token: 0x06009496 RID: 38038 RVA: 0x001531D9 File Offset: 0x001513D9
		protected GameObject LastCheckedGameObject { get; set; }

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06009497 RID: 38039 RVA: 0x001531E2 File Offset: 0x001513E2
		protected GameObject CurrentSelectedGameObject
		{
			get
			{
				return EventSystem.current.currentSelectedGameObject;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06009498 RID: 38040 RVA: 0x001531EE File Offset: 0x001513EE
		// (set) Token: 0x06009499 RID: 38041 RVA: 0x001531F6 File Offset: 0x001513F6
		protected RectTransform CurrentTargetRectTransform { get; set; }

		// Token: 0x0600949A RID: 38042 RVA: 0x001531FF File Offset: 0x001513FF
		protected virtual void Awake()
		{
			this.TargetScrollRect = base.GetComponent<ScrollRect>();
			this.ScrollWindow = this.TargetScrollRect.GetComponent<RectTransform>();
		}

		// Token: 0x0600949B RID: 38043 RVA: 0x0015321E File Offset: 0x0015141E
		protected virtual void Update()
		{
			this.UpdateReferences();
			this.ScrollRectToLevelSelection();
		}

		// Token: 0x0600949C RID: 38044 RVA: 0x0015322C File Offset: 0x0015142C
		private void UpdateReferences()
		{
			if (this.CurrentSelectedGameObject != this.LastCheckedGameObject)
			{
				this.CurrentTargetRectTransform = ((this.CurrentSelectedGameObject != null) ? this.CurrentSelectedGameObject.GetComponent<RectTransform>() : null);
				if (this.CurrentTargetRectTransform != null && !this.CurrentTargetRectTransform.IsChildOf(this.LayoutListGroup))
				{
					this.CurrentTargetRectTransform = null;
				}
			}
			else
			{
				this.CurrentTargetRectTransform = null;
			}
			this.LastCheckedGameObject = this.CurrentSelectedGameObject;
		}

		// Token: 0x0600949D RID: 38045 RVA: 0x001532AC File Offset: 0x001514AC
		private void ScrollRectToLevelSelection()
		{
			if (this.TargetScrollRect == null || this.LayoutListGroup == null || this.ScrollWindow == null || Cursor.lockState == CursorLockMode.None)
			{
				return;
			}
			RectTransform selection = this.CurrentTargetRectTransform;
			if (selection == null)
			{
				return;
			}
			switch (this.ScrollDirection)
			{
			case UIScrollToSelection.ScrollType.VERTICAL:
				this.UpdateVerticalScrollPosition(selection, 0.1f);
				return;
			case UIScrollToSelection.ScrollType.HORIZONTAL:
				this.UpdateHorizontalScrollPosition(selection, 0.1f);
				return;
			case UIScrollToSelection.ScrollType.BOTH:
				this.UpdateVerticalScrollPosition(selection, 0.1f);
				this.UpdateHorizontalScrollPosition(selection, 0.1f);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600949E RID: 38046 RVA: 0x0015334B File Offset: 0x0015154B
		public void VerticalScrollTo(RectTransform selection)
		{
			this.UpdateVerticalScrollPosition(selection, 0f);
		}

		// Token: 0x0600949F RID: 38047 RVA: 0x0015335C File Offset: 0x0015155C
		private void UpdateVerticalScrollPosition(RectTransform selection, float duration = 0.1f)
		{
			float elementHeight = selection.rect.height;
			float selectionPosition = -selection.anchoredPosition.y - elementHeight * (1f - selection.pivot.y);
			RectTransform parent = selection.parent as RectTransform;
			while (parent != this.LayoutListGroup)
			{
				selectionPosition += -parent.anchoredPosition.y - parent.rect.height * (1f - parent.pivot.y);
				parent = parent.parent as RectTransform;
			}
			float maskHeight = this.ScrollWindow.rect.height;
			float listAnchorPosition = this.LayoutListGroup.anchoredPosition.y;
			float offlimitsValue = this.GetVerticalScrollOffset(selectionPosition, listAnchorPosition, elementHeight, maskHeight);
			if (this.verticalTweener != null && this.verticalTweener.IsActive())
			{
				this.verticalTweener.Kill(false);
			}
			this.verticalTweener = this.LayoutListGroup.DOAnchorPosY(this.LayoutListGroup.anchoredPosition.y - offlimitsValue, duration, false);
		}

		// Token: 0x060094A0 RID: 38048 RVA: 0x00153470 File Offset: 0x00151670
		private void UpdateHorizontalScrollPosition(RectTransform selection, float duration = 0.1f)
		{
			float elementWidth = selection.rect.width;
			float selectionPosition = -selection.anchoredPosition.x - elementWidth * (1f - selection.pivot.x);
			RectTransform parent = selection.parent as RectTransform;
			while (parent != this.LayoutListGroup)
			{
				selectionPosition += -parent.anchoredPosition.x - parent.rect.height * (1f - parent.pivot.x);
				parent = parent.parent as RectTransform;
			}
			float maskWidth = this.ScrollWindow.rect.width;
			float listAnchorPosition = -this.LayoutListGroup.anchoredPosition.x;
			float offlimitsValue = -this.GetScrollOffset(selectionPosition, listAnchorPosition, elementWidth, maskWidth);
			if (this.horizontalTweener != null && this.horizontalTweener.IsActive())
			{
				this.horizontalTweener.Kill(false);
			}
			this.horizontalTweener = this.LayoutListGroup.DOAnchorPosX(this.LayoutListGroup.anchoredPosition.x - offlimitsValue, duration, false);
		}

		// Token: 0x060094A1 RID: 38049 RVA: 0x00153584 File Offset: 0x00151784
		private float GetScrollOffset(float position, float listAnchorPosition, float targetLength, float maskLength)
		{
			if (position < listAnchorPosition + targetLength / 2f)
			{
				return listAnchorPosition + maskLength - (position - targetLength);
			}
			if (position + targetLength > listAnchorPosition + maskLength)
			{
				return listAnchorPosition + maskLength - (position + targetLength);
			}
			return 0f;
		}

		// Token: 0x060094A2 RID: 38050 RVA: 0x001535B2 File Offset: 0x001517B2
		private float GetVerticalScrollOffset(float position, float listAnchorPosition, float targetLength, float maskLength)
		{
			if (position < listAnchorPosition + this.topPadding)
			{
				return listAnchorPosition + this.topPadding - position;
			}
			if (position + targetLength > listAnchorPosition + maskLength - this.bottomPadding)
			{
				return listAnchorPosition + maskLength - this.bottomPadding - (position + targetLength);
			}
			return 0f;
		}

		// Token: 0x0400D2DB RID: 53979
		[SerializeField]
		private UIScrollToSelection.ScrollType scrollDirection;

		// Token: 0x0400D2DC RID: 53980
		public float topPadding;

		// Token: 0x0400D2DD RID: 53981
		public float bottomPadding;

		// Token: 0x0400D2E2 RID: 53986
		private Tweener verticalTweener;

		// Token: 0x0400D2E3 RID: 53987
		private Tweener horizontalTweener;

		// Token: 0x02001414 RID: 5140
		public enum ScrollType
		{
			// Token: 0x0400D2E5 RID: 53989
			VERTICAL,
			// Token: 0x0400D2E6 RID: 53990
			HORIZONTAL,
			// Token: 0x0400D2E7 RID: 53991
			BOTH
		}
	}
}
