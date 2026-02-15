using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomGame.TextIDs;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Settings
{
	// Token: 0x0200097C RID: 2428
	public class SettingMenuViewController : BaseMenuViewController
	{
		// Token: 0x06004729 RID: 18217 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetHomeArgs(int selectorGroupPriority)
		{
			return null;
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTitleArgs()
		{
			return null;
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelPlayerArgs(int selectorGroupPriority, Action onClosed, Action onSurrenderedCallback, Action<SettingsUtil.DuelParam.MANUAL_TYPE> onChangeConfirm, Action<bool> onChangeShowAudience, Action<bool> onChangeShowSetCard, Action<bool> onChangeShowBattleStep, string surrenderButtonText, string surrenderDialogText, bool isDuelTutorial, bool showHelpButton, Action onHelp, bool showRetryButton = false, Action onRetry = null)
		{
			return null;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelAudienceArgs(int selectorGroupPriority, Action onClosed, Action onSurrenderedCallback, Action<bool> onChangeShowAudience, Action<bool> onChangeShowSetCard, Action<bool> onChangeShowBattleStep, string surrenderButtonText, string surrenderDialogText)
		{
			return null;
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x0000216D File Offset: 0x0000036D
		private void GetArgs()
		{
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupUI()
		{
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ShowLangChangedDialog(string changeAlartText, string okText)
		{
			return null;
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupMode(SettingMenuViewController.Mode mode)
		{
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x0000216A File Offset: 0x0000036A
		private ElementObjectManager GetMenu(SettingMenuViewController.Menu menu)
		{
			return null;
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadIDS()
		{
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadIDS()
		{
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowPageMenu(SettingMenuViewController.Page page)
		{
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCancel(bool pageBack)
		{
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x0000216D File Offset: 0x0000036D
		private void Close(bool playSE = true)
		{
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSurrender()
		{
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnHelp()
		{
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnRetry()
		{
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBGM(float volume)
		{
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSE(float volume)
		{
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetQuality(SettingsUtil.BasicParam.QUALITY quality)
		{
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPower(float volume)
		{
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetResolution(int index)
		{
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDisplayMode(bool isFullScreen)
		{
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardTextSize(SettingsUtil.BasicParam.CARD_TEXT_SIZE type)
		{
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetVibration(bool active)
		{
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowOfficialIcon()
		{
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCrossPlay()
		{
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSelfChain(SettingsUtil.DuelParam.CHAIN_TYPE type)
		{
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetManualConfirm(SettingsUtil.DuelParam.MANUAL_TYPE type)
		{
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLocateType(SettingsUtil.DuelParam.LOCATE_TYPE type)
		{
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDecideActivateOrder(bool active)
		{
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowCardInfoType(int type)
		{
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowSetCard(bool active)
		{
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowAudienceInfo(bool active)
		{
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowBattleStep(bool active)
		{
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCommandType(SettingsUtil.DuelParam.COMMAND_TYPE type)
		{
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSkipSummonEffectType(SettingsUtil.DuelParam.SKIP_TYPE type)
		{
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSkipMonsterCutinType(SettingsUtil.DuelParam.SKIP_TYPE type)
		{
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSkipCardRunEffectType(SettingsUtil.DuelParam.SKIP_TYPE type)
		{
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetUseConsoleLayout(bool active)
		{
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCameraType(SettingsUtil.DuelParam.CAMERA_TYPE type)
		{
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowRivalName(int type)
		{
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowCardReport(int type)
		{
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowHappenedEffectType(int type)
		{
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceShowCardInfoType(int type)
		{
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceShowSetCard(bool active)
		{
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceShowAudienceInfo(bool active)
		{
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceShowBattleStep(bool active)
		{
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceSkipSummonEffectType(SettingsUtil.AudienceParam.SKIP_TYPE type)
		{
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceSkipMonsterCutinType(SettingsUtil.AudienceParam.SKIP_TYPE type)
		{
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceSkipCardRunEffectType(SettingsUtil.AudienceParam.SKIP_TYPE type)
		{
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceUseConsoleLayout(bool active)
		{
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceCameraType(SettingsUtil.AudienceParam.CAMERA_TYPE type)
		{
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceShowRivalName(int type)
		{
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceShowCardReport(int type)
		{
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x0000216D File Offset: 0x0000036D
		private void Save()
		{
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ScrollToSelectingItem(RectTransform itemRect, ScrollRect scroll, LayoutGroup layout)
		{
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAudienceShowHappenedEffectType(int type)
		{
		}

		// Token: 0x0400854E RID: 34126
		private Dictionary<SettingMenuViewController.Page, SettingMenuViewController.PageInfo> pageInfo;

		// Token: 0x0400854F RID: 34127
		private SettingMenuViewController.Page currentPage;

		// Token: 0x04008550 RID: 34128
		private bool isDirty;

		// Token: 0x04008551 RID: 34129
		private SettingsUtil.DuelParam duelParam;

		// Token: 0x04008552 RID: 34130
		private SettingsUtil.BasicParam basicParam;

		// Token: 0x04008553 RID: 34131
		private SettingsUtil.SoundParam soundParam;

		// Token: 0x04008554 RID: 34132
		private SettingsUtil.SystemParam systemParam;

		// Token: 0x04008555 RID: 34133
		private SettingsUtil.AudienceParam audienceParam;

		// Token: 0x04008556 RID: 34134
		private SettingMenuViewController.Mode mode;

		// Token: 0x04008557 RID: 34135
		private int selectorGroupPriority;

		// Token: 0x04008558 RID: 34136
		private Action onSurrendered;

		// Token: 0x04008559 RID: 34137
		private Action<SettingsUtil.DuelParam.MANUAL_TYPE> onChangeConfirm;

		// Token: 0x0400855A RID: 34138
		private Action<bool> onChangeShowAudience;

		// Token: 0x0400855B RID: 34139
		private Action<bool> onChangeShowBattleStep;

		// Token: 0x0400855C RID: 34140
		private Action<bool> onChangeShowSetCard;

		// Token: 0x0400855D RID: 34141
		private Action onClosed;

		// Token: 0x0400855E RID: 34142
		private bool showSurrenderButton;

		// Token: 0x0400855F RID: 34143
		private string surrenderButtonText;

		// Token: 0x04008560 RID: 34144
		private string surrenderDialogText;

		// Token: 0x04008561 RID: 34145
		private bool isDuelTutorial;

		// Token: 0x04008562 RID: 34146
		private bool showHelpButton;

		// Token: 0x04008563 RID: 34147
		private Action onHelp;

		// Token: 0x04008564 RID: 34148
		private bool showRetryButton;

		// Token: 0x04008565 RID: 34149
		private Action onRetry;

		// Token: 0x04008566 RID: 34150
		public const string keyMode = "Mode";

		// Token: 0x04008567 RID: 34151
		public const string keySelectorGroupPriority = "SelectorGroupPriority";

		// Token: 0x04008568 RID: 34152
		public const string keyOnSurrenderedCallback = "OnSurrenderedCallback";

		// Token: 0x04008569 RID: 34153
		public const string keyOnChangeConfirmCallback = "OnChangeConfirmCallback";

		// Token: 0x0400856A RID: 34154
		public const string keyOnChangeShowAudienceCallback = "OnChangeShowAudienceCallback";

		// Token: 0x0400856B RID: 34155
		public const string keyOnChangeShowBattleStepCallback = "OnChangeShowBattleStepCallback";

		// Token: 0x0400856C RID: 34156
		public const string keyOnChangeShowSetCardCallback = "OnChangeShowSetCardCallback";

		// Token: 0x0400856D RID: 34157
		public const string keyOnClosedCallback = "OnClosedCallback";

		// Token: 0x0400856E RID: 34158
		public const string keyShowSurrenderButton = "ShowSurrenderButton";

		// Token: 0x0400856F RID: 34159
		public const string keySurrenderButtonText = "SurrenderButtonText";

		// Token: 0x04008570 RID: 34160
		public const string keySurrenderDialogText = "SurrenderDialogText";

		// Token: 0x04008571 RID: 34161
		public const string keyIsDuelTutorial = "IsDuelTutorial";

		// Token: 0x04008572 RID: 34162
		public const string keyShowHelpButton = "ShowHelpButton";

		// Token: 0x04008573 RID: 34163
		public const string keyOnHelpCallback = "OnHelpCallback";

		// Token: 0x04008574 RID: 34164
		public const string keyShowRetryButton = "ShowRetryButton";

		// Token: 0x04008575 RID: 34165
		public const string keyOnRetryCallback = "OnRetryCallback";

		// Token: 0x04008576 RID: 34166
		public const string keyViewStyleOVerride = "viewStyleOverride";

		// Token: 0x0200097D RID: 2429
		private enum Mode
		{
			// Token: 0x04008578 RID: 34168
			Home,
			// Token: 0x04008579 RID: 34169
			DuelPlayer,
			// Token: 0x0400857A RID: 34170
			DuelAudience,
			// Token: 0x0400857B RID: 34171
			Title
		}

		// Token: 0x0200097E RID: 2430
		public enum Page
		{
			// Token: 0x0400857D RID: 34173
			General,
			// Token: 0x0400857E RID: 34174
			Duel,
			// Token: 0x0400857F RID: 34175
			Audience,
			// Token: 0x04008580 RID: 34176
			CrossPlay,
			// Token: 0x04008581 RID: 34177
			Num
		}

		// Token: 0x0200097F RID: 2431
		private class PageInfo
		{
			// Token: 0x06004767 RID: 18279 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddMenu(SettingMenuViewController.Menu menuType, bool leftBack, ElementObjectManager template, Transform parent, IDS_PREFERENCE titleText, bool showArrow)
			{
			}

			// Token: 0x06004768 RID: 18280 RVA: 0x0000216D File Offset: 0x0000036D
			private void AddMenu(SettingMenuViewController.Menu menuType, ElementObjectManager ui, bool leftBack, IDS_PREFERENCE titleText, bool showArrow)
			{
			}

			// Token: 0x06004769 RID: 18281 RVA: 0x0000216D File Offset: 0x0000036D
			public void SelectTopOrPreSelectedMenu()
			{
			}

			// Token: 0x0600476A RID: 18282 RVA: 0x0000216D File Offset: 0x0000036D
			public void ShowPage()
			{
			}

			// Token: 0x0600476B RID: 18283 RVA: 0x0000216D File Offset: 0x0000036D
			public void HidePage()
			{
			}

			// Token: 0x0600476C RID: 18284 RVA: 0x0000216D File Offset: 0x0000036D
			public void ShowMenu(bool show)
			{
			}

			// Token: 0x04008582 RID: 34178
			public SettingMenuViewController.Page page;

			// Token: 0x04008583 RID: 34179
			public ElementObjectManager root;

			// Token: 0x04008584 RID: 34180
			public ElementObject buttonEO;

			// Token: 0x04008585 RID: 34181
			public SelectionButton button;

			// Token: 0x04008586 RID: 34182
			public GameObject on;

			// Token: 0x04008587 RID: 34183
			public GameObject off;

			// Token: 0x04008588 RID: 34184
			public GameObject menuGroup;

			// Token: 0x04008589 RID: 34185
			public ExtendedScrollRect scroll;

			// Token: 0x0400858A RID: 34186
			public LayoutGroup layout;

			// Token: 0x0400858B RID: 34187
			public Dictionary<SettingMenuViewController.Menu, ElementObjectManager> menu;

			// Token: 0x0400858C RID: 34188
			public SettingMenuViewController.Menu currentMenu;

			// Token: 0x0400858D RID: 34189
			public SelectionItem preSelectedMenuItem;
		}

		// Token: 0x02000980 RID: 2432
		public enum Menu
		{
			// Token: 0x0400858F RID: 34191
			BGM,
			// Token: 0x04008590 RID: 34192
			SE,
			// Token: 0x04008591 RID: 34193
			Quality,
			// Token: 0x04008592 RID: 34194
			Power,
			// Token: 0x04008593 RID: 34195
			Resolution,
			// Token: 0x04008594 RID: 34196
			DisplayMode,
			// Token: 0x04008595 RID: 34197
			Language,
			// Token: 0x04008596 RID: 34198
			CardTextSize,
			// Token: 0x04008597 RID: 34199
			Vibration,
			// Token: 0x04008598 RID: 34200
			ShowOfficialIcon,
			// Token: 0x04008599 RID: 34201
			SelfChain,
			// Token: 0x0400859A RID: 34202
			ActivateConfirm,
			// Token: 0x0400859B RID: 34203
			AutoLocation,
			// Token: 0x0400859C RID: 34204
			AutoActivateOrder,
			// Token: 0x0400859D RID: 34205
			AutoCardInfo,
			// Token: 0x0400859E RID: 34206
			ShowSetCard,
			// Token: 0x0400859F RID: 34207
			ShowAudience,
			// Token: 0x040085A0 RID: 34208
			ShowBattleStep,
			// Token: 0x040085A1 RID: 34209
			CommandType,
			// Token: 0x040085A2 RID: 34210
			SkipSummonEffect,
			// Token: 0x040085A3 RID: 34211
			SkipMonsterCutin,
			// Token: 0x040085A4 RID: 34212
			SkipCardRunEffect,
			// Token: 0x040085A5 RID: 34213
			UseConsoleLayout,
			// Token: 0x040085A6 RID: 34214
			CameraType,
			// Token: 0x040085A7 RID: 34215
			ShowRivalName,
			// Token: 0x040085A8 RID: 34216
			ShowCardReport,
			// Token: 0x040085A9 RID: 34217
			ShowHappenedEffect,
			// Token: 0x040085AA RID: 34218
			AudienceAutoCardInfo,
			// Token: 0x040085AB RID: 34219
			AudienceShowSetCard,
			// Token: 0x040085AC RID: 34220
			AudienceShowAudience,
			// Token: 0x040085AD RID: 34221
			AudienceShowBattleStep,
			// Token: 0x040085AE RID: 34222
			AudienceSkipSummonEffect,
			// Token: 0x040085AF RID: 34223
			AudienceSkipMonsterCutin,
			// Token: 0x040085B0 RID: 34224
			AudienceSkipCardRunEffect,
			// Token: 0x040085B1 RID: 34225
			AudienceUseConsoleLayout,
			// Token: 0x040085B2 RID: 34226
			AudienceCameraType,
			// Token: 0x040085B3 RID: 34227
			AudienceShowRivalName,
			// Token: 0x040085B4 RID: 34228
			AudienceShowCardReport,
			// Token: 0x040085B5 RID: 34229
			AudienceShowHappenedEffect,
			// Token: 0x040085B6 RID: 34230
			CrossPlay,
			// Token: 0x040085B7 RID: 34231
			None
		}
	}
}
