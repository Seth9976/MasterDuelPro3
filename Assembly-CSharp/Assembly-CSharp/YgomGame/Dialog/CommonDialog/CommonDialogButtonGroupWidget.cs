using System;
using System.Runtime.CompilerServices;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F60 RID: 3936
	public class CommonDialogButtonGroupWidget : ElementWidgetBehaviourBase<CommonDialogButtonGroupWidget>
	{
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x060073F4 RID: 29684 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060073F5 RID: 29685 RVA: 0x0000216D File Offset: 0x0000036D
		public bool endDefault
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060073F6 RID: 29686 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogButtonGroupWidget Create(ElementObjectManager eom, bool endDefault)
		{
			return null;
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x0000216A File Offset: 0x0000036A
		public CommonDialogButtonWidget GetButtonWidget(CommonDialogButtonGroupWidget.ButtonType buttonType)
		{
			return null;
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSpace()
		{
		}

		// Token: 0x0400ACE8 RID: 44264
		private readonly string k_ELabelButtonPositive;

		// Token: 0x0400ACE9 RID: 44265
		private readonly string k_ELabelButtonDestructive;

		// Token: 0x0400ACEA RID: 44266
		private readonly string k_ELabelButtonDisable;

		// Token: 0x0400ACEB RID: 44267
		private readonly string k_ELabelButtonHighlight;

		// Token: 0x0400ACEC RID: 44268
		private readonly string k_ELabelSpace;

		// Token: 0x0400ACED RID: 44269
		private CommonDialogButtonWidget m_ButtonPositive;

		// Token: 0x0400ACEE RID: 44270
		private CommonDialogButtonWidget m_ButtonDestructive;

		// Token: 0x0400ACEF RID: 44271
		private CommonDialogButtonWidget m_ButtonDisable;

		// Token: 0x0400ACF0 RID: 44272
		private CommonDialogButtonWidget m_ButtonHighlight;

		// Token: 0x02000F61 RID: 3937
		public enum ButtonType
		{
			// Token: 0x0400ACF2 RID: 44274
			Positive,
			// Token: 0x0400ACF3 RID: 44275
			Destructive,
			// Token: 0x0400ACF4 RID: 44276
			Disable,
			// Token: 0x0400ACF5 RID: 44277
			Highlight
		}
	}
}
