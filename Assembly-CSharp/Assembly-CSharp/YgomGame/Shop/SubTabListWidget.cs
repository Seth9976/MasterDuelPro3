using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000977 RID: 2423
	public class SubTabListWidget : ElementWidgetBase
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060046F0 RID: 18160 RVA: 0x000029CC File Offset: 0x00000BCC
		public int dataCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool GetAcordionValue(int categoryId, int subCategoryId)
		{
			return false;
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAcordionValue(int categoryId, int subCategoryId, bool value)
		{
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool GetAcordionManualFlag(int categoryId, int subCategoryId)
		{
			return false;
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAcordionManualFlag(int categoryId, int subCategoryId, bool value)
		{
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x0000216A File Offset: 0x0000036A
		public ISubTabWidget GetSubTabWidget(int dataIdx)
		{
			return null;
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabGroupWidget GetGroupWidgetByDataIdx(int dataIdx)
		{
			return null;
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabSingleWidget GetSectionWidget(SubTabGroupWidget groupWidget, int sectionIdx)
		{
			return null;
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SubTabListWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init(ISubTabListWidgetHandler handler, ISubTabListWidgetListener listener)
		{
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopMovement()
		{
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetPos()
		{
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataCount()
		{
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateData()
		{
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayShow(bool isNext)
		{
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectCurrentIdx(bool isInitializeSelect = false, bool selectSection = false)
		{
			return false;
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool FocusCurrentIdx(bool containSection = false, bool selectItem = false, bool initializeSelection = false, bool immediate = false)
		{
			return false;
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CheckRecoverSelectItem()
		{
			return false;
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject entity, int templateIdx, SubTabGroupWidget parentGroup = null)
		{
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivateEntity(GameObject entity)
		{
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeactivateEntity(GameObject entity)
		{
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x0000216A File Offset: 0x0000036A
		private SubTabSingleWidget OnCreatedEntitySingle(GameObject entity, SubTabGroupWidget parentGroup = null)
		{
			return null;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x0000216A File Offset: 0x0000036A
		private SubTabGroupWidget OnCreatedEntityGroup(GameObject entity)
		{
			return null;
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject entity, int dataIdx)
		{
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntitySignle(SubTabSingleWidget widget, int dataIdx)
		{
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntityGroup(SubTabGroupWidget widget, int dataIdx)
		{
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntityGroupSection(GameObject sectionEntity, SubTabGroupWidget parentGroup = null)
		{
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntityGroupSection(GameObject sectionEntity, int dataIdx, int sectionDataIdx)
		{
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnInputDirection(PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnGroupParentSelected()
		{
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnGroupSectionSelected()
		{
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnSelectorSelected()
		{
			return false;
		}

		// Token: 0x04008526 RID: 34086
		internal const int k_TemplateId_Single = 0;

		// Token: 0x04008527 RID: 34087
		internal const int k_TemplateId_Group = 1;

		// Token: 0x04008528 RID: 34088
		private const string k_ELabelTabsList = "TabsList";

		// Token: 0x04008529 RID: 34089
		private const string k_TLabel_ShowNext = "ShowNext";

		// Token: 0x0400852A RID: 34090
		private const string k_TLabel_ShowBack = "ShowBack";

		// Token: 0x0400852B RID: 34091
		private readonly ElementEntityFactory m_EntityFactory;

		// Token: 0x0400852C RID: 34092
		private readonly SnapScrollView m_ScrollView;

		// Token: 0x0400852D RID: 34093
		private Dictionary<GameObject, ISubTabWidget> m_TabWidgetMap;

		// Token: 0x0400852E RID: 34094
		private Dictionary<int, Dictionary<int, bool>> m_AcordionValueMap;

		// Token: 0x0400852F RID: 34095
		private Dictionary<int, Dictionary<int, bool>> m_AcordionManualMap;

		// Token: 0x04008530 RID: 34096
		private ISubTabListWidgetHandler m_Handler;

		// Token: 0x04008531 RID: 34097
		private ISubTabListWidgetListener m_Listener;

		// Token: 0x04008532 RID: 34098
		private List<int> m_TemplateIds;

		// Token: 0x04008533 RID: 34099
		public readonly Selector selector;

		// Token: 0x04008534 RID: 34100
		private bool m_PreSelectedGroupParent;

		// Token: 0x02000978 RID: 2424
		public class TabContext
		{
			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x06004710 RID: 18192 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool hasChildren
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004711 RID: 18193 RVA: 0x00002739 File Offset: 0x00000939
			public TabContext(IShopProductGruopData groupData, string label)
			{
			}

			// Token: 0x06004712 RID: 18194 RVA: 0x00002739 File Offset: 0x00000939
			public TabContext(IShopProductGruopData setting, string label, List<SubTabListWidget.TabContext> children)
			{
			}

			// Token: 0x04008535 RID: 34101
			public IShopProductGruopData groupData;

			// Token: 0x04008536 RID: 34102
			public string label;

			// Token: 0x04008537 RID: 34103
			public bool badge;

			// Token: 0x04008538 RID: 34104
			public List<SubTabListWidget.TabContext> children;
		}
	}
}
