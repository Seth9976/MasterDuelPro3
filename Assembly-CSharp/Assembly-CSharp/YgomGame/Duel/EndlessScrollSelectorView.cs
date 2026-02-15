using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000E2B RID: 3627
	public class EndlessScrollSelectorView : EndlessScrollView
	{
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06006883 RID: 26755 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_CurrentItemRT
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06006884 RID: 26756 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_ViewportRT
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06006885 RID: 26757 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector selector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06006886 RID: 26758 RVA: 0x000F5DB0 File Offset: 0x000F3FB0
		private Vector2 m_ViewportTopPos
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06006887 RID: 26759 RVA: 0x000F5DC8 File Offset: 0x000F3FC8
		private Vector2 m_ViewportBottomPos
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06006888 RID: 26760 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ishorizontalscroll
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006889 RID: 26761 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCallBack()
		{
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsCurrentItemInHere()
		{
			return false;
		}

		// Token: 0x0600688B RID: 26763 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InputCallBack(PadInputDirection direction)
		{
		}

		// Token: 0x0600688C RID: 26764 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetSelectItemByDataIndex(int dataIndex, bool forceSelect = false)
		{
			return false;
		}

		// Token: 0x0600688D RID: 26765 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Update()
		{
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize(Func<int> getDataNum, Action<GameObject, int> onItemUpdate, Action<GameObject> onItemInitialize = null)
		{
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetItemTrsnsitionMode(ref SelectionItem selitem, int dataindex, PadInputDirection direction, SelectionItem.TransitionMode defaultMode)
		{
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x000029CC File Offset: 0x00000BCC
		private int NextDataIndex(PadInputDirection direction)
		{
			return 0;
		}

		// Token: 0x06006891 RID: 26769 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsObeyScrollDirection(SelectorGroup.Direction direction)
		{
			return false;
		}

		// Token: 0x06006892 RID: 26770 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsCurrentDataInViewport()
		{
			return false;
		}

		// Token: 0x06006893 RID: 26771 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsDataInViewport(int dataindex)
		{
			return false;
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float GetCurrentDataViewportBias()
		{
			return 0f;
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float GetDataViewportBias(int dataindex)
		{
			return 0f;
		}

		// Token: 0x0400A38B RID: 41867
		public bool loopInScrollView;

		// Token: 0x0400A38C RID: 41868
		private bool m_IsItemInHere;

		// Token: 0x0400A38D RID: 41869
		private int m_SelectedItemIndex;

		// Token: 0x0400A38E RID: 41870
		private Vector2 m_OldPos;

		// Token: 0x0400A38F RID: 41871
		[HideInInspector]
		public List<int> unselectableIndexList;

		// Token: 0x0400A390 RID: 41872
		public List<int> uninteractableIndexList;

		// Token: 0x0400A391 RID: 41873
		public Dictionary<ManualTransition, SelectionItem> manualTransitionTable;
	}
}
