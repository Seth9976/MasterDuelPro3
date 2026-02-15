using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Enquete
{
	// Token: 0x02000C23 RID: 3107
	public class InputTextWidget : SheetContentWidget
	{
		// Token: 0x060058A6 RID: 22694 RVA: 0x000F4D13 File Offset: 0x000F2F13
		public InputTextWidget(ElementObjectManager eom, string label)
			: base(null, null)
		{
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ImportInputValues(Dictionary<string, object> importValues)
		{
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CollectInputValues(Dictionary<string, object> resultValues)
		{
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CollectSelectionItems(SheetSelectionItemMap sheetSelectionItemMap)
		{
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToValidatedText()
		{
		}

		// Token: 0x040094D4 RID: 38100
		public readonly InputFieldWidget inputField;

		// Token: 0x040094D5 RID: 38101
		private readonly MDText m_ValidatedText;
	}
}
