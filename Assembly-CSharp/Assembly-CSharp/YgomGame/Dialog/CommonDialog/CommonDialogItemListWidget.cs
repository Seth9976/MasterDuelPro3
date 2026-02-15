using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.UI.PropertyOverrider;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F6E RID: 3950
	public class CommonDialogItemListWidget : ContentWidgetBase<CommonDialogItemListWidget, EntryItemListData>, IContentWidgetAsyncLoader, IContentWidgetDirectionalInputListener, IContentLifecycleHandler
	{
		// Token: 0x06007439 RID: 29753 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogItemListWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x0600743A RID: 29754 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600743B RID: 29755 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryItemListData entryData)
		{
		}

		// Token: 0x0600743C RID: 29756 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yAsyncBinding()
		{
			return null;
		}

		// Token: 0x0600743D RID: 29757 RVA: 0x0000216D File Offset: 0x0000036D
		public void AsyncBinding(IEntryData entryData, Action onComplete)
		{
		}

		// Token: 0x0600743E RID: 29758 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollInitialized()
		{
		}

		// Token: 0x0600743F RID: 29759 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntityCallback(GameObject entity)
		{
		}

		// Token: 0x06007440 RID: 29760 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdatedEntityCallback(GameObject entity, int idx)
		{
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnCustomEdgeTransition(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06007442 RID: 29762 RVA: 0x0000216D File Offset: 0x0000036D
		private void TrySelectHeadItem()
		{
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x0000216D File Offset: 0x0000036D
		private void TrySelectBottomItem()
		{
		}

		// Token: 0x06007444 RID: 29764 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectItem(int entityIdx, SelectionItem currentItem)
		{
			return false;
		}

		// Token: 0x06007445 RID: 29765 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectItem(GameObject activeEntity, SelectionItem currentItem)
		{
			return false;
		}

		// Token: 0x06007446 RID: 29766 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckSelectOnAnalogInput(Vector2 dir)
		{
		}

		// Token: 0x06007447 RID: 29767 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnMainAnalogInput(Vector2 dir)
		{
		}

		// Token: 0x06007448 RID: 29768 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSubAnalogInput(Vector2 dir)
		{
		}

		// Token: 0x06007449 RID: 29769 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnLeftInput()
		{
		}

		// Token: 0x0600744A RID: 29770 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnRightInput()
		{
		}

		// Token: 0x0600744B RID: 29771 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpInput()
		{
		}

		// Token: 0x0600744C RID: 29772 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDownInput()
		{
		}

		// Token: 0x0600744D RID: 29773 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnBack()
		{
			return false;
		}

		// Token: 0x0400AD57 RID: 44375
		private readonly string k_ELabelScrollView;

		// Token: 0x0400AD58 RID: 44376
		private InfinityScrollView m_ScrollView;

		// Token: 0x0400AD59 RID: 44377
		private Selector m_Selector;

		// Token: 0x0400AD5A RID: 44378
		private Dictionary<GameObject, CommonDialogItemListWidget.ItemLineWidget> m_EntityMap;

		// Token: 0x0400AD5B RID: 44379
		private EntryItemListData m_EntryData;

		// Token: 0x0400AD5C RID: 44380
		private Action m_OnCompleteCallback;

		// Token: 0x02000F6F RID: 3951
		public class ItemLineWidget : ElementWidgetBase
		{
			// Token: 0x0600744F RID: 29775 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public ItemLineWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x06007450 RID: 29776 RVA: 0x0000216D File Offset: 0x0000036D
			public void ApplySwitchLabel(bool isPeriod, int itemCategory, int itemId)
			{
			}

			// Token: 0x0400AD5D RID: 44381
			private readonly string k_ELabelItemButton;

			// Token: 0x0400AD5E RID: 44382
			private readonly string k_ELabelItemIcon;

			// Token: 0x0400AD5F RID: 44383
			private readonly string k_ELabelItemNameText;

			// Token: 0x0400AD60 RID: 44384
			private readonly string k_ELabelItemNumText;

			// Token: 0x0400AD61 RID: 44385
			private readonly string k_OVGroupLabel_Default;

			// Token: 0x0400AD62 RID: 44386
			private readonly string k_OVGroupLabel_Structure;

			// Token: 0x0400AD63 RID: 44387
			private readonly PlatformOverriderGroup m_OvGroup;

			// Token: 0x0400AD64 RID: 44388
			public readonly SelectionButton button;

			// Token: 0x0400AD65 RID: 44389
			public readonly GameObject itemIcon;

			// Token: 0x0400AD66 RID: 44390
			public readonly TMP_Text itemNameText;

			// Token: 0x0400AD67 RID: 44391
			public readonly TMP_Text itemNumText;
		}
	}
}
