using System;
using System.Collections;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C5D RID: 3165
	public class DuelLiveViewController : BaseMenuViewController, IBackButtonWithoutSCSupported, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setProgressOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06005A53 RID: 23123 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setSurfaceActiveOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int menuId = 0, int sectionId = 0)
		{
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yInitialize()
		{
			return null;
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeData()
		{
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnHelpButton()
		{
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputShortcutL1()
		{
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputShortcutR1()
		{
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectCurrentItem()
		{
			return false;
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPrechangeSubTabIdx(int preSubTabIdx, int preSubTabSectionIdx, int newSubTabIdx, int newSubTabSectionIdx)
		{
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangedSubTabIdx(int idx, int sectionIdx)
		{
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSubCategory(int dataIdx)
		{
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSubCategoryGroup(int dataIdx)
		{
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSubCategorySection(int dataIdx, int sectionIdx)
		{
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnProductListScrolled(Vector2 value)
		{
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickProduct(ProductWidget productWidget)
		{
		}

		// Token: 0x040095CF RID: 38351
		private const string k_ELabelHelpButton = "ButtonHelp";

		// Token: 0x040095D0 RID: 38352
		private const string k_ELabelAnalogDirectionItem = "AnalogDirectionItem";

		// Token: 0x040095D1 RID: 38353
		private const string k_ELabelShortcutButtonBack = "ShortcutButtonBack";

		// Token: 0x040095D2 RID: 38354
		private const string k_ELabelShortcutButtonCancel = "ShortcutButtonCancel";

		// Token: 0x040095D3 RID: 38355
		private const string k_ELabelShortcutButtonL1 = "ShortcutButtonL1";

		// Token: 0x040095D4 RID: 38356
		private const string k_ELabelShortcutButtonR1 = "ShortcutButtonR1";

		// Token: 0x040095D5 RID: 38357
		public const string k_ArgsLaunchMenuID = "menuId";

		// Token: 0x040095D6 RID: 38358
		public const string k_ArgsLaunchSectionID = "sectionId";

		// Token: 0x040095D7 RID: 38359
		private DuelLiveSettings m_DuelLiveSettings;

		// Token: 0x040095D8 RID: 38360
		private DuelLiveRootWidget m_RootWidget;

		// Token: 0x040095D9 RID: 38361
		private bool m_IsStarted;

		// Token: 0x040095DA RID: 38362
		private bool m_IsHighEnd;

		// Token: 0x040095DB RID: 38363
		private int m_PreSubTabIdx;

		// Token: 0x040095DC RID: 38364
		private int m_PreSubTabSectionIdx;

		// Token: 0x040095DD RID: 38365
		private bool m_OnChangedSubTabIdxFocusBlocker;

		// Token: 0x040095DE RID: 38366
		private bool m_ProductListScrollCheckBlocker;
	}
}
