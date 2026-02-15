using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F63 RID: 3939
	public class CommonDialogCheckBoxGroupWidget : ContentWidgetBase<CommonDialogCheckBoxGroupWidget, EntryCheckBoxListData>
	{
		// Token: 0x06007403 RID: 29699 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogCheckBoxGroupWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x0000216A File Offset: 0x0000036A
		public CommonDialogCheckBoxWidget GetCheckBoxWidget(int idx)
		{
			return null;
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryCheckBoxListData entryData)
		{
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCheckBoxes(int index, bool isOn)
		{
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCompleteEvent()
		{
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x0000216A File Offset: 0x0000036A
		public List<bool> GetCheckValues()
		{
			return null;
		}

		// Token: 0x0400ACFE RID: 44286
		private readonly string k_ELabelTemplateCheckBox;

		// Token: 0x0400ACFF RID: 44287
		private bool m_IsEnableMulti;

		// Token: 0x0400AD00 RID: 44288
		private ElementObjectManager m_TemplateCheckBoxEom;

		// Token: 0x0400AD01 RID: 44289
		private List<CommonDialogCheckBoxWidget> m_CheckBoxes;

		// Token: 0x0400AD02 RID: 44290
		private Action<List<bool>> OnCompleteCallback;
	}
}
