using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI
{
	// Token: 0x02000598 RID: 1432
	public class GenericScrollView : MonoBehaviour
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x000029CC File Offset: 0x00000BCC
		public int ConstraintCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x000029CC File Offset: 0x00000BCC
		public int DataIndexOfItemBegin
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000029CC File Offset: 0x00000BCC
		public int DataIndexOfItemEnd
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CurrentItemIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CurrentDataIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000029CC File Offset: 0x00000BCC
		public int ItemCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000F1F84 File Offset: 0x000F0184
		public Vector2 UnitSize
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector selector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x0000216A File Offset: 0x0000036A
		public ScrollRect scrollrect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMoving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_LastLineItemCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_LastLineItemCountInView
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_IsHorizontalScroll
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_CurrentContentPos
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_ViewSizeAlongScrollDirection
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_UnitSizeAlongScrollDirection
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_SpacingAlongScrollDirection
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionItem m_CurrentItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_CurrentItemRT
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_DataIndexOfListBegin
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_VerticalOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_HorizontalOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_DataIndexOfListEnd
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_PaddingBias
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_PaddingBegin
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_PaddingEnd
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_IsMoving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isStandby
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialzize(IGenericScrollViewSupport igsvsupport, string templateLabelName = null)
		{
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void UpdateDataCount(int dataCount)
		{
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateData()
		{
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetItemByListPos(int x, int y)
		{
			return null;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000F1F9C File Offset: 0x000F019C
		public ValueTuple<int, int> GetListPosByItemIndex(int itemIndex)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetItemIndexByListPos(int x, int y)
		{
			return 0;
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDataIndexByItemIndex(int itemIndex)
		{
			return 0;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDataIndexByListPos(int x, int y)
		{
			return 0;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetItemIndexByDataIndex(int dataindex)
		{
			return 0;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetItemByDataIndex(int dataindex)
		{
			return null;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000F1FB4 File Offset: 0x000F01B4
		public T GetItemByDataIndex<T>(int dataindex)
		{
			return default(T);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectItemByDataIndex(int dataindex, bool forcemovetofirst = false, bool forceInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetContentPosition()
		{
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetContentPosByDataLine(int index)
		{
			return 0f;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetItemPosByDataLine(int index)
		{
			return 0f;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeltaMove(float deltamove)
		{
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InitContentRT()
		{
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0000216A File Offset: 0x0000036A
		protected IEnumerator ReadRectSize(string templateLabelName = "template")
		{
			return null;
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InstantiateTemplate()
		{
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InstantiateImpl(int itemcount)
		{
			return null;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InitLayout()
		{
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool AddTopItem()
		{
			return false;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool AddBottomItem()
		{
			return false;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool RemoveTopItem()
		{
			return false;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool RemoveBottomItem()
		{
			return false;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x0000216D File Offset: 0x0000036D
		protected void UpdateContentPos()
		{
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CheckWaitForSelectData()
		{
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int GetDataLineByContentPos(float pos)
		{
			return 0;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool AddItem(int dataindex, int posInHierachy)
		{
			return false;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool RemoveItem(int itemindex)
		{
			return false;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x0000216D File Offset: 0x0000036D
		protected void RemoveAllItem()
		{
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ChangeContentSize()
		{
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void PadInputCallBack(PadInputDirection direction)
		{
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x0000216D File Offset: 0x0000036D
		protected void PadInputCallBackUp()
		{
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x0000216D File Offset: 0x0000036D
		protected void PadInputCallBackDown()
		{
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x0000216D File Offset: 0x0000036D
		protected void PadInputCallBackLeft()
		{
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x0000216D File Offset: 0x0000036D
		protected void PadInputCallBackRight()
		{
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int GetNextDataIndexByPadInput(PadInputDirection direction)
		{
			return 0;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool IsIndexInSameLine(int index0, int index1)
		{
			return false;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool CheckItemIndexCorrect(int itemindex)
		{
			return false;
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool CheckDataIndexCorrect(int dataindex)
		{
			return false;
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool CheckItemInViewByDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x0000216D File Offset: 0x0000036D
		protected void MoveContent(float targetpos)
		{
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool MoveContentToFitDataPos(int dataindex, bool forcemovetofirst)
		{
			return false;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void SetItemTransitionMode(ref SelectionItem item, int dataindex)
		{
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void InnerItemInitialize(int itemindex, SelectionItem selectionItem)
		{
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void InnerItemActivate(SelectionItem selitem)
		{
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void InnerItemDeactivate(SelectionItem selitem)
		{
		}

		// Token: 0x04002B50 RID: 11088
		private const string LABEL_EO_TEMPLATE = "template";

		// Token: 0x04002B51 RID: 11089
		[SerializeField]
		protected GridLayoutGroup.Axis m_ScrollMode;

		// Token: 0x04002B52 RID: 11090
		[SerializeField]
		protected Vector2 m_Spacing;

		// Token: 0x04002B53 RID: 11091
		[SerializeField]
		protected RectOffset m_Padding;

		// Token: 0x04002B54 RID: 11092
		[SerializeField]
		protected GenericScrollView.Alignment m_Alignment;

		// Token: 0x04002B55 RID: 11093
		[SerializeField]
		protected int m_ConstraintCount;

		// Token: 0x04002B56 RID: 11094
		protected IGenericScrollViewSupport m_IGsvHelper;

		// Token: 0x04002B57 RID: 11095
		protected Selector m_Selector;

		// Token: 0x04002B58 RID: 11096
		protected ElementObjectManager m_EOManager;

		// Token: 0x04002B59 RID: 11097
		protected RectTransform m_ContentRT;

		// Token: 0x04002B5A RID: 11098
		protected Vector2 m_ViewportSize;

		// Token: 0x04002B5B RID: 11099
		protected ExtendedScrollRect m_ScrollRect;

		// Token: 0x04002B5C RID: 11100
		protected GridLayoutGroup m_LayoutGroup;

		// Token: 0x04002B5D RID: 11101
		protected Dictionary<SelectionItem, int> m_ItemDataIndexTable;

		// Token: 0x04002B5E RID: 11102
		protected Dictionary<SelectionItem, GameObject> m_SIGOTable;

		// Token: 0x04002B5F RID: 11103
		protected Stack<SelectionItem> m_FreeItemStack;

		// Token: 0x04002B60 RID: 11104
		protected List<SelectionItem> m_ActiveItemList;

		// Token: 0x04002B61 RID: 11105
		protected int m_DataCount;

		// Token: 0x04002B62 RID: 11106
		protected Vector2 m_UnitSize;

		// Token: 0x04002B63 RID: 11107
		protected int m_WaitForSelectDataIndex;

		// Token: 0x04002B64 RID: 11108
		protected bool m_IsStandby;

		// Token: 0x04002B65 RID: 11109
		protected IEnumerator m_yMoveContentImpl;

		// Token: 0x04002B66 RID: 11110
		protected GameObject m_Template;

		// Token: 0x04002B67 RID: 11111
		private int m_WaitCount;

		// Token: 0x02000599 RID: 1433
		protected enum Alignment
		{
			// Token: 0x04002B69 RID: 11113
			Begin,
			// Token: 0x04002B6A RID: 11114
			Center,
			// Token: 0x04002B6B RID: 11115
			End
		}
	}
}
