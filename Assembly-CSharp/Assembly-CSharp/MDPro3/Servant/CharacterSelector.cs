using System;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MDPro3.Servant
{
	// Token: 0x020012CC RID: 4812
	public class CharacterSelector : Servant
	{
		// Token: 0x06008CAD RID: 36013 RVA: 0x00126FF2 File Offset: 0x001251F2
		public void SwitchCondition(CharacterSelector.Condition condition)
		{
			CharacterSelector.condition = condition;
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06008CAE RID: 36014 RVA: 0x00126FFA File Offset: 0x001251FA
		public override int Depth
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06008CAF RID: 36015 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06008CB0 RID: 36016 RVA: 0x00124D11 File Offset: 0x00122F11
		protected override float SubBlackAlpha
		{
			get
			{
				return 0.9f;
			}
		}

		// Token: 0x06008CB1 RID: 36017 RVA: 0x00127000 File Offset: 0x00125200
		public override void Initialize()
		{
			base.Initialize();
			Addressables.LoadAssetAsync<Characters>("ScriptableObjects/Characters.asset").Completed += delegate(AsyncOperationHandle<Characters> result)
			{
				CharacterSelector.characters = result.Result;
				this.LoadCharacters();
				Program.instance.setting.RefreshCharacterName();
			};
			Addressables.LoadAssetAsync<GameObject>("UI/ItemCharacter.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				CharacterSelector.characterItem = result.Result;
			};
		}

		// Token: 0x06008CB2 RID: 36018 RVA: 0x00127064 File Offset: 0x00125264
		public override void PerFrameFunction()
		{
			if (!this.NeedResponseInput())
			{
				return;
			}
			if (UserInput.WasLeftShoulderPressed)
			{
				this.GetUI<CharacterSelectorUI>().TogglePlayer0.OnLeftSelection();
			}
			if (UserInput.WasRightShoulderPressed)
			{
				this.GetUI<CharacterSelectorUI>().TogglePlayer0.OnRightSelection();
			}
			if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
			{
				this.OnReturn();
			}
		}

		// Token: 0x06008CB3 RID: 36019 RVA: 0x001270BC File Offset: 0x001252BC
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (!(this.lastSelectable != null))
			{
				this.servantUI.SelectDefaultSelectable();
				return;
			}
			SelectionToggle_CharacterItem selectionToggle_CharacterItem;
			SelectionToggle_CharacterSeries selectionToggle_CharacterSeries;
			if (this.lastSelectable.TryGetComponent<SelectionToggle_CharacterItem>(out selectionToggle_CharacterItem) || this.lastSelectable.TryGetComponent<SelectionToggle_CharacterSeries>(out selectionToggle_CharacterSeries))
			{
				this.lastSelectable.Select();
				return;
			}
			this.servantUI.SelectDefaultSelectable();
		}

		// Token: 0x06008CB4 RID: 36020 RVA: 0x00127124 File Offset: 0x00125324
		public override void OnReturn()
		{
			if (this.inTransition)
			{
				return;
			}
			if (this.returnAction != null)
			{
				this.returnAction();
				return;
			}
			AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
			GameObject selected = EventSystem.current.currentSelectedGameObject;
			if (selected == null)
			{
				this.OnExit();
				return;
			}
			if (Cursor.lockState == CursorLockMode.None)
			{
				this.OnExit();
				return;
			}
			SelectionToggle_CharacterItem selectionToggle_CharacterItem;
			if (!selected.TryGetComponent<SelectionToggle_CharacterItem>(out selectionToggle_CharacterItem))
			{
				this.OnExit();
				return;
			}
			if (this.lastSelectedToggle != null)
			{
				EventSystem.current.SetSelectedGameObject(this.lastSelectedToggle.gameObject);
				return;
			}
			this.servantUI.SelectDefaultSelectable();
		}

		// Token: 0x06008CB5 RID: 36021 RVA: 0x001271C6 File Offset: 0x001253C6
		public override void OnExit()
		{
			if (Program.instance.currentSubServant == this)
			{
				Program.instance.ShowSubServant(Program.instance.setting);
				return;
			}
			Program.instance.ShiftToServant(Program.instance.setting);
		}

		// Token: 0x06008CB6 RID: 36022 RVA: 0x00127203 File Offset: 0x00125403
		public void LoadCharacters()
		{
			CharacterSelector.characters.Initialize();
			CharacterSelector.characters.ChangeLanguage(Language.GetConfig());
		}

		// Token: 0x0400CACF RID: 51919
		[HideInInspector]
		public SelectionToggle_CharacterSeries lastSelectedToggle;

		// Token: 0x0400CAD0 RID: 51920
		[HideInInspector]
		public SelectionToggle_CharacterItem lastSelectedCharacter;

		// Token: 0x0400CAD1 RID: 51921
		public static Characters characters;

		// Token: 0x0400CAD2 RID: 51922
		public static GameObject characterItem;

		// Token: 0x0400CAD3 RID: 51923
		public static CharacterSelector.Condition condition;

		// Token: 0x020012CD RID: 4813
		public enum Condition
		{
			// Token: 0x0400CAD5 RID: 51925
			Duel,
			// Token: 0x0400CAD6 RID: 51926
			Watch,
			// Token: 0x0400CAD7 RID: 51927
			Replay
		}
	}
}
