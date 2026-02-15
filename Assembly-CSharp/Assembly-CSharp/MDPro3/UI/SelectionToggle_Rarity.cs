using System;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x020013CE RID: 5070
	public class SelectionToggle_Rarity : SelectionToggle
	{
		// Token: 0x060092EC RID: 37612 RVA: 0x0014A83C File Offset: 0x00148A3C
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.exclusiveCallOffEvent = false;
			this.canToggleOffSelf = true;
			this.toggleWhenSelected = false;
		}

		// Token: 0x060092ED RID: 37613 RVA: 0x00146321 File Offset: 0x00144521
		public override void SetToggleOff(bool fromSelf = true)
		{
			this.isOn = false;
			this.ToggleOff();
			if (fromSelf)
			{
				this.CallToggleOffEvent();
			}
		}

		// Token: 0x060092EE RID: 37614 RVA: 0x0014A860 File Offset: 0x00148A60
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.deckEditor.GetUI<DeckEditorUI>().ChangeRarity(this.rarity);
		}

		// Token: 0x060092EF RID: 37615 RVA: 0x0014A882 File Offset: 0x00148A82
		protected override void CallToggleOffEvent()
		{
			base.CallToggleOffEvent();
			Program.instance.deckEditor.GetUI<DeckEditorUI>().ChangeRarity(CardRarity.Rarity.Normal);
		}

		// Token: 0x0400D152 RID: 53586
		[Header("SelectionToggle Rarity")]
		[SerializeField]
		public CardRarity.Rarity rarity;
	}
}
