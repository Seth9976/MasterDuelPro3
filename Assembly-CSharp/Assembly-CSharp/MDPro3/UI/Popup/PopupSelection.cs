using System;
using System.Collections.Generic;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI.Popup
{
	// Token: 0x0200148E RID: 5262
	public class PopupSelection : Popup
	{
		// Token: 0x06009A18 RID: 39448 RVA: 0x001707E0 File Offset: 0x0016E9E0
		protected override void InitializeSelections()
		{
			base.InitializeSelections();
			base.Manager.GetElement<TextMeshProUGUI>("MessageText").text = this.args[1];
			base.Manager.GetElement("MessageArea").SetActive(this.args[1] != string.Empty);
			float itemWidth = (PropertyOverrider.NeedMobileLayout() ? 940f : 748f);
			float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 98f : 72f);
			float topPadding = (PropertyOverrider.NeedMobileLayout() ? 47f : 30f);
			float space = (PropertyOverrider.NeedMobileLayout() ? 20f : 16f);
			float preferredHeight = (float)(this.args.Count - 2) * (itemHeight + space) + topPadding * 2f - space;
			base.Manager.GetElement<LayoutElement>("EntryButtonsScrollView").preferredHeight = preferredHeight;
			Addressables.LoadAssetAsync<GameObject>("Popup/PopupSelectionItem.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				SuperScrollView superScrollView = new SuperScrollView(1, itemWidth, itemHeight + space, topPadding, topPadding - space, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.Manager.GetElement<ScrollRect>("EntryButtonsScrollView"), 2);
				List<string[]> tasks = new List<string[]>();
				for (int i = 2; i < this.args.Count; i++)
				{
					string[] task = this.args[i].Split(":", StringSplitOptions.None);
					tasks.Add(task);
				}
				superScrollView.Print(tasks);
				if (superScrollView.items.Count > 0)
				{
					EventSystem.current.SetSelectedGameObject(superScrollView.items[0].gameObject);
				}
			};
		}

		// Token: 0x06009A19 RID: 39449 RVA: 0x00170918 File Offset: 0x0016EB18
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_PopupSelectionItem handler = item.GetComponent<SelectionToggle_PopupSelectionItem>();
			if (task.Length == 2)
			{
				handler.color = task[0];
				handler.selection = task[1];
			}
			else
			{
				handler.color = string.Empty;
				handler.selection = task[0];
			}
			handler.clickAction = new Action(this.OnClick);
			handler.manager = this;
			handler.Refresh();
		}

		// Token: 0x06009A1A RID: 39450 RVA: 0x00170979 File Offset: 0x0016EB79
		private void OnClick()
		{
			Action action = this.decideAction;
			if (action != null)
			{
				action();
			}
			this.Hide();
		}

		// Token: 0x06009A1B RID: 39451 RVA: 0x00170994 File Offset: 0x0016EB94
		protected override void SelectLastSelected()
		{
			if (this.lastSelectedItem != null)
			{
				EventSystem.current.SetSelectedGameObject(this.lastSelectedItem.gameObject);
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.Manager.GetElement<ScrollRect>("EntryButtonsScrollView").content.GetChild(0).gameObject);
		}

		// Token: 0x06009A1C RID: 39452 RVA: 0x001707D2 File Offset: 0x0016E9D2
		public void SelectLastItem()
		{
			UserInput.NextSelectionIsAxis = true;
			this.SelectLastSelected();
		}

		// Token: 0x0400D7BF RID: 55231
		[Header("Popup Selection")]
		public SelectionToggle_PopupSelectionItem lastSelectedItem;

		// Token: 0x0400D7C0 RID: 55232
		public Action decideAction;
	}
}
