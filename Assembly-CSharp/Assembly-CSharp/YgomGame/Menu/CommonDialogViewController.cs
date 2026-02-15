using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Dialog.CommonDialog;
using YgomSystem.ElementSystem;

namespace YgomGame.Menu
{
	// Token: 0x02000A4E RID: 2638
	public class CommonDialogViewController : DialogViewControllerBase, IBokeSupported
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06004CEE RID: 19694 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool setSurfaceActiveOnInitialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool LoadPrefab()
		{
			return false;
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenConfirmationDialog(string title, string message, string buttonLabel, Action action, Dictionary<string, object> args = null, bool allowCancel = true, CommonDialogTitleWidget.IconType iconType = CommonDialogTitleWidget.IconType.None, CommonDialogButtonGroupWidget.ButtonType buttonType = CommonDialogButtonGroupWidget.ButtonType.Positive)
		{
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenConfirmationDialogScroll(string title, string message, string buttonLabel, Action action, Dictionary<string, object> args = null, bool allowCancel = true, int height = 120, CommonDialogTitleWidget.IconType iconType = CommonDialogTitleWidget.IconType.None, CommonDialogButtonGroupWidget.ButtonType buttonType = CommonDialogButtonGroupWidget.ButtonType.Positive)
		{
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenYesNoConfirmationDialogScroll(string title, string message, Action action, Action noAction = null, string yesLabel = null, string noLabel = null, bool allowCancel = true, int height = 120, Dictionary<string, object> args = null, CommonDialogTitleWidget.IconType iconType = CommonDialogTitleWidget.IconType.None, CommonDialogButtonGroupWidget.ButtonType yesButtonType = CommonDialogButtonGroupWidget.ButtonType.Positive, bool selectedNoButton = false)
		{
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenErrorDialog(string title, string message, string buttonLabel, Action action, Dictionary<string, object> args = null, bool allowCancel = true, CommonDialogTitleWidget.IconType iconType = CommonDialogTitleWidget.IconType.None, CommonDialogButtonGroupWidget.ButtonType buttonType = CommonDialogButtonGroupWidget.ButtonType.Positive)
		{
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAlertDialog(string title, string message, Action action, string buttonLabel = null, Dictionary<string, object> args = null, bool allowCancel = true, CommonDialogButtonGroupWidget.ButtonType buttonType = CommonDialogButtonGroupWidget.ButtonType.Positive)
		{
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenConfirmationPartDialog(string title, string message, string buttonLabel, Action action, Dictionary<string, object> args = null, bool allowCancel = true, CommonDialogTitleWidget.IconType iconType = CommonDialogTitleWidget.IconType.None, CommonDialogButtonGroupWidget.ButtonType buttonType = CommonDialogButtonGroupWidget.ButtonType.Positive)
		{
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenYesNoConfirmationDialog(string title, string message, Action action, Action noAction = null, Dictionary<string, object> args = null, string yesLabel = null, string noLabel = null, bool allowCancel = true, CommonDialogTitleWidget.IconType iconType = CommonDialogTitleWidget.IconType.None, CommonDialogButtonGroupWidget.ButtonType yesButtonType = CommonDialogButtonGroupWidget.ButtonType.Positive, bool selectedNoButton = false)
		{
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenNoticeYesNoDialog(string title, string message, Action action, Action noAction = null, string yesLabel = null, string noLabel = null, Dictionary<string, object> args = null, bool allowCancel = true, CommonDialogButtonGroupWidget.ButtonType yesButtonType = CommonDialogButtonGroupWidget.ButtonType.Positive, bool selectedNoButton = false)
		{
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemConfirmDialog(string title, string message, int itemId, Action action, string buttonLabel = null, string itemMessage = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemConfirmDialog(string title, string message, bool isPeriod, int itemCategory, int itemId, Action action, string buttonLabel = null, string itemMessage = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenCheckBoxDialog(string title, string message, List<EntryCheckBoxListData.EntryCheckBoxData> checkBoxList, bool isEnableMulti, Action<List<bool>> action = null, Action noAction = null, string buttonLabel = null, string noButtonLabel = null, bool interactable = false, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(IReadOnlyList<IEntryData> entryDatas, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float Progress()
		{
			return 0f;
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSendCloseByWidgets()
		{
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x04008AAD RID: 35501
		private const string k_ArgKeyEntryDatas = "entryDatas";

		// Token: 0x04008AAE RID: 35502
		public const string k_ArgKeyOpenSe = "opense";

		// Token: 0x04008AAF RID: 35503
		public const string k_ArgKeyViewStyleOverride = "viewStyleOverride";

		// Token: 0x04008AB0 RID: 35504
		public const string k_ArgKeyViewPathOverride = "viewPathOverride";

		// Token: 0x04008AB1 RID: 35505
		public const string k_ArgKeyAlignmentOptions = "AlignmentOptions";

		// Token: 0x04008AB2 RID: 35506
		private readonly string k_ELabelContent;

		// Token: 0x04008AB3 RID: 35507
		private readonly string k_ELabelBackKeyShortcutButton;

		// Token: 0x04008AB4 RID: 35508
		[SerializeField]
		private ElementObjectManager m_ItemListWidgetPref;

		// Token: 0x04008AB5 RID: 35509
		private CommonDialogContentContainerWidget m_ContentWidget;

		// Token: 0x04008AB6 RID: 35510
		private bool m_DoneSelectorLabelUnique;

		// Token: 0x04008AB7 RID: 35511
		private static GameObject prefab;
	}
}
