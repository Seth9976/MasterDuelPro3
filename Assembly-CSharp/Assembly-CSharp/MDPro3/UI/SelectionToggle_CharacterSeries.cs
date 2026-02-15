using System;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013BF RID: 5055
	public class SelectionToggle_CharacterSeries : SelectionToggle
	{
		// Token: 0x06009278 RID: 37496 RVA: 0x0014662E File Offset: 0x0014482E
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.toggleWhenSelected = true;
		}

		// Token: 0x06009279 RID: 37497 RVA: 0x00147EF4 File Offset: 0x001460F4
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.character.GetUI<CharacterSelectorUI>().SetHoverText(string.Empty);
			Program.instance.character.GetUI<CharacterSelectorUI>().ShowCharacters(this.serialIndex);
			Program.instance.character.lastSelectedToggle = this;
		}

		// Token: 0x0600927A RID: 37498 RVA: 0x00147F4C File Offset: 0x0014614C
		protected override void OnSubmit()
		{
			base.OnSubmit();
			UserInput.NextSelectionIsAxis = true;
			GameObject target = Program.instance.character.lastSelectedCharacter.gameObject;
			if (!target.activeSelf)
			{
				target = Program.instance.character.GetUI<CharacterSelectorUI>().GetFirstActiveCharacterItem();
			}
			EventSystem.current.SetSelectedGameObject(target);
		}

		// Token: 0x0400D0E5 RID: 53477
		[SerializeField]
		private string serialIndex;
	}
}
