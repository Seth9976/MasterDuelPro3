using System;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013BC RID: 5052
	public class SelectionToggle_CharacterItem : SelectionToggle
	{
		// Token: 0x06009267 RID: 37479 RVA: 0x00146802 File Offset: 0x00144A02
		protected override void Awake()
		{
			base.Awake();
			this.HoverOff(false);
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.manuallySetNavigation = false;
		}

		// Token: 0x06009268 RID: 37480 RVA: 0x00147B7C File Offset: 0x00145D7C
		public void Refresh()
		{
			if (CharacterSelector.characters == null)
			{
				return;
			}
			this.charaName = CharacterSelector.characters.GetName(this.characterID);
			this.charaProfile = CharacterSelector.characters.GetProfile(this.characterID);
			this.charaProfile = Cid2Ydk.ReplaceWithCardName(this.charaProfile);
			Addressables.LoadAssetAsync<Sprite>("sn" + this.characterID).Completed += delegate(AsyncOperationHandle<Sprite> result)
			{
				Image element = base.Manager.GetElement<Image>("Out");
				element.sprite = result.Result;
				element.color = Color.white;
			};
		}

		// Token: 0x06009269 RID: 37481 RVA: 0x00147BFD File Offset: 0x00145DFD
		protected override void CallHoverOnEvent()
		{
			base.CallHoverOnEvent();
			Program.instance.character.GetUI<CharacterSelectorUI>().SetHoverText(this.charaName);
		}

		// Token: 0x0600926A RID: 37482 RVA: 0x00147C20 File Offset: 0x00145E20
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			this.CallHoverOnEvent();
			Program.instance.character.GetUI<CharacterSelectorUI>().TextDetailName.text = this.charaName;
			Program.instance.character.GetUI<CharacterSelectorUI>().TextDetailDescription.text = this.charaProfile;
			Config.Set(CharacterSelector.condition.ToString() + "Character" + CharacterSelectorUI.player, this.characterID);
			Program.instance.ocgcore.CheckCharaFace();
			Program.instance.character.lastSelectedCharacter = this;
			Program.instance.currentServant.lastSelectable = base.Selectable;
			base.GetSelectable().Select();
			Image detailImage = Program.instance.character.GetUI<CharacterSelectorUI>().ImageDetail;
			detailImage.color = Color.clear;
			Addressables.LoadAssetAsync<Sprite>("sn" + this.characterID + "_2").Completed += delegate(AsyncOperationHandle<Sprite> result)
			{
				if (result.Result == null)
				{
					return;
				}
				detailImage.color = Color.white;
				detailImage.sprite = result.Result;
			};
		}

		// Token: 0x0600926B RID: 37483 RVA: 0x00146C7B File Offset: 0x00144E7B
		protected override void OnClick()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			this.SetToggleOn(true);
			Program.instance.currentServant.lastSelectable = base.Selectable;
		}

		// Token: 0x0600926C RID: 37484 RVA: 0x00147D3E File Offset: 0x00145F3E
		protected override int GetButtonsCount()
		{
			return Program.instance.character.GetUI<CharacterSelectorUI>().GetCurrentSerialCount();
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x0600926D RID: 37485 RVA: 0x00147D54 File Offset: 0x00145F54
		private static GridLayoutGroup Grid
		{
			get
			{
				return SelectionToggle_CharacterItem.m_grid = ((SelectionToggle_CharacterItem.m_grid != null) ? SelectionToggle_CharacterItem.m_grid : Program.instance.character.GetUI<CharacterSelectorUI>().ScrollRect.GetComponent<GridLayoutGroup>());
			}
		}

		// Token: 0x0600926E RID: 37486 RVA: 0x00147D8C File Offset: 0x00145F8C
		protected override int GetColumnsCount()
		{
			return SelectionToggle_CharacterItem.Grid.Size().x;
		}

		// Token: 0x0400D0DF RID: 53471
		public string characterID;

		// Token: 0x0400D0E0 RID: 53472
		private string charaName;

		// Token: 0x0400D0E1 RID: 53473
		private string charaProfile;

		// Token: 0x0400D0E2 RID: 53474
		private static GridLayoutGroup m_grid;
	}
}
