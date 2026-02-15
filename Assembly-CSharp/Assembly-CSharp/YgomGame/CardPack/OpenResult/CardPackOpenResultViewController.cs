using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Dialog.CommonDialog;
using YgomGame.Menu;
using YgomGame.Shop;
using YgomSystem.UI;

namespace YgomGame.CardPack.OpenResult
{
	// Token: 0x020010B6 RID: 4278
	public class CardPackOpenResultViewController : BaseMenuViewController
	{
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06007F12 RID: 32530 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06007F13 RID: 32531 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06007F14 RID: 32532 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setSurfaceActiveOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SwapOpen(ViewController fromVc)
		{
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007F18 RID: 32536 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckOpenNotices()
		{
		}

		// Token: 0x06007F1A RID: 32538 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPack(ProductContext packProductContext)
		{
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDecidedOpenSecretPacks(int shopId)
		{
		}

		// Token: 0x06007F1D RID: 32541 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ReplaceOpenSecretPacks(int shopId)
		{
			return false;
		}

		// Token: 0x06007F1E RID: 32542 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0400B7A8 RID: 47016
		private readonly string k_ELabelAnalogDirectionItem;

		// Token: 0x0400B7A9 RID: 47017
		private readonly string k_ELabelObtainedCardsRoot;

		// Token: 0x0400B7AA RID: 47018
		private readonly string k_ELabelFoundedSecretPacksRoot;

		// Token: 0x0400B7AB RID: 47019
		private readonly string k_ELabelShowOwnedNumToggle;

		// Token: 0x0400B7AC RID: 47020
		private readonly string k_ELabelFootBar;

		// Token: 0x0400B7AD RID: 47021
		private readonly string k_ELabelOKButton;

		// Token: 0x0400B7AE RID: 47022
		private Dictionary<string, object> m_GachaResultWork;

		// Token: 0x0400B7AF RID: 47023
		private Selector m_FootBarSelector;

		// Token: 0x0400B7B0 RID: 47024
		private ObtainedCardsWidget m_ObtainedCardsWidget;

		// Token: 0x0400B7B1 RID: 47025
		private FoundedSecretPacksWidget m_FoundedSecretPacksWidget;

		// Token: 0x0400B7B2 RID: 47026
		private bool m_OpenFoundedSecretTrigger;

		// Token: 0x0400B7B3 RID: 47027
		private bool m_OpenNextFinalizedURTrigger;

		// Token: 0x0400B7B4 RID: 47028
		private ValueTuple<EntryItemListData, bool, bool> m_PlayObtainItems;
	}
}
