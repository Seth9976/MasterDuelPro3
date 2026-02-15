using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI.Popup
{
	// Token: 0x0200148C RID: 5260
	public class PopupSearchFilter : Popup
	{
		// Token: 0x06009A0B RID: 39435 RVA: 0x0016FBE8 File Offset: 0x0016DDE8
		private void Start()
		{
			List<long> f = CardCollectionView.filters;
			if (f.Count > 0)
			{
				foreach (SelectionToggle_SearchFilter toggle in base.transform.GetComponentsInChildren<SelectionToggle_SearchFilter>())
				{
					if (toggle.group == 0 && (toggle.filterCode & f[0]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 1 && (toggle.filterCode & f[1]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 2 && (toggle.filterCode & f[2]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 3 && (toggle.filterCode & f[3]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 4 && (toggle.filterCode & f[4]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 5 && (toggle.filterCode & f[5]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 6 && (toggle.filterCode & f[6]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 7 && (toggle.filterCode & f[7]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 8 && (toggle.filterCode & f[8]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 9 && (toggle.filterCode & f[9]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 10 && (toggle.filterCode & f[10]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
					else if (toggle.group == 11 && (toggle.filterCode & f[11]) > 0L)
					{
						toggle.SetToggleOn(true);
					}
				}
				if (f[12] > 0L)
				{
					this.inputLevelFrom.text = f[12].ToString();
				}
				if (f[13] > 0L)
				{
					this.inputLevelTo.text = f[13].ToString();
				}
				if (f[14] > 0L)
				{
					this.inputAttackFrom.text = f[14].ToString();
				}
				if (f[15] > 0L)
				{
					this.inputAttackTo.text = f[15].ToString();
				}
				if (f[16] > 0L)
				{
					this.inputDefenceFrom.text = f[16].ToString();
				}
				if (f[17] > 0L)
				{
					this.inputDefenceTo.text = f[17].ToString();
				}
				if (f[18] > 0L)
				{
					this.inputScaleFrom.text = f[18].ToString();
				}
				if (f[19] > 0L)
				{
					this.inputScaleTo.text = f[19].ToString();
				}
				if (f[20] >= 0L)
				{
					this.inputGPFrom.text = f[20].ToString();
				}
				if (f[21] >= 0L)
				{
					this.inputGPTo.text = f[21].ToString();
				}
				if (f[22] > 0L)
				{
					this.inputYearFrom.text = f[22].ToString();
				}
				if (f[23] > 0L)
				{
					this.inputYearTo.text = f[23].ToString();
				}
			}
			base.Manager.GetElement<SelectionButton>("ButtonPack").SetButtonText((CardCollectionView.packName == string.Empty) ? InterString.Get("所有卡包", 0) : CardCollectionView.packName);
		}

		// Token: 0x06009A0C RID: 39436 RVA: 0x0016F7F9 File Offset: 0x0016D9F9
		public override void Show()
		{
			base.Show();
			AudioManager.PlaySE("SE_SYS_VERIFY", 1f);
		}

		// Token: 0x06009A0D RID: 39437 RVA: 0x00170010 File Offset: 0x0016E210
		protected override void OnDecide()
		{
			this.Hide();
			long type = 0L;
			long attribute = 0L;
			long spellType = 0L;
			long race = 0L;
			long ability = 0L;
			long limit = 0L;
			long pool = 0L;
			long effect = 0L;
			long rarity = 0L;
			long cutin = 0L;
			long video = 0L;
			long link = 0L;
			bool dirty = false;
			foreach (SelectionToggle_SearchFilter toggle in base.transform.GetComponentsInChildren<SelectionToggle_SearchFilter>())
			{
				if (toggle.isOn)
				{
					dirty = true;
					if (toggle.group == 0)
					{
						type += toggle.filterCode;
					}
					else if (toggle.group == 1)
					{
						attribute += toggle.filterCode;
					}
					else if (toggle.group == 2)
					{
						spellType += toggle.filterCode;
					}
					else if (toggle.group == 3)
					{
						race += toggle.filterCode;
					}
					else if (toggle.group == 4)
					{
						ability += toggle.filterCode;
					}
					else if (toggle.group == 5)
					{
						limit += toggle.filterCode;
					}
					else if (toggle.group == 6)
					{
						pool += toggle.filterCode;
					}
					else if (toggle.group == 7)
					{
						effect += toggle.filterCode;
					}
					else if (toggle.group == 8)
					{
						rarity += toggle.filterCode;
					}
					else if (toggle.group == 9)
					{
						cutin += toggle.filterCode;
					}
					else if (toggle.group == 10)
					{
						video += toggle.filterCode;
					}
					else if (toggle.group == 11)
					{
						link += toggle.filterCode;
					}
				}
			}
			List<long> filters = new List<long>
			{
				type, attribute, spellType, race, ability, limit, pool, effect, rarity, cutin,
				video, link
			};
			if (this.inputLevelFrom.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputLevelFrom.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputLevelTo.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputLevelTo.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputAttackFrom.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputAttackFrom.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputAttackTo.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputAttackTo.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputDefenceFrom.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputDefenceFrom.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputDefenceTo.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputDefenceTo.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputScaleFrom.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputScaleFrom.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputScaleTo.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputScaleTo.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputGPFrom.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputGPFrom.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputGPTo.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputGPTo.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputYearFrom.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputYearFrom.text));
			}
			else
			{
				filters.Add(-233L);
			}
			if (this.inputYearTo.text.Length > 0)
			{
				dirty = true;
				filters.Add(long.Parse(this.inputYearTo.text));
			}
			else
			{
				filters.Add(-233L);
			}
			SelectionButton btnPack = base.Manager.GetElement<SelectionButton>("ButtonPack");
			if (btnPack.GetButtonText() != InterString.Get("所有卡包", 0))
			{
				dirty = true;
				CardCollectionView.packName = btnPack.GetButtonText();
			}
			if (dirty)
			{
				SelectionToggle_CardFilter.Instance.SetToggleOn(true);
				CardCollectionView.filters = filters;
			}
			else
			{
				SelectionToggle_CardFilter.Instance.SetToggleOff(true);
				CardCollectionView.filters.Clear();
			}
			Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.PrintSearchCards("");
		}

		// Token: 0x06009A0E RID: 39438 RVA: 0x00170570 File Offset: 0x0016E770
		public void OnReset()
		{
			SelectionToggle_SearchFilter[] componentsInChildren = base.transform.GetComponentsInChildren<SelectionToggle_SearchFilter>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetToggleOff(true);
			}
			this.inputLevelFrom.text = string.Empty;
			this.inputLevelTo.text = string.Empty;
			this.inputAttackFrom.text = string.Empty;
			this.inputAttackTo.text = string.Empty;
			this.inputDefenceFrom.text = string.Empty;
			this.inputDefenceTo.text = string.Empty;
			this.inputScaleFrom.text = string.Empty;
			this.inputScaleTo.text = string.Empty;
			this.inputGPFrom.text = string.Empty;
			this.inputGPTo.text = string.Empty;
			this.inputYearFrom.text = string.Empty;
			this.inputYearTo.text = string.Empty;
			base.Manager.GetElement<SelectionButton>("ButtonPack").SetButtonText(InterString.Get("所有卡包", 0));
		}

		// Token: 0x06009A0F RID: 39439 RVA: 0x00170680 File Offset: 0x0016E880
		public void OnPack()
		{
			List<string> selections = new List<string>
			{
				InterString.Get("卡包", 0),
				string.Empty
			};
			foreach (PacksManager.PackName pack in PacksManager.packs)
			{
				selections.Add(pack.fullName);
			}
			UIManager.ShowPopupSelection(selections, new Action(this.OnPackSelect), new Action(this.OnPackClose));
		}

		// Token: 0x06009A10 RID: 39440 RVA: 0x00170718 File Offset: 0x0016E918
		private void OnPackSelect()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			base.Manager.GetElement<SelectionButton>("ButtonPack").SetButtonText(selected);
		}

		// Token: 0x06009A11 RID: 39441 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPackClose()
		{
		}

		// Token: 0x06009A12 RID: 39442 RVA: 0x00170750 File Offset: 0x0016E950
		protected override void Update()
		{
			if (!this.NeedResponseInput())
			{
				return;
			}
			if ((UserInput.MouseRightDown || UserInput.WasCancelPressed) && this.cancelCallHide)
			{
				AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
				this.Hide();
			}
			if (UserInput.WasGamepadButtonWestPressed)
			{
				AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
				if (UserInput.WasLeftShoulderPressing)
				{
					this.OnReset();
					return;
				}
				this.OnDecide();
			}
		}

		// Token: 0x0400D7B1 RID: 55217
		[Header("Popup SearchFilter")]
		public SelectionToggle_SearchFilter lastSelectedToggle;

		// Token: 0x0400D7B2 RID: 55218
		public TMP_InputField inputLevelFrom;

		// Token: 0x0400D7B3 RID: 55219
		public TMP_InputField inputLevelTo;

		// Token: 0x0400D7B4 RID: 55220
		public TMP_InputField inputAttackFrom;

		// Token: 0x0400D7B5 RID: 55221
		public TMP_InputField inputAttackTo;

		// Token: 0x0400D7B6 RID: 55222
		public TMP_InputField inputDefenceFrom;

		// Token: 0x0400D7B7 RID: 55223
		public TMP_InputField inputDefenceTo;

		// Token: 0x0400D7B8 RID: 55224
		public TMP_InputField inputScaleFrom;

		// Token: 0x0400D7B9 RID: 55225
		public TMP_InputField inputScaleTo;

		// Token: 0x0400D7BA RID: 55226
		public TMP_InputField inputGPFrom;

		// Token: 0x0400D7BB RID: 55227
		public TMP_InputField inputGPTo;

		// Token: 0x0400D7BC RID: 55228
		public TMP_InputField inputYearFrom;

		// Token: 0x0400D7BD RID: 55229
		public TMP_InputField inputYearTo;
	}
}
