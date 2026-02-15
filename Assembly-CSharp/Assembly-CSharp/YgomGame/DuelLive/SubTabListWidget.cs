using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C6E RID: 3182
	public class SubTabListWidget : ElementWidgetBase
	{
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06005B0E RID: 23310 RVA: 0x000029CC File Offset: 0x00000BCC
		public int idx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06005B0F RID: 23311 RVA: 0x000029CC File Offset: 0x00000BCC
		public int sectionIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06005B10 RID: 23312 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool exists
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06005B11 RID: 23313 RVA: 0x0000216A File Offset: 0x0000036A
		public InputFieldWidget searchNameInputWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06005B12 RID: 23314 RVA: 0x0000216A File Offset: 0x0000036A
		public ElementEntityFactory entityFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06005B13 RID: 23315 RVA: 0x0000216A File Offset: 0x0000036A
		public SnapScrollView scrollView
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06005B14 RID: 23316 RVA: 0x0000216A File Offset: 0x0000036A
		public UnityEvent<string> onSubmitSearchField
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06005B15 RID: 23317 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005B16 RID: 23318 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int, int, int, int> onPreChangeIdxEvent
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

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06005B17 RID: 23319 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005B18 RID: 23320 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int, int> onChangedIdxEvent
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

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06005B19 RID: 23321 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005B1A RID: 23322 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int> onClickSubCategory
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

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06005B1B RID: 23323 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005B1C RID: 23324 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int> onClickedSubCategoryGroup
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

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06005B1D RID: 23325 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005B1E RID: 23326 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<int, int> onClickedSubCategorySection
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

		// Token: 0x06005B1F RID: 23327 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool GetAcordionByDataIdx(int dataIdx)
		{
			return false;
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetAcordionByDataIdx(int dataIdx, bool value)
		{
			return false;
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x0000216A File Offset: 0x0000036A
		public ISubTabWidget SearchSubTabWidgetBySelectionItem(SelectionItem item)
		{
			return null;
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSectionButton(SelectionItem selectionItem)
		{
			return false;
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabGroupWidget GetGroupWidgetByDataIdx(int dataIdx)
		{
			return null;
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SubTabListWidget(ElementObjectManager eom, DuelLiveRootWidget owner)
			: base(null)
		{
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init(List<SubTabListWidget.TabContext> contextDatas)
		{
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x0000216D File Offset: 0x0000036D
		public void ExpandAcordionImmediate()
		{
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x0000216D File Offset: 0x0000036D
		public void CheckSectionIdxByCurrentPos()
		{
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetIdx(int idx, int sectionIdx = 0)
		{
			return false;
		}

		// Token: 0x06005B29 RID: 23337 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectCurrentIdx(bool initializeSelection = false, bool selectSection = false)
		{
			return false;
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusCurrentIdx(bool containSection = false, bool selectItem = false, bool initializeSelection = false)
		{
			return false;
		}

		// Token: 0x06005B2B RID: 23339 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject entity, int templateIdx, SubTabGroupWidget parentGroup = null)
		{
		}

		// Token: 0x06005B2C RID: 23340 RVA: 0x0000216A File Offset: 0x0000036A
		private SubTabSingleWidget OnCreatedEntitySingle(GameObject entity, SubTabGroupWidget parentGroup = null)
		{
			return null;
		}

		// Token: 0x06005B2D RID: 23341 RVA: 0x0000216A File Offset: 0x0000036A
		private SubTabGroupWidget OnCreatedEntityGroup(GameObject entity)
		{
			return null;
		}

		// Token: 0x06005B2E RID: 23342 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject entity, int dataIdx)
		{
		}

		// Token: 0x06005B2F RID: 23343 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntitySignle(SubTabSingleWidget widget, int dataIdx)
		{
		}

		// Token: 0x06005B30 RID: 23344 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntityGroup(SubTabGroupWidget widget, int dataIdx)
		{
		}

		// Token: 0x06005B31 RID: 23345 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntityGroupSection(GameObject sectionEntity, SubTabGroupWidget parentGroup = null)
		{
		}

		// Token: 0x06005B32 RID: 23346 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntityGroupSection(GameObject sectionEntity, int dataIdx, int sectionDataIdx)
		{
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnInputDirection(PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnGroupParentSelected()
		{
		}

		// Token: 0x06005B35 RID: 23349 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnGroupSectionSelected()
		{
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnSelectorSelected()
		{
			return false;
		}

		// Token: 0x04009653 RID: 38483
		internal const int k_TemplateId_Single = 0;

		// Token: 0x04009654 RID: 38484
		internal const int k_TemplateId_Group = 1;

		// Token: 0x04009655 RID: 38485
		private const string k_ELabelTabsList = "TabsList";

		// Token: 0x04009656 RID: 38486
		private const string k_ELabelSearchInputField = "SearchInputField";

		// Token: 0x04009657 RID: 38487
		private readonly DuelLiveRootWidget m_Owner;

		// Token: 0x04009658 RID: 38488
		private readonly ElementEntityFactory m_EntityFactory;

		// Token: 0x04009659 RID: 38489
		private readonly InputFieldWidget m_SearchNameInputWidget;

		// Token: 0x0400965A RID: 38490
		private readonly SnapScrollView m_ScrollView;

		// Token: 0x0400965B RID: 38491
		private Dictionary<GameObject, ISubTabWidget> m_TabWidgetMap;

		// Token: 0x0400965C RID: 38492
		private Dictionary<GameObject, bool> m_AcordionMap;

		// Token: 0x0400965D RID: 38493
		private int m_Idx;

		// Token: 0x0400965E RID: 38494
		private int m_SectionIdx;

		// Token: 0x0400965F RID: 38495
		private List<SubTabListWidget.TabContext> m_ContextDatas;

		// Token: 0x04009660 RID: 38496
		private List<int> m_TemplateIds;

		// Token: 0x04009661 RID: 38497
		public int defaultIdx;

		// Token: 0x04009662 RID: 38498
		public int defaultSectionIdx;

		// Token: 0x04009663 RID: 38499
		public readonly Selector selector;

		// Token: 0x04009664 RID: 38500
		public bool isPopWallPaper;

		// Token: 0x04009665 RID: 38501
		private bool m_PreSelectedGroupParent;

		// Token: 0x02000C6F RID: 3183
		public class TabContext
		{
			// Token: 0x17000994 RID: 2452
			// (get) Token: 0x06005B37 RID: 23351 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool hasChildren
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06005B38 RID: 23352 RVA: 0x00002739 File Offset: 0x00000939
			public TabContext(IDuelLiveProductGruopData groupData, string label)
			{
			}

			// Token: 0x06005B39 RID: 23353 RVA: 0x00002739 File Offset: 0x00000939
			public TabContext(IDuelLiveProductGruopData setting, string label, List<SubTabListWidget.TabContext> children)
			{
			}

			// Token: 0x04009666 RID: 38502
			public IDuelLiveProductGruopData groupData;

			// Token: 0x04009667 RID: 38503
			public string label;

			// Token: 0x04009668 RID: 38504
			public bool badge;

			// Token: 0x04009669 RID: 38505
			public List<SubTabListWidget.TabContext> children;
		}
	}
}
