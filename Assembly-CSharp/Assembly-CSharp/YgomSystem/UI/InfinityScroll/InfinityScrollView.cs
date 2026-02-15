using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YgomSystem.UI.InfinityScroll
{
	// Token: 0x02000689 RID: 1673
	public class InfinityScrollView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x0000216A File Offset: 0x0000036A
		public EntityPoolSettings entityPoolSettings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x0000216A File Offset: 0x0000036A
		public EntityPoolController.WaitFocusData waitFocusData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003445 RID: 13381 RVA: 0x0000216D File Offset: 0x0000036D
		public string eLabelTemplate
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06003446 RID: 13382 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] eLabelAdditionalTemplates
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCompletedReserve
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMoving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<GameObject> activeEntityList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isHorizontal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000029CC File Offset: 0x00000BCC
		public int dataCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600344E RID: 13390 RVA: 0x0000216D File Offset: 0x0000036D
		public bool useViewportSize
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000385 RID: 901
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<int, ValueTuple<bool, float>> customMoveContentToFitDataFunc
		{
			set
			{
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000029CC File Offset: 0x00000BCC
		public int constraintCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000029CC File Offset: 0x00000BCC
		public int activeEntityLineCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000388 RID: 904
		// (set) Token: 0x06003452 RID: 13394 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<Vector2, Vector2, bool> dragStarterFunc
		{
			set
			{
			}
		}

		// Token: 0x17000389 RID: 905
		// (set) Token: 0x06003453 RID: 13395 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<bool> onSelectorSelectoredFunc
		{
			set
			{
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003455 RID: 13397 RVA: 0x0000216D File Offset: 0x0000036D
		public int defaultIdx
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentSubIdxX
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentSubIdxY
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000029CC File Offset: 0x00000BCC
		public int contentCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000029CC File Offset: 0x00000BCC
		public int beginIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x000029CC File Offset: 0x00000BCC
		public int endIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000391 RID: 913
		// (set) Token: 0x0600345C RID: 13404 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<int, bool> isSelectableDataIndexFunc
		{
			set
			{
			}
		}

		// Token: 0x17000392 RID: 914
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<GameObject, IReadOnlyList<ValueTuple<SelectionItem, int, int>>> customCollectSelectionItemsFunc
		{
			set
			{
			}
		}

		// Token: 0x17000393 RID: 915
		// (set) Token: 0x0600345E RID: 13406 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<SelectionItem, PadInputDirection, bool> customEdgeTransitionFunc
		{
			set
			{
			}
		}

		// Token: 0x17000394 RID: 916
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<SelectionItem, PadInputDirection, bool> customInnerTransitionFunc
		{
			set
			{
			}
		}

		// Token: 0x17000395 RID: 917
		// (set) Token: 0x06003460 RID: 13408 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<GameObject, int, bool, bool> customOnFocusSelectFunc
		{
			set
			{
			}
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(Action onCompleteCallback)
		{
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(GameObject template = null, Action onCompleteCallback = null)
		{
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(List<GameObject> templates = null, Action onCompleteCallback = null)
		{
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(EntityPoolController entityPool = null, EntitySelectorController entitySelector = null, Action onCompleteCallback = null)
		{
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(EntityPoolController entityPool = null, EntitySelectorController entitySelector = null, List<GameObject> templates = null, Action onCompleteCallback = null)
		{
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreateEntity(GameObject entity)
		{
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivateEntity(GameObject entity)
		{
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject entity, int dataindex)
		{
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFocusEntity(GameObject entity, int dataindex, bool selectItem, bool isInitializeSelect = false)
		{
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeactivateEntity(GameObject entity)
		{
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnRemoveEntity(GameObject entity, int dataindex, bool isTop)
		{
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReserveTemplate(int templateIdx = 0, int asyncPerFrame = 0, Action onComplete = null)
		{
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x0000216D File Offset: 0x0000036D
		public void TerminateReserve()
		{
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataCount(int dataCount, List<int> templateList = null)
		{
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataCountAsync(int dataCount, List<int> templateList = null, int updatePerFrame = 1, Action onComplete = null)
		{
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateData()
		{
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetContentPosition()
		{
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusItemByDataIndex(int dataindex, bool selectItem = true, bool isInitializeSelect = false, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TryFocusInnerViewport(bool selectItem, bool isInitializeSelect = false, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetTemplateSizeAlongScrollDirection(int templateIdx)
		{
			return 0f;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetEntityByDataIndex(int dataindex)
		{
			return null;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000F2BD0 File Offset: 0x000F0DD0
		public T GetEntityByDataIndex<T>(int dataindex)
		{
			return default(T);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetLineByDataIndex(int dataindex)
		{
			return 0;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDataIndexByEntity(GameObject entity)
		{
			return 0;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetTemplateIndexByDataIndex(int dataindex)
		{
			return 0;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetTemplateIndexByEntity(GameObject entity)
		{
			return 0;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSideIndex(int frontIdx, PadInputDirection sideDirection)
		{
			return 0;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeByDataIndex(int dataindex, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImmediateApplyMovement()
		{
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateContentPos()
		{
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopAutoScroll()
		{
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTemplateLabelByDataIndex(int dataindex)
		{
			return null;
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTemplateLabelByTemplateIndex(int templateindex)
		{
			return null;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdatePaddingSize()
		{
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateViewportRectSize()
		{
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetEntityBySelectionItem(SelectionItem selectionItem)
		{
			return null;
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x0000216A File Offset: 0x0000036A
		public List<SelectionItem> GetSelectionItemsByEntity(GameObject entity)
		{
			return null;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelectIdx(int dataIdx, int xPos = 0, int yPos = 0, bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool JumpToDirection(PadInputDirection direction, bool selectItem = true, bool isInitializeSelect = false, int jumpLength = 0)
		{
			return false;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFractionIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<SelectionItem, int> GetSelectionItemXMap()
		{
			return null;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<SelectionItem, int> GetSelectionItemYMap()
		{
			return null;
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSelectableDataIndex(int dataIndex)
		{
			return false;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsRegistedSelectionItem(SelectionItem selectionItem)
		{
			return false;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegistSelectionItem(GameObject entity, SelectionItem selectionItem, int xPos, int yPos, int dataindex = -1)
		{
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnregistSelectionItem(GameObject entity = null, SelectionItem selectionItem = null)
		{
		}

		// Token: 0x04002FF3 RID: 12275
		[SerializeField]
		private string m_ELabelTemplate;

		// Token: 0x04002FF4 RID: 12276
		[SerializeField]
		private string[] m_ELabelAdditionalTemplates;

		// Token: 0x04002FF5 RID: 12277
		[SerializeField]
		private EntityPoolSettings m_EntityPoolSettings;

		// Token: 0x04002FF6 RID: 12278
		[SerializeField]
		private EntitySelectorSettings m_EntitySelectorSettings;

		// Token: 0x04002FF7 RID: 12279
		private EntityPoolController m_EntityPool;

		// Token: 0x04002FF8 RID: 12280
		private EntitySelectorController m_EntitySelector;

		// Token: 0x04002FF9 RID: 12281
		public Action<GameObject> onCreatedEntityCallback;

		// Token: 0x04002FFA RID: 12282
		public Action<GameObject> onActivateEntityCallback;

		// Token: 0x04002FFB RID: 12283
		public Action<GameObject, int> onUpdateEntityCallback;

		// Token: 0x04002FFC RID: 12284
		public Action<GameObject, int, bool, bool> onFocusEntityCallback;

		// Token: 0x04002FFD RID: 12285
		public Action<GameObject> onDeactivateEntityCallback;

		// Token: 0x04002FFE RID: 12286
		public Action<GameObject, int, bool> onRemoveEntityCallback;
	}
}
