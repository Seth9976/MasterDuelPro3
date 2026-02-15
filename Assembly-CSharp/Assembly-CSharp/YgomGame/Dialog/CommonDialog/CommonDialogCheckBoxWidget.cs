using System;
using TMPro;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F64 RID: 3940
	public class CommonDialogCheckBoxWidget : ElementWidgetBehaviourBase<CommonDialogCheckBoxWidget>
	{
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x0600740B RID: 29707 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600740C RID: 29708 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isOn
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600740D RID: 29709 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogCheckBoxWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x0600740E RID: 29710 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x0600740F RID: 29711 RVA: 0x0000216A File Offset: 0x0000036A
		public CommonDialogCheckBoxWidget Binding(EntryCheckBoxListData.EntryCheckBoxData entryData, int index)
		{
			return null;
		}

		// Token: 0x06007410 RID: 29712 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x0400AD03 RID: 44291
		private readonly string k_ELabelText;

		// Token: 0x0400AD04 RID: 44292
		private readonly string k_ELabelToggle;

		// Token: 0x0400AD05 RID: 44293
		private int index;

		// Token: 0x0400AD06 RID: 44294
		private TMP_Text m_Text;

		// Token: 0x0400AD07 RID: 44295
		private Toggle m_Toggle;

		// Token: 0x0400AD08 RID: 44296
		private SelectionButton m_Button;

		// Token: 0x0400AD09 RID: 44297
		public Action<int, bool> OnValueChanegedCallBack;
	}
}
