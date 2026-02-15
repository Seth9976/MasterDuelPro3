using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Dialog.CommonDialog;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Mission
{
	// Token: 0x02000A2A RID: 2602
	public class MissionBulkRecieveDialogWidget : ContentWidgetBase<MissionBulkRecieveDialogWidget, EntryInsertWidgetData>, IContentWidgetAsyncLoader, IContentPostAllInsertedHandler, IContentWidgetDirectionalInputListener, IContentLifecycleHandler
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool rebuildLayoutOnPostAllInserted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x0000216A File Offset: 0x0000036A
		public static MissionBulkRecieveDialogWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x0000216D File Offset: 0x0000036D
		public void AsyncBinding(IEntryData entryData, Action onComplete)
		{
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryInsertWidgetData entryData)
		{
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnPostAllInserted()
		{
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollInitialized()
		{
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdatedEntityCallback(GameObject entity, int idx)
		{
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnCustomEdgeTransition(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnCustomInnerTransition(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnFocusSelectEntity(GameObject entity, int dataIndex, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertGroupName(string name)
		{
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertGroupBorder()
		{
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertItemEntity(EntryItemListData.Context itemContext)
		{
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertMissionEntity(MissionBulkRecieveDialogWidget.ClearInfoContext clearInfo)
		{
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertEntityBorder()
		{
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateGroupName(GameObject entity, object data)
		{
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateItemEntity(GameObject entity, object data)
		{
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMissionEntity(GameObject entity, object data)
		{
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectHeadItem(bool focus = false)
		{
			return false;
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x0000216D File Offset: 0x0000036D
		private void TrySelectBottomItem(bool focus = false)
		{
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectItem(int entityIdx, SelectionItem currentItem, bool focus = false)
		{
			return false;
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectItem(GameObject activeEntity, SelectionItem currentItem, bool focus = false)
		{
			return false;
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckSelectOnAnalogInput(Vector2 dir)
		{
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnMainAnalogInput(Vector2 dir)
		{
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSubAnalogInput(Vector2 dir)
		{
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnLeftInput()
		{
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnRightInput()
		{
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpInput()
		{
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDownInput()
		{
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnBack()
		{
			return false;
		}

		// Token: 0x04008966 RID: 35174
		private readonly int k_TemplateItemIdx;

		// Token: 0x04008967 RID: 35175
		private readonly int k_TemplateGroupNameIdx;

		// Token: 0x04008968 RID: 35176
		private readonly int k_TemplateGroupBorderIdx;

		// Token: 0x04008969 RID: 35177
		private readonly int k_TemplateMissionIdx;

		// Token: 0x0400896A RID: 35178
		private readonly int k_TemplateItemBorderIdx;

		// Token: 0x0400896B RID: 35179
		public List<EntryItemListData.Context> totalItems;

		// Token: 0x0400896C RID: 35180
		public List<MissionBulkRecieveDialogWidget.ClearTabContext> clearTabInfos;

		// Token: 0x0400896D RID: 35181
		private InfinityScrollView m_ScrollView;

		// Token: 0x0400896E RID: 35182
		private Selector m_Selector;

		// Token: 0x0400896F RID: 35183
		private List<object> m_ScrollDataList;

		// Token: 0x04008970 RID: 35184
		private List<int> m_ScrollTemplateIdxList;

		// Token: 0x04008971 RID: 35185
		private Dictionary<GameObject, CommonDialogItemListWidget.ItemLineWidget> m_ItemWidgetMap;

		// Token: 0x04008972 RID: 35186
		private Dictionary<SelectionItem, GameObject> m_ItemButtonEntityMap;

		// Token: 0x04008973 RID: 35187
		private Action m_OnCompleteCallback;

		// Token: 0x02000A2B RID: 2603
		public class ClearTabContext
		{
			// Token: 0x06004B8E RID: 19342 RVA: 0x00002739 File Offset: 0x00000939
			public ClearTabContext(string tabLabel)
			{
			}

			// Token: 0x04008974 RID: 35188
			public string tabLabel;

			// Token: 0x04008975 RID: 35189
			public List<MissionBulkRecieveDialogWidget.ClearInfoContext> clearInfos;
		}

		// Token: 0x02000A2C RID: 2604
		public class ClearInfoContext
		{
			// Token: 0x06004B8F RID: 19343 RVA: 0x00002739 File Offset: 0x00000939
			public ClearInfoContext(string missionName, EntryItemListData.Context item, int goalCnt, int goalMax)
			{
			}

			// Token: 0x04008976 RID: 35190
			public string missionName;

			// Token: 0x04008977 RID: 35191
			public EntryItemListData.Context item;

			// Token: 0x04008978 RID: 35192
			public int goalCnt;

			// Token: 0x04008979 RID: 35193
			public int goalMax;
		}
	}
}
