using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005FF RID: 1535
	public class SnapScrollView : MonoBehaviour
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x0000216A File Offset: 0x0000036A
		public RectOffset padding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x0000216A File Offset: 0x0000036A
		public ExtendedScrollRect scrollRect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector mainSelector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ContainsItem(SelectionItem selectionItem)
		{
			return false;
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionTransitionSetting GetDirectionSetting(PadInputDirection direction)
		{
			return null;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x0000216D File Offset: 0x0000036D
		public void AssignSelector(Selector selector)
		{
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x0000216D File Offset: 0x0000036D
		public void RefreshSelectionItems()
		{
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetContentPosition()
		{
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x0000216D File Offset: 0x0000036D
		public void FocusBySelectionItem(SelectionItem selectionItem, bool selectItem = true, bool isInitializeSelect = false)
		{
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImmediateApplyMovement()
		{
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopAutoScroll()
		{
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000F22C0 File Offset: 0x000F04C0
		public Vector2 CalcFitNormalize(RectTransform target)
		{
			return default(Vector2);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000F22D8 File Offset: 0x000F04D8
		private Vector2 CheckRectToFitDiff(RectTransform target)
		{
			return default(Vector2);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x0000216D File Offset: 0x0000036D
		public void InputAnalogDirection(Vector2 analogValue)
		{
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x0000216D File Offset: 0x0000036D
		public void InputDirection(PadInputDirection direction)
		{
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollValueChanged(Vector2 bias)
		{
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnMainSelectorSelected()
		{
			return false;
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedItem()
		{
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputUp()
		{
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputDown()
		{
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputLeft()
		{
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputRight()
		{
		}

		// Token: 0x04002D87 RID: 11655
		[SerializeField]
		private SelectionTransitionSetting m_UpEdgeTransition;

		// Token: 0x04002D88 RID: 11656
		[SerializeField]
		private SelectionTransitionSetting m_DownEdgeTransition;

		// Token: 0x04002D89 RID: 11657
		[SerializeField]
		private SelectionTransitionSetting m_RightEdgeTransition;

		// Token: 0x04002D8A RID: 11658
		[SerializeField]
		private SelectionTransitionSetting m_LeftEdgeTransition;

		// Token: 0x04002D8B RID: 11659
		[SerializeField]
		public bool m_ScrollAnalogMain;

		// Token: 0x04002D8C RID: 11660
		[SerializeField]
		public bool m_ScrollAnalogSub;

		// Token: 0x04002D8D RID: 11661
		[SerializeField]
		public float m_ThresInput;

		// Token: 0x04002D8E RID: 11662
		private Selector m_MainSelectorCache;

		// Token: 0x04002D8F RID: 11663
		private List<Selector> m_Selectors;

		// Token: 0x04002D90 RID: 11664
		private ExtendedScrollRect m_ScrollRectCache;

		// Token: 0x04002D91 RID: 11665
		private RectOffset m_PaddingCache;

		// Token: 0x04002D92 RID: 11666
		private List<SelectionItem> m_AssignedItems;

		// Token: 0x04002D93 RID: 11667
		private bool m_TransitionBlocker;

		// Token: 0x04002D94 RID: 11668
		private List<SelectionItem> m_SortTargetSelections;

		// Token: 0x04002D95 RID: 11669
		private Dictionary<SelectionItem, float> m_SortAmounts;

		// Token: 0x04002D96 RID: 11670
		private SelectionItem m_LastSelectedItem;

		// Token: 0x04002D97 RID: 11671
		private SnapScrollView.SearchNearPtCalculator m_SearchNearPtCalculator;

		// Token: 0x04002D98 RID: 11672
		public Func<bool> customSelectorSelectedFunc;

		// Token: 0x04002D99 RID: 11673
		public Func<PadInputDirection, bool> customInputDirectionFunc;

		// Token: 0x02000600 RID: 1536
		private class SearchNearPtCalculator
		{
			// Token: 0x06003134 RID: 12596 RVA: 0x000F22F0 File Offset: 0x000F04F0
			public ValueTuple<Vector2, Vector2> SearchNearPtPair(Bounds fromBounds, Bounds toBounds)
			{
				return default(ValueTuple<Vector2, Vector2>);
			}

			// Token: 0x04002D9A RID: 11674
			private List<SnapScrollView.SearchNearPtCalculator.PointDiffData> m_PointDiffs;

			// Token: 0x02000601 RID: 1537
			private class PointDiffData : IComparable<SnapScrollView.SearchNearPtCalculator.PointDiffData>
			{
				// Token: 0x170002EB RID: 747
				// (get) Token: 0x06003136 RID: 12598 RVA: 0x000029C5 File Offset: 0x00000BC5
				public float diff
				{
					get
					{
						return 0f;
					}
				}

				// Token: 0x06003137 RID: 12599 RVA: 0x00002739 File Offset: 0x00000939
				public PointDiffData(float fromPoint, float toPoint)
				{
				}

				// Token: 0x06003138 RID: 12600 RVA: 0x000029CC File Offset: 0x00000BCC
				public int CompareTo(SnapScrollView.SearchNearPtCalculator.PointDiffData other)
				{
					return 0;
				}

				// Token: 0x04002D9B RID: 11675
				public readonly float fromPoint;

				// Token: 0x04002D9C RID: 11676
				public readonly float toPoint;
			}
		}
	}
}
