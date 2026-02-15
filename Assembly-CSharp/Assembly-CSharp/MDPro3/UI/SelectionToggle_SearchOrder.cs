using System;
using MDPro3.UI.Popup;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013D3 RID: 5075
	public class SelectionToggle_SearchOrder : SelectionToggle
	{
		// Token: 0x0600930B RID: 37643 RVA: 0x00149E76 File Offset: 0x00148076
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
		}

		// Token: 0x0600930C RID: 37644 RVA: 0x0014B1A9 File Offset: 0x001493A9
		private void Start()
		{
			if (this.sortOrder == CardCollectionView._SortOrder)
			{
				this.SetToggleOn(true);
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
		}

		// Token: 0x0600930D RID: 37645 RVA: 0x0014B1CF File Offset: 0x001493CF
		protected override void CallHoverOnEvent()
		{
			base.CallHoverOnEvent();
			((PopupSearchOrder)Program.instance.ui_.currentPopupB).lastSelectedToggle = this;
		}

		// Token: 0x0600930E RID: 37646 RVA: 0x0014B1F4 File Offset: 0x001493F4
		protected override void CallSubmitEvent()
		{
			CardCollectionView._SortOrder = this.sortOrder;
			Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.SetSortIcon(this.GetIconSprite());
			Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.SetSortText(this.GetSortText());
			Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.PrintSearchCards("");
			Program.instance.ui_.currentPopupB.Hide();
		}

		// Token: 0x0600930F RID: 37647 RVA: 0x0014B27C File Offset: 0x0014947C
		protected string GetSortText()
		{
			string text;
			switch (this.sortOrder)
			{
			case CardCollectionView.SortOrder.ByType:
				text = InterString.Get("种类", 0);
				break;
			case CardCollectionView.SortOrder.ByTypeReverse:
				text = InterString.Get("种类", 0);
				break;
			case CardCollectionView.SortOrder.ByLevelUp:
				text = InterString.Get("等级·阶级·连接", 0);
				break;
			case CardCollectionView.SortOrder.ByLevelDown:
				text = InterString.Get("等级·阶级·连接", 0);
				break;
			case CardCollectionView.SortOrder.ByAttackUp:
				text = InterString.Get("攻击力", 0);
				break;
			case CardCollectionView.SortOrder.ByAttackDown:
				text = InterString.Get("攻击力", 0);
				break;
			case CardCollectionView.SortOrder.ByDefenceUp:
				text = InterString.Get("守备力", 0);
				break;
			case CardCollectionView.SortOrder.ByDefenceDown:
				text = InterString.Get("守备力", 0);
				break;
			case CardCollectionView.SortOrder.ByRarityUp:
				text = InterString.Get("稀有度", 0);
				break;
			case CardCollectionView.SortOrder.ByRarityDown:
				text = InterString.Get("稀有度", 0);
				break;
			case CardCollectionView.SortOrder.ByGPUp:
				text = InterString.Get("Genesys分数", 0);
				break;
			case CardCollectionView.SortOrder.ByGPDown:
				text = InterString.Get("Genesys分数", 0);
				break;
			default:
				text = string.Empty;
				break;
			}
			return text;
		}

		// Token: 0x06009310 RID: 37648 RVA: 0x0014B385 File Offset: 0x00149585
		protected override void OnClick()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			this.SetToggleOn(true);
			this.CallSubmitEvent();
		}

		// Token: 0x06009311 RID: 37649 RVA: 0x0014B3A4 File Offset: 0x001495A4
		protected override void OnSubmit()
		{
			base.OnSubmit();
			this.OnClick();
		}

		// Token: 0x0400D169 RID: 53609
		[Header("SelectionToggle SearchOrder")]
		[SerializeField]
		private CardCollectionView.SortOrder sortOrder = CardCollectionView.SortOrder.ByType;
	}
}
