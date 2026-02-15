using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Shop
{
	// Token: 0x0200093D RID: 2365
	public class ProductListWidget : ElementWidgetBase
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedScrollRect scrollRect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x0000216A File Offset: 0x0000036A
		private List<float> entityVirtualPositions
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMoving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x0000216A File Offset: 0x0000036A
		public List<ProductContainerWidget.Context> productContainerCtxs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetHeaderIdx(int categoryId, int subCategoryId, int sectionId)
		{
			return 0;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000F4850 File Offset: 0x000F2A50
		private ValueTuple<int, int> GetProductIdx(int shopId)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetHeadProductId(int categoryId, int subCategoryId = 0, int sectionId = 0)
		{
			return 0;
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductContext GetProductContextByDataIdx(int dataIdx, int containerIdx)
		{
			return null;
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductWidget TryGetProductWidgetByDataIdx(int dataIdx, int containerIdx)
		{
			return null;
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000F4868 File Offset: 0x000F2A68
		public ValueTuple<int, int, int> GetIdByCurrentPos()
		{
			return default(ValueTuple<int, int, int>);
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductListWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(IProductListWidgetHandler handler, IProductListWidgetListener listener, int reservePerFrame = 0, Action onComplete = null)
		{
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetPreIdx()
		{
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataCount(int asyncCnt = 0, bool resetPos = true, Action onComplete = null)
		{
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateContentPos()
		{
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateData()
		{
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePadding()
		{
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x0000216D File Offset: 0x0000036D
		private void CalcCheckEntityVirtualPositions()
		{
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetEntityVirtualPositions(int idx, float value)
		{
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetScrollEnable(bool enable)
		{
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusProduct(int shopId, bool selectItem = true, bool isInitializeSelect = false, bool immediate = false)
		{
			return false;
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusHeadProduct(int categoryId, int subCategoryId = 0, int sectionId = 0, bool selectItem = true, bool isInitializeSelect = false, bool immediate = false)
		{
			return false;
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusDataIndex(int dataIdx, int contentIdx = 0, bool selectItem = true, bool isInitializeSelect = false, bool immediate = false)
		{
			return false;
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusHeader(int categoryId, int subCategoryId, int sectionId, bool immediate = false)
		{
			return false;
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool FocusPreSelectedProduct(bool selectItem = true, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x0000216D File Offset: 0x0000036D
		public void JumpToDirection(PadInputDirection dir)
		{
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject entity)
		{
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivateEntity(GameObject entity)
		{
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeactivateEntity(GameObject entity)
		{
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnFocusSelectEntity(GameObject entity, int dataIndex, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnSelectorSelectored()
		{
			return false;
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectorSelect(bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x000F4880 File Offset: 0x000F2A80
		private ValueTuple<bool, float> MoveContentToFitDataPos(int dataIndex)
		{
			return default(ValueTuple<bool, float>);
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSelectableEntityIndex(int dataIndex)
		{
			return false;
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject entity, int dataIdx)
		{
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedContainersProduct(int dataIdx, int contentIdx)
		{
		}

		// Token: 0x04008344 RID: 33604
		private const string k_ELabelProductList = "ProductList";

		// Token: 0x04008345 RID: 33605
		private const string k_ELabelInFilterMessageText = "InFilterMessageText";

		// Token: 0x04008346 RID: 33606
		private const string k_ELabelEmptyMessageText = "EmptyMessageText";

		// Token: 0x04008347 RID: 33607
		public readonly Selector selector;

		// Token: 0x04008348 RID: 33608
		private readonly InfinityScrollView m_ScrollView;

		// Token: 0x04008349 RID: 33609
		private readonly GameObject m_InFilterMessageText;

		// Token: 0x0400834A RID: 33610
		private readonly TMP_Text m_EmptyMessageText;

		// Token: 0x0400834B RID: 33611
		private ExtendedScrollRect m_ScrollRectCache;

		// Token: 0x0400834C RID: 33612
		private IProductListWidgetHandler m_Handler;

		// Token: 0x0400834D RID: 33613
		private IProductListWidgetListener m_Listener;

		// Token: 0x0400834E RID: 33614
		private readonly Dictionary<GameObject, ProductGroupHeaderWidget> m_HeaderWidgetMap;

		// Token: 0x0400834F RID: 33615
		private readonly Dictionary<GameObject, ProductContainerWidget> m_ContainerWidgetMap;

		// Token: 0x04008350 RID: 33616
		private readonly List<float> m_EntityVirtualPositions;

		// Token: 0x04008351 RID: 33617
		private RectOffset m_BasePadding;

		// Token: 0x04008352 RID: 33618
		private RectOffset m_Padding;

		// Token: 0x04008353 RID: 33619
		private float m_Spacing;

		// Token: 0x04008354 RID: 33620
		private bool m_EntityVirtualPositionsDirty;

		// Token: 0x04008355 RID: 33621
		private readonly List<int> m_TemplateIdxList;

		// Token: 0x04008356 RID: 33622
		private readonly List<string> m_HeaderLabels;

		// Token: 0x04008357 RID: 33623
		private readonly List<ProductContainerWidget.Context> m_ProductContainerCtxs;

		// Token: 0x04008358 RID: 33624
		private readonly Dictionary<int, Dictionary<int, Dictionary<int, int>>> m_HeaderDataIdxMap;

		// Token: 0x04008359 RID: 33625
		public bool filteredVisible;

		// Token: 0x0400835A RID: 33626
		public string emptyMessageText;

		// Token: 0x0400835B RID: 33627
		private int m_PreSelectedDataIdx;

		// Token: 0x0400835C RID: 33628
		private int m_PreSelectedContentIdx;
	}
}
