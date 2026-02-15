using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DG.Tweening;
using MDPro3.Servant;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001481 RID: 5249
	public class SettingServantUI : ServantUI
	{
		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x060098C9 RID: 39113 RVA: 0x0016979C File Offset: 0x0016799C
		private SelectionToggle_Setting ToggleSystem
		{
			get
			{
				return this.m_ToggleSystem = ((this.m_ToggleSystem != null) ? this.m_ToggleSystem : base.Manager.GetElement<SelectionToggle_Setting>("ToggleSystem"));
			}
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x060098CA RID: 39114 RVA: 0x001697D8 File Offset: 0x001679D8
		private SelectionToggle_Setting ToggleDuel
		{
			get
			{
				return this.m_ToggleDuel = ((this.m_ToggleDuel != null) ? this.m_ToggleDuel : base.Manager.GetElement<SelectionToggle_Setting>("ToggleDuel"));
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x060098CB RID: 39115 RVA: 0x00169814 File Offset: 0x00167A14
		private SelectionToggle_Setting ToggleWatch
		{
			get
			{
				return this.m_ToggleWatch = ((this.m_ToggleWatch != null) ? this.m_ToggleWatch : base.Manager.GetElement<SelectionToggle_Setting>("ToggleWatch"));
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x060098CC RID: 39116 RVA: 0x00169850 File Offset: 0x00167A50
		private SelectionToggle_Setting ToggleReplay
		{
			get
			{
				return this.m_ToggleReplay = ((this.m_ToggleReplay != null) ? this.m_ToggleReplay : base.Manager.GetElement<SelectionToggle_Setting>("ToggleReplay"));
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x060098CD RID: 39117 RVA: 0x0016988C File Offset: 0x00167A8C
		private SelectionToggle_Setting TogglePort
		{
			get
			{
				return this.m_TogglePort = ((this.m_TogglePort != null) ? this.m_TogglePort : base.Manager.GetElement<SelectionToggle_Setting>("TogglePort"));
			}
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x060098CE RID: 39118 RVA: 0x001698C8 File Offset: 0x00167AC8
		private SelectionToggle_Setting ToggleExpansions
		{
			get
			{
				return this.m_ToggleExpansions = ((this.m_ToggleExpansions != null) ? this.m_ToggleExpansions : base.Manager.GetElement<SelectionToggle_Setting>("ToggleExpansions"));
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x060098CF RID: 39119 RVA: 0x00169904 File Offset: 0x00167B04
		private SelectionToggle_Setting ToggleAbout
		{
			get
			{
				return this.m_ToggleAbout = ((this.m_ToggleAbout != null) ? this.m_ToggleAbout : base.Manager.GetElement<SelectionToggle_Setting>("ToggleAbout"));
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x060098D0 RID: 39120 RVA: 0x00169940 File Offset: 0x00167B40
		private SelectionButton ButtonRetry
		{
			get
			{
				return this.m_ButtonRetry = ((this.m_ButtonRetry != null) ? this.m_ButtonRetry : base.Manager.GetElement<SelectionButton>("ButtonRetry"));
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x060098D1 RID: 39121 RVA: 0x0016997C File Offset: 0x00167B7C
		private SelectionButton ButtonSurrender
		{
			get
			{
				return this.m_ButtonSurrender = ((this.m_ButtonSurrender != null) ? this.m_ButtonSurrender : base.Manager.GetElement<SelectionButton>("ButtonSurrender"));
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x060098D2 RID: 39122 RVA: 0x001699B8 File Offset: 0x00167BB8
		private SelectionButton_Setting ButtonBGM
		{
			get
			{
				return this.m_ButtonBGM = ((this.m_ButtonBGM != null) ? this.m_ButtonBGM : base.Manager.GetElement<SelectionButton_Setting>("BGM"));
			}
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x060098D3 RID: 39123 RVA: 0x001699F4 File Offset: 0x00167BF4
		private SelectionButton_Setting ButtonSE
		{
			get
			{
				return this.m_ButtonSE = ((this.m_ButtonSE != null) ? this.m_ButtonSE : base.Manager.GetElement<SelectionButton_Setting>("SE"));
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x060098D4 RID: 39124 RVA: 0x00169A30 File Offset: 0x00167C30
		private SelectionButton_Setting ButtonVoice
		{
			get
			{
				return this.m_ButtonVoice = ((this.m_ButtonVoice != null) ? this.m_ButtonVoice : base.Manager.GetElement<SelectionButton_Setting>("Voice"));
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x060098D5 RID: 39125 RVA: 0x00169A6C File Offset: 0x00167C6C
		private SelectionButton_Setting ButtonScreenMode
		{
			get
			{
				return this.m_ButtonScreenMode = ((this.m_ButtonScreenMode != null) ? this.m_ButtonScreenMode : base.Manager.GetElement<SelectionButton_Setting>("ScreenMode"));
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x060098D6 RID: 39126 RVA: 0x00169AA8 File Offset: 0x00167CA8
		private SelectionButton_Setting ButtonResolution
		{
			get
			{
				return this.m_ButtonResolution = ((this.m_ButtonResolution != null) ? this.m_ButtonResolution : base.Manager.GetElement<SelectionButton_Setting>("Resolution"));
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x060098D7 RID: 39127 RVA: 0x00169AE4 File Offset: 0x00167CE4
		private SelectionButton_Setting ButtonScale
		{
			get
			{
				return this.m_ButtonScale = ((this.m_ButtonScale != null) ? this.m_ButtonScale : base.Manager.GetElement<SelectionButton_Setting>("Scale"));
			}
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x060098D8 RID: 39128 RVA: 0x00169B20 File Offset: 0x00167D20
		private SelectionButton_Setting ButtonQuality
		{
			get
			{
				return this.m_ButtonQuality = ((this.m_ButtonQuality != null) ? this.m_ButtonQuality : base.Manager.GetElement<SelectionButton_Setting>("Quality"));
			}
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x060098D9 RID: 39129 RVA: 0x00169B5C File Offset: 0x00167D5C
		private SelectionButton_Setting ButtonFAA
		{
			get
			{
				return this.m_ButtonFAA = ((this.m_ButtonFAA != null) ? this.m_ButtonFAA : base.Manager.GetElement<SelectionButton_Setting>("FAA"));
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x060098DA RID: 39130 RVA: 0x00169B98 File Offset: 0x00167D98
		private SelectionButton_Setting ButtonAAA
		{
			get
			{
				return this.m_ButtonAAA = ((this.m_ButtonAAA != null) ? this.m_ButtonAAA : base.Manager.GetElement<SelectionButton_Setting>("AAA"));
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x060098DB RID: 39131 RVA: 0x00169BD4 File Offset: 0x00167DD4
		private SelectionButton_Setting ButtonShadow
		{
			get
			{
				return this.m_ButtonShadow = ((this.m_ButtonShadow != null) ? this.m_ButtonShadow : base.Manager.GetElement<SelectionButton_Setting>("Shadow"));
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x060098DC RID: 39132 RVA: 0x00169C10 File Offset: 0x00167E10
		private SelectionButton_Setting ButtonFPS
		{
			get
			{
				return this.m_ButtonFPS = ((this.m_ButtonFPS != null) ? this.m_ButtonFPS : base.Manager.GetElement<SelectionButton_Setting>("FPS"));
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x060098DD RID: 39133 RVA: 0x00169C4C File Offset: 0x00167E4C
		private SelectionButton_Setting ButtonShowFPS
		{
			get
			{
				return this.m_ButtonShowFPS = ((this.m_ButtonShowFPS != null) ? this.m_ButtonShowFPS : base.Manager.GetElement<SelectionButton_Setting>("ShowFPS"));
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x060098DE RID: 39134 RVA: 0x00169C88 File Offset: 0x00167E88
		private SelectionButton_Setting ButtonRumble
		{
			get
			{
				return this.m_ButtonRumble = ((this.m_ButtonRumble != null) ? this.m_ButtonRumble : base.Manager.GetElement<SelectionButton_Setting>("Rumble"));
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x060098DF RID: 39135 RVA: 0x00169CC4 File Offset: 0x00167EC4
		private SelectionButton_Setting ButtonConfirm
		{
			get
			{
				return this.m_ButtonConfirm = ((this.m_ButtonConfirm != null) ? this.m_ButtonConfirm : base.Manager.GetElement<SelectionButton_Setting>("Confirm"));
			}
		}

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x060098E0 RID: 39136 RVA: 0x00169D00 File Offset: 0x00167F00
		private SelectionButton_Setting ButtonLayout
		{
			get
			{
				return this.m_ButtonLayout = ((this.m_ButtonLayout != null) ? this.m_ButtonLayout : base.Manager.GetElement<SelectionButton_Setting>("Layout"));
			}
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x060098E1 RID: 39137 RVA: 0x00169D3C File Offset: 0x00167F3C
		private SelectionButton_Setting ButtonBackground
		{
			get
			{
				return this.m_ButtonBackground = ((this.m_ButtonBackground != null) ? this.m_ButtonBackground : base.Manager.GetElement<SelectionButton_Setting>("Background"));
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x060098E2 RID: 39138 RVA: 0x00169D78 File Offset: 0x00167F78
		private SelectionButton_Setting ButtonBgmBy
		{
			get
			{
				return this.m_ButtonBgmBy = ((this.m_ButtonBgmBy != null) ? this.m_ButtonBgmBy : base.Manager.GetElement<SelectionButton_Setting>("BgmBy"));
			}
		}

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x060098E3 RID: 39139 RVA: 0x00169DB4 File Offset: 0x00167FB4
		private SelectionButton_Setting ButtonCardStyle
		{
			get
			{
				return this.m_ButtonCardStyle = ((this.m_ButtonCardStyle != null) ? this.m_ButtonCardStyle : base.Manager.GetElement<SelectionButton_Setting>("CardStyle"));
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x060098E4 RID: 39140 RVA: 0x00169DF0 File Offset: 0x00167FF0
		private SelectionButton_Setting ButtonVideoCard
		{
			get
			{
				return this.m_ButtonVideoCard = ((this.m_ButtonVideoCard != null) ? this.m_ButtonVideoCard : base.Manager.GetElement<SelectionButton_Setting>("VideoCard"));
			}
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x060098E5 RID: 39141 RVA: 0x00169E2C File Offset: 0x0016802C
		private SelectionButton_Setting ButtonCardLanguage
		{
			get
			{
				return this.m_ButtonCardLanguage = ((this.m_ButtonCardLanguage != null) ? this.m_ButtonCardLanguage : base.Manager.GetElement<SelectionButton_Setting>("CardLanguage"));
			}
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x060098E6 RID: 39142 RVA: 0x00169E68 File Offset: 0x00168068
		private SelectionButton_Setting ButtonLanguage
		{
			get
			{
				return this.m_ButtonLanguage = ((this.m_ButtonLanguage != null) ? this.m_ButtonLanguage : base.Manager.GetElement<SelectionButton_Setting>("Language"));
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x060098E7 RID: 39143 RVA: 0x00169EA4 File Offset: 0x001680A4
		private SelectionButton_Setting ButtonDuelAppearance
		{
			get
			{
				return this.m_ButtonDuelAppearance = ((this.m_ButtonDuelAppearance != null) ? this.m_ButtonDuelAppearance : base.Manager.GetElement<SelectionButton_Setting>("DuelAppearance"));
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x060098E8 RID: 39144 RVA: 0x00169EE0 File Offset: 0x001680E0
		private SelectionButton_Setting ButtonWatchAppearance
		{
			get
			{
				return this.m_ButtonWatchAppearance = ((this.m_ButtonWatchAppearance != null) ? this.m_ButtonWatchAppearance : base.Manager.GetElement<SelectionButton_Setting>("WatchAppearance"));
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x060098E9 RID: 39145 RVA: 0x00169F1C File Offset: 0x0016811C
		private SelectionButton_Setting ButtonReplayAppearance
		{
			get
			{
				return this.m_ButtonReplayAppearance = ((this.m_ButtonReplayAppearance != null) ? this.m_ButtonReplayAppearance : base.Manager.GetElement<SelectionButton_Setting>("ReplayAppearance"));
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060098EA RID: 39146 RVA: 0x00169F58 File Offset: 0x00168158
		private SelectionButton_Setting ButtonDuelCharacter
		{
			get
			{
				return this.m_ButtonDuelCharacter = ((this.m_ButtonDuelCharacter != null) ? this.m_ButtonDuelCharacter : base.Manager.GetElement<SelectionButton_Setting>("DuelCharacter"));
			}
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x060098EB RID: 39147 RVA: 0x00169F94 File Offset: 0x00168194
		private SelectionButton_Setting ButtonWatchCharacter
		{
			get
			{
				return this.m_ButtonWatchCharacter = ((this.m_ButtonWatchCharacter != null) ? this.m_ButtonWatchCharacter : base.Manager.GetElement<SelectionButton_Setting>("WatchCharacter"));
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060098EC RID: 39148 RVA: 0x00169FD0 File Offset: 0x001681D0
		private SelectionButton_Setting ButtonReplayCharacter
		{
			get
			{
				return this.m_ButtonReplayCharacter = ((this.m_ButtonReplayCharacter != null) ? this.m_ButtonReplayCharacter : base.Manager.GetElement<SelectionButton_Setting>("ReplayCharacter"));
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x060098ED RID: 39149 RVA: 0x0016A00C File Offset: 0x0016820C
		private SelectionButton_Setting ButtonDuelVoice
		{
			get
			{
				return this.m_ButtonDuelVoice = ((this.m_ButtonDuelVoice != null) ? this.m_ButtonDuelVoice : base.Manager.GetElement<SelectionButton_Setting>("DuelVoice"));
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x060098EE RID: 39150 RVA: 0x0016A048 File Offset: 0x00168248
		private SelectionButton_Setting ButtonWatchVoice
		{
			get
			{
				return this.m_ButtonWatchVoice = ((this.m_ButtonWatchVoice != null) ? this.m_ButtonWatchVoice : base.Manager.GetElement<SelectionButton_Setting>("WatchVoice"));
			}
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x060098EF RID: 39151 RVA: 0x0016A084 File Offset: 0x00168284
		private SelectionButton_Setting ButtonReplayVoice
		{
			get
			{
				return this.m_ButtonReplayVoice = ((this.m_ButtonReplayVoice != null) ? this.m_ButtonReplayVoice : base.Manager.GetElement<SelectionButton_Setting>("ReplayVoice"));
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x060098F0 RID: 39152 RVA: 0x0016A0C0 File Offset: 0x001682C0
		private SelectionButton_Setting ButtonDuelCloseup
		{
			get
			{
				return this.m_ButtonDuelCloseup = ((this.m_ButtonDuelCloseup != null) ? this.m_ButtonDuelCloseup : base.Manager.GetElement<SelectionButton_Setting>("DuelCloseup"));
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x060098F1 RID: 39153 RVA: 0x0016A0FC File Offset: 0x001682FC
		private SelectionButton_Setting ButtonWatchCloseup
		{
			get
			{
				return this.m_ButtonWatchCloseup = ((this.m_ButtonWatchCloseup != null) ? this.m_ButtonWatchCloseup : base.Manager.GetElement<SelectionButton_Setting>("WatchCloseup"));
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x060098F2 RID: 39154 RVA: 0x0016A138 File Offset: 0x00168338
		private SelectionButton_Setting ButtonReplayCloseup
		{
			get
			{
				return this.m_ButtonReplayCloseup = ((this.m_ButtonReplayCloseup != null) ? this.m_ButtonReplayCloseup : base.Manager.GetElement<SelectionButton_Setting>("ReplayCloseup"));
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x060098F3 RID: 39155 RVA: 0x0016A174 File Offset: 0x00168374
		private SelectionButton_Setting ButtonDuelPendulum
		{
			get
			{
				return this.m_ButtonDuelPendulum = ((this.m_ButtonDuelPendulum != null) ? this.m_ButtonDuelPendulum : base.Manager.GetElement<SelectionButton_Setting>("DuelPendulum"));
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x060098F4 RID: 39156 RVA: 0x0016A1B0 File Offset: 0x001683B0
		private SelectionButton_Setting ButtonWatchPendulum
		{
			get
			{
				return this.m_ButtonWatchPendulum = ((this.m_ButtonWatchPendulum != null) ? this.m_ButtonWatchPendulum : base.Manager.GetElement<SelectionButton_Setting>("WatchPendulum"));
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x060098F5 RID: 39157 RVA: 0x0016A1EC File Offset: 0x001683EC
		private SelectionButton_Setting ButtonReplayPendulum
		{
			get
			{
				return this.m_ButtonReplayPendulum = ((this.m_ButtonReplayPendulum != null) ? this.m_ButtonReplayPendulum : base.Manager.GetElement<SelectionButton_Setting>("ReplayPendulum"));
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x060098F6 RID: 39158 RVA: 0x0016A228 File Offset: 0x00168428
		private SelectionButton_Setting ButtonDuelCutin
		{
			get
			{
				return this.m_ButtonDuelCutin = ((this.m_ButtonDuelCutin != null) ? this.m_ButtonDuelCutin : base.Manager.GetElement<SelectionButton_Setting>("DuelCutin"));
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060098F7 RID: 39159 RVA: 0x0016A264 File Offset: 0x00168464
		private SelectionButton_Setting ButtonWatchCutin
		{
			get
			{
				return this.m_ButtonWatchCutin = ((this.m_ButtonWatchCutin != null) ? this.m_ButtonWatchCutin : base.Manager.GetElement<SelectionButton_Setting>("WatchCutin"));
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060098F8 RID: 39160 RVA: 0x0016A2A0 File Offset: 0x001684A0
		private SelectionButton_Setting ButtonReplayCutin
		{
			get
			{
				return this.m_ButtonReplayCutin = ((this.m_ButtonReplayCutin != null) ? this.m_ButtonReplayCutin : base.Manager.GetElement<SelectionButton_Setting>("ReplayCutin"));
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060098F9 RID: 39161 RVA: 0x0016A2DC File Offset: 0x001684DC
		private SelectionButton_Setting ButtonDuelEffect
		{
			get
			{
				return this.m_ButtonDuelEffect = ((this.m_ButtonDuelEffect != null) ? this.m_ButtonDuelEffect : base.Manager.GetElement<SelectionButton_Setting>("DuelEffect"));
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060098FA RID: 39162 RVA: 0x0016A318 File Offset: 0x00168518
		private SelectionButton_Setting ButtonWatchEffect
		{
			get
			{
				return this.m_ButtonWatchEffect = ((this.m_ButtonWatchEffect != null) ? this.m_ButtonWatchEffect : base.Manager.GetElement<SelectionButton_Setting>("WatchEffect"));
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060098FB RID: 39163 RVA: 0x0016A354 File Offset: 0x00168554
		private SelectionButton_Setting ButtonReplayEffect
		{
			get
			{
				return this.m_ButtonReplayEffect = ((this.m_ButtonReplayEffect != null) ? this.m_ButtonReplayEffect : base.Manager.GetElement<SelectionButton_Setting>("ReplayEffect"));
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060098FC RID: 39164 RVA: 0x0016A390 File Offset: 0x00168590
		private SelectionButton_Setting ButtonDuelChain
		{
			get
			{
				return this.m_ButtonDuelChain = ((this.m_ButtonDuelChain != null) ? this.m_ButtonDuelChain : base.Manager.GetElement<SelectionButton_Setting>("DuelChain"));
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x060098FD RID: 39165 RVA: 0x0016A3CC File Offset: 0x001685CC
		private SelectionButton_Setting ButtonWatchChain
		{
			get
			{
				return this.m_ButtonWatchChain = ((this.m_ButtonWatchChain != null) ? this.m_ButtonWatchChain : base.Manager.GetElement<SelectionButton_Setting>("WatchChain"));
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060098FE RID: 39166 RVA: 0x0016A408 File Offset: 0x00168608
		private SelectionButton_Setting ButtonReplayChain
		{
			get
			{
				return this.m_ButtonReplayChain = ((this.m_ButtonReplayChain != null) ? this.m_ButtonReplayChain : base.Manager.GetElement<SelectionButton_Setting>("ReplayChain"));
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x060098FF RID: 39167 RVA: 0x0016A444 File Offset: 0x00168644
		private SelectionButton_Setting ButtonDuelDice
		{
			get
			{
				return this.m_ButtonDuelDice = ((this.m_ButtonDuelDice != null) ? this.m_ButtonDuelDice : base.Manager.GetElement<SelectionButton_Setting>("DuelDice"));
			}
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06009900 RID: 39168 RVA: 0x0016A480 File Offset: 0x00168680
		private SelectionButton_Setting ButtonWatchDice
		{
			get
			{
				return this.m_ButtonWatchDice = ((this.m_ButtonWatchDice != null) ? this.m_ButtonWatchDice : base.Manager.GetElement<SelectionButton_Setting>("WatchDice"));
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06009901 RID: 39169 RVA: 0x0016A4BC File Offset: 0x001686BC
		private SelectionButton_Setting ButtonReplayDice
		{
			get
			{
				return this.m_ButtonReplayDice = ((this.m_ButtonReplayDice != null) ? this.m_ButtonReplayDice : base.Manager.GetElement<SelectionButton_Setting>("ReplayDice"));
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06009902 RID: 39170 RVA: 0x0016A4F8 File Offset: 0x001686F8
		private SelectionButton_Setting ButtonDuelCoin
		{
			get
			{
				return this.m_ButtonDuelCoin = ((this.m_ButtonDuelCoin != null) ? this.m_ButtonDuelCoin : base.Manager.GetElement<SelectionButton_Setting>("DuelCoin"));
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06009903 RID: 39171 RVA: 0x0016A534 File Offset: 0x00168734
		private SelectionButton_Setting ButtonWatchCoin
		{
			get
			{
				return this.m_ButtonWatchCoin = ((this.m_ButtonWatchCoin != null) ? this.m_ButtonWatchCoin : base.Manager.GetElement<SelectionButton_Setting>("WatchCoin"));
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06009904 RID: 39172 RVA: 0x0016A570 File Offset: 0x00168770
		private SelectionButton_Setting ButtonReplayCoin
		{
			get
			{
				return this.m_ButtonReplayCoin = ((this.m_ButtonReplayCoin != null) ? this.m_ButtonReplayCoin : base.Manager.GetElement<SelectionButton_Setting>("ReplayCoin"));
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06009905 RID: 39173 RVA: 0x0016A5AC File Offset: 0x001687AC
		private SelectionButton_Setting ButtonDuelAutoInfo
		{
			get
			{
				return this.m_ButtonDuelAutoInfo = ((this.m_ButtonDuelAutoInfo != null) ? this.m_ButtonDuelAutoInfo : base.Manager.GetElement<SelectionButton_Setting>("DuelAutoInfo"));
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06009906 RID: 39174 RVA: 0x0016A5E8 File Offset: 0x001687E8
		private SelectionButton_Setting ButtonWatchAutoInfo
		{
			get
			{
				return this.m_ButtonWatchAutoInfo = ((this.m_ButtonWatchAutoInfo != null) ? this.m_ButtonWatchAutoInfo : base.Manager.GetElement<SelectionButton_Setting>("WatchAutoInfo"));
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06009907 RID: 39175 RVA: 0x0016A624 File Offset: 0x00168824
		private SelectionButton_Setting ButtonReplayAutoInfo
		{
			get
			{
				return this.m_ButtonReplayAutoInfo = ((this.m_ButtonReplayAutoInfo != null) ? this.m_ButtonReplayAutoInfo : base.Manager.GetElement<SelectionButton_Setting>("ReplayAutoInfo"));
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06009908 RID: 39176 RVA: 0x0016A660 File Offset: 0x00168860
		private SelectionButton_Setting ButtonDuelFaceDown
		{
			get
			{
				return this.m_ButtonDuelFaceDown = ((this.m_ButtonDuelFaceDown != null) ? this.m_ButtonDuelFaceDown : base.Manager.GetElement<SelectionButton_Setting>("DuelFaceDown"));
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06009909 RID: 39177 RVA: 0x0016A69C File Offset: 0x0016889C
		private SelectionButton_Setting ButtonWatchFaceDown
		{
			get
			{
				return this.m_ButtonWatchFaceDown = ((this.m_ButtonWatchFaceDown != null) ? this.m_ButtonWatchFaceDown : base.Manager.GetElement<SelectionButton_Setting>("WatchFaceDown"));
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x0600990A RID: 39178 RVA: 0x0016A6D8 File Offset: 0x001688D8
		private SelectionButton_Setting ButtonReplayFaceDown
		{
			get
			{
				return this.m_ButtonReplayFaceDown = ((this.m_ButtonReplayFaceDown != null) ? this.m_ButtonReplayFaceDown : base.Manager.GetElement<SelectionButton_Setting>("ReplayFaceDown"));
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x0600990B RID: 39179 RVA: 0x0016A714 File Offset: 0x00168914
		private SelectionButton_Setting ButtonDuelPlayerMessage
		{
			get
			{
				return this.m_ButtonDuelPlayerMessage = ((this.m_ButtonDuelPlayerMessage != null) ? this.m_ButtonDuelPlayerMessage : base.Manager.GetElement<SelectionButton_Setting>("DuelPlayerMessage"));
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x0600990C RID: 39180 RVA: 0x0016A750 File Offset: 0x00168950
		private SelectionButton_Setting ButtonWatchPlayerMessage
		{
			get
			{
				return this.m_ButtonWatchPlayerMessage = ((this.m_ButtonWatchPlayerMessage != null) ? this.m_ButtonWatchPlayerMessage : base.Manager.GetElement<SelectionButton_Setting>("WatchPlayerMessage"));
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x0600990D RID: 39181 RVA: 0x0016A78C File Offset: 0x0016898C
		private SelectionButton_Setting ButtonReplayPlayerMessage
		{
			get
			{
				return this.m_ButtonReplayPlayerMessage = ((this.m_ButtonReplayPlayerMessage != null) ? this.m_ButtonReplayPlayerMessage : base.Manager.GetElement<SelectionButton_Setting>("ReplayPlayerMessage"));
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x0600990E RID: 39182 RVA: 0x0016A7C8 File Offset: 0x001689C8
		private SelectionButton_Setting ButtonDuelSystemMessage
		{
			get
			{
				return this.m_ButtonDuelSystemMessage = ((this.m_ButtonDuelSystemMessage != null) ? this.m_ButtonDuelSystemMessage : base.Manager.GetElement<SelectionButton_Setting>("DuelSystemMessage"));
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x0600990F RID: 39183 RVA: 0x0016A804 File Offset: 0x00168A04
		private SelectionButton_Setting ButtonWatchSystemMessage
		{
			get
			{
				return this.m_ButtonWatchSystemMessage = ((this.m_ButtonWatchSystemMessage != null) ? this.m_ButtonWatchSystemMessage : base.Manager.GetElement<SelectionButton_Setting>("WatchSystemMessage"));
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06009910 RID: 39184 RVA: 0x0016A840 File Offset: 0x00168A40
		private SelectionButton_Setting ButtonReplaySystemMessage
		{
			get
			{
				return this.m_ButtonReplaySystemMessage = ((this.m_ButtonReplaySystemMessage != null) ? this.m_ButtonReplaySystemMessage : base.Manager.GetElement<SelectionButton_Setting>("ReplaySystemMessage"));
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06009911 RID: 39185 RVA: 0x0016A87C File Offset: 0x00168A7C
		private SelectionButton_Setting ButtonDuelAcc
		{
			get
			{
				return this.m_ButtonDuelAcc = ((this.m_ButtonDuelAcc != null) ? this.m_ButtonDuelAcc : base.Manager.GetElement<SelectionButton_Setting>("DuelAcc"));
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06009912 RID: 39186 RVA: 0x0016A8B8 File Offset: 0x00168AB8
		private SelectionButton_Setting ButtonWatchAcc
		{
			get
			{
				return this.m_ButtonWatchAcc = ((this.m_ButtonWatchAcc != null) ? this.m_ButtonWatchAcc : base.Manager.GetElement<SelectionButton_Setting>("WatchAcc"));
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06009913 RID: 39187 RVA: 0x0016A8F4 File Offset: 0x00168AF4
		private SelectionButton_Setting ButtonReplayAcc
		{
			get
			{
				return this.m_ButtonReplayAcc = ((this.m_ButtonReplayAcc != null) ? this.m_ButtonReplayAcc : base.Manager.GetElement<SelectionButton_Setting>("ReplayAcc"));
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06009914 RID: 39188 RVA: 0x0016A930 File Offset: 0x00168B30
		private SelectionButton_Setting ButtonDuelAutoAcc
		{
			get
			{
				return this.m_ButtonDuelAutoAcc = ((this.m_ButtonDuelAutoAcc != null) ? this.m_ButtonDuelAutoAcc : base.Manager.GetElement<SelectionButton_Setting>("DuelAutoAcc"));
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06009915 RID: 39189 RVA: 0x0016A96C File Offset: 0x00168B6C
		private SelectionButton_Setting ButtonWatchAutoAcc
		{
			get
			{
				return this.m_ButtonWatchAutoAcc = ((this.m_ButtonWatchAutoAcc != null) ? this.m_ButtonWatchAutoAcc : base.Manager.GetElement<SelectionButton_Setting>("WatchAutoAcc"));
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06009916 RID: 39190 RVA: 0x0016A9A8 File Offset: 0x00168BA8
		private SelectionButton_Setting ButtonReplayAutoAcc
		{
			get
			{
				return this.m_ButtonReplayAutoAcc = ((this.m_ButtonReplayAutoAcc != null) ? this.m_ButtonReplayAutoAcc : base.Manager.GetElement<SelectionButton_Setting>("ReplayAutoAcc"));
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06009917 RID: 39191 RVA: 0x0016A9E4 File Offset: 0x00168BE4
		private SelectionButton_Setting ButtonTiming
		{
			get
			{
				return this.m_ButtonTiming = ((this.m_ButtonTiming != null) ? this.m_ButtonTiming : base.Manager.GetElement<SelectionButton_Setting>("Timing"));
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06009918 RID: 39192 RVA: 0x0016AA20 File Offset: 0x00168C20
		private SelectionButton_Setting ButtonAutoRPS
		{
			get
			{
				return this.m_ButtonAutoRPS = ((this.m_ButtonAutoRPS != null) ? this.m_ButtonAutoRPS : base.Manager.GetElement<SelectionButton_Setting>("AutoRPS"));
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06009919 RID: 39193 RVA: 0x0016AA5C File Offset: 0x00168C5C
		private SelectionButton_Setting ButtonExpansionSupport
		{
			get
			{
				return this.m_ButtonExpansionSupport = ((this.m_ButtonExpansionSupport != null) ? this.m_ButtonExpansionSupport : base.Manager.GetElement<SelectionButton_Setting>("ExpansionSupport"));
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x0600991A RID: 39194 RVA: 0x0016AA98 File Offset: 0x00168C98
		public SelectionButton_Setting ButtonUpdatePrerelease
		{
			get
			{
				return this.m_ButtonUpdatePrerelease = ((this.m_ButtonUpdatePrerelease != null) ? this.m_ButtonUpdatePrerelease : base.Manager.GetElement<SelectionButton_Setting>("UpdatePrerelease"));
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x0600991B RID: 39195 RVA: 0x0016AAD4 File Offset: 0x00168CD4
		public SelectionButton_Setting ButtonDownloadYPK
		{
			get
			{
				return this.m_ButtonDownloadYPK = ((this.m_ButtonDownloadYPK != null) ? this.m_ButtonDownloadYPK : base.Manager.GetElement<SelectionButton_Setting>("DonwloadYPK"));
			}
		}

		// Token: 0x0600991C RID: 39196 RVA: 0x0016AB10 File Offset: 0x00168D10
		public override void Initialize(Servant servant)
		{
			base.Initialize(servant);
			this.InitializeVolume();
			this.InitializeScreenMode();
			this.InitializeResolution();
			this.InitializeScale();
			this.InitializeQuality();
			this.InitializeFAA();
			this.InitializeAAA();
			this.InitializeShadow();
			this.InitializeFPS();
			this.InitializeShowFPS();
			this.InitializeRumble();
			this.InitializeConfirm();
			this.InitializeLayout();
			this.InitializeBackground();
			this.InitializeBgmBy();
			this.InitializeCardStyle();
			this.InitializeVideoCard();
			this.InitializeCardLanguage();
			this.InitializeLanguage();
			this.InitializeAppearance();
			this.InitializeCharacter();
			this.InitializeVoice();
			this.InitializeCloseup();
			this.InitializeSummon();
			this.InitializePendulum();
			this.InitializeCutin();
			this.InitializeEffect();
			this.InitializeChain();
			this.InitializeDice();
			this.InitializeCoin();
			this.InitializeAutoInfo();
			this.InitializeFaceDown();
			this.InitializePlayerMessage();
			this.InitializeSystemMessage();
			this.InitializeAcc();
			this.InitializeAutoAcc();
			this.InitializeTimming();
			this.InitializeAutoRPS();
			this.InitializeExpansions();
		}

		// Token: 0x0600991D RID: 39197 RVA: 0x0016AC0E File Offset: 0x00168E0E
		public override void SelectDefaultSelectable()
		{
			this.defaultToggle.GetSelectable().Select();
			this.defaultToggle.ScrollRectToTop();
		}

		// Token: 0x0600991E RID: 39198 RVA: 0x0016AC2C File Offset: 0x00168E2C
		public override void ShowEvent()
		{
			base.ShowEvent();
			if (Program.instance.currentServant == Program.instance.ocgcore)
			{
				if (OcgCore.condition == OcgCore.Condition.Duel)
				{
					this.ToggleDuel.gameObject.SetActive(true);
					this.ToggleWatch.gameObject.SetActive(false);
					this.ToggleReplay.gameObject.SetActive(false);
					this.ButtonRetry.gameObject.SetActive(false);
					this.ButtonSurrender.gameObject.SetActive(true);
					this.ButtonSurrender.SetButtonText(InterString.Get("投降", 0));
				}
				else if (OcgCore.condition == OcgCore.Condition.Watch)
				{
					this.ToggleDuel.gameObject.SetActive(false);
					this.ToggleWatch.gameObject.SetActive(true);
					this.ToggleReplay.gameObject.SetActive(false);
					this.ButtonRetry.gameObject.SetActive(false);
					this.ButtonSurrender.gameObject.SetActive(true);
					this.ButtonSurrender.SetButtonText(InterString.Get("退出观战", 0));
				}
				else if (OcgCore.condition == OcgCore.Condition.Replay)
				{
					this.ToggleDuel.gameObject.SetActive(false);
					this.ToggleWatch.gameObject.SetActive(false);
					this.ToggleReplay.gameObject.SetActive(true);
					this.ButtonRetry.gameObject.SetActive(false);
					this.ButtonSurrender.gameObject.SetActive(true);
					this.ButtonSurrender.SetButtonText(InterString.Get("退出回放", 0));
				}
				this.TogglePort.gameObject.SetActive(false);
				this.ToggleExpansions.gameObject.SetActive(false);
				return;
			}
			this.ToggleDuel.gameObject.SetActive(true);
			this.ToggleWatch.gameObject.SetActive(true);
			this.ToggleReplay.gameObject.SetActive(true);
			this.TogglePort.gameObject.SetActive(true);
			this.ToggleExpansions.gameObject.SetActive(true);
			this.ButtonRetry.gameObject.SetActive(false);
			this.ButtonSurrender.gameObject.SetActive(false);
		}

		// Token: 0x0600991F RID: 39199 RVA: 0x0016AE58 File Offset: 0x00169058
		protected override void HideEvent()
		{
			base.HideEvent();
			this.Save();
			if (Program.instance.ocgcore.showing)
			{
				UIManager.HideBlackBack(Program.instance.setting.TransitionTime);
				UIManager.HideExitButton(Program.instance.setting.TransitionTime, Ease.Linear);
			}
			if (this.videoCardConfigChanged)
			{
				this.videoCardConfigChanged = false;
				SystemEvent.CallVideoCardConfigChangeEvent();
			}
		}

		// Token: 0x06009920 RID: 39200 RVA: 0x0016AEBF File Offset: 0x001690BF
		protected override void AfterHideEvent()
		{
			base.AfterHideEvent();
			if (Program.instance.ocgcore.showing)
			{
				UIManager.ShowFPSLeft();
			}
		}

		// Token: 0x06009921 RID: 39201 RVA: 0x0016AEE0 File Offset: 0x001690E0
		private void Save()
		{
			Config.SetFloat("BgmVol", this.GetBGMVolum());
			Config.SetFloat("SEVol", this.GetSEVolum());
			Config.SetFloat("VoiceVol", this.GetVoiceVolum());
			Config.SetFloat("Scale", this.ButtonScale.GetSliderValue());
			this.FpsSave();
			Config.SetFloat("DuelAcc", this.ButtonDuelAcc.GetSliderValue());
			Config.SetFloat("WatchAcc", this.ButtonWatchAcc.GetSliderValue());
			Config.SetFloat("ReplayAcc", this.ButtonReplayAcc.GetSliderValue());
			Config.Save();
		}

		// Token: 0x06009922 RID: 39202 RVA: 0x0016AF7C File Offset: 0x0016917C
		private void InitializeVolume()
		{
			this.ButtonBGM.SetSliderEvent(new UnityAction<float>(this.OnBgmVolChange));
			this.ButtonBGM.SetSliderValue(Config.GetFloat("BgmVol", 0.7f));
			this.ButtonSE.SetSliderEvent(new UnityAction<float>(this.OnSeVolChange));
			this.ButtonSE.SetSliderValue(Config.GetFloat("SEVol", 0.7f));
			this.ButtonVoice.SetSliderEvent(new UnityAction<float>(this.OnVoiceVolChange));
			this.ButtonVoice.SetSliderValue(Config.GetFloat("VoiceVol", 0.7f));
			this.ButtonVoice.SetClickEvent(new UnityAction(this.OnPlayTestVoice));
		}

		// Token: 0x06009923 RID: 39203 RVA: 0x0016B033 File Offset: 0x00169233
		public float GetBGMVolum()
		{
			return this.ButtonBGM.GetSliderValue();
		}

		// Token: 0x06009924 RID: 39204 RVA: 0x0016B040 File Offset: 0x00169240
		public float GetSEVolum()
		{
			return this.ButtonSE.GetSliderValue();
		}

		// Token: 0x06009925 RID: 39205 RVA: 0x0016B04D File Offset: 0x0016924D
		public float GetVoiceVolum()
		{
			return this.ButtonVoice.GetSliderValue();
		}

		// Token: 0x06009926 RID: 39206 RVA: 0x0016B05A File Offset: 0x0016925A
		private void OnPlayTestVoice()
		{
			AudioManager.PlayVoiceByResourcePath("VOICE/VoiceSample");
		}

		// Token: 0x06009927 RID: 39207 RVA: 0x0016B066 File Offset: 0x00169266
		private void OnBgmVolChange(float vol)
		{
			AudioManager.SetBGMVol(vol);
		}

		// Token: 0x06009928 RID: 39208 RVA: 0x0016B06E File Offset: 0x0016926E
		private void OnSeVolChange(float vol)
		{
			AudioManager.SetSeVol(vol);
		}

		// Token: 0x06009929 RID: 39209 RVA: 0x0016B076 File Offset: 0x00169276
		private void OnVoiceVolChange(float vol)
		{
			AudioManager.SetVoiceVol(vol);
		}

		// Token: 0x0600992A RID: 39210 RVA: 0x0016B080 File Offset: 0x00169280
		private void InitializeScreenMode()
		{
			this.ButtonScreenMode.SetClickEvent(new UnityAction(this.OnScreenModeChange));
			string value = Config.Get("ScreenMode", "1");
			if (value == "0")
			{
				this.ButtonScreenMode.SetModeText(InterString.Get("独占全屏", 0));
				this.ButtonScreenMode.SetNoteText(InterString.Get("独占全屏（仅Windows端有效）", 0));
				return;
			}
			if (value == "1")
			{
				this.ButtonScreenMode.SetModeText(InterString.Get("窗口全屏", 0));
				this.ButtonScreenMode.SetNoteText(InterString.Get("全屏显示", 0));
				return;
			}
			this.ButtonScreenMode.SetModeText(InterString.Get("窗口化", 0));
			this.ButtonScreenMode.SetNoteText(InterString.Get("窗口化（仅桌面端有效）", 0));
		}

		// Token: 0x0600992B RID: 39211 RVA: 0x0016B154 File Offset: 0x00169354
		private void OnScreenModeChange()
		{
			UIManager.ShowPopupSelection(new List<string>
			{
				InterString.Get("显示模式", 0),
				string.Empty,
				InterString.Get("独占全屏", 0),
				InterString.Get("窗口全屏", 0),
				InterString.Get("窗口化", 0)
			}, new Action(this.ScreenModeChange), null);
		}

		// Token: 0x0600992C RID: 39212 RVA: 0x0016B1C8 File Offset: 0x001693C8
		private void ScreenModeChange()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			if (selected == InterString.Get("独占全屏", 0))
			{
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.ExclusiveFullScreen);
				this.ButtonScreenMode.SetModeText(InterString.Get("独占全屏", 0));
				this.ButtonScreenMode.SetNoteText(InterString.Get("独占全屏（仅Windows端有效）", 0));
			}
			else if (selected == InterString.Get("窗口全屏", 0))
			{
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
				this.ButtonScreenMode.SetModeText(InterString.Get("窗口全屏", 0));
				this.ButtonScreenMode.SetNoteText(InterString.Get("全屏显示", 0));
			}
			else
			{
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
				this.ButtonScreenMode.SetModeText(InterString.Get("窗口化", 0));
				this.ButtonScreenMode.SetNoteText(InterString.Get("窗口化（仅桌面端有效）", 0));
			}
			Config.Set("ScreenMode", this.SaveScreenMode(selected));
		}

		// Token: 0x0600992D RID: 39213 RVA: 0x0016B30C File Offset: 0x0016950C
		private string SaveScreenMode(string value)
		{
			string returnValue = "1";
			if (value == InterString.Get("独占全屏", 0))
			{
				returnValue = "0";
			}
			else if (value == InterString.Get("窗口全屏", 0))
			{
				returnValue = "1";
			}
			else if (value == InterString.Get("窗口化", 0))
			{
				returnValue = "2";
			}
			return returnValue;
		}

		// Token: 0x0600992E RID: 39214 RVA: 0x0016B36F File Offset: 0x0016956F
		private void InitializeResolution()
		{
			this.ButtonResolution.SetClickEvent(new UnityAction(this.OnResolutionChange));
			this.SetResolutionText();
			SystemEvent.OnResolutionChange += this.SetResolutionText;
		}

		// Token: 0x0600992F RID: 39215 RVA: 0x0016B3A0 File Offset: 0x001695A0
		private void OnResolutionChange()
		{
			List<string> selections = new List<string>
			{
				InterString.Get("分辨率", 0),
				string.Empty
			};
			foreach (Resolution resolution in Screen.resolutions)
			{
				string selection = Regex.Split(resolution.ToString(), " @ ")[0];
				if (!selections.Contains(selection))
				{
					selections.Add(selection);
				}
			}
			UIManager.ShowPopupSelection(selections, new Action(this.ResolutioChange), null);
		}

		// Token: 0x06009930 RID: 39216 RVA: 0x0016B42C File Offset: 0x0016962C
		private void ResolutioChange()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			Screen.SetResolution(int.Parse(Regex.Split(selected, " x ")[0]), int.Parse(Regex.Split(selected, " x ")[1]), Screen.fullScreen);
			Config.Set("Resolution", selected);
		}

		// Token: 0x06009931 RID: 39217 RVA: 0x0016B487 File Offset: 0x00169687
		private void SetResolutionText()
		{
			this.ButtonResolution.SetModeText(string.Format("{0} x {1}", Screen.width, Screen.height));
		}

		// Token: 0x06009932 RID: 39218 RVA: 0x0016B4B2 File Offset: 0x001696B2
		private void InitializeScale()
		{
			this.ButtonScale.SetSliderEvent(new UnityAction<float>(this.OnScaleChange));
			this.ButtonScale.SetSliderValue(SettingServantUI.GetScale());
		}

		// Token: 0x06009933 RID: 39219 RVA: 0x0016B4DC File Offset: 0x001696DC
		private void OnScaleChange(float vol)
		{
			string value = vol.ToString();
			value = ((value.Length > 4) ? value.Substring(0, 4) : value);
			this.ButtonScale.SetModeText(value);
			Program.instance.camera_.urpAsset.renderScale = float.Parse(value);
		}

		// Token: 0x06009934 RID: 39220 RVA: 0x0016B52C File Offset: 0x0016972C
		public static float GetScale()
		{
			float defau = 1f;
			return Config.GetFloat("Scale", defau);
		}

		// Token: 0x06009935 RID: 39221 RVA: 0x0016B54C File Offset: 0x0016974C
		private void InitializeQuality()
		{
			this.ButtonQuality.SetSliderEvent(new UnityAction<float>(this.OnQualityChange));
			float configQuality = Config.GetFloat("Quality", 2f);
			this.ButtonQuality.SetSliderValue(configQuality);
		}

		// Token: 0x06009936 RID: 39222 RVA: 0x0016B58C File Offset: 0x0016978C
		private void OnQualityChange(float value)
		{
			string text;
			switch ((int)value)
			{
			case 0:
				text = InterString.Get("非常低", 0);
				break;
			case 1:
				text = InterString.Get("低", 0);
				break;
			case 2:
				text = InterString.Get("中等", 0);
				break;
			case 3:
				text = InterString.Get("高", 0);
				break;
			case 4:
				text = InterString.Get("非常高", 0);
				break;
			case 5:
				text = InterString.Get("极致", 0);
				break;
			default:
				text = InterString.Get("中等", 0);
				break;
			}
			string qualityText = text;
			Config.SetFloat("Quality", value);
			this.ButtonQuality.SetModeText(qualityText);
		}

		// Token: 0x06009937 RID: 39223 RVA: 0x0016B635 File Offset: 0x00169835
		private void InitializeFAA()
		{
			this.ButtonFAA.SetSliderEvent(new UnityAction<float>(this.OnFAAChange));
			this.ButtonFAA.SetSliderValue(Config.GetFloat("FAA", 1f));
		}

		// Token: 0x06009938 RID: 39224 RVA: 0x0016B668 File Offset: 0x00169868
		private void OnFAAChange(float value)
		{
			value = 1f;
			string modeText = "Off";
			switch ((int)value)
			{
			case 1:
				modeText = InterString.Get("Off", 0);
				Program.instance.camera_.urpAsset.msaaSampleCount = 1;
				Program.instance.camera_.urpAssetForUI.msaaSampleCount = 1;
				break;
			case 2:
				modeText = "MSAA 2x";
				Program.instance.camera_.urpAsset.msaaSampleCount = 2;
				Program.instance.camera_.urpAssetForUI.msaaSampleCount = 2;
				break;
			case 3:
				modeText = "MSAA 4x";
				Program.instance.camera_.urpAsset.msaaSampleCount = 4;
				Program.instance.camera_.urpAssetForUI.msaaSampleCount = 4;
				break;
			case 4:
				modeText = "MSAA 8x";
				Program.instance.camera_.urpAsset.msaaSampleCount = 8;
				Program.instance.camera_.urpAssetForUI.msaaSampleCount = 8;
				break;
			}
			this.ButtonFAA.SetModeText(modeText);
			Config.SetFloat("FAA", value);
		}

		// Token: 0x06009939 RID: 39225 RVA: 0x0016B788 File Offset: 0x00169988
		private void InitializeAAA()
		{
			this.ButtonAAA.SetSliderEvent(new UnityAction<float>(this.OnAAAChange));
			this.ButtonAAA.SetSliderValue(Config.GetFloat("AAA", 0f));
		}

		// Token: 0x0600993A RID: 39226 RVA: 0x0016B7BB File Offset: 0x001699BB
		private void OnAAAChange(float value)
		{
			this.ButtonAAA.SetModeText(SettingServantUI.ChangeAAA(value));
			Config.SetFloat("AAA", value);
		}

		// Token: 0x0600993B RID: 39227 RVA: 0x0016B7DC File Offset: 0x001699DC
		public static string ChangeAAA(float value)
		{
			UniversalAdditionalCameraData cameraData3D = Program.instance.camera_.cameraMain.GetUniversalAdditionalCameraData();
			string modeText = "Off";
			switch ((int)value)
			{
			case 0:
				modeText = InterString.Get("Off", 0);
				cameraData3D.antialiasing = AntialiasingMode.None;
				break;
			case 1:
				modeText = "FAA";
				cameraData3D.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
				break;
			case 2:
				modeText = "SMAA Low";
				cameraData3D.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
				cameraData3D.antialiasingQuality = AntialiasingQuality.Low;
				break;
			case 3:
				modeText = "SMAA Medium";
				cameraData3D.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
				cameraData3D.antialiasingQuality = AntialiasingQuality.Medium;
				break;
			case 4:
				modeText = "SMAA High";
				cameraData3D.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
				cameraData3D.antialiasingQuality = AntialiasingQuality.High;
				break;
			case 5:
				modeText = "TAA";
				cameraData3D.antialiasing = AntialiasingMode.TemporalAntiAliasing;
				Program.instance.camera_.urpAsset.msaaSampleCount = 1;
				Program.instance.camera_.urpAssetForUI.msaaSampleCount = 1;
				break;
			}
			return modeText;
		}

		// Token: 0x0600993C RID: 39228 RVA: 0x0016B8CB File Offset: 0x00169ACB
		private void InitializeShadow()
		{
			this.ButtonShadow.SetSliderEvent(new UnityAction<float>(this.OnShadowChange));
			this.ButtonShadow.SetSliderValue(Config.GetFloat("Shadow", 0f));
		}

		// Token: 0x0600993D RID: 39229 RVA: 0x0016B8FE File Offset: 0x00169AFE
		private void OnShadowChange(float value)
		{
			this.ButtonShadow.SetModeText(SettingServantUI.ChangeShadow(value));
			Config.SetFloat("Shadow", value);
		}

		// Token: 0x0600993E RID: 39230 RVA: 0x0016B91C File Offset: 0x00169B1C
		public static string ChangeShadow(float value)
		{
			SROptions sr = new SROptions();
			string modeText = InterString.Get("非常低", 0);
			switch ((int)value)
			{
			case 0:
				modeText = InterString.Get("非常低", 0);
				sr.MainLightShadowResolution = ShadowResolution._256;
				sr.SupportsSoftShadows = false;
				break;
			case 1:
				modeText = InterString.Get("低", 0);
				sr.MainLightShadowResolution = ShadowResolution._512;
				sr.SupportsSoftShadows = false;
				break;
			case 2:
				modeText = InterString.Get("中等", 0);
				sr.MainLightShadowResolution = ShadowResolution._1024;
				sr.SupportsSoftShadows = false;
				break;
			case 3:
				modeText = InterString.Get("高", 0);
				sr.MainLightShadowResolution = ShadowResolution._2048;
				sr.SupportsSoftShadows = true;
				break;
			case 4:
				modeText = InterString.Get("非常高", 0);
				sr.MainLightShadowResolution = ShadowResolution._4096;
				sr.SupportsSoftShadows = true;
				break;
			}
			return modeText;
		}

		// Token: 0x0600993F RID: 39231 RVA: 0x0016B9FC File Offset: 0x00169BFC
		private void InitializeFPS()
		{
			this.ButtonFPS.SetSliderEvent(new UnityAction<float>(this.OnFpsChange));
			int configFPS = SettingServantUI.GetFPS();
			if (configFPS == 0)
			{
				configFPS = 29;
			}
			this.ButtonFPS.SetSliderValue((float)configFPS);
		}

		// Token: 0x06009940 RID: 39232 RVA: 0x0016BA39 File Offset: 0x00169C39
		private void OnFpsChange(float value)
		{
			QualitySettings.vSyncCount = 0;
			if (value == 29f)
			{
				value = 0f;
			}
			Application.targetFrameRate = (int)value;
			this.ButtonFPS.SetModeText(value.ToString());
		}

		// Token: 0x06009941 RID: 39233 RVA: 0x0016BA69 File Offset: 0x00169C69
		public static int ChangeFPS(float value)
		{
			QualitySettings.vSyncCount = 0;
			if (value == 29f)
			{
				value = 0f;
			}
			Application.targetFrameRate = (int)value;
			return (int)value;
		}

		// Token: 0x06009942 RID: 39234 RVA: 0x0016BA8C File Offset: 0x00169C8C
		public static int GetFPS()
		{
			float defau = 60f;
			if (DeviceInfo.OnMobile())
			{
				defau = 30f;
			}
			return (int)Config.GetFloat("FPS", defau);
		}

		// Token: 0x06009943 RID: 39235 RVA: 0x0016BAB8 File Offset: 0x00169CB8
		private void FpsSave()
		{
			float config = this.ButtonFPS.GetSliderValue();
			if (config == 29f)
			{
				config = 0f;
			}
			Config.SetFloat("FPS", config);
		}

		// Token: 0x06009944 RID: 39236 RVA: 0x0016BAEC File Offset: 0x00169CEC
		private void InitializeShowFPS()
		{
			this.ButtonShowFPS.SetClickEvent(new UnityAction(this.OnShowFPSClicked));
			bool config = Config.GetBool("ShowFPS", true);
			this.ButtonShowFPS.SetModeText(InterString.Get(config ? "开" : "关", 0));
			SettingServantUI.ChangeShowFPS();
		}

		// Token: 0x06009945 RID: 39237 RVA: 0x0016BB44 File Offset: 0x00169D44
		private void OnShowFPSClicked()
		{
			bool config = Config.GetBool("ShowFPS", true);
			Config.SetBool("ShowFPS", !config);
			this.ButtonShowFPS.SetModeText(InterString.Get(config ? "关" : "开", 0));
			SettingServantUI.ChangeShowFPS();
		}

		// Token: 0x06009946 RID: 39238 RVA: 0x0016BB90 File Offset: 0x00169D90
		public static void ChangeShowFPS()
		{
			if (Config.GetBool("ShowFPS", true))
			{
				UIManager.ShowFPS();
				return;
			}
			UIManager.HideFPS();
		}

		// Token: 0x06009947 RID: 39239 RVA: 0x0016BBAC File Offset: 0x00169DAC
		private void InitializeRumble()
		{
			this.ButtonRumble.SetClickEvent(new UnityAction(this.OnRumbleClicked));
			bool config = Config.GetBool("Rumble", true);
			this.ButtonRumble.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x06009948 RID: 39240 RVA: 0x0016BBFC File Offset: 0x00169DFC
		private void OnRumbleClicked()
		{
			bool config = Config.GetBool("Rumble", true);
			this.ButtonRumble.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("Rumble", !config);
		}

		// Token: 0x06009949 RID: 39241 RVA: 0x0016BC44 File Offset: 0x00169E44
		private void InitializeConfirm()
		{
			this.ButtonConfirm.SetClickEvent(new UnityAction(this.OnConfirmClicked));
			bool config = Config.GetBool("Confirm", false);
			this.ButtonConfirm.SetModeText(InterString.Get(config ? "左" : "右", 0));
		}

		// Token: 0x0600994A RID: 39242 RVA: 0x0016BC94 File Offset: 0x00169E94
		private void OnConfirmClicked()
		{
			bool config = Config.GetBool("Confirm", false);
			this.ButtonConfirm.SetModeText(InterString.Get(config ? "右" : "左", 0));
			Config.SetBool("Confirm", !config);
		}

		// Token: 0x0600994B RID: 39243 RVA: 0x0016BCDB File Offset: 0x00169EDB
		private void InitializeLayout()
		{
			this.ButtonLayout.SetClickEvent(new UnityAction(this.OnLayoutClicked));
			this.SetLayoutMode(Config.GetFloat("Layout", 0f));
		}

		// Token: 0x0600994C RID: 39244 RVA: 0x0016BD0C File Offset: 0x00169F0C
		private void OnLayoutClicked()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能更改此选项。", 0));
				return;
			}
			UIManager.ShowPopupSelection(new List<string>
			{
				InterString.Get("UI布局", 0),
				string.Empty,
				InterString.Get("自动判断", 0),
				InterString.Get("桌面布局", 0),
				InterString.Get("移动布局", 0)
			}, new Action(this.LayoutChange), null);
		}

		// Token: 0x0600994D RID: 39245 RVA: 0x0016BDA4 File Offset: 0x00169FA4
		private void LayoutChange()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			this.ButtonLayout.SetModeText(selected);
			float config = 0f;
			if (selected == InterString.Get("桌面布局", 0))
			{
				config = 1f;
			}
			else if (selected == InterString.Get("移动布局", 0))
			{
				config = 2f;
			}
			Config.SetFloat("Layout", config);
			UIManager.ChangeLayout();
		}

		// Token: 0x0600994E RID: 39246 RVA: 0x0016BE1C File Offset: 0x0016A01C
		private void SetLayoutMode(float config)
		{
			string value = InterString.Get("自动判断", 0);
			if (config == 1f)
			{
				value = InterString.Get("桌面布局", 0);
			}
			else if (config == 2f)
			{
				value = InterString.Get("移动布局", 0);
			}
			this.ButtonLayout.SetModeText(value);
		}

		// Token: 0x0600994F RID: 39247 RVA: 0x0016BE6B File Offset: 0x0016A06B
		private void InitializeBackground()
		{
			this.ButtonBackground.SetClickEvent(new UnityAction(this.OnBackgroundClicked));
			this.ChangeBackgroundModeText();
		}

		// Token: 0x06009950 RID: 39248 RVA: 0x0016BE8C File Offset: 0x0016A08C
		private void ChangeBackgroundModeText()
		{
			int id = int.Parse(Config.Get("Background", "0"));
			string value = InterString.Get("随机", 0);
			if (id != 0 && !BackgroundManager.backgrounds.TryGetValue(id, out value))
			{
				value = "Classic";
			}
			if (string.IsNullOrEmpty(value))
			{
				value = InterString.Get("随机", 0);
			}
			this.ButtonBackground.SetModeText(value);
		}

		// Token: 0x06009951 RID: 39249 RVA: 0x0016BEF4 File Offset: 0x0016A0F4
		private void OnBackgroundClicked()
		{
			List<string> selections = new List<string>
			{
				InterString.Get("更换背景", 0),
				string.Empty,
				InterString.Get("随机", 0)
			};
			foreach (KeyValuePair<int, string> background in BackgroundManager.backgrounds)
			{
				selections.Add(background.Value);
			}
			UIManager.ShowPopupSelection(selections, new Action(this.BackgroundChange), null);
		}

		// Token: 0x06009952 RID: 39250 RVA: 0x0016BF94 File Offset: 0x0016A194
		private void BackgroundChange()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			int id = Program.instance.background_.GetIDByName(selected);
			Config.Set("Background", id.ToString());
			Program.instance.background_.Change(id);
			this.ChangeBackgroundModeText();
		}

		// Token: 0x06009953 RID: 39251 RVA: 0x0016BFF0 File Offset: 0x0016A1F0
		private void InitializeBgmBy()
		{
			this.ButtonBgmBy.SetClickEvent(new UnityAction(this.OnBgmByClicked));
			bool config = Config.GetBool("BGMbyMySide", true);
			this.ButtonBgmBy.SetModeText(InterString.Get(config ? "我方" : "对方", 0));
		}

		// Token: 0x06009954 RID: 39252 RVA: 0x0016C040 File Offset: 0x0016A240
		private void OnBgmByClicked()
		{
			bool config = Config.GetBool("BGMbyMySide", true);
			this.ButtonBgmBy.SetModeText(InterString.Get(config ? "对方" : "我方", 0));
			Config.SetBool("BGMbyMySide", !config);
		}

		// Token: 0x06009955 RID: 39253 RVA: 0x0016C088 File Offset: 0x0016A288
		private void InitializeCardStyle()
		{
			this.ButtonCardStyle.SetClickEvent(new UnityAction(this.OnCardStyleChange));
			this.ButtonCardStyle.SetModeText(Config.Get("CardStyle", CardRenderer.CardStyle.OCG_TCG.ToString()));
		}

		// Token: 0x06009956 RID: 39254 RVA: 0x0016C0D0 File Offset: 0x0016A2D0
		private void OnCardStyleChange()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能更改此选项。", 0));
				return;
			}
			List<string> selections = new List<string>
			{
				InterString.Get("卡图风格", 0),
				string.Empty
			};
			foreach (object value in Enum.GetValues(typeof(CardRenderer.CardStyle)))
			{
				selections.Add(value.ToString());
			}
			UIManager.ShowPopupSelection(selections, new Action(this.ChangeCardStyle), null);
		}

		// Token: 0x06009957 RID: 39255 RVA: 0x0016C18C File Offset: 0x0016A38C
		private void ChangeCardStyle()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			Config.Set("CardStyle", selected);
			this.ButtonCardStyle.SetModeText(selected);
			UIManager.ChangeLanguage();
		}

		// Token: 0x06009958 RID: 39256 RVA: 0x0016C1CC File Offset: 0x0016A3CC
		private void InitializeVideoCard()
		{
			this.ButtonVideoCard.SetClickEvent(new UnityAction(this.OnVideoCardClicked));
			bool config = Config.GetBool("VideoCard", true);
			this.ButtonVideoCard.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x06009959 RID: 39257 RVA: 0x0016C21C File Offset: 0x0016A41C
		private void OnVideoCardClicked()
		{
			bool config = Config.GetBool("VideoCard", true);
			Config.SetBool("VideoCard", !config);
			this.ButtonVideoCard.SetModeText(InterString.Get(config ? "关" : "开", 0));
			this.videoCardConfigChanged = true;
		}

		// Token: 0x0600995A RID: 39258 RVA: 0x0016C26A File Offset: 0x0016A46A
		private void InitializeCardLanguage()
		{
			this.ButtonCardLanguage.SetClickEvent(new UnityAction(this.OnCardLanguageClicked));
			this.ButtonCardLanguage.SetModeText(InterString.Get(Language.GetCardConfig(), 0));
		}

		// Token: 0x0600995B RID: 39259 RVA: 0x0016C29C File Offset: 0x0016A49C
		private void OnCardLanguageClicked()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能更改此选项。", 0));
				return;
			}
			List<string> selections = new List<string>
			{
				InterString.Get("卡图语言", 0),
				string.Empty
			};
			foreach (DirectoryInfo info in new DirectoryInfo("Data/locales/").GetDirectories())
			{
				selections.Add(InterString.Get(info.Name, 0));
			}
			UIManager.ShowPopupSelection(selections, new Action(this.ChangeCardLanguage), null);
		}

		// Token: 0x0600995C RID: 39260 RVA: 0x0016C334 File Offset: 0x0016A534
		private void ChangeCardLanguage()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			this.ButtonCardLanguage.SetModeText(selected);
			Language.SetCardConfig(InterString.GetOriginal(selected));
			UIManager.ChangeLanguage();
		}

		// Token: 0x0600995D RID: 39261 RVA: 0x0016C372 File Offset: 0x0016A572
		private void InitializeLanguage()
		{
			this.ButtonLanguage.SetClickEvent(new UnityAction(this.OnLanguageClicked));
			this.ButtonLanguage.SetModeText(InterString.Get(Language.GetConfig(), 0));
		}

		// Token: 0x0600995E RID: 39262 RVA: 0x0016C3A4 File Offset: 0x0016A5A4
		private void OnLanguageClicked()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能更改此选项。", 0));
				return;
			}
			List<string> selections = new List<string>
			{
				InterString.Get("语言", 0),
				string.Empty
			};
			foreach (DirectoryInfo info in new DirectoryInfo("Data/locales/").GetDirectories())
			{
				selections.Add(InterString.Get(info.Name, 0));
			}
			UIManager.ShowPopupSelection(selections, new Action(this.OnLanguageSelection), null);
		}

		// Token: 0x0600995F RID: 39263 RVA: 0x0016C43C File Offset: 0x0016A63C
		private void OnLanguageSelection()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			this.ButtonLanguage.SetModeText(selected);
			Language.SetConfig(InterString.GetOriginal(selected));
			UIManager.ChangeLanguage();
		}

		// Token: 0x06009960 RID: 39264 RVA: 0x0016C47C File Offset: 0x0016A67C
		private void InitializeAppearance()
		{
			this.ButtonDuelAppearance.SetClickEvent(new UnityAction(this.OnDuelAppearcanceClick));
			this.ButtonWatchAppearance.SetClickEvent(new UnityAction(this.OnWatchAppearcanceClick));
			this.ButtonReplayAppearance.SetClickEvent(new UnityAction(this.OnReplayAppearcanceClick));
			this.RefreshAppearanceModeText();
		}

		// Token: 0x06009961 RID: 39265 RVA: 0x0016C4D4 File Offset: 0x0016A6D4
		private void OnDuelAppearcanceClick()
		{
			Program.instance.appearance.SwitchCondition(Appearance.Condition.Duel);
			if (Program.instance.currentSubServant == Program.instance.setting)
			{
				Program.instance.ShowSubServant(Program.instance.appearance);
				return;
			}
			Program.instance.ShiftToServant(Program.instance.appearance);
		}

		// Token: 0x06009962 RID: 39266 RVA: 0x0016C538 File Offset: 0x0016A738
		private void OnWatchAppearcanceClick()
		{
			Program.instance.appearance.SwitchCondition(Appearance.Condition.Watch);
			if (Program.instance.currentSubServant == Program.instance.setting)
			{
				Program.instance.ShowSubServant(Program.instance.appearance);
				return;
			}
			Program.instance.ShiftToServant(Program.instance.appearance);
		}

		// Token: 0x06009963 RID: 39267 RVA: 0x0016C59C File Offset: 0x0016A79C
		private void OnReplayAppearcanceClick()
		{
			Program.instance.appearance.SwitchCondition(Appearance.Condition.Replay);
			if (Program.instance.currentSubServant == Program.instance.setting)
			{
				Program.instance.ShowSubServant(Program.instance.appearance);
				return;
			}
			Program.instance.ShiftToServant(Program.instance.appearance);
		}

		// Token: 0x06009964 RID: 39268 RVA: 0x0016C600 File Offset: 0x0016A800
		public void RefreshAppearanceModeText()
		{
			this.ButtonDuelAppearance.SetModeText(Config.Get("DuelPlayerName0", "@ui"));
			this.ButtonWatchAppearance.SetModeText(Config.Get("WatchPlayerName0", "@ui"));
			this.ButtonReplayAppearance.SetModeText(Config.Get("ReplayPlayerName0", "@ui"));
		}

		// Token: 0x06009965 RID: 39269 RVA: 0x0016C65C File Offset: 0x0016A85C
		private void InitializeCharacter()
		{
			this.ButtonDuelCharacter.SetClickEvent(new UnityAction(this.OnDuelCharacterClick));
			this.ButtonWatchCharacter.SetClickEvent(new UnityAction(this.OnWatchCharacterClick));
			this.ButtonReplayCharacter.SetClickEvent(new UnityAction(this.OnReplayCharacterClick));
			this.RefreshCharacterName();
		}

		// Token: 0x06009966 RID: 39270 RVA: 0x0016C6B4 File Offset: 0x0016A8B4
		private void OnDuelCharacterClick()
		{
			Program.instance.character.SwitchCondition(CharacterSelector.Condition.Duel);
			if (Program.instance.currentSubServant == Program.instance.setting)
			{
				Program.instance.ShowSubServant(Program.instance.character);
				return;
			}
			Program.instance.ShiftToServant(Program.instance.character);
		}

		// Token: 0x06009967 RID: 39271 RVA: 0x0016C718 File Offset: 0x0016A918
		private void OnWatchCharacterClick()
		{
			Program.instance.character.SwitchCondition(CharacterSelector.Condition.Watch);
			if (Program.instance.currentSubServant == Program.instance.setting)
			{
				Program.instance.ShowSubServant(Program.instance.character);
				return;
			}
			Program.instance.ShiftToServant(Program.instance.character);
		}

		// Token: 0x06009968 RID: 39272 RVA: 0x0016C77C File Offset: 0x0016A97C
		private void OnReplayCharacterClick()
		{
			Program.instance.character.SwitchCondition(CharacterSelector.Condition.Replay);
			if (Program.instance.currentSubServant == Program.instance.setting)
			{
				Program.instance.ShowSubServant(Program.instance.character);
				return;
			}
			Program.instance.ShiftToServant(Program.instance.character);
		}

		// Token: 0x06009969 RID: 39273 RVA: 0x0016C7E0 File Offset: 0x0016A9E0
		public void RefreshCharacterName()
		{
			if (CharacterSelector.characters == null)
			{
				return;
			}
			string characterName = CharacterSelector.characters.GetName(Config.Get("DuelCharacter0", "0001"));
			this.ButtonDuelCharacter.SetModeText(characterName);
			characterName = CharacterSelector.characters.GetName(Config.Get("WatchCharacter0", "0001"));
			this.ButtonWatchCharacter.SetModeText(characterName);
			characterName = CharacterSelector.characters.GetName(Config.Get("ReplayCharacter0", "0001"));
			this.ButtonReplayCharacter.SetModeText(characterName);
		}

		// Token: 0x0600996A RID: 39274 RVA: 0x0016C870 File Offset: 0x0016AA70
		private void InitializeVoice()
		{
			bool config = Config.GetBool("DuelVoice", false);
			this.ButtonDuelVoice.SetClickEvent(new UnityAction(this.OnDuelVoiceClick));
			this.ButtonDuelVoice.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchVoice", false);
			this.ButtonWatchVoice.SetClickEvent(new UnityAction(this.OnWatchVoiceClick));
			this.ButtonWatchVoice.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayVoice", false);
			this.ButtonReplayVoice.SetClickEvent(new UnityAction(this.OnReplayVoiceClick));
			this.ButtonReplayVoice.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x0600996B RID: 39275 RVA: 0x0016C948 File Offset: 0x0016AB48
		private void OnDuelVoiceClick()
		{
			bool config = Config.GetBool("DuelVoice", false);
			this.ButtonDuelVoice.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelVoice", !config);
			Program.instance.ocgcore.CheckCharaFace();
		}

		// Token: 0x0600996C RID: 39276 RVA: 0x0016C9A0 File Offset: 0x0016ABA0
		private void OnWatchVoiceClick()
		{
			bool config = Config.GetBool("WatchVoice", false);
			this.ButtonWatchVoice.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchVoice", !config);
			Program.instance.ocgcore.CheckCharaFace();
		}

		// Token: 0x0600996D RID: 39277 RVA: 0x0016C9F8 File Offset: 0x0016ABF8
		private void OnReplayVoiceClick()
		{
			bool config = Config.GetBool("ReplayVoice", false);
			this.ButtonReplayVoice.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayVoice", !config);
			Program.instance.ocgcore.CheckCharaFace();
		}

		// Token: 0x0600996E RID: 39278 RVA: 0x0016CA50 File Offset: 0x0016AC50
		private void InitializeCloseup()
		{
			bool config = Config.GetBool("DuelCloseup", false);
			this.ButtonDuelCloseup.SetClickEvent(new UnityAction(this.OnDuelCloseupClick));
			this.ButtonDuelCloseup.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchCloseup", false);
			this.ButtonWatchCloseup.SetClickEvent(new UnityAction(this.OnWatchCloseupClick));
			this.ButtonWatchCloseup.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayCloseup", false);
			this.ButtonReplayCloseup.SetClickEvent(new UnityAction(this.OnReplayCloseupClick));
			this.ButtonReplayCloseup.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x0600996F RID: 39279 RVA: 0x0016CB28 File Offset: 0x0016AD28
		private void OnDuelCloseupClick()
		{
			bool config = Config.GetBool("DuelCloseup", false);
			this.ButtonDuelCloseup.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelCloseup", !config);
			Program.instance.ocgcore.RefreshAllCardsLabel();
		}

		// Token: 0x06009970 RID: 39280 RVA: 0x0016CB80 File Offset: 0x0016AD80
		private void OnWatchCloseupClick()
		{
			bool config = Config.GetBool("WatchCloseup", false);
			this.ButtonWatchCloseup.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchCloseup", !config);
			Program.instance.ocgcore.RefreshAllCardsLabel();
		}

		// Token: 0x06009971 RID: 39281 RVA: 0x0016CBD8 File Offset: 0x0016ADD8
		private void OnReplayCloseupClick()
		{
			bool config = Config.GetBool("ReplayCloseup", false);
			this.ButtonReplayCloseup.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayCloseup", !config);
			Program.instance.ocgcore.RefreshAllCardsLabel();
		}

		// Token: 0x06009972 RID: 39282 RVA: 0x0016CC30 File Offset: 0x0016AE30
		private void InitializeSummon()
		{
			SelectionButton_Setting element = base.Manager.GetElement<SelectionButton_Setting>("DuelSummon");
			element.SetClickEvent(new UnityAction(this.OnDuelSummonClick));
			element.SetModeText(InterString.Get(Config.GetBool("DuelSummon", true) ? "开" : "关", 0));
			SelectionButton_Setting element2 = base.Manager.GetElement<SelectionButton_Setting>("WatchSummon");
			element2.SetClickEvent(new UnityAction(this.OnWatchSummonClick));
			element2.SetModeText(InterString.Get(Config.GetBool("WatchSummon", true) ? "开" : "关", 0));
			SelectionButton_Setting element3 = base.Manager.GetElement<SelectionButton_Setting>("ReplaySummon");
			element3.SetClickEvent(new UnityAction(this.OnReplaySummonClick));
			element3.SetModeText(InterString.Get(Config.GetBool("ReplaySummon", true) ? "开" : "关", 0));
		}

		// Token: 0x06009973 RID: 39283 RVA: 0x0016CD18 File Offset: 0x0016AF18
		private void OnDuelSummonClick()
		{
			SelectionButton_Setting element = base.Manager.GetElement<SelectionButton_Setting>("DuelSummon");
			bool config = Config.GetBool("DuelSummon", true);
			element.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelSummon", !config);
		}

		// Token: 0x06009974 RID: 39284 RVA: 0x0016CD6C File Offset: 0x0016AF6C
		private void OnWatchSummonClick()
		{
			SelectionButton_Setting element = base.Manager.GetElement<SelectionButton_Setting>("WatchSummon");
			bool config = Config.GetBool("WatchSummon", true);
			element.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchSummon", !config);
		}

		// Token: 0x06009975 RID: 39285 RVA: 0x0016CDC0 File Offset: 0x0016AFC0
		private void OnReplaySummonClick()
		{
			SelectionButton_Setting element = base.Manager.GetElement<SelectionButton_Setting>("ReplaySummon");
			bool config = Config.GetBool("ReplaySummon", true);
			element.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplaySummon", !config);
		}

		// Token: 0x06009976 RID: 39286 RVA: 0x0016CE14 File Offset: 0x0016B014
		private void InitializePendulum()
		{
			bool config = Config.GetBool("DuelPendulum", true);
			this.ButtonDuelPendulum.SetClickEvent(new UnityAction(this.OnDuelPendulumClick));
			this.ButtonDuelPendulum.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchPendulum", true);
			this.ButtonWatchPendulum.SetClickEvent(new UnityAction(this.OnWatchPendulumClick));
			this.ButtonWatchPendulum.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayPendulum", true);
			this.ButtonReplayPendulum.SetClickEvent(new UnityAction(this.OnReplayPendulumClick));
			this.ButtonReplayPendulum.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x06009977 RID: 39287 RVA: 0x0016CEEC File Offset: 0x0016B0EC
		private void OnDuelPendulumClick()
		{
			bool config = Config.GetBool("DuelPendulum", true);
			this.ButtonDuelPendulum.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelPendulum", !config);
		}

		// Token: 0x06009978 RID: 39288 RVA: 0x0016CF34 File Offset: 0x0016B134
		private void OnWatchPendulumClick()
		{
			bool config = Config.GetBool("WatchPendulum", true);
			this.ButtonWatchPendulum.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchPendulum", !config);
		}

		// Token: 0x06009979 RID: 39289 RVA: 0x0016CF7C File Offset: 0x0016B17C
		private void OnReplayPendulumClick()
		{
			bool config = Config.GetBool("ReplayPendulum", true);
			this.ButtonReplayPendulum.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayPendulum", !config);
		}

		// Token: 0x0600997A RID: 39290 RVA: 0x0016CFC4 File Offset: 0x0016B1C4
		private void InitializeCutin()
		{
			bool config = Config.GetBool("DuelCutin", true);
			this.ButtonDuelCutin.SetClickEvent(new UnityAction(this.OnDuelCutinClick));
			this.ButtonDuelCutin.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchCutin", true);
			this.ButtonWatchCutin.SetClickEvent(new UnityAction(this.OnWatchCutinClick));
			this.ButtonWatchCutin.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayCutin", true);
			this.ButtonReplayCutin.SetClickEvent(new UnityAction(this.OnReplayCutinClick));
			this.ButtonReplayCutin.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x0600997B RID: 39291 RVA: 0x0016D09C File Offset: 0x0016B29C
		private void OnDuelCutinClick()
		{
			bool config = Config.GetBool("DuelCutin", true);
			this.ButtonDuelCutin.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelCutin", !config);
		}

		// Token: 0x0600997C RID: 39292 RVA: 0x0016D0E4 File Offset: 0x0016B2E4
		private void OnWatchCutinClick()
		{
			bool config = Config.GetBool("WatchCutin", true);
			this.ButtonWatchCutin.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchCutin", !config);
		}

		// Token: 0x0600997D RID: 39293 RVA: 0x0016D12C File Offset: 0x0016B32C
		private void OnReplayCutinClick()
		{
			bool config = Config.GetBool("ReplayCutin", true);
			this.ButtonReplayCutin.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayCutin", !config);
		}

		// Token: 0x0600997E RID: 39294 RVA: 0x0016D174 File Offset: 0x0016B374
		private void InitializeEffect()
		{
			bool config = Config.GetBool("DuelEffect", true);
			this.ButtonDuelEffect.SetClickEvent(new UnityAction(this.OnDuelEffectClick));
			this.ButtonDuelEffect.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchEffect", true);
			this.ButtonWatchEffect.SetClickEvent(new UnityAction(this.OnWatchEffectClick));
			this.ButtonWatchEffect.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayEffect", true);
			this.ButtonReplayEffect.SetClickEvent(new UnityAction(this.OnReplayEffectClick));
			this.ButtonReplayEffect.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x0600997F RID: 39295 RVA: 0x0016D24C File Offset: 0x0016B44C
		private void OnDuelEffectClick()
		{
			bool config = Config.GetBool("DuelEffect", true);
			this.ButtonDuelEffect.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelEffect", !config);
		}

		// Token: 0x06009980 RID: 39296 RVA: 0x0016D294 File Offset: 0x0016B494
		private void OnWatchEffectClick()
		{
			bool config = Config.GetBool("WatchEffect", true);
			this.ButtonWatchEffect.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchEffect", !config);
		}

		// Token: 0x06009981 RID: 39297 RVA: 0x0016D2DC File Offset: 0x0016B4DC
		private void OnReplayEffectClick()
		{
			bool config = Config.GetBool("ReplayEffect", true);
			this.ButtonReplayEffect.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayEffect", !config);
		}

		// Token: 0x06009982 RID: 39298 RVA: 0x0016D324 File Offset: 0x0016B524
		private void InitializeChain()
		{
			bool config = Config.GetBool("DuelChain", true);
			this.ButtonDuelChain.SetClickEvent(new UnityAction(this.OnDuelChainClick));
			this.ButtonDuelChain.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchChain", true);
			this.ButtonWatchChain.SetClickEvent(new UnityAction(this.OnWatchChainClick));
			this.ButtonWatchChain.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayChain", true);
			this.ButtonReplayChain.SetClickEvent(new UnityAction(this.OnReplayChainClick));
			this.ButtonReplayChain.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x06009983 RID: 39299 RVA: 0x0016D3FC File Offset: 0x0016B5FC
		private void OnDuelChainClick()
		{
			bool config = Config.GetBool("DuelChain", true);
			this.ButtonDuelChain.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelChain", !config);
		}

		// Token: 0x06009984 RID: 39300 RVA: 0x0016D444 File Offset: 0x0016B644
		private void OnWatchChainClick()
		{
			bool config = Config.GetBool("WatchChain", true);
			this.ButtonWatchChain.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchChain", !config);
		}

		// Token: 0x06009985 RID: 39301 RVA: 0x0016D48C File Offset: 0x0016B68C
		private void OnReplayChainClick()
		{
			bool config = Config.GetBool("ReplayChain", true);
			this.ButtonReplayChain.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayChain", !config);
		}

		// Token: 0x06009986 RID: 39302 RVA: 0x0016D4D4 File Offset: 0x0016B6D4
		private void InitializeDice()
		{
			bool config = Config.GetBool("DuelDice", true);
			this.ButtonDuelDice.SetClickEvent(new UnityAction(this.OnDuelDiceClick));
			this.ButtonDuelDice.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchDice", true);
			this.ButtonWatchDice.SetClickEvent(new UnityAction(this.OnWatchDiceClick));
			this.ButtonWatchDice.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayDice", true);
			this.ButtonReplayDice.SetClickEvent(new UnityAction(this.OnReplayDiceClick));
			this.ButtonReplayDice.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x06009987 RID: 39303 RVA: 0x0016D5AC File Offset: 0x0016B7AC
		private void OnDuelDiceClick()
		{
			bool config = Config.GetBool("DuelDice", true);
			this.ButtonDuelDice.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelDice", !config);
		}

		// Token: 0x06009988 RID: 39304 RVA: 0x0016D5F4 File Offset: 0x0016B7F4
		private void OnWatchDiceClick()
		{
			bool config = Config.GetBool("WatchDice", true);
			this.ButtonWatchDice.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchDice", !config);
		}

		// Token: 0x06009989 RID: 39305 RVA: 0x0016D63C File Offset: 0x0016B83C
		private void OnReplayDiceClick()
		{
			bool config = Config.GetBool("ReplayDice", true);
			this.ButtonReplayDice.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayDice", !config);
		}

		// Token: 0x0600998A RID: 39306 RVA: 0x0016D684 File Offset: 0x0016B884
		private void InitializeCoin()
		{
			bool config = Config.GetBool("DuelCoin", true);
			this.ButtonDuelCoin.SetClickEvent(new UnityAction(this.OnDuelCoinClick));
			this.ButtonDuelCoin.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchCoin", true);
			this.ButtonWatchCoin.SetClickEvent(new UnityAction(this.OnWatchCoinClick));
			this.ButtonWatchCoin.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayCoin", true);
			this.ButtonReplayCoin.SetClickEvent(new UnityAction(this.OnReplayCoinClick));
			this.ButtonReplayCoin.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x0600998B RID: 39307 RVA: 0x0016D75C File Offset: 0x0016B95C
		private void OnDuelCoinClick()
		{
			bool config = Config.GetBool("DuelCoin", true);
			this.ButtonDuelCoin.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelCoin", !config);
		}

		// Token: 0x0600998C RID: 39308 RVA: 0x0016D7A4 File Offset: 0x0016B9A4
		private void OnWatchCoinClick()
		{
			bool config = Config.GetBool("WatchCoin", true);
			this.ButtonWatchCoin.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchCoin", !config);
		}

		// Token: 0x0600998D RID: 39309 RVA: 0x0016D7EC File Offset: 0x0016B9EC
		private void OnReplayCoinClick()
		{
			bool config = Config.GetBool("ReplayCoin", true);
			this.ButtonReplayCoin.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayCoin", !config);
		}

		// Token: 0x0600998E RID: 39310 RVA: 0x0016D834 File Offset: 0x0016BA34
		private void InitializeAutoInfo()
		{
			bool config = Config.GetBool("DuelAutoInfo", true);
			this.ButtonDuelAutoInfo.SetClickEvent(new UnityAction(this.OnDuelAutoInfoClick));
			this.ButtonDuelAutoInfo.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchAutoInfo", true);
			this.ButtonWatchAutoInfo.SetClickEvent(new UnityAction(this.OnWatchAutoInfoClick));
			this.ButtonWatchAutoInfo.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayAutoInfo", true);
			this.ButtonReplayAutoInfo.SetClickEvent(new UnityAction(this.OnReplayAutoInfoClick));
			this.ButtonReplayAutoInfo.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x0600998F RID: 39311 RVA: 0x0016D90C File Offset: 0x0016BB0C
		private void OnDuelAutoInfoClick()
		{
			bool config = Config.GetBool("DuelAutoInfo", true);
			this.ButtonDuelAutoInfo.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelAutoInfo", !config);
		}

		// Token: 0x06009990 RID: 39312 RVA: 0x0016D954 File Offset: 0x0016BB54
		private void OnWatchAutoInfoClick()
		{
			bool config = Config.GetBool("WatchAutoInfo", true);
			this.ButtonWatchAutoInfo.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchAutoInfo", !config);
		}

		// Token: 0x06009991 RID: 39313 RVA: 0x0016D99C File Offset: 0x0016BB9C
		private void OnReplayAutoInfoClick()
		{
			bool config = Config.GetBool("ReplayAutoInfo", true);
			this.ButtonReplayAutoInfo.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayAutoInfo", !config);
		}

		// Token: 0x06009992 RID: 39314 RVA: 0x0016D9E4 File Offset: 0x0016BBE4
		private void InitializeFaceDown()
		{
			bool config = Config.GetBool("DuelFaceDown", true);
			this.ButtonDuelFaceDown.SetClickEvent(new UnityAction(this.OnDuelFaceDownClick));
			this.ButtonDuelFaceDown.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchFaceDown", true);
			this.ButtonWatchFaceDown.SetClickEvent(new UnityAction(this.OnWatchFaceDownClick));
			this.ButtonWatchFaceDown.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayFaceDown", true);
			this.ButtonReplayFaceDown.SetClickEvent(new UnityAction(this.OnReplayFaceDownClick));
			this.ButtonReplayFaceDown.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x06009993 RID: 39315 RVA: 0x0016DABC File Offset: 0x0016BCBC
		private void OnDuelFaceDownClick()
		{
			bool config = Config.GetBool("DuelFaceDown", true);
			this.ButtonDuelFaceDown.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelFaceDown", !config);
			foreach (GameCard gameCard in OcgCore.cards)
			{
				gameCard.ShowFaceDownCardOrNot(gameCard.NeedShowFaceDownCard());
			}
		}

		// Token: 0x06009994 RID: 39316 RVA: 0x0016DB4C File Offset: 0x0016BD4C
		private void OnWatchFaceDownClick()
		{
			bool config = Config.GetBool("WatchFaceDown", true);
			this.ButtonWatchFaceDown.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchFaceDown", !config);
			foreach (GameCard gameCard in OcgCore.cards)
			{
				gameCard.ShowFaceDownCardOrNot(gameCard.NeedShowFaceDownCard());
			}
		}

		// Token: 0x06009995 RID: 39317 RVA: 0x0016DBDC File Offset: 0x0016BDDC
		private void OnReplayFaceDownClick()
		{
			bool config = Config.GetBool("ReplayFaceDown", true);
			this.ButtonReplayFaceDown.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayFaceDown", !config);
			foreach (GameCard gameCard in OcgCore.cards)
			{
				gameCard.ShowFaceDownCardOrNot(gameCard.NeedShowFaceDownCard());
			}
		}

		// Token: 0x06009996 RID: 39318 RVA: 0x0016DC6C File Offset: 0x0016BE6C
		private void InitializePlayerMessage()
		{
			bool config = Config.GetBool("DuelPlayerMessage", true);
			this.ButtonDuelPlayerMessage.SetClickEvent(new UnityAction(this.OnDuelPlayerMessageClick));
			this.ButtonDuelPlayerMessage.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchPlayerMessage", true);
			this.ButtonWatchPlayerMessage.SetClickEvent(new UnityAction(this.OnWatchPlayerMessageClick));
			this.ButtonWatchPlayerMessage.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayPlayerMessage", true);
			this.ButtonReplayPlayerMessage.SetClickEvent(new UnityAction(this.OnReplayPlayerMessageClick));
			this.ButtonReplayPlayerMessage.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x06009997 RID: 39319 RVA: 0x0016DD44 File Offset: 0x0016BF44
		private void OnDuelPlayerMessageClick()
		{
			bool config = Config.GetBool("DuelPlayerMessage", true);
			this.ButtonDuelPlayerMessage.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelPlayerMessage", !config);
		}

		// Token: 0x06009998 RID: 39320 RVA: 0x0016DD8C File Offset: 0x0016BF8C
		private void OnWatchPlayerMessageClick()
		{
			bool config = Config.GetBool("WatchPlayerMessage", true);
			this.ButtonWatchPlayerMessage.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchPlayerMessage", !config);
		}

		// Token: 0x06009999 RID: 39321 RVA: 0x0016DDD4 File Offset: 0x0016BFD4
		private void OnReplayPlayerMessageClick()
		{
			bool config = Config.GetBool("ReplayPlayerMessage", true);
			this.ButtonReplayPlayerMessage.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayPlayerMessage", !config);
		}

		// Token: 0x0600999A RID: 39322 RVA: 0x0016DE1C File Offset: 0x0016C01C
		private void InitializeSystemMessage()
		{
			bool config = Config.GetBool("DuelSystemMessage", true);
			this.ButtonDuelSystemMessage.SetClickEvent(new UnityAction(this.OnDuelSystemMessageClick));
			this.ButtonDuelSystemMessage.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchSystemMessage", true);
			this.ButtonWatchSystemMessage.SetClickEvent(new UnityAction(this.OnWatchSystemMessageClick));
			this.ButtonWatchSystemMessage.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplaySystemMessage", true);
			this.ButtonReplaySystemMessage.SetClickEvent(new UnityAction(this.OnReplaySystemMessageClick));
			this.ButtonReplaySystemMessage.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x0600999B RID: 39323 RVA: 0x0016DEF4 File Offset: 0x0016C0F4
		private void OnDuelSystemMessageClick()
		{
			bool config = Config.GetBool("DuelSystemMessage", true);
			this.ButtonDuelSystemMessage.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelSystemMessage", !config);
		}

		// Token: 0x0600999C RID: 39324 RVA: 0x0016DF3C File Offset: 0x0016C13C
		private void OnWatchSystemMessageClick()
		{
			bool config = Config.GetBool("WatchSystemMessage", true);
			this.ButtonWatchSystemMessage.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchSystemMessage", !config);
		}

		// Token: 0x0600999D RID: 39325 RVA: 0x0016DF84 File Offset: 0x0016C184
		private void OnReplaySystemMessageClick()
		{
			bool config = Config.GetBool("ReplaySystemMessage", true);
			this.ButtonReplaySystemMessage.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplaySystemMessage", !config);
		}

		// Token: 0x0600999E RID: 39326 RVA: 0x0016DFCC File Offset: 0x0016C1CC
		private void InitializeAcc()
		{
			float config = Config.GetFloat("DuelAcc", 2f);
			this.ButtonDuelAcc.SetSliderEvent(new UnityAction<float>(this.OnDuelAccChange));
			this.ButtonDuelAcc.SetSliderValue(config);
			config = Config.GetFloat("WatchAcc", 2f);
			this.ButtonWatchAcc.SetSliderEvent(new UnityAction<float>(this.OnWatchAccChange));
			this.ButtonWatchAcc.SetSliderValue(config);
			config = Config.GetFloat("ReplayAcc", 2f);
			this.ButtonReplayAcc.SetSliderEvent(new UnityAction<float>(this.OnReplayAccChange));
			this.ButtonReplayAcc.SetSliderValue(config);
		}

		// Token: 0x0600999F RID: 39327 RVA: 0x0016E074 File Offset: 0x0016C274
		private void OnDuelAccChange(float value)
		{
			string result = value.ToString();
			this.ButtonDuelAcc.SetModeText((result.Length > 4) ? result.Substring(0, 4) : result);
			if (Program.instance.ocgcore.showing && OcgCore.condition == OcgCore.Condition.Duel && OcgCore.Accing)
			{
				Program.instance.ocgcore.GetUI<OcgCoreUI>().OnAcc();
			}
		}

		// Token: 0x060099A0 RID: 39328 RVA: 0x0016E0DC File Offset: 0x0016C2DC
		private void OnWatchAccChange(float value)
		{
			string result = value.ToString();
			this.ButtonWatchAcc.SetModeText((result.Length > 4) ? result.Substring(0, 4) : result);
			if (Program.instance.ocgcore.showing && OcgCore.condition == OcgCore.Condition.Watch && OcgCore.Accing)
			{
				Program.instance.ocgcore.GetUI<OcgCoreUI>().OnAcc();
			}
		}

		// Token: 0x060099A1 RID: 39329 RVA: 0x0016E144 File Offset: 0x0016C344
		private void OnReplayAccChange(float value)
		{
			string result = value.ToString();
			this.ButtonReplayAcc.SetModeText((result.Length > 4) ? result.Substring(0, 4) : result);
			if (Program.instance.ocgcore.showing && OcgCore.condition == OcgCore.Condition.Replay && OcgCore.Accing)
			{
				Program.instance.ocgcore.GetUI<OcgCoreUI>().OnAcc();
			}
		}

		// Token: 0x060099A2 RID: 39330 RVA: 0x0016E1AC File Offset: 0x0016C3AC
		private void InitializeAutoAcc()
		{
			bool config = Config.GetBool("DuelAutoAcc", true);
			this.ButtonDuelAutoAcc.SetClickEvent(new UnityAction(this.OnDuelAutoAccClick));
			this.ButtonDuelAutoAcc.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("WatchAutoAcc", true);
			this.ButtonWatchAutoAcc.SetClickEvent(new UnityAction(this.OnWatchAutoAccClick));
			this.ButtonWatchAutoAcc.SetModeText(InterString.Get(config ? "开" : "关", 0));
			config = Config.GetBool("ReplayAutoAcc", true);
			this.ButtonReplayAutoAcc.SetClickEvent(new UnityAction(this.OnReplayAutoAccClick));
			this.ButtonReplayAutoAcc.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x060099A3 RID: 39331 RVA: 0x0016E284 File Offset: 0x0016C484
		private void OnDuelAutoAccClick()
		{
			bool config = Config.GetBool("DuelAutoAcc", true);
			this.ButtonDuelAutoAcc.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("DuelAutoAcc", !config);
		}

		// Token: 0x060099A4 RID: 39332 RVA: 0x0016E2CC File Offset: 0x0016C4CC
		private void OnWatchAutoAccClick()
		{
			bool config = Config.GetBool("WatchAutoAcc", true);
			this.ButtonWatchAutoAcc.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("WatchAutoAcc", !config);
		}

		// Token: 0x060099A5 RID: 39333 RVA: 0x0016E314 File Offset: 0x0016C514
		private void OnReplayAutoAccClick()
		{
			bool config = Config.GetBool("ReplayAutoAcc", true);
			this.ButtonReplayAutoAcc.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("ReplayAutoAcc", !config);
		}

		// Token: 0x060099A6 RID: 39334 RVA: 0x0016E35C File Offset: 0x0016C55C
		private void InitializeTimming()
		{
			bool config = Config.GetBool("Timing", false);
			this.ButtonTiming.SetClickEvent(new UnityAction(this.OnTimmingClick));
			this.ButtonTiming.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x060099A7 RID: 39335 RVA: 0x0016E3AC File Offset: 0x0016C5AC
		private void OnTimmingClick()
		{
			bool config = Config.GetBool("Timing", false);
			this.ButtonTiming.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("Timing", !config);
		}

		// Token: 0x060099A8 RID: 39336 RVA: 0x0016E3F4 File Offset: 0x0016C5F4
		private void InitializeAutoRPS()
		{
			bool config = Config.GetBool("AutoRPS", false);
			this.ButtonAutoRPS.SetClickEvent(new UnityAction(this.OnAutoRPSClick));
			this.ButtonAutoRPS.SetModeText(InterString.Get(config ? "开" : "关", 0));
		}

		// Token: 0x060099A9 RID: 39337 RVA: 0x0016E444 File Offset: 0x0016C644
		private void OnAutoRPSClick()
		{
			bool config = Config.GetBool("AutoRPS", false);
			this.ButtonAutoRPS.SetModeText(InterString.Get(config ? "关" : "开", 0));
			Config.SetBool("AutoRPS", !config);
		}

		// Token: 0x060099AA RID: 39338 RVA: 0x0016E48B File Offset: 0x0016C68B
		public void OnImport()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能进行此操作。", 0));
				return;
			}
			PortHelper.ImportFiles();
		}

		// Token: 0x060099AB RID: 39339 RVA: 0x0016E4B4 File Offset: 0x0016C6B4
		public void OnImportBG()
		{
			PortHelper.ImportBG();
		}

		// Token: 0x060099AC RID: 39340 RVA: 0x0016E4BB File Offset: 0x0016C6BB
		public void OnExportDeck()
		{
			PortHelper.ExportAllDecks();
		}

		// Token: 0x060099AD RID: 39341 RVA: 0x0016E4C2 File Offset: 0x0016C6C2
		public void OnExportReplay()
		{
			PortHelper.ExportAllReplays();
		}

		// Token: 0x060099AE RID: 39342 RVA: 0x0016E4C9 File Offset: 0x0016C6C9
		public void OnExportPicture()
		{
			PortHelper.ExportAllPictures();
		}

		// Token: 0x060099AF RID: 39343 RVA: 0x0016E4D0 File Offset: 0x0016C6D0
		public void OnClearPicture()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能进行此操作。", 0));
				return;
			}
			List<string> list = new List<string>();
			list.Add(InterString.Get("确定清空", 0));
			list.Add(InterString.Get("是否确认删除所有导入的卡图？", 0));
			list.Add(InterString.Get("确认", 0));
			list.Add(InterString.Get("取消", 0));
			UIManager.ShowPopupYesOrNo(list, delegate
			{
				if (!Directory.Exists("Picture/Art2/"))
				{
					Directory.CreateDirectory("Picture/Art2/");
				}
				string[] files = Directory.GetFiles("Picture/Art2/");
				for (int i = 0; i < files.Length; i++)
				{
					File.Delete(files[i]);
				}
			}, null);
		}

		// Token: 0x060099B0 RID: 39344 RVA: 0x0016E570 File Offset: 0x0016C770
		public void OnClearArtVideos()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能进行此操作。", 0));
				return;
			}
			List<string> list = new List<string>();
			list.Add(InterString.Get("确定清空", 0));
			list.Add(InterString.Get("是否确认删除所有的动态卡图？@n您可以在系统设置中选择关闭动态卡图的显示。", 0));
			list.Add(InterString.Get("确认", 0));
			list.Add(InterString.Get("取消", 0));
			UIManager.ShowPopupYesOrNo(list, delegate
			{
				if (!Directory.Exists("Video/Art/"))
				{
					Directory.CreateDirectory("Picture/Art2/");
				}
				string[] files = Directory.GetFiles("Video/Art/");
				for (int i = 0; i < files.Length; i++)
				{
					File.Delete(files[i]);
				}
				CardImageLoader.ReloadArtVideos();
			}, null);
		}

		// Token: 0x060099B1 RID: 39345 RVA: 0x0016E610 File Offset: 0x0016C810
		private void InitializeExpansions()
		{
			bool config = Config.GetBool("Expansions", true);
			this.ButtonExpansionSupport.SetModeText(InterString.Get(config ? "是" : "否", 0));
		}

		// Token: 0x060099B2 RID: 39346 RVA: 0x0016E64C File Offset: 0x0016C84C
		public void OnSupportExpansions()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能更改此选项。", 0));
				return;
			}
			bool config = Config.GetBool("Expansions", true);
			this.ButtonExpansionSupport.SetModeText(InterString.Get(config ? "否" : "是", 0));
			Config.SetBool("Expansions", !config);
			Program.instance.InitializeForDataChange();
		}

		// Token: 0x060099B3 RID: 39347 RVA: 0x0016E6C0 File Offset: 0x0016C8C0
		public void OnClearExpansions()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能进行此操作。", 0));
				return;
			}
			List<string> list = new List<string>();
			list.Add(InterString.Get("确定清空", 0));
			list.Add(InterString.Get("是否确认删除所有导入的扩展卡包？", 0));
			list.Add(InterString.Get("确认", 0));
			list.Add(InterString.Get("取消", 0));
			UIManager.ShowPopupYesOrNo(list, delegate
			{
				ZipHelper.Dispose();
				if (!Directory.Exists("Expansions/"))
				{
					Directory.CreateDirectory("Expansions/");
				}
				string[] files = Directory.GetFiles("Expansions/");
				for (int i = 0; i < files.Length; i++)
				{
					File.Delete(files[i]);
				}
				Program.instance.InitializeForDataChange();
			}, null);
		}

		// Token: 0x060099B4 RID: 39348 RVA: 0x0016E75D File Offset: 0x0016C95D
		public void OnUpdatePrerelease()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能进行此操作。", 0));
				return;
			}
			Program.instance.setting.UpdatePrerelease();
		}

		// Token: 0x060099B5 RID: 39349 RVA: 0x0016E790 File Offset: 0x0016C990
		public void OnDownloadYPK()
		{
			if (Program.instance.ocgcore.showing)
			{
				MessageManager.Toast(InterString.Get("决斗中不能进行此操作。", 0));
				return;
			}
			Program.instance.setting.DownloadYPK();
		}

		// Token: 0x060099B6 RID: 39350 RVA: 0x0016E7C4 File Offset: 0x0016C9C4
		public void OnAboutGame()
		{
			Addressables.LoadAssetAsync<TextAsset>("AboutGame").Completed += delegate(AsyncOperationHandle<TextAsset> result)
			{
				UIManager.ShowPopupText(new List<string>
				{
					InterString.Get("关于游戏", 0),
					result.Result.text
				}, HorizontalAlignmentOptions.Center);
			};
		}

		// Token: 0x060099B7 RID: 39351 RVA: 0x0016E804 File Offset: 0x0016CA04
		public void OnAboutVersion()
		{
			Addressables.LoadAssetAsync<TextAsset>("AboutVersion").Completed += delegate(AsyncOperationHandle<TextAsset> result)
			{
				UIManager.ShowPopupText(new List<string>
				{
					InterString.Get("关于版本号", 0),
					result.Result.text
				}, HorizontalAlignmentOptions.Left);
			};
		}

		// Token: 0x060099B8 RID: 39352 RVA: 0x0016E844 File Offset: 0x0016CA44
		public void OnAboutUpdate()
		{
			Addressables.LoadAssetAsync<TextAsset>("AboutUpdate").Completed += delegate(AsyncOperationHandle<TextAsset> result)
			{
				UIManager.ShowPopupText(new List<string>
				{
					InterString.Get("关于更新", 0),
					result.Result.text
				}, HorizontalAlignmentOptions.Center);
			};
		}

		// Token: 0x060099B9 RID: 39353 RVA: 0x0016E884 File Offset: 0x0016CA84
		public void OnUpdateContent()
		{
			Addressables.LoadAssetAsync<TextAsset>("UpdateContent").Completed += delegate(AsyncOperationHandle<TextAsset> result)
			{
				UIManager.ShowPopupText(new List<string>
				{
					InterString.Get("更新内容", 0),
					result.Result.text
				}, HorizontalAlignmentOptions.Left);
			};
		}

		// Token: 0x060099BA RID: 39354 RVA: 0x0016E8C2 File Offset: 0x0016CAC2
		public void OnSurrender()
		{
			Program.instance.ocgcore.OnDuelResultConfirmed(true);
		}

		// Token: 0x0400D6CF RID: 54991
		[Header("Setting Servant UI")]
		[SerializeField]
		private SelectionToggle_Setting defaultToggle;

		// Token: 0x0400D6D0 RID: 54992
		private const string LABEL_STG_SYSTEM = "ToggleSystem";

		// Token: 0x0400D6D1 RID: 54993
		private SelectionToggle_Setting m_ToggleSystem;

		// Token: 0x0400D6D2 RID: 54994
		private const string LABEL_STG_DUEL = "ToggleDuel";

		// Token: 0x0400D6D3 RID: 54995
		private SelectionToggle_Setting m_ToggleDuel;

		// Token: 0x0400D6D4 RID: 54996
		private const string LABEL_STG_WATCH = "ToggleWatch";

		// Token: 0x0400D6D5 RID: 54997
		private SelectionToggle_Setting m_ToggleWatch;

		// Token: 0x0400D6D6 RID: 54998
		private const string LABEL_STG_REPLAY = "ToggleReplay";

		// Token: 0x0400D6D7 RID: 54999
		private SelectionToggle_Setting m_ToggleReplay;

		// Token: 0x0400D6D8 RID: 55000
		private const string LABEL_STG_PORT = "TogglePort";

		// Token: 0x0400D6D9 RID: 55001
		private SelectionToggle_Setting m_TogglePort;

		// Token: 0x0400D6DA RID: 55002
		private const string LABEL_STG_EXPANSIONS = "ToggleExpansions";

		// Token: 0x0400D6DB RID: 55003
		private SelectionToggle_Setting m_ToggleExpansions;

		// Token: 0x0400D6DC RID: 55004
		private const string LABEL_STG_ABOUT = "ToggleAbout";

		// Token: 0x0400D6DD RID: 55005
		private SelectionToggle_Setting m_ToggleAbout;

		// Token: 0x0400D6DE RID: 55006
		private const string LABEL_SBN_RETRY = "ButtonRetry";

		// Token: 0x0400D6DF RID: 55007
		private SelectionButton m_ButtonRetry;

		// Token: 0x0400D6E0 RID: 55008
		private const string LABEL_SBN_SURRENDER = "ButtonSurrender";

		// Token: 0x0400D6E1 RID: 55009
		private SelectionButton m_ButtonSurrender;

		// Token: 0x0400D6E2 RID: 55010
		private const string LABEL_SBN_BGM = "BGM";

		// Token: 0x0400D6E3 RID: 55011
		private SelectionButton_Setting m_ButtonBGM;

		// Token: 0x0400D6E4 RID: 55012
		private const string LABEL_SBN_SE = "SE";

		// Token: 0x0400D6E5 RID: 55013
		private SelectionButton_Setting m_ButtonSE;

		// Token: 0x0400D6E6 RID: 55014
		private const string LABEL_SBN_VOICE = "Voice";

		// Token: 0x0400D6E7 RID: 55015
		private SelectionButton_Setting m_ButtonVoice;

		// Token: 0x0400D6E8 RID: 55016
		private const string LABEL_SBN_SCREENMODE = "ScreenMode";

		// Token: 0x0400D6E9 RID: 55017
		private SelectionButton_Setting m_ButtonScreenMode;

		// Token: 0x0400D6EA RID: 55018
		private const string LABEL_SBN_RESOLUTION = "Resolution";

		// Token: 0x0400D6EB RID: 55019
		private SelectionButton_Setting m_ButtonResolution;

		// Token: 0x0400D6EC RID: 55020
		private const string LABEL_SBN_SCALE = "Scale";

		// Token: 0x0400D6ED RID: 55021
		private SelectionButton_Setting m_ButtonScale;

		// Token: 0x0400D6EE RID: 55022
		private const string LABEL_SBN_QUALITY = "Quality";

		// Token: 0x0400D6EF RID: 55023
		private SelectionButton_Setting m_ButtonQuality;

		// Token: 0x0400D6F0 RID: 55024
		private const string LABEL_SBN_FAA = "FAA";

		// Token: 0x0400D6F1 RID: 55025
		private SelectionButton_Setting m_ButtonFAA;

		// Token: 0x0400D6F2 RID: 55026
		private const string LABEL_SBN_AAA = "AAA";

		// Token: 0x0400D6F3 RID: 55027
		private SelectionButton_Setting m_ButtonAAA;

		// Token: 0x0400D6F4 RID: 55028
		private const string LABEL_SBN_SHADOW = "Shadow";

		// Token: 0x0400D6F5 RID: 55029
		private SelectionButton_Setting m_ButtonShadow;

		// Token: 0x0400D6F6 RID: 55030
		private const string LABEL_SBN_FPS = "FPS";

		// Token: 0x0400D6F7 RID: 55031
		private SelectionButton_Setting m_ButtonFPS;

		// Token: 0x0400D6F8 RID: 55032
		private const string LABEL_SBN_SHOWFPS = "ShowFPS";

		// Token: 0x0400D6F9 RID: 55033
		private SelectionButton_Setting m_ButtonShowFPS;

		// Token: 0x0400D6FA RID: 55034
		private const string LABEL_SBN_RUMBLE = "Rumble";

		// Token: 0x0400D6FB RID: 55035
		private SelectionButton_Setting m_ButtonRumble;

		// Token: 0x0400D6FC RID: 55036
		private const string LABEL_SBN_Confirm = "Confirm";

		// Token: 0x0400D6FD RID: 55037
		private SelectionButton_Setting m_ButtonConfirm;

		// Token: 0x0400D6FE RID: 55038
		private const string LABEL_SBN_LAYOUT = "Layout";

		// Token: 0x0400D6FF RID: 55039
		private SelectionButton_Setting m_ButtonLayout;

		// Token: 0x0400D700 RID: 55040
		private const string LABEL_SBN_BACKGROUND = "Background";

		// Token: 0x0400D701 RID: 55041
		private SelectionButton_Setting m_ButtonBackground;

		// Token: 0x0400D702 RID: 55042
		private const string LABEL_SBN_BGMBY = "BgmBy";

		// Token: 0x0400D703 RID: 55043
		private SelectionButton_Setting m_ButtonBgmBy;

		// Token: 0x0400D704 RID: 55044
		private const string LABEL_SBN_CARDSTYLE = "CardStyle";

		// Token: 0x0400D705 RID: 55045
		private SelectionButton_Setting m_ButtonCardStyle;

		// Token: 0x0400D706 RID: 55046
		private const string LABEL_SBN_VIDEO_CARD = "VideoCard";

		// Token: 0x0400D707 RID: 55047
		private SelectionButton_Setting m_ButtonVideoCard;

		// Token: 0x0400D708 RID: 55048
		private const string LABEL_SBN_CARDLANGUAGE = "CardLanguage";

		// Token: 0x0400D709 RID: 55049
		private SelectionButton_Setting m_ButtonCardLanguage;

		// Token: 0x0400D70A RID: 55050
		private const string LABEL_SBN_LANGUAGE = "Language";

		// Token: 0x0400D70B RID: 55051
		private SelectionButton_Setting m_ButtonLanguage;

		// Token: 0x0400D70C RID: 55052
		private const string LABEL_SBN_DUELAPPEARANCE = "DuelAppearance";

		// Token: 0x0400D70D RID: 55053
		private SelectionButton_Setting m_ButtonDuelAppearance;

		// Token: 0x0400D70E RID: 55054
		private const string LABEL_SBN_WATCHAPPEARANCE = "WatchAppearance";

		// Token: 0x0400D70F RID: 55055
		private SelectionButton_Setting m_ButtonWatchAppearance;

		// Token: 0x0400D710 RID: 55056
		private const string LABEL_SBN_REPLAYAPPEARANCE = "ReplayAppearance";

		// Token: 0x0400D711 RID: 55057
		private SelectionButton_Setting m_ButtonReplayAppearance;

		// Token: 0x0400D712 RID: 55058
		private const string LABEL_SBN_DUELCHARACTER = "DuelCharacter";

		// Token: 0x0400D713 RID: 55059
		private SelectionButton_Setting m_ButtonDuelCharacter;

		// Token: 0x0400D714 RID: 55060
		private const string LABEL_SBN_WATCHCHARACTER = "WatchCharacter";

		// Token: 0x0400D715 RID: 55061
		private SelectionButton_Setting m_ButtonWatchCharacter;

		// Token: 0x0400D716 RID: 55062
		private const string LABEL_SBN_REPLAYCHARACTER = "ReplayCharacter";

		// Token: 0x0400D717 RID: 55063
		private SelectionButton_Setting m_ButtonReplayCharacter;

		// Token: 0x0400D718 RID: 55064
		private const string LABEL_SBN_DUELVOICE = "DuelVoice";

		// Token: 0x0400D719 RID: 55065
		private SelectionButton_Setting m_ButtonDuelVoice;

		// Token: 0x0400D71A RID: 55066
		private const string LABEL_SBN_WATCHVOICE = "WatchVoice";

		// Token: 0x0400D71B RID: 55067
		private SelectionButton_Setting m_ButtonWatchVoice;

		// Token: 0x0400D71C RID: 55068
		private const string LABEL_SBN_REPLAYVOICE = "ReplayVoice";

		// Token: 0x0400D71D RID: 55069
		private SelectionButton_Setting m_ButtonReplayVoice;

		// Token: 0x0400D71E RID: 55070
		private const string LABEL_SBN_DUELCLOSEUP = "DuelCloseup";

		// Token: 0x0400D71F RID: 55071
		private SelectionButton_Setting m_ButtonDuelCloseup;

		// Token: 0x0400D720 RID: 55072
		private const string LABEL_SBN_WATCHCLOSEUP = "WatchCloseup";

		// Token: 0x0400D721 RID: 55073
		private SelectionButton_Setting m_ButtonWatchCloseup;

		// Token: 0x0400D722 RID: 55074
		private const string LABEL_SBN_REPLAYCLOSEUP = "ReplayCloseup";

		// Token: 0x0400D723 RID: 55075
		private SelectionButton_Setting m_ButtonReplayCloseup;

		// Token: 0x0400D724 RID: 55076
		private const string LABEL_SBN_DUELPENDULUM = "DuelPendulum";

		// Token: 0x0400D725 RID: 55077
		private SelectionButton_Setting m_ButtonDuelPendulum;

		// Token: 0x0400D726 RID: 55078
		private const string LABEL_SBN_WATCHPENDULUM = "WatchPendulum";

		// Token: 0x0400D727 RID: 55079
		private SelectionButton_Setting m_ButtonWatchPendulum;

		// Token: 0x0400D728 RID: 55080
		private const string LABEL_SBN_REPLAYPENDULUM = "ReplayPendulum";

		// Token: 0x0400D729 RID: 55081
		private SelectionButton_Setting m_ButtonReplayPendulum;

		// Token: 0x0400D72A RID: 55082
		private const string LABEL_SBN_DUELCUTIN = "DuelCutin";

		// Token: 0x0400D72B RID: 55083
		private SelectionButton_Setting m_ButtonDuelCutin;

		// Token: 0x0400D72C RID: 55084
		private const string LABEL_SBN_WATCHCUTIN = "WatchCutin";

		// Token: 0x0400D72D RID: 55085
		private SelectionButton_Setting m_ButtonWatchCutin;

		// Token: 0x0400D72E RID: 55086
		private const string LABEL_SBN_REPLAYCUTIN = "ReplayCutin";

		// Token: 0x0400D72F RID: 55087
		private SelectionButton_Setting m_ButtonReplayCutin;

		// Token: 0x0400D730 RID: 55088
		private const string LABEL_SBN_DUELEFFECT = "DuelEffect";

		// Token: 0x0400D731 RID: 55089
		private SelectionButton_Setting m_ButtonDuelEffect;

		// Token: 0x0400D732 RID: 55090
		private const string LABEL_SBN_WATCHEFFECT = "WatchEffect";

		// Token: 0x0400D733 RID: 55091
		private SelectionButton_Setting m_ButtonWatchEffect;

		// Token: 0x0400D734 RID: 55092
		private const string LABEL_SBN_REPLAYEFFECT = "ReplayEffect";

		// Token: 0x0400D735 RID: 55093
		private SelectionButton_Setting m_ButtonReplayEffect;

		// Token: 0x0400D736 RID: 55094
		private const string LABEL_SBN_DUELCHAIN = "DuelChain";

		// Token: 0x0400D737 RID: 55095
		private SelectionButton_Setting m_ButtonDuelChain;

		// Token: 0x0400D738 RID: 55096
		private const string LABEL_SBN_WATCHCHAIN = "WatchChain";

		// Token: 0x0400D739 RID: 55097
		private SelectionButton_Setting m_ButtonWatchChain;

		// Token: 0x0400D73A RID: 55098
		private const string LABEL_SBN_REPLAYCHAIN = "ReplayChain";

		// Token: 0x0400D73B RID: 55099
		private SelectionButton_Setting m_ButtonReplayChain;

		// Token: 0x0400D73C RID: 55100
		private const string LABEL_SBN_DUELDICE = "DuelDice";

		// Token: 0x0400D73D RID: 55101
		private SelectionButton_Setting m_ButtonDuelDice;

		// Token: 0x0400D73E RID: 55102
		private const string LABEL_SBN_WATCHDICE = "WatchDice";

		// Token: 0x0400D73F RID: 55103
		private SelectionButton_Setting m_ButtonWatchDice;

		// Token: 0x0400D740 RID: 55104
		private const string LABEL_SBN_REPLAYDICE = "ReplayDice";

		// Token: 0x0400D741 RID: 55105
		private SelectionButton_Setting m_ButtonReplayDice;

		// Token: 0x0400D742 RID: 55106
		private const string LABEL_SBN_DUELCOIN = "DuelCoin";

		// Token: 0x0400D743 RID: 55107
		private SelectionButton_Setting m_ButtonDuelCoin;

		// Token: 0x0400D744 RID: 55108
		private const string LABEL_SBN_WATCHCOIN = "WatchCoin";

		// Token: 0x0400D745 RID: 55109
		private SelectionButton_Setting m_ButtonWatchCoin;

		// Token: 0x0400D746 RID: 55110
		private const string LABEL_SBN_REPLAYCOIN = "ReplayCoin";

		// Token: 0x0400D747 RID: 55111
		private SelectionButton_Setting m_ButtonReplayCoin;

		// Token: 0x0400D748 RID: 55112
		private const string LABEL_SBN_DUELAUTOINFO = "DuelAutoInfo";

		// Token: 0x0400D749 RID: 55113
		private SelectionButton_Setting m_ButtonDuelAutoInfo;

		// Token: 0x0400D74A RID: 55114
		private const string LABEL_SBN_WATCHAUTOINFO = "WatchAutoInfo";

		// Token: 0x0400D74B RID: 55115
		private SelectionButton_Setting m_ButtonWatchAutoInfo;

		// Token: 0x0400D74C RID: 55116
		private const string LABEL_SBN_REPLAYAUTOINFO = "ReplayAutoInfo";

		// Token: 0x0400D74D RID: 55117
		private SelectionButton_Setting m_ButtonReplayAutoInfo;

		// Token: 0x0400D74E RID: 55118
		private const string LABEL_SBN_DUELFACEDOWN = "DuelFaceDown";

		// Token: 0x0400D74F RID: 55119
		private SelectionButton_Setting m_ButtonDuelFaceDown;

		// Token: 0x0400D750 RID: 55120
		private const string LABEL_SBN_WATCHFACEDOWN = "WatchFaceDown";

		// Token: 0x0400D751 RID: 55121
		private SelectionButton_Setting m_ButtonWatchFaceDown;

		// Token: 0x0400D752 RID: 55122
		private const string LABEL_SBN_REPLAYFACEDOWN = "ReplayFaceDown";

		// Token: 0x0400D753 RID: 55123
		private SelectionButton_Setting m_ButtonReplayFaceDown;

		// Token: 0x0400D754 RID: 55124
		private const string LABEL_SBN_DUELPLAYERMESSAGE = "DuelPlayerMessage";

		// Token: 0x0400D755 RID: 55125
		private SelectionButton_Setting m_ButtonDuelPlayerMessage;

		// Token: 0x0400D756 RID: 55126
		private const string LABEL_SBN_WATCHPLAYERMESSAGE = "WatchPlayerMessage";

		// Token: 0x0400D757 RID: 55127
		private SelectionButton_Setting m_ButtonWatchPlayerMessage;

		// Token: 0x0400D758 RID: 55128
		private const string LABEL_SBN_REPLAYPLAYERMESSAGE = "ReplayPlayerMessage";

		// Token: 0x0400D759 RID: 55129
		private SelectionButton_Setting m_ButtonReplayPlayerMessage;

		// Token: 0x0400D75A RID: 55130
		private const string LABEL_SBN_DUELSYSTEMMESSAGE = "DuelSystemMessage";

		// Token: 0x0400D75B RID: 55131
		private SelectionButton_Setting m_ButtonDuelSystemMessage;

		// Token: 0x0400D75C RID: 55132
		private const string LABEL_SBN_WATCHSYSTEMMESSAGE = "WatchSystemMessage";

		// Token: 0x0400D75D RID: 55133
		private SelectionButton_Setting m_ButtonWatchSystemMessage;

		// Token: 0x0400D75E RID: 55134
		private const string LABEL_SBN_REPLAYSYSTEMMESSAGE = "ReplaySystemMessage";

		// Token: 0x0400D75F RID: 55135
		private SelectionButton_Setting m_ButtonReplaySystemMessage;

		// Token: 0x0400D760 RID: 55136
		private const string LABEL_SBN_DUELACC = "DuelAcc";

		// Token: 0x0400D761 RID: 55137
		private SelectionButton_Setting m_ButtonDuelAcc;

		// Token: 0x0400D762 RID: 55138
		private const string LABEL_SBN_WATCHACC = "WatchAcc";

		// Token: 0x0400D763 RID: 55139
		private SelectionButton_Setting m_ButtonWatchAcc;

		// Token: 0x0400D764 RID: 55140
		private const string LABEL_SBN_REPLAYACC = "ReplayAcc";

		// Token: 0x0400D765 RID: 55141
		private SelectionButton_Setting m_ButtonReplayAcc;

		// Token: 0x0400D766 RID: 55142
		private const string LABEL_SBN_DUELAUTOACC = "DuelAutoAcc";

		// Token: 0x0400D767 RID: 55143
		private SelectionButton_Setting m_ButtonDuelAutoAcc;

		// Token: 0x0400D768 RID: 55144
		private const string LABEL_SBN_WATCHAUTOACC = "WatchAutoAcc";

		// Token: 0x0400D769 RID: 55145
		private SelectionButton_Setting m_ButtonWatchAutoAcc;

		// Token: 0x0400D76A RID: 55146
		private const string LABEL_SBN_REPLAYAUTOACC = "ReplayAutoAcc";

		// Token: 0x0400D76B RID: 55147
		private SelectionButton_Setting m_ButtonReplayAutoAcc;

		// Token: 0x0400D76C RID: 55148
		private const string LABEL_SBN_TIMING = "Timing";

		// Token: 0x0400D76D RID: 55149
		private SelectionButton_Setting m_ButtonTiming;

		// Token: 0x0400D76E RID: 55150
		private const string LABEL_SBN_AUTORPS = "AutoRPS";

		// Token: 0x0400D76F RID: 55151
		private SelectionButton_Setting m_ButtonAutoRPS;

		// Token: 0x0400D770 RID: 55152
		private const string LABEL_SBN_EXPANSIONSUPPORT = "ExpansionSupport";

		// Token: 0x0400D771 RID: 55153
		private SelectionButton_Setting m_ButtonExpansionSupport;

		// Token: 0x0400D772 RID: 55154
		private const string LABEL_SBN_UPDATEPRERELEASE = "UpdatePrerelease";

		// Token: 0x0400D773 RID: 55155
		private SelectionButton_Setting m_ButtonUpdatePrerelease;

		// Token: 0x0400D774 RID: 55156
		private const string LABEL_SBN_DOWNLOAD_YPK = "DonwloadYPK";

		// Token: 0x0400D775 RID: 55157
		private SelectionButton_Setting m_ButtonDownloadYPK;

		// Token: 0x0400D776 RID: 55158
		private bool videoCardConfigChanged;
	}
}
