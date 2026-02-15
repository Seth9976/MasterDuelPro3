using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001464 RID: 5220
	public class DeckSelectorUI : ServantUI
	{
		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x0600979B RID: 38811 RVA: 0x00162BE0 File Offset: 0x00160DE0
		private ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x0600979C RID: 38812 RVA: 0x00162C1C File Offset: 0x00160E1C
		private TMP_InputField Input
		{
			get
			{
				return this.m_Input = ((this.m_Input != null) ? this.m_Input : base.Manager.GetElement<TMP_InputField>("InputField"));
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x0600979D RID: 38813 RVA: 0x00162C58 File Offset: 0x00160E58
		private TextMeshProUGUI TextDeckNumValue
		{
			get
			{
				return this.m_TextDeckNumValue = ((this.m_TextDeckNumValue != null) ? this.m_TextDeckNumValue : base.Manager.GetElement<TextMeshProUGUI>("TextDeckNumValue"));
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x0600979E RID: 38814 RVA: 0x00162C94 File Offset: 0x00160E94
		public SelectionToggle TogglePickupCard
		{
			get
			{
				return this.m_TogglePickupCard = ((this.m_TogglePickupCard != null) ? this.m_TogglePickupCard : base.Manager.GetElement<SelectionToggle>("TogglePickupCard"));
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x0600979F RID: 38815 RVA: 0x00162CD0 File Offset: 0x00160ED0
		public SelectionButton ButtonOnline
		{
			get
			{
				return this.m_ButtonOnline = ((this.m_ButtonOnline != null) ? this.m_ButtonOnline : base.Manager.GetElement<SelectionButton>("ButtonOnline"));
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060097A0 RID: 38816 RVA: 0x00162D0C File Offset: 0x00160F0C
		private RectTransform Header
		{
			get
			{
				return this.m_Header = ((this.m_Header != null) ? this.m_Header : base.Manager.GetElement<RectTransform>("Header"));
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x060097A1 RID: 38817 RVA: 0x00162D48 File Offset: 0x00160F48
		private RectTransform Footer
		{
			get
			{
				return this.m_Footer = ((this.m_Footer != null) ? this.m_Footer : base.Manager.GetElement<RectTransform>("Footer"));
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x060097A2 RID: 38818 RVA: 0x00162D84 File Offset: 0x00160F84
		public SelectionButton ButtonType
		{
			get
			{
				return this.m_ButtonType = ((this.m_ButtonType != null) ? this.m_ButtonType : base.Manager.GetElement<SelectionButton>("ButtonType"));
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x060097A3 RID: 38819 RVA: 0x00162DC0 File Offset: 0x00160FC0
		private SelectionButton ButtonDelete
		{
			get
			{
				return this.m_ButtonDelete = ((this.m_ButtonDelete != null) ? this.m_ButtonDelete : base.Manager.GetElement<SelectionButton>("ButtonDelete"));
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x060097A4 RID: 38820 RVA: 0x00162DFC File Offset: 0x00160FFC
		private SelectionButton ButtonCancel
		{
			get
			{
				return this.m_ButtonCancel = ((this.m_ButtonCancel != null) ? this.m_ButtonCancel : base.Manager.GetElement<SelectionButton>("ButtonCancel"));
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060097A5 RID: 38821 RVA: 0x00162E38 File Offset: 0x00161038
		private SelectionButton ButtonConfirm
		{
			get
			{
				return this.m_ButtonConfirm = ((this.m_ButtonConfirm != null) ? this.m_ButtonConfirm : base.Manager.GetElement<SelectionButton>("ButtonConfirm"));
			}
		}

		// Token: 0x060097A6 RID: 38822 RVA: 0x00162E74 File Offset: 0x00161074
		public override void ShowEvent()
		{
			base.ShowEvent();
			switch (DeckSelector.condition)
			{
			case DeckSelector.Condition.ForEdit:
				this.ButtonOnline.gameObject.SetActive(true);
				base.Title.text = InterString.Get("编辑卡组", 0);
				break;
			case DeckSelector.Condition.ForDuel:
				this.ButtonOnline.gameObject.SetActive(false);
				base.Title.text = InterString.Get("选择卡组", 0);
				break;
			case DeckSelector.Condition.ForSolo:
				this.ButtonOnline.gameObject.SetActive(false);
				base.Title.text = InterString.Get("选择卡组", 0);
				break;
			case DeckSelector.Condition.MyCard:
				this.ButtonOnline.gameObject.SetActive(false);
				base.Title.text = InterString.Get("选择卡组", 0);
				break;
			}
			this.ShowDefaultButtons();
		}

		// Token: 0x060097A7 RID: 38823 RVA: 0x00162F50 File Offset: 0x00161150
		public override void AfterShowEvent()
		{
			base.AfterShowEvent();
			this.RefreshList();
		}

		// Token: 0x060097A8 RID: 38824 RVA: 0x00162F5E File Offset: 0x0016115E
		protected override void AfterHideEvent()
		{
			base.AfterHideEvent();
			Config.Save();
			this.TogglePickupCard.SetToggleOff(true);
			this.superScrollView.Clear();
		}

		// Token: 0x060097A9 RID: 38825 RVA: 0x00162F84 File Offset: 0x00161184
		public void RefreshList()
		{
			this.decks.Clear();
			this.ShowDefaultButtons();
			this.TogglePickupCard.SetToggleOff(true);
			this.ButtonType.SetButtonText(this.GetTypeName());
			if (!Directory.Exists("Deck/"))
			{
				Directory.CreateDirectory("Deck/");
			}
			string[] files = Directory.GetFiles(this.GetCurrentTypePath(), "*.ydk");
			List<string> fileList = files.ToList<string>();
			foreach (string file in files)
			{
				string text = Path.GetFileName(file);
				if (text.Substring(0, text.Length - 4) == Config.GetConfigDeckName(false))
				{
					fileList.Remove(file);
					fileList.Insert(0, file);
					break;
				}
			}
			foreach (string deck in fileList)
			{
				string name = Path.GetFileName(deck);
				string text = name;
				name = text.Substring(0, text.Length - 4);
				this.decks.Add(name, new Deck(deck));
			}
			this.Print(this.Input.text);
		}

		// Token: 0x060097AA RID: 38826 RVA: 0x001630BC File Offset: 0x001612BC
		public void ActivateInputField()
		{
			this.Input.ActivateInputField();
		}

		// Token: 0x060097AB RID: 38827 RVA: 0x001630CC File Offset: 0x001612CC
		public void Print(string search = "")
		{
			this.SwitchToDefaultLayout();
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			Addressables.LoadAssetAsync<GameObject>("UI/ItemDeck.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemWidth = (PropertyOverrider.NeedMobileLayout() ? 336f : 260f);
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 300f : 232f);
				float space = (PropertyOverrider.NeedMobileLayout() ? 30f : 24f);
				float bottomPadding = (PropertyOverrider.NeedMobileLayout() ? 196f : 150f) - space;
				this.superScrollView = new SuperScrollView(-1, itemWidth + space, itemHeight + space, 10f, bottomPadding, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.Manager.GetElement<ScrollRect>("ScrollRect"), 2);
				List<string[]> tasks = new List<string[]> { new string[]
				{
					string.Empty,
					"0",
					"0",
					"0",
					"0",
					"0",
					"0"
				} };
				foreach (KeyValuePair<string, Deck> deck in this.decks)
				{
					if (deck.Key.Contains(search))
					{
						string[] task = new string[]
						{
							deck.Key,
							deck.Value.Case.ToString(),
							"0",
							"0",
							"0",
							deck.Value.Protector.ToString(),
							"0",
							deck.Value.deckId
						};
						if (deck.Value.Pickup.Count > 0)
						{
							task[2] = deck.Value.Pickup[0].ToString();
						}
						if (deck.Value.Pickup.Count > 1)
						{
							task[3] = deck.Value.Pickup[1].ToString();
						}
						if (deck.Value.Pickup.Count > 2)
						{
							task[4] = deck.Value.Pickup[2].ToString();
						}
						tasks.Add(task);
					}
				}
				this.superScrollView.Print(tasks);
				Program.instance.deckSelector.lastSelectedDeckItem = this.superScrollView.items[0].gameObject.GetComponent<SelectionToggle_Deck>();
				if (Cursor.lockState == CursorLockMode.Locked)
				{
					Program.instance.deckSelector.Select(false);
				}
				this.UpdateDeckNum();
			};
		}

		// Token: 0x060097AC RID: 38828 RVA: 0x00163124 File Offset: 0x00161324
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_Deck component = item.GetComponent<SelectionToggle_Deck>();
			component.deckName = task[0];
			component.deckCase = int.Parse(task[1]);
			component.card0 = int.Parse(task[2]);
			component.card1 = int.Parse(task[3]);
			component.card2 = int.Parse(task[4]);
			component.protector = task[5];
			component.isOn = task[6] != "0";
			component.Refresh();
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x060097AD RID: 38829 RVA: 0x00163199 File Offset: 0x00161399
		// (set) Token: 0x060097AE RID: 38830 RVA: 0x001631A1 File Offset: 0x001613A1
		public bool PickupShowing
		{
			get
			{
				return this.m_pickupShowing;
			}
			set
			{
				this.m_pickupShowing = value;
				this.DeckHover();
			}
		}

		// Token: 0x060097AF RID: 38831 RVA: 0x001631B0 File Offset: 0x001613B0
		public void DeckHover()
		{
			if (this.superScrollView == null)
			{
				return;
			}
			foreach (SuperScrollView.Item item in this.superScrollView.items)
			{
				if (!(item.gameObject == null))
				{
					SelectionToggle_Deck handler = item.gameObject.GetComponent<SelectionToggle_Deck>();
					if (this.PickupShowing)
					{
						handler.ShowPickup(true);
					}
					else
					{
						handler.HidePickup(true);
					}
				}
			}
		}

		// Token: 0x060097B0 RID: 38832 RVA: 0x0016323C File Offset: 0x0016143C
		public void DeckCreate()
		{
			this.SwitchToDefaultLayout();
			UIManager.ShowPopupInput(new List<string>
			{
				InterString.Get("请输入卡组名。@n创建卡组时会自动导入剪切板中的卡组码。", 0),
				string.Empty
			}, new Action<string>(this.DeckCheck), null, TmpInputValidation.ValidationType.Path);
		}

		// Token: 0x060097B1 RID: 38833 RVA: 0x00163278 File Offset: 0x00161478
		private void DeckCheck(string deckName)
		{
			if (File.Exists(this.GetDeckPath(deckName)))
			{
				DeckSelectorUI.deckInUse = deckName;
				List<string> tasks = new List<string>
				{
					InterString.Get("该卡组名已存在", 0),
					InterString.Get("该卡组名的文件已存在，是否直接覆盖创建？", 0),
					InterString.Get("覆盖", 0),
					InterString.Get("取消", 0)
				};
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, 0.6f).OnComplete(delegate
				{
					UIManager.ShowPopupYesOrNo(tasks, new Action(this.DeckFileCreateWithName), null);
				});
				return;
			}
			this.DeckFileCreate(deckName);
		}

		// Token: 0x060097B2 RID: 38834 RVA: 0x00163345 File Offset: 0x00161545
		private void DeckFileCreateWithName()
		{
			this.DeckFileCreate(DeckSelectorUI.deckInUse);
		}

		// Token: 0x060097B3 RID: 38835 RVA: 0x00163354 File Offset: 0x00161554
		private void DeckFileCreate(string deckName)
		{
			try
			{
				string path = this.GetDeckPath(deckName);
				File.Create(path).Close();
				string clipBoard = GUIUtility.systemCopyBuffer;
				if (clipBoard.Contains("#main"))
				{
					File.WriteAllText(path, clipBoard, Encoding.UTF8);
				}
				else if (clipBoard.Contains("ygotype=deck&v=1&d="))
				{
					Deck deck = DeckShareURL.UriToDeck(new Uri(clipBoard));
					deck.type = this.deckType;
					deck.Save(deckName, DateTime.UtcNow, true, true);
				}
				else if (clipBoard.Contains(YdkeConverter.ydkeHeader))
				{
					Deck deck2 = YdkeConverter.Ydke2Deck(clipBoard);
					deck2.type = this.deckType;
					deck2.Save(deckName, DateTime.UtcNow, true, true);
				}
				Config.SetConfigDeck(deckName, true);
				this.RefreshList();
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				MessageManager.Cast(InterString.Get("创建卡组失败！请检查文件夹权限。", 0));
			}
		}

		// Token: 0x060097B4 RID: 38836 RVA: 0x0016342C File Offset: 0x0016162C
		private void DeleteOnlineDecks(List<string> ids)
		{
			if (MyCard.account == null)
			{
				return;
			}
			OnlineDeck.DeleteDecks(ids);
		}

		// Token: 0x060097B5 RID: 38837 RVA: 0x0016343D File Offset: 0x0016163D
		public void OnOnlineDeckView()
		{
			Program.instance.ShiftToServant(Program.instance.onlineDeckViewer);
		}

		// Token: 0x060097B6 RID: 38838 RVA: 0x00163453 File Offset: 0x00161653
		public void OnShowPickup()
		{
			this.PickupShowing = true;
		}

		// Token: 0x060097B7 RID: 38839 RVA: 0x0016345C File Offset: 0x0016165C
		public void OnHidePickup()
		{
			this.PickupShowing = false;
		}

		// Token: 0x060097B8 RID: 38840 RVA: 0x00163468 File Offset: 0x00161668
		public void OnType()
		{
			string[] folders = this.GetAllDeckTypes(null);
			List<string> list = new List<string>();
			list.Add(InterString.Get("卡组分组", 0));
			list.Add(InterString.Get("彩色选项为功能选项，白色选项为卡组分组。", 0));
			list.Add("g:" + InterString.Get("新建分组", 0));
			list.Add("r:" + InterString.Get("删除分组", 0));
			list.Add("b:" + InterString.Get("重命名分组", 0));
			list.Add("b:" + InterString.Get("移动到分组", 0));
			list.Add("b:" + InterString.Get("复制到分组", 0));
			list.Add(InterString.Get("默认分组", 0));
			list.AddRange(folders);
			UIManager.ShowPopupSelection(list, new Action(this.OnTypeChange), null);
		}

		// Token: 0x060097B9 RID: 38841 RVA: 0x00163558 File Offset: 0x00161758
		private void OnTypeChange()
		{
			SelectionButton component = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>();
			Color color = component.GetButtonTextColor();
			string selected = component.GetButtonText();
			if (color != Color.white)
			{
				if (selected == InterString.Get("新建分组", 0))
				{
					this.AddDeckType();
				}
				if (selected == InterString.Get("删除分组", 0))
				{
					this.RemoveDeckType();
				}
				if (selected == InterString.Get("重命名分组", 0))
				{
					this.RenameDeckType();
				}
				if (selected == InterString.Get("移动到分组", 0))
				{
					this.MoveToType();
				}
				if (selected == InterString.Get("复制到分组", 0))
				{
					this.CopyToType();
				}
				return;
			}
			string type = selected;
			if (selected == InterString.Get("默认分组", 0))
			{
				type = string.Empty;
			}
			this.deckType = type;
			this.RefreshList();
		}

		// Token: 0x060097BA RID: 38842 RVA: 0x00163634 File Offset: 0x00161834
		private void AddDeckType()
		{
			UIManager.ShowPopupInput(new List<string>
			{
				InterString.Get("添加新卡组分组", 0),
				string.Empty
			}, new Action<string>(this.AddDeckType), null, TmpInputValidation.ValidationType.Path);
		}

		// Token: 0x060097BB RID: 38843 RVA: 0x0016366C File Offset: 0x0016186C
		private void AddDeckType(string type)
		{
			if (type == string.Empty)
			{
				return;
			}
			string path = "Deck/" + type + "/";
			if (Directory.Exists(path))
			{
				MessageManager.Cast(InterString.Get("该分组已存在！", 0));
				return;
			}
			Directory.CreateDirectory(path);
			this.deckType = type;
			this.RefreshList();
		}

		// Token: 0x060097BC RID: 38844 RVA: 0x001636C8 File Offset: 0x001618C8
		private void RemoveDeckType()
		{
			string[] folders = this.GetAllDeckTypes(null);
			List<string> list = new List<string>();
			list.Add(InterString.Get("删除分组", 0));
			list.Add(string.Empty);
			list.AddRange(folders);
			UIManager.ShowPopupSelection(list, new Action(this.DeleteDeckType), null);
		}

		// Token: 0x060097BD RID: 38845 RVA: 0x00163718 File Offset: 0x00161918
		private void DeleteDeckType()
		{
			string type = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			string path = "Deck/" + type + "/";
			if (!Directory.Exists(path))
			{
				return;
			}
			string[] files = Directory.GetFiles(path, "*.ydk");
			if (files.Length != 0)
			{
				UIManager.ShowPopupYesOrNo(new List<string>
				{
					InterString.Get("删除分组", 0),
					InterString.Get("该卡组分类包含[?]个卡组，是否确认删除？", files.Length.ToString(), 0),
					InterString.Get("确认", 0),
					InterString.Get("取消", 0)
				}, delegate
				{
					this.DeleteDeckType(type);
				}, null);
				return;
			}
			this.DeleteDeckType(type);
		}

		// Token: 0x060097BE RID: 38846 RVA: 0x001637F0 File Offset: 0x001619F0
		private void DeleteDeckType(string type)
		{
			string path = "Deck/" + type + "/";
			this.DeleteOnlineDecks(this.GetAllDeckIds(path));
			string[] files = Directory.GetFiles(path, "*.ydk");
			for (int i = 0; i < files.Length; i++)
			{
				File.Delete(files[i]);
			}
			Directory.Delete(path);
			if (this.deckType == type)
			{
				this.deckType = string.Empty;
			}
			this.RefreshList();
		}

		// Token: 0x060097BF RID: 38847 RVA: 0x00163864 File Offset: 0x00161A64
		private List<string> GetAllDeckIds(string dir)
		{
			List<string> value = new List<string>();
			string[] files = Directory.GetFiles(dir, "*.ydk");
			for (int i = 0; i < files.Length; i++)
			{
				Deck deck = new Deck(files[i]);
				value.Add(deck.deckId);
				Debug.Log("--: " + deck.deckId);
			}
			return value;
		}

		// Token: 0x060097C0 RID: 38848 RVA: 0x001638BC File Offset: 0x00161ABC
		private void RenameDeckType()
		{
			string[] folders = this.GetAllDeckTypes(null);
			List<string> list = new List<string>();
			list.Add(InterString.Get("重命名分组", 0));
			list.Add(string.Empty);
			list.AddRange(folders);
			UIManager.ShowPopupSelection(list, new Action(this.RenameType), null);
		}

		// Token: 0x060097C1 RID: 38849 RVA: 0x0016390C File Offset: 0x00161B0C
		private void RenameType()
		{
			this.selectedType = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			UIManager.ShowPopupInput(new List<string>
			{
				InterString.Get("重命名分组", 0),
				this.selectedType
			}, new Action<string>(this.CheckRenamedDeckType), null, TmpInputValidation.ValidationType.Path);
		}

		// Token: 0x060097C2 RID: 38850 RVA: 0x00163968 File Offset: 0x00161B68
		private void CheckRenamedDeckType(string newType)
		{
			if (newType == this.selectedType)
			{
				return;
			}
			if (this.GetAllDeckTypes(null).Any((string type) => type != this.selectedType && type == newType))
			{
				MessageManager.Cast(InterString.Get("该分组已存在！", 0));
				return;
			}
			string text = "Deck/" + this.selectedType;
			string newPath = "Deck/" + newType;
			Directory.Move(text, newPath);
			foreach (string file in Directory.GetFiles(newPath, "*.ydk"))
			{
				new Deck(file)
				{
					type = newType
				}.Save(Path.GetFileNameWithoutExtension(file), DateTime.UtcNow, true, false);
			}
			this.deckType = newType;
			this.RefreshList();
		}

		// Token: 0x060097C3 RID: 38851 RVA: 0x00163A48 File Offset: 0x00161C48
		private void MoveToType()
		{
			string[] folders = this.GetAllDeckTypes(this.deckType);
			List<string> types = new List<string>
			{
				InterString.Get("移动到分组", 0),
				string.Empty
			};
			if (this.deckType != string.Empty)
			{
				types.Add(InterString.Get("默认分组", 0));
			}
			types.AddRange(folders);
			UIManager.ShowPopupSelection(types, new Action(this.OnMoveToType), null);
		}

		// Token: 0x060097C4 RID: 38852 RVA: 0x00163AC4 File Offset: 0x00161CC4
		private void CopyToType()
		{
			string[] folders = this.GetAllDeckTypes(this.deckType);
			List<string> types = new List<string>
			{
				InterString.Get("复制到分组", 0),
				string.Empty
			};
			if (this.deckType != string.Empty)
			{
				types.Add(InterString.Get("默认分组", 0));
			}
			types.AddRange(folders);
			UIManager.ShowPopupSelection(types, new Action(this.OnCopyToType), null);
		}

		// Token: 0x060097C5 RID: 38853 RVA: 0x00163B3D File Offset: 0x00161D3D
		private string GetCurrentTypePath()
		{
			if (this.deckType == string.Empty)
			{
				return "Deck/";
			}
			return "Deck/" + this.deckType + "/";
		}

		// Token: 0x060097C6 RID: 38854 RVA: 0x00163B6C File Offset: 0x00161D6C
		private string GetTypeName(string type)
		{
			if (type == string.Empty)
			{
				return InterString.Get("默认分组", 0);
			}
			return type;
		}

		// Token: 0x060097C7 RID: 38855 RVA: 0x00163B88 File Offset: 0x00161D88
		private string GetTypeName()
		{
			return this.GetTypeName(this.deckType);
		}

		// Token: 0x060097C8 RID: 38856 RVA: 0x00163B96 File Offset: 0x00161D96
		private string GetTypePath(string type)
		{
			if (type == string.Empty)
			{
				return "Deck/";
			}
			return "Deck/" + type + "/";
		}

		// Token: 0x060097C9 RID: 38857 RVA: 0x00163BBB File Offset: 0x00161DBB
		private string GetDeckPath(string deckName, string type)
		{
			return this.GetTypePath(type) + deckName + ".ydk";
		}

		// Token: 0x060097CA RID: 38858 RVA: 0x00163BCF File Offset: 0x00161DCF
		private string GetDeckPath(string deckName)
		{
			return this.GetDeckPath(deckName, this.deckType);
		}

		// Token: 0x060097CB RID: 38859 RVA: 0x00163BE0 File Offset: 0x00161DE0
		private string[] GetAllDeckTypes(string excludeType = null)
		{
			string[] types = (from f in Directory.GetDirectories("Deck/")
				select Path.GetFileName(f)).ToArray<string>();
			if (excludeType != null)
			{
				types = types.Where((string t) => t != excludeType).ToArray<string>();
			}
			return types;
		}

		// Token: 0x060097CC RID: 38860 RVA: 0x00163C4F File Offset: 0x00161E4F
		public void OnDelete()
		{
			if (this.buttonLayoutSwitching)
			{
				return;
			}
			this.SwitchButtonLayouts(DeckSelectorUI.LayoutType.Delete);
		}

		// Token: 0x060097CD RID: 38861 RVA: 0x00163C64 File Offset: 0x00161E64
		private void OnDeleteConfirm()
		{
			if (this.buttonLayoutSwitching)
			{
				return;
			}
			List<int> deleteIndexs = new List<int>();
			List<string> deleteIds = new List<string>();
			for (int i = 0; i < this.superScrollView.items.Count; i++)
			{
				if (this.superScrollView.items[i].args[6] != "0")
				{
					File.Delete(this.GetDeckPath(this.superScrollView.items[i].args[0]));
					deleteIndexs.Add(i);
					deleteIds.Add(this.superScrollView.items[i].args[7]);
				}
			}
			int lastSelect = Program.instance.deckSelector.lastSelectedDeckItem.index;
			int removedCount = 0;
			for (int j = 0; j < deleteIndexs.Count; j++)
			{
				this.superScrollView.RemoveAt(deleteIndexs[j] - removedCount);
				removedCount++;
			}
			Program.instance.deckSelector.lastSelectedDeckItem = (SelectionToggle_Deck)this.superScrollView.GetItemByIndex(lastSelect);
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				Program.instance.deckSelector.Select(false);
			}
			this.DeleteOnlineDecks(deleteIds);
			this.SwitchToDefaultLayout();
			this.UpdateDeckNum();
		}

		// Token: 0x060097CE RID: 38862 RVA: 0x00163DA8 File Offset: 0x00161FA8
		private void OnMoveToType()
		{
			if (this.buttonLayoutSwitching)
			{
				return;
			}
			this.selectedType = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			if (this.selectedType == InterString.Get("默认分组", 0))
			{
				this.selectedType = string.Empty;
			}
			this.SwitchButtonLayouts(DeckSelectorUI.LayoutType.MoveToType);
		}

		// Token: 0x060097CF RID: 38863 RVA: 0x00163E04 File Offset: 0x00162004
		private void OnMoveToTypeConfirm()
		{
			if (this.buttonLayoutSwitching)
			{
				return;
			}
			List<int> moveIndexs = new List<int>();
			for (int i = 0; i < this.superScrollView.items.Count; i++)
			{
				if (this.superScrollView.items[i].args[6] != "0")
				{
					string filePath = this.GetDeckPath(this.superScrollView.items[i].args[0]);
					if (File.Exists(this.GetDeckPath(this.superScrollView.items[i].args[0], this.selectedType)))
					{
						MessageManager.Cast(InterString.Get("操作失败，目标分组已存在同名卡组：[[?]]。", this.superScrollView.items[i].args[0], 0));
					}
					else
					{
						new Deck(filePath)
						{
							type = this.selectedType
						}.Save(this.superScrollView.items[i].args[0], DateTime.UtcNow, true, false);
						File.Delete(filePath);
						moveIndexs.Add(i);
					}
				}
			}
			int lastSelect = Program.instance.deckSelector.lastSelectedDeckItem.index;
			int removedCount = 0;
			for (int j = 0; j < moveIndexs.Count; j++)
			{
				this.superScrollView.RemoveAt(moveIndexs[j] - removedCount);
				removedCount++;
			}
			Program.instance.deckSelector.lastSelectedDeckItem = (SelectionToggle_Deck)this.superScrollView.GetItemByIndex(lastSelect);
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				Program.instance.deckSelector.Select(false);
			}
			this.SwitchToDefaultLayout();
			this.UpdateDeckNum();
		}

		// Token: 0x060097D0 RID: 38864 RVA: 0x00163FAC File Offset: 0x001621AC
		private void OnCopyToType()
		{
			if (this.buttonLayoutSwitching)
			{
				return;
			}
			this.selectedType = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			if (this.selectedType == InterString.Get("默认分组", 0))
			{
				this.selectedType = string.Empty;
			}
			this.SwitchButtonLayouts(DeckSelectorUI.LayoutType.CopyToType);
		}

		// Token: 0x060097D1 RID: 38865 RVA: 0x00164008 File Offset: 0x00162208
		private void OnCopyToTypeConfirm()
		{
			if (this.buttonLayoutSwitching)
			{
				return;
			}
			for (int i = 0; i < this.superScrollView.items.Count; i++)
			{
				if (this.superScrollView.items[i].args[6] != "0")
				{
					string filePath = this.GetDeckPath(this.superScrollView.items[i].args[0]);
					if (File.Exists(this.GetDeckPath(this.superScrollView.items[i].args[0], this.selectedType)))
					{
						MessageManager.Cast(InterString.Get("操作失败，目标分组已存在同名卡组：[[?]]。", this.superScrollView.items[i].args[0], 0));
					}
					else
					{
						new Deck(filePath)
						{
							deckId = string.Empty,
							type = this.selectedType
						}.Save(this.superScrollView.items[i].args[0], DateTime.UtcNow, true, false);
					}
				}
			}
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				Program.instance.deckSelector.Select(false);
			}
			this.SwitchToDefaultLayout();
		}

		// Token: 0x060097D2 RID: 38866 RVA: 0x0016413C File Offset: 0x0016233C
		private void ShowDefaultButtons()
		{
			this.ButtonDelete.gameObject.SetActive(true);
			this.ButtonCancel.gameObject.SetActive(false);
			this.ButtonOnline.gameObject.SetActive(true);
			this.ButtonConfirm.gameObject.SetActive(false);
			this.Input.gameObject.SetActive(true);
			this.ButtonType.gameObject.SetActive(true);
		}

		// Token: 0x060097D3 RID: 38867 RVA: 0x001641B0 File Offset: 0x001623B0
		private void ShowDeleteButtons()
		{
			this.ButtonDelete.gameObject.SetActive(false);
			this.ButtonCancel.gameObject.SetActive(true);
			this.ButtonOnline.gameObject.SetActive(false);
			this.ButtonConfirm.gameObject.SetActive(true);
			this.Input.gameObject.SetActive(false);
			this.ButtonType.gameObject.SetActive(false);
			this.ButtonCancel.ShowIcon(true);
			this.ButtonConfirm.SetButtonText(InterString.Get("确认删除", 0));
		}

		// Token: 0x060097D4 RID: 38868 RVA: 0x00164248 File Offset: 0x00162448
		private void ShowMoveOrCopyButtons()
		{
			this.ButtonDelete.gameObject.SetActive(false);
			this.ButtonCancel.gameObject.SetActive(true);
			this.ButtonOnline.gameObject.SetActive(false);
			this.ButtonConfirm.gameObject.SetActive(true);
			this.Input.gameObject.SetActive(false);
			this.ButtonType.gameObject.SetActive(false);
			this.ButtonCancel.ShowIcon(false);
			this.ButtonConfirm.SetButtonText(InterString.Get("确认", 0));
		}

		// Token: 0x060097D5 RID: 38869 RVA: 0x001642E0 File Offset: 0x001624E0
		private void UpdateDeckNum()
		{
			this.TextDeckNumValue.text = this.decks.Count.ToString();
		}

		// Token: 0x060097D6 RID: 38870 RVA: 0x0016430B File Offset: 0x0016250B
		private void SwitchToDefaultLayout()
		{
			this.HideAllToggles();
			this.SwitchButtonLayouts(DeckSelectorUI.LayoutType.Default);
		}

		// Token: 0x060097D7 RID: 38871 RVA: 0x0016431C File Offset: 0x0016251C
		private void SwitchButtonLayouts(DeckSelectorUI.LayoutType layoutType)
		{
			if (this.layoutType == layoutType)
			{
				return;
			}
			this.buttonLayoutSwitching = true;
			this.layoutType = layoutType;
			RectTransform header = base.Manager.GetElement<RectTransform>("Header");
			RectTransform footer = base.Manager.GetElement<RectTransform>("Footer");
			UIManager.HideExitButton(0.2f, Ease.Linear);
			DOTween.Sequence().Append(header.DOAnchorPosY(PropertyOverrider.NeedMobileLayout() ? 130f : 120f, 0.2f, false).OnComplete(delegate
			{
				if (layoutType == DeckSelectorUI.LayoutType.Default)
				{
					this.ShowDefaultButtons();
				}
				else if (layoutType == DeckSelectorUI.LayoutType.Delete)
				{
					this.ShowDeleteButtons();
				}
				else
				{
					this.ShowMoveOrCopyButtons();
				}
				UIManager.ShowExitButton(0.3f, Ease.OutQuart);
				if (layoutType != DeckSelectorUI.LayoutType.Default)
				{
					foreach (SuperScrollView.Item item in this.superScrollView.items)
					{
						if (!(item.gameObject == null))
						{
							item.gameObject.GetComponent<SelectionToggle_Deck>().ShowToggle();
						}
					}
				}
				TextMeshProUGUI title = this.Title;
				string text;
				switch (layoutType)
				{
				case DeckSelectorUI.LayoutType.Delete:
					text = InterString.Get("删除卡组", 0);
					break;
				case DeckSelectorUI.LayoutType.MoveToType:
					text = InterString.Get("移动到分组", 0) + " [" + this.GetTypeName(this.selectedType) + "]";
					break;
				case DeckSelectorUI.LayoutType.CopyToType:
					text = InterString.Get("复制到分组", 0) + " [" + this.GetTypeName(this.selectedType) + "]";
					break;
				default:
					text = InterString.Get("编辑卡组", 0);
					break;
				}
				title.text = text;
			})).Join(footer.DOAnchorPosY(PropertyOverrider.NeedMobileLayout() ? (-186f) : (-140f), 0.2f, false))
				.Append(header.DOAnchorPosY(0f, 0.3f, false).SetEase(Ease.OutQuart))
				.Join(footer.DOAnchorPosY(0f, 0.3f, false).SetEase(Ease.OutQuart))
				.OnComplete(delegate
				{
					this.buttonLayoutSwitching = false;
				});
		}

		// Token: 0x060097D8 RID: 38872 RVA: 0x00164438 File Offset: 0x00162638
		private void HideAllToggles()
		{
			if (this.superScrollView == null || this.superScrollView.items == null)
			{
				return;
			}
			foreach (SuperScrollView.Item item2 in this.superScrollView.items)
			{
				item2.args[6] = "0";
			}
			foreach (SuperScrollView.Item item in this.superScrollView.items)
			{
				if (!(item.gameObject == null))
				{
					item.gameObject.GetComponent<SelectionToggle_Deck>().HideToggle();
				}
			}
		}

		// Token: 0x060097D9 RID: 38873 RVA: 0x00164508 File Offset: 0x00162708
		public void OnConfirm()
		{
			if (this.layoutType == DeckSelectorUI.LayoutType.Delete)
			{
				this.OnDeleteConfirm();
				return;
			}
			if (this.layoutType == DeckSelectorUI.LayoutType.MoveToType)
			{
				this.OnMoveToTypeConfirm();
				return;
			}
			if (this.layoutType == DeckSelectorUI.LayoutType.CopyToType)
			{
				this.OnCopyToTypeConfirm();
			}
		}

		// Token: 0x060097DA RID: 38874 RVA: 0x00164539 File Offset: 0x00162739
		public void OnCancel()
		{
			if (this.buttonLayoutSwitching)
			{
				return;
			}
			this.SwitchToDefaultLayout();
		}

		// Token: 0x0400D5E2 RID: 54754
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D5E3 RID: 54755
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D5E4 RID: 54756
		private const string LABEL_IPT = "InputField";

		// Token: 0x0400D5E5 RID: 54757
		private TMP_InputField m_Input;

		// Token: 0x0400D5E6 RID: 54758
		private const string LABEL_TXT_DECKNUMVALUE = "TextDeckNumValue";

		// Token: 0x0400D5E7 RID: 54759
		private TextMeshProUGUI m_TextDeckNumValue;

		// Token: 0x0400D5E8 RID: 54760
		private const string LABEL_STG_PICKUPCARD = "TogglePickupCard";

		// Token: 0x0400D5E9 RID: 54761
		private SelectionToggle m_TogglePickupCard;

		// Token: 0x0400D5EA RID: 54762
		private const string LABEL_SBN_ONLINE = "ButtonOnline";

		// Token: 0x0400D5EB RID: 54763
		private SelectionButton m_ButtonOnline;

		// Token: 0x0400D5EC RID: 54764
		private const string LABEL_RT_HEADER = "Header";

		// Token: 0x0400D5ED RID: 54765
		private RectTransform m_Header;

		// Token: 0x0400D5EE RID: 54766
		private const string LABEL_RT_FOOTER = "Footer";

		// Token: 0x0400D5EF RID: 54767
		private RectTransform m_Footer;

		// Token: 0x0400D5F0 RID: 54768
		private const string LABEL_SBN_TYPE = "ButtonType";

		// Token: 0x0400D5F1 RID: 54769
		private SelectionButton m_ButtonType;

		// Token: 0x0400D5F2 RID: 54770
		private const string LABEL_SBN_DELETE = "ButtonDelete";

		// Token: 0x0400D5F3 RID: 54771
		private SelectionButton m_ButtonDelete;

		// Token: 0x0400D5F4 RID: 54772
		private const string LABEL_SBN_CANCEL = "ButtonCancel";

		// Token: 0x0400D5F5 RID: 54773
		private SelectionButton m_ButtonCancel;

		// Token: 0x0400D5F6 RID: 54774
		private const string LABEL_SBN_CONFIRM = "ButtonConfirm";

		// Token: 0x0400D5F7 RID: 54775
		private SelectionButton m_ButtonConfirm;

		// Token: 0x0400D5F8 RID: 54776
		public SuperScrollView superScrollView;

		// Token: 0x0400D5F9 RID: 54777
		public string deckType = string.Empty;

		// Token: 0x0400D5FA RID: 54778
		private string selectedType;

		// Token: 0x0400D5FB RID: 54779
		public Dictionary<string, Deck> decks = new Dictionary<string, Deck>();

		// Token: 0x0400D5FC RID: 54780
		public bool buttonLayoutSwitching;

		// Token: 0x0400D5FD RID: 54781
		public static string deckInUse;

		// Token: 0x0400D5FE RID: 54782
		private bool m_pickupShowing;

		// Token: 0x0400D5FF RID: 54783
		private DeckSelectorUI.LayoutType layoutType;

		// Token: 0x02001465 RID: 5221
		private enum LayoutType
		{
			// Token: 0x0400D601 RID: 54785
			Default,
			// Token: 0x0400D602 RID: 54786
			Delete,
			// Token: 0x0400D603 RID: 54787
			MoveToType,
			// Token: 0x0400D604 RID: 54788
			CopyToType
		}
	}
}
