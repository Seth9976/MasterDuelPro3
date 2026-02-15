using System;
using System.Collections;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Tutorial
{
	// Token: 0x02000838 RID: 2104
	public class InitialSettingsViewController : BaseMenuViewController
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator VerifyName(string newName, Action<bool> onEnd)
		{
			return null;
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenCautionDialog()
		{
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator SelectDelayOK()
		{
			return null;
		}

		// Token: 0x040039E0 RID: 14816
		private readonly string INPUT_LABEL;

		// Token: 0x040039E1 RID: 14817
		private readonly string BTN_LABEL;

		// Token: 0x040039E2 RID: 14818
		private readonly string BTN_CAUTION_LABEL;

		// Token: 0x040039E3 RID: 14819
		private SelectionButton m_ButtonOK;

		// Token: 0x040039E4 RID: 14820
		private InputFieldWidget _inputFieldWidget;

		// Token: 0x040039E5 RID: 14821
		private string _nameCanditate;
	}
}
