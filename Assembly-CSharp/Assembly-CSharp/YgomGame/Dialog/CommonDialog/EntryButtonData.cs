using System;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F76 RID: 3958
	public class EntryButtonData : IEntryData
	{
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x0600746D RID: 29805 RVA: 0x000029CC File Offset: 0x00000BCC
		public CommonDialogDef.ContentType contentType
		{
			get
			{
				return CommonDialogDef.ContentType.Title;
			}
		}

		// Token: 0x0600746E RID: 29806 RVA: 0x00002739 File Offset: 0x00000939
		public EntryButtonData(string text = null, Action onClickCallback = null, string onClickUrlScheme = null, bool closeOnClick = true, bool interactable = true, bool isCancel = false, bool replaceCallback = false, Action<CommonDialogButtonWidget> onClickWidgetCallback = null, CommonDialogButtonGroupWidget.ButtonType buttonType = CommonDialogButtonGroupWidget.ButtonType.Positive, bool muteClickSe = false, bool isDefault = false, string goName = null)
		{
		}

		// Token: 0x0400AD78 RID: 44408
		public CommonDialogButtonGroupWidget.ButtonType buttonType;

		// Token: 0x0400AD79 RID: 44409
		public Action onClickCallback;

		// Token: 0x0400AD7A RID: 44410
		public Action<CommonDialogButtonWidget> onClickWidgetCallback;

		// Token: 0x0400AD7B RID: 44411
		public string onClickUrlScheme;

		// Token: 0x0400AD7C RID: 44412
		public bool interactable;

		// Token: 0x0400AD7D RID: 44413
		public string text;

		// Token: 0x0400AD7E RID: 44414
		public bool closeOnClick;

		// Token: 0x0400AD7F RID: 44415
		public bool isCancel;

		// Token: 0x0400AD80 RID: 44416
		public bool muteClickSe;

		// Token: 0x0400AD81 RID: 44417
		public bool isDefault;

		// Token: 0x0400AD82 RID: 44418
		public string goName;
	}
}
