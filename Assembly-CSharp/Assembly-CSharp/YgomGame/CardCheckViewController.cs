using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame
{
	// Token: 0x020007AD RID: 1965
	public class CardCheckViewController : ViewController
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_CardId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_FSize
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_FSize_P
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003CEB RID: 15595 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_NLength
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003CED RID: 15597 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_TLength
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003CEF RID: 15599 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_TLength_P
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003CF1 RID: 15601 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_StartId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_EndId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x0000216D File Offset: 0x0000036D
		private float m_Duration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_CheckButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06003CF7 RID: 15607 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_RandomButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_UpdateFontButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x0000216A File Offset: 0x0000036A
		private RawImage m_CardPicture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_Play
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_Pause
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x0000216A File Offset: 0x0000036A
		private Toggle[] m_LanguageToggles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton[] m_UpdateLanguage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06003CFE RID: 15614 RVA: 0x0000216A File Offset: 0x0000036A
		private Dropdown m_DropDwonLanguage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_Language
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x0000216A File Offset: 0x0000036A
		private Toggle m_ToggleAllLanguage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_AllLanguage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x0000216A File Offset: 0x0000036A
		private MDText m_imageTypeText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x0000216A File Offset: 0x0000036A
		private Toggle m_ToggleFrame_M
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_Frame_M
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool m_IsFrame_M
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x0000216A File Offset: 0x0000036A
		private Toggle m_ToggleFrame_P
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06003D07 RID: 15623 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_Frame_P
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool m_IsFrame_P
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x0000216A File Offset: 0x0000036A
		private Toggle m_ToggleFrame_ST
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_Frame_ST
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool m_IsFrame_ST
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_NormalStyleButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06003D0D RID: 15629 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_ShineStyleButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_RoyalStyleButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_InputListButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_SaveImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06003D11 RID: 15633 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_SaveFontSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_ClearFontSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_CheckDataMode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x0000216A File Offset: 0x0000036A
		private Toggle m_ToggleSaveImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool m_IsAllLanguage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator OnStart()
		{
			return null;
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x0000216D File Offset: 0x0000036D
		private void DebugPrintCardData(int mrk)
		{
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x0000216D File Offset: 0x0000036D
		private void MakeTargetList()
		{
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x0000216D File Offset: 0x0000036D
		private void MakeTargetListforAll()
		{
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateFontSize()
		{
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCardAsync()
		{
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCardOneAsync()
		{
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeLanguage(string lang)
		{
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x0000216D File Offset: 0x0000036D
		private void SaveCardImageAsJpg(Texture texture, int cardId)
		{
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator AutoUpdateCard()
		{
			return null;
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x0000216D File Offset: 0x0000036D
		private void InputList(string country)
		{
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x0000216D File Offset: 0x0000036D
		private void NameCheck(string country)
		{
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x0000216D File Offset: 0x0000036D
		private void ContainsCheck(string country)
		{
		}

		// Token: 0x04003562 RID: 13666
		private const string LABEL_EO_CARDID = "CardId";

		// Token: 0x04003563 RID: 13667
		private const string LABEL_EO_TSIZE = "TextSize";

		// Token: 0x04003564 RID: 13668
		private const string LABEL_EO_TSIZE_P = "TextSize_P";

		// Token: 0x04003565 RID: 13669
		private const string LABEL_EO_NLENGTH = "NameLength";

		// Token: 0x04003566 RID: 13670
		private const string LABEL_EO_TLENGTH = "TextLength";

		// Token: 0x04003567 RID: 13671
		private const string LABEL_EO_TLENGTH_P = "TextLength_P";

		// Token: 0x04003568 RID: 13672
		private const string LABEL_EO_TOGGLE = "Toggle";

		// Token: 0x04003569 RID: 13673
		private const string LABEL_EO_CHECK = "Check";

		// Token: 0x0400356A RID: 13674
		private const string LABEL_EO_RANDOM = "Random";

		// Token: 0x0400356B RID: 13675
		private const string LABEL_EO_UPDATEFONT = "UpdateFont";

		// Token: 0x0400356C RID: 13676
		private const string LABEL_EO_MASK = "Mask";

		// Token: 0x0400356D RID: 13677
		private const string LABEL_EO_INPUT_FIELD = "InputField";

		// Token: 0x0400356E RID: 13678
		private const string LABEL_EO_COUNTER = "Counter";

		// Token: 0x0400356F RID: 13679
		private const string LABEL_EO_STARTID = "StartId";

		// Token: 0x04003570 RID: 13680
		private const string LABEL_EO_ENDID = "EndId";

		// Token: 0x04003571 RID: 13681
		private const string LABEL_EO_DURATION = "Duration";

		// Token: 0x04003572 RID: 13682
		private const string LABEL_EO_PLAY = "Play";

		// Token: 0x04003573 RID: 13683
		private const string LABEL_EO_PAUSE = "Pause";

		// Token: 0x04003574 RID: 13684
		private const string LABEL_EO_DROPDOWNLANGUAGE = "DropDownLanguage";

		// Token: 0x04003575 RID: 13685
		private const string LABEL_EO_UPDATELANGUAGE = "UpdateLanguage";

		// Token: 0x04003576 RID: 13686
		private const string LABEL_EO_TOGGLEALLLANGUAGE = "ToggleAllLanguage";

		// Token: 0x04003577 RID: 13687
		private const string LABEL_EO_LANGUAGE00 = "ja-JP";

		// Token: 0x04003578 RID: 13688
		private const string LABEL_EO_LANGUAGE01 = "en-US";

		// Token: 0x04003579 RID: 13689
		private const string LABEL_EO_LANGUAGE02 = "fr-FR";

		// Token: 0x0400357A RID: 13690
		private const string LABEL_EO_LANGUAGE03 = "it-IT";

		// Token: 0x0400357B RID: 13691
		private const string LABEL_EO_LANGUAGE04 = "de-DE";

		// Token: 0x0400357C RID: 13692
		private const string LABEL_EO_LANGUAGE05 = "es-ES";

		// Token: 0x0400357D RID: 13693
		private const string LABEL_EO_LANGUAGE06 = "pt-BR";

		// Token: 0x0400357E RID: 13694
		private const string LABEL_EO_LANGUAGE07 = "ko-KR";

		// Token: 0x0400357F RID: 13695
		private const string LABEL_EO_LANGUAGE08 = "zh-TW";

		// Token: 0x04003580 RID: 13696
		private const string LABEL_EO_LANGUAGE09 = "zh-CN";

		// Token: 0x04003581 RID: 13697
		private string[] LABEL_EO_LANGUAGE;

		// Token: 0x04003582 RID: 13698
		private const string LABEL_EO_IMAGETYPE = "ImageType";

		// Token: 0x04003583 RID: 13699
		private const string LABEL_EO_OCG = "OCG";

		// Token: 0x04003584 RID: 13700
		private const string LABEL_EO_TCG = "TCG";

		// Token: 0x04003585 RID: 13701
		private const string LABEL_EO_ILLUST = "Illust";

		// Token: 0x04003586 RID: 13702
		private const string LABEL_EO_ILLUST_HD = "Illust_HD";

		// Token: 0x04003587 RID: 13703
		private const string LABEL_EO_FRAME_M = "FrameM";

		// Token: 0x04003588 RID: 13704
		private const string LABEL_EO_FRAME_P = "FrameP";

		// Token: 0x04003589 RID: 13705
		private const string LABEL_EO_FRAME_ST = "FrameST";

		// Token: 0x0400358A RID: 13706
		private const string LABEL_EO_INPUT = "Input";

		// Token: 0x0400358B RID: 13707
		private const string LABEL_EO_STYLE_NORMAL = "NormalStyle";

		// Token: 0x0400358C RID: 13708
		private const string LABEL_EO_STYLE_SHINE = "ShineStyle";

		// Token: 0x0400358D RID: 13709
		private const string LABEL_EO_STYLE_ROYAL = "RoyalStyle";

		// Token: 0x0400358E RID: 13710
		private const string LABEL_EO_TOGGLE_SAVE_IMAGE = "SaveImage";

		// Token: 0x0400358F RID: 13711
		private const string LABEL_EO_TOGGLE_SAVE_FONTINFO = "SaveFontSize";

		// Token: 0x04003590 RID: 13712
		private const string LABEL_EO_TOGGLE_CLEAR_FONTINFO = "ClearFontSize";

		// Token: 0x04003591 RID: 13713
		private const string LABEL_EO_TOGGLE_CHECK_FONTINFO = "CheckFontSize";

		// Token: 0x04003592 RID: 13714
		private List<int> m_InputMRKList;

		// Token: 0x04003593 RID: 13715
		private bool m_AutoPlay;

		// Token: 0x04003594 RID: 13716
		private ElementObjectManager m_Eom;

		// Token: 0x04003595 RID: 13717
		private int CARDNUM;

		// Token: 0x04003596 RID: 13718
		private int m_CurrentPicture;

		// Token: 0x04003597 RID: 13719
		private List<RawImage> m_CardPictures;

		// Token: 0x04003598 RID: 13720
		private List<int> m_TargetList;

		// Token: 0x04003599 RID: 13721
		private int m_ListId;

		// Token: 0x0400359A RID: 13722
		private int m_ListEnd;

		// Token: 0x0400359B RID: 13723
		private int m_StyleId;

		// Token: 0x0400359C RID: 13724
		private Selector m_Selector;

		// Token: 0x0400359D RID: 13725
		private bool m_isOCG;

		// Token: 0x0400359E RID: 13726
		private string CurrentLang;
	}
}
