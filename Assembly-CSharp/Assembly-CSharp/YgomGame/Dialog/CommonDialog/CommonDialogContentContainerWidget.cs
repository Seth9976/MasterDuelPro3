using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F65 RID: 3941
	public class CommonDialogContentContainerWidget : ElementWidgetBehaviourBase<CommonDialogContentContainerWidget>
	{
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06007412 RID: 29714 RVA: 0x0000216A File Offset: 0x0000036A
		public CommonDialogButtonWidget cancelButtonWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06007413 RID: 29715 RVA: 0x0000216A File Offset: 0x0000036A
		public CommonDialogCheckBoxGroupWidget checkBoxGroupWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06007414 RID: 29716 RVA: 0x0000216A File Offset: 0x0000036A
		public CommonDialogButtonGroupWidget buttonGroupWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06007415 RID: 29717 RVA: 0x0000216A File Offset: 0x0000036A
		public TextGroupLoadHolder textGroupLoadHolder
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06007416 RID: 29718 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007417 RID: 29719 RVA: 0x0000216D File Offset: 0x0000036D
		public ElementObjectManager itemListWidgetPref
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06007418 RID: 29720 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool directionalInputListenerExist
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007419 RID: 29721 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogContentContainerWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x0600741A RID: 29722 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600741B RID: 29723 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertEntries(IReadOnlyList<IEntryData> entryDatas, Action onComplete = null, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x0000216D File Offset: 0x0000036D
		private void InsertContentWidget(IEntryData entryData)
		{
		}

		// Token: 0x0600741D RID: 29725 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLoadingCompleteCheck()
		{
		}

		// Token: 0x0600741E RID: 29726 RVA: 0x0000216A File Offset: 0x0000036A
		public IContentWidget GetContentWidget(IEntryData entryData)
		{
			return null;
		}

		// Token: 0x0600741F RID: 29727 RVA: 0x0000216D File Offset: 0x0000036D
		public void SendMainAnalogInputToListeneres(Vector2 dir)
		{
		}

		// Token: 0x06007420 RID: 29728 RVA: 0x0000216D File Offset: 0x0000036D
		public void SendSubAnalogInputToListeneres(Vector2 dir)
		{
		}

		// Token: 0x06007421 RID: 29729 RVA: 0x0000216D File Offset: 0x0000036D
		public void SendLeftInputToListeneres()
		{
		}

		// Token: 0x06007422 RID: 29730 RVA: 0x0000216D File Offset: 0x0000036D
		public void SendRightInputToListeneres()
		{
		}

		// Token: 0x06007423 RID: 29731 RVA: 0x0000216D File Offset: 0x0000036D
		public void SendUpInputToListeneres()
		{
		}

		// Token: 0x06007424 RID: 29732 RVA: 0x0000216D File Offset: 0x0000036D
		public void SendDownInputToListeneres()
		{
		}

		// Token: 0x06007425 RID: 29733 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSendClose()
		{
		}

		// Token: 0x06007426 RID: 29734 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnBack()
		{
			return false;
		}

		// Token: 0x0400AD0A RID: 44298
		private readonly string k_ELabelTitleGrp;

		// Token: 0x0400AD0B RID: 44299
		private readonly string k_ELabelText;

		// Token: 0x0400AD0C RID: 44300
		private readonly string k_ELabelScrollText;

		// Token: 0x0400AD0D RID: 44301
		private readonly string k_ELabelImage;

		// Token: 0x0400AD0E RID: 44302
		private readonly string k_ELabelMaintenance;

		// Token: 0x0400AD0F RID: 44303
		private readonly string k_ELabelIconText;

		// Token: 0x0400AD10 RID: 44304
		private readonly string k_ELabelItemContent;

		// Token: 0x0400AD11 RID: 44305
		private readonly string k_ELabelCheckBoxGroup;

		// Token: 0x0400AD12 RID: 44306
		private readonly string k_ELabelButtonGroup;

		// Token: 0x0400AD13 RID: 44307
		private readonly string k_ELabelButtonGroupVertical;

		// Token: 0x0400AD14 RID: 44308
		private CommonDialogTitleWidget m_TitleWidget;

		// Token: 0x0400AD15 RID: 44309
		private CommonDialogTextWidget m_TextWidget;

		// Token: 0x0400AD16 RID: 44310
		private CommonDialogScrollTextWidget m_ScrollTextWidget;

		// Token: 0x0400AD17 RID: 44311
		private CommonDialogImageWidget m_ImageWidget;

		// Token: 0x0400AD18 RID: 44312
		private CommonDialogMaintenanceImageWidget m_MaintenanceWidget;

		// Token: 0x0400AD19 RID: 44313
		private CommonDialogIconTextWidget m_IconTextWidget;

		// Token: 0x0400AD1A RID: 44314
		private CommonDialogItemContentWidget m_ItemContentWidget;

		// Token: 0x0400AD1B RID: 44315
		private CommonDialogCheckBoxGroupWidget m_CheckBoxGroupWidget;

		// Token: 0x0400AD1C RID: 44316
		private CommonDialogButtonGroupWidget m_ButtonGroupWidget;

		// Token: 0x0400AD1D RID: 44317
		private CommonDialogButtonWidget m_CancelButtonWidget;

		// Token: 0x0400AD1E RID: 44318
		private ElementObjectManager m_ItemListWidgetPref;

		// Token: 0x0400AD1F RID: 44319
		private CommonDialogItemListWidget m_ItemListWidget;

		// Token: 0x0400AD20 RID: 44320
		private bool m_RebuildLayoutOnPostAllInserted;

		// Token: 0x0400AD21 RID: 44321
		private List<IContentPostAllInsertedHandler> m_PostAllInsertedHandlers;

		// Token: 0x0400AD22 RID: 44322
		private List<IContentWidgetDirectionalInputListener> m_DirectionalInputListeners;

		// Token: 0x0400AD23 RID: 44323
		private List<IContentLifecycleHandler> m_LifecycleHandlers;

		// Token: 0x0400AD24 RID: 44324
		private List<SelectionButton> m_Buttons;

		// Token: 0x0400AD25 RID: 44325
		private TextGroupLoadHolder m_TextGroupLoadHolder;

		// Token: 0x0400AD26 RID: 44326
		public Action onSendCloseCallback;

		// Token: 0x0400AD27 RID: 44327
		private CommonDialogButtonWidget defaultButton;

		// Token: 0x0400AD28 RID: 44328
		private Action m_CompleteCallback;

		// Token: 0x0400AD29 RID: 44329
		private int m_LoadingCnt;
	}
}
