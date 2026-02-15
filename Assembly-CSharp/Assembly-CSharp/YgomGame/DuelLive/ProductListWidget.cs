using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C68 RID: 3176
	public class ProductListWidget : ElementWidgetBase
	{
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x0000216A File Offset: 0x0000036A
		public ExtendedScrollRect scrollRect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06005AB4 RID: 23220 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AB5 RID: 23221 RVA: 0x0000216D File Offset: 0x0000036D
		public int containerLength
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AB7 RID: 23223 RVA: 0x0000216D File Offset: 0x0000036D
		public int largeItemContainerLength
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06005AB8 RID: 23224 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AB9 RID: 23225 RVA: 0x0000216D File Offset: 0x0000036D
		public int productTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06005ABA RID: 23226 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005ABB RID: 23227 RVA: 0x0000216D File Offset: 0x0000036D
		public int containerTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06005ABC RID: 23228 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005ABD RID: 23229 RVA: 0x0000216D File Offset: 0x0000036D
		public int headerTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06005ABE RID: 23230 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005ABF RID: 23231 RVA: 0x0000216D File Offset: 0x0000036D
		public int emptyTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AC1 RID: 23233 RVA: 0x0000216D File Offset: 0x0000036D
		public int randomTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06005AC2 RID: 23234 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AC3 RID: 23235 RVA: 0x0000216D File Offset: 0x0000036D
		public int vsTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06005AC4 RID: 23236 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AC5 RID: 23237 RVA: 0x0000216D File Offset: 0x0000036D
		public int eventTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06005AC6 RID: 23238 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AC7 RID: 23239 RVA: 0x0000216D File Offset: 0x0000036D
		public int officialAccountTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06005AC8 RID: 23240 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005AC9 RID: 23241 RVA: 0x0000216D File Offset: 0x0000036D
		public int commingSoonTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06005ACA RID: 23242 RVA: 0x0000216A File Offset: 0x0000036A
		public List<float> entityVirtualPositions
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06005ACB RID: 23243 RVA: 0x0000216A File Offset: 0x0000036A
		public RectOffset padding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06005ACC RID: 23244 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float spacing
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06005ACD RID: 23245 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isContainerList
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06005ACE RID: 23246 RVA: 0x0000216A File Offset: 0x0000036A
		public InfinityScrollView scrollView
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06005ACF RID: 23247 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005AD0 RID: 23248 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<ProductWidget> onClickedProductEvent
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

		// Token: 0x06005AD1 RID: 23249 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetHeaderIndexByCurrentPos()
		{
			return 0;
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetHeaderIndexByDataIdx(int dataIdx)
		{
			return 0;
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductWidget SearchProductWidget(int dataIdx, int sectionDataIdx = 0)
		{
			return null;
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductListWidget(ElementObjectManager eom, DuelLiveRootWidget owner, bool frag = false)
			: base(null)
		{
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(GameObject productContainerWidgetPref, GameObject productGroupHeaderPref, GameObject productGroupEmptyPref, GameObject productWidgetPref, GameObject productRandomWidgetPref, GameObject productVSWidgetPref, GameObject productEventWidgetPref, GameObject productOfficialAccountWidgetPref, GameObject productCommingSoonWidgetPref, int reservePerFrame = 0, Action onComplete = null)
		{
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetPreIdx()
		{
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetZeroProducts(int asyncCnt = 0, bool resetPos = true, Action onComplete = null)
		{
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetProducts(ProductListWidget.Context ctx, int asyncCnt = 0, bool resetPos = true, Action onComplete = null)
		{
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePadding()
		{
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x0000216D File Offset: 0x0000036D
		private void CalcCheckEntityVirtualPositions()
		{
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject AssginPref(GameObject pref)
		{
			return null;
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float ReadTemplateHeight(GameObject pref)
		{
			return 0f;
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetEntityVirtualPositions(int idx, float value)
		{
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x0000216D File Offset: 0x0000036D
		public void JumpToDirection(PadInputDirection dir)
		{
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelectHeadProduct(bool isInitializeSelect = false, bool immediate = false)
		{
			return false;
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject entity)
		{
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivateEntity(GameObject entity)
		{
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeactivateEntity(GameObject entity)
		{
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnFocusSelectEntity(GameObject entity, int dataIndex, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnSelectorSelectored()
		{
			return false;
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExistsCurrentProduct(bool checkSectionIdx = false)
		{
			return false;
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x000029CC File Offset: 0x00000BCC
		public int SearchHeaderDataIdx(int headerIdx)
		{
			return 0;
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x000029CC File Offset: 0x00000BCC
		public int SearchHeadProductDataIdx()
		{
			return 0;
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x0000216D File Offset: 0x0000036D
		public void FocusHeader(int headerIdx, bool immediate = false)
		{
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem SearchPreSelectedProduct()
		{
			return null;
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x0000216D File Offset: 0x0000036D
		public void FocusProduct(int dataIdx, int sectionDataIdx = 0, bool selectItem = true, bool isInitializeSelect = false, bool immediate = false)
		{
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x000F4DC8 File Offset: 0x000F2FC8
		private ValueTuple<bool, float> MoveContentToFitDataPos(int dataIndex)
		{
			return default(ValueTuple<bool, float>);
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yFocusProduct(int dataIdx, int sectionDataIdx = 0, bool selectItem = true, bool isInitializeSelect = false, bool immediate = false)
		{
			return null;
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TryFocusPreSelectedProduct(bool selectItem = true, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TryFocusHeadProduct(bool selectItem = true, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSelectableEntityIndex(int dataIndex)
		{
			return false;
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CollectSelectionItems(GameObject entity)
		{
			return null;
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject entity, int dataIdx)
		{
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickContainersWidget(ProductContainerWidget containerWidget, ProductWidget productWidget)
		{
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickWidget(ProductWidget productWidget)
		{
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedProduct(int dataIdx, int sectionIdx)
		{
		}

		// Token: 0x040095EB RID: 38379
		private const string k_ELabelGroupHeaderWidget = "ProductGroupHeader";

		// Token: 0x040095EC RID: 38380
		private const string k_ELabelGroupEmptyWidget = "ProductGroupEmpty";

		// Token: 0x040095ED RID: 38381
		private const string k_ELabelProductContainer = "ProductContainer";

		// Token: 0x040095EE RID: 38382
		private const string k_ELabelProductList = "ProductList";

		// Token: 0x040095EF RID: 38383
		private const string k_ELabelInFilterMessageText = "InFilterMessageText";

		// Token: 0x040095F0 RID: 38384
		private const string k_ELabelEmptyMessageText = "EmptyMessageText";

		// Token: 0x040095F1 RID: 38385
		private readonly DuelLiveRootWidget m_Owner;

		// Token: 0x040095F2 RID: 38386
		public readonly Selector selector;

		// Token: 0x040095F3 RID: 38387
		private readonly InfinityScrollView m_ScrollView;

		// Token: 0x040095F4 RID: 38388
		private readonly TMP_Text m_EmptyMessageText;

		// Token: 0x040095F5 RID: 38389
		private ExtendedScrollRect m_ScrollRectCache;

		// Token: 0x040095F6 RID: 38390
		private float m_GroupHeaderHeight;

		// Token: 0x040095F7 RID: 38391
		private float m_GroupEmptyHeight;

		// Token: 0x040095F8 RID: 38392
		private float m_ProductContainerHeight;

		// Token: 0x040095F9 RID: 38393
		private ElementObjectManager m_ProductWidgetPref;

		// Token: 0x040095FA RID: 38394
		private ElementObjectManager m_ProductRandomWidgetPref;

		// Token: 0x040095FB RID: 38395
		private ElementObjectManager m_ProductVSWidgetPref;

		// Token: 0x040095FC RID: 38396
		private ElementObjectManager m_ProductEventWidgetPref;

		// Token: 0x040095FD RID: 38397
		private ElementObjectManager m_ProductOfficialAccountWidgetPref;

		// Token: 0x040095FE RID: 38398
		private ElementObjectManager m_ProductCommingSoonWidgetPref;

		// Token: 0x040095FF RID: 38399
		private readonly Dictionary<GameObject, ProductGroupHeaderWidget> m_HeaderWidgetMap;

		// Token: 0x04009600 RID: 38400
		private readonly Dictionary<GameObject, ProductContainerWidget> m_ContainerWidgetMap;

		// Token: 0x04009601 RID: 38401
		private readonly Dictionary<GameObject, ProductWidget> m_ProductWidgetMap;

		// Token: 0x04009602 RID: 38402
		private readonly List<float> m_EntityVirtualPositions;

		// Token: 0x04009603 RID: 38403
		private RectOffset m_BasePadding;

		// Token: 0x04009604 RID: 38404
		private RectOffset m_Padding;

		// Token: 0x04009605 RID: 38405
		private float m_Spacing;

		// Token: 0x04009606 RID: 38406
		private ProductListWidget.Context m_Context;

		// Token: 0x04009607 RID: 38407
		private bool m_EntityVirtualPositionsDirty;

		// Token: 0x04009608 RID: 38408
		private int m_PreSelectedDataIdx;

		// Token: 0x04009609 RID: 38409
		private int m_PreSelectedSectionDataIdx;

		// Token: 0x0400960A RID: 38410
		private Coroutine m_FocusProductRoutine;

		// Token: 0x02000C69 RID: 3177
		public class Context
		{
			// Token: 0x17000985 RID: 2437
			// (get) Token: 0x06005AF5 RID: 23285 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool hasHeader
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool hasProduct
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400960B RID: 38411
			public List<int> templateIdxList;

			// Token: 0x0400960C RID: 38412
			public readonly List<string> headerLabels;

			// Token: 0x0400960D RID: 38413
			public readonly List<IProductContext[]> productContainers;

			// Token: 0x0400960E RID: 38414
			public readonly List<IProductContext> productCtxs;

			// Token: 0x0400960F RID: 38415
			public string emptyMessageText;
		}
	}
}
