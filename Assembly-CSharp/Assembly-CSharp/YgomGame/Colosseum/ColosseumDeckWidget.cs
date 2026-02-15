using System;
using System.Collections.Generic;
using UnityEngine.Events;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Colosseum
{
	// Token: 0x02001028 RID: 4136
	public class ColosseumDeckWidget : ElementWidgetBehaviourBase<ColosseumDeckWidget>
	{
		// Token: 0x06007C35 RID: 31797 RVA: 0x0000216A File Offset: 0x0000036A
		public static ColosseumDeckWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06007C37 RID: 31799 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(Selector parentSelector, List<ColosseumDeckWidget.ButtonInfo> buttonInfos)
		{
		}

		// Token: 0x06007C38 RID: 31800 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDisp(List<ColosseumDeckWidget.ButtonInfo> buttonInfos)
		{
		}

		// Token: 0x06007C39 RID: 31801 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectDeckButton()
		{
		}

		// Token: 0x06007C3A RID: 31802 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShortcutLRDeck(bool isSet)
		{
		}

		// Token: 0x06007C3B RID: 31803 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsSetDeck(int index, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x06007C3C RID: 31804 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveIdxToggle(int index)
		{
		}

		// Token: 0x06007C3D RID: 31805 RVA: 0x0000216A File Offset: 0x0000036A
		public List<ColosseumDeckWidget.ColosseumDeck> GetColosseumDecks()
		{
			return null;
		}

		// Token: 0x0400B3FF RID: 46079
		private const string E_TabGroup = "TabGroup";

		// Token: 0x0400B400 RID: 46080
		private const string E_IconL = "IconL";

		// Token: 0x0400B401 RID: 46081
		private const string E_IconR = "IconR";

		// Token: 0x0400B402 RID: 46082
		private const string E_TemplateTab = "TemplateTab";

		// Token: 0x0400B403 RID: 46083
		private const string E_TextOff = "TextOff";

		// Token: 0x0400B404 RID: 46084
		private const string E_TextOn = "TextOn";

		// Token: 0x0400B405 RID: 46085
		private const string E_TemplateDeckButton = "TemplateDeckButton";

		// Token: 0x0400B406 RID: 46086
		private const string E_ButtonDeck = "ButtonDeck";

		// Token: 0x0400B407 RID: 46087
		private const string E_ButtonPlay = "ButtonPlay";

		// Token: 0x0400B408 RID: 46088
		private const string E_ImageDeck = "ImageDeck";

		// Token: 0x0400B409 RID: 46089
		public const string E_ImageDeckBg = "ImageDeckBg";

		// Token: 0x0400B40A RID: 46090
		private const string E_ImageDeckDisabled = "ImageDeckDisabled";

		// Token: 0x0400B40B RID: 46091
		private const string E_ImageDeckEmpty = "ImageDeckEmpty";

		// Token: 0x0400B40C RID: 46092
		private const string E_TextDeck = "TextDeck";

		// Token: 0x0400B40D RID: 46093
		private const string E_TextPlay = "TextPlay";

		// Token: 0x0400B40E RID: 46094
		private const string E_ImageBack = "ImageBack";

		// Token: 0x0400B40F RID: 46095
		private DirectionalToggleGroupWidget toggleGroup;

		// Token: 0x0400B410 RID: 46096
		private List<ColosseumDeckWidget.ColosseumDeck> colosseumDecks;

		// Token: 0x02001029 RID: 4137
		public class ButtonInfo
		{
			// Token: 0x06007C3F RID: 31807 RVA: 0x00002739 File Offset: 0x00000939
			public ButtonInfo(ColosseumPathManager pathManager, bool isSetDeck, bool cantUseDeck, bool duelInteractable, string duelLabel, string deckLabel, string tabLabel, int rentalState = 0)
			{
			}

			// Token: 0x06007C40 RID: 31808 RVA: 0x00002739 File Offset: 0x00000939
			public ButtonInfo(ColosseumDeckManager deckManager, bool isSetDeck, bool cantUseDeck, bool duelInteractable, string duelLabel, string deckLabel, string tabLabel, int rentalState = 0)
			{
			}

			// Token: 0x0400B411 RID: 46097
			public bool isSetDeck;

			// Token: 0x0400B412 RID: 46098
			public bool cantUseDeck;

			// Token: 0x0400B413 RID: 46099
			public bool duelInteractable;

			// Token: 0x0400B414 RID: 46100
			public string duelLabel;

			// Token: 0x0400B415 RID: 46101
			public string deckLabel;

			// Token: 0x0400B416 RID: 46102
			public string tabLabel;

			// Token: 0x0400B417 RID: 46103
			public int rentalState;

			// Token: 0x0400B418 RID: 46104
			public ColosseumDeckManager deckManager;

			// Token: 0x0400B419 RID: 46105
			public UnityAction onClickDeck;

			// Token: 0x0400B41A RID: 46106
			public UnityAction onClickDuel;

			// Token: 0x0400B41B RID: 46107
			public UnityAction onClickDuelInactive;
		}

		// Token: 0x0200102A RID: 4138
		public class ColosseumDeck
		{
			// Token: 0x06007C41 RID: 31809 RVA: 0x00002739 File Offset: 0x00000939
			public ColosseumDeck(ColosseumDeckWidget.ButtonInfo buttonInfo, ElementObjectManager tabEom, ElementObjectManager btnEom)
			{
			}

			// Token: 0x06007C42 RID: 31810 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateDisp()
			{
			}

			// Token: 0x06007C43 RID: 31811 RVA: 0x0000216D File Offset: 0x0000036D
			public void SelectDeckButton()
			{
			}

			// Token: 0x0400B41C RID: 46108
			public ColosseumDeckWidget.ButtonInfo buttonInfo;

			// Token: 0x0400B41D RID: 46109
			public ElementObjectManager tabEom;

			// Token: 0x0400B41E RID: 46110
			public ElementObjectManager btnEom;

			// Token: 0x0400B41F RID: 46111
			private DeckCaseWidget deckCase;
		}
	}
}
