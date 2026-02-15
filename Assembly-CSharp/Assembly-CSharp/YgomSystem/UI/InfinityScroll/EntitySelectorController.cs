using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI.InfinityScroll
{
	// Token: 0x02000687 RID: 1671
	public class EntitySelectorController
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<SelectionItem, int> SelectionItemXMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<SelectionItem, int> SelectionItemYMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject selectedEntity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000029CC File Offset: 0x00000BCC
		public int selectedEntityIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000371 RID: 881
		// (set) Token: 0x06003419 RID: 13337 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<Vector2, Vector2, bool> dragStarterFunc
		{
			set
			{
			}
		}

		// Token: 0x17000372 RID: 882
		// (set) Token: 0x0600341A RID: 13338 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<bool> onSelectorSelectoredFunc
		{
			set
			{
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600341C RID: 13340 RVA: 0x0000216D File Offset: 0x0000036D
		public int defaultIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600341E RID: 13342 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentIdx
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

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003420 RID: 13344 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentSubIdxX
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

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003422 RID: 13346 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentSubIdxY
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

		// Token: 0x06003423 RID: 13347 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(EntitySelectorSettings selectorSettings, InfinityScrollView owner, Selector selector, Action onComplete = null, GridLayoutGroup.Axis axis = GridLayoutGroup.Axis.Vertical)
		{
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCreateEntity(GameObject entity)
		{
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnActivateEntity(GameObject entity)
		{
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpdateEntity(GameObject entity, int dataindex)
		{
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFocusEntity(GameObject entity, int dataindex, bool selectItem, bool isInitializeSelect = false)
		{
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDeactivateEntity(GameObject entity)
		{
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTransitionModeAsEdge(SelectionItem selectionItem, PadInputDirection direction)
		{
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTransitionModeAsFraction(SelectionItem selectionItem, PadInputDirection direction)
		{
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTransitionModeAsInner(SelectionItem selectionItem, PadInputDirection direction)
		{
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedItem(SelectionItem selectionItem)
		{
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnSelectorSelected()
		{
			return false;
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEntityPadInput(SelectionItem selectionItem, PadInputDirection direction)
		{
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetContentPosition()
		{
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSelectableDataIndex(int dataIndex)
		{
			return false;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSelectableSideIndex(int dataIndex, PadInputDirection direction)
		{
			return 0;
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEdgeSelectionItem(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetEntityBySelectionItem(SelectionItem selectionItem)
		{
			return null;
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x0000216A File Offset: 0x0000036A
		public List<SelectionItem> GetSelectionItemsByEntity(GameObject entity)
		{
			return null;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelectIdx(int dataIdx, int xPos = 0, int yPos = 0, bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool JumpToDirection(PadInputDirection direction, bool selectItem = true, bool isInitializeSelect = false, int jumpLength = 0)
		{
			return false;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsRegistedSelectionItem(SelectionItem selectionItem)
		{
			return false;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitSelectionItems(SelectionItem selectionItem)
		{
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegistSelectionItem(GameObject entity, SelectionItem selectionItem, int xPos, int yPos, int dataindex = -1)
		{
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnregistSelectionItem(GameObject entity = null, SelectionItem selectionItem = null)
		{
		}

		// Token: 0x04002FDC RID: 12252
		private EntitySelectorSettings m_SelectorSettings;

		// Token: 0x04002FDD RID: 12253
		private InfinityScrollView m_Owner;

		// Token: 0x04002FDE RID: 12254
		private Selector m_Selector;

		// Token: 0x04002FDF RID: 12255
		private GridLayoutGroup.Axis m_ScrollAxis;

		// Token: 0x04002FE0 RID: 12256
		private readonly Dictionary<SelectionItem, int> m_SelectionItemXMap;

		// Token: 0x04002FE1 RID: 12257
		private readonly Dictionary<SelectionItem, int> m_SelectionItemYMap;

		// Token: 0x04002FE2 RID: 12258
		private readonly Dictionary<GameObject, List<SelectionItem>> m_EntitySelectionItemsMap;

		// Token: 0x04002FE3 RID: 12259
		private List<SelectionItem> m_TmpSearchSelectionItems;

		// Token: 0x04002FE4 RID: 12260
		private List<SelectionItem> m_InitializedSelectionItems;

		// Token: 0x04002FE5 RID: 12261
		public Func<GameObject, IReadOnlyList<ValueTuple<SelectionItem, int, int>>> customCollectSelectionItemsFunc;

		// Token: 0x04002FE6 RID: 12262
		public Func<int, bool> isSelectableDataIndexFunc;

		// Token: 0x04002FE7 RID: 12263
		public Func<SelectionItem, PadInputDirection, bool> customEdgeTransitionFunc;

		// Token: 0x04002FE8 RID: 12264
		public Func<SelectionItem, PadInputDirection, bool> customInnerTransitionFunc;

		// Token: 0x04002FE9 RID: 12265
		public Func<GameObject, int, bool, bool> customOnFocusSelectFunc;

		// Token: 0x04002FEA RID: 12266
		public Func<Vector2, Vector2, bool> m_DragStarterFunc;

		// Token: 0x04002FEB RID: 12267
		public Func<bool> m_OnSelectorSelectedFunc;
	}
}
