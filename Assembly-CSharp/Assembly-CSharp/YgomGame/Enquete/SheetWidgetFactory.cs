using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Enquete
{
	// Token: 0x02000C34 RID: 3124
	public class SheetWidgetFactory
	{
		// Token: 0x06005905 RID: 22789 RVA: 0x00002739 File Offset: 0x00000939
		public SheetWidgetFactory(Transform owner)
		{
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x0000216D File Offset: 0x0000036D
		public void AssignTemplate(SheetContentType sheetContentType, ElementObjectManager template)
		{
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x0000216A File Offset: 0x0000036A
		public ISheetContentWidget Create(ISheetContentContext sheetContentContext, Transform parent)
		{
			return null;
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x0000216A File Offset: 0x0000036A
		private ISheetContentWidget SetGroupWidget(ElementObjectManager eom, SheetContentSpacerContext context)
		{
			return null;
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0000216A File Offset: 0x0000036A
		private ISheetContentWidget SetGroupWidget(ElementObjectManager eom, SheetContentGroupContext context)
		{
			return null;
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x0000216A File Offset: 0x0000036A
		private ISheetContentWidget SetCaptionWidget(ElementObjectManager eom, SheetContentCaptionContext context)
		{
			return null;
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x0000216A File Offset: 0x0000036A
		private ISheetContentWidget SetTextWidget(ElementObjectManager eom, SheetContentTextContext context)
		{
			return null;
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x0000216A File Offset: 0x0000036A
		private ISheetContentWidget SetInputAmountWidget(ElementObjectManager eom, SheetContentInputAmountContext context)
		{
			return null;
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x0000216A File Offset: 0x0000036A
		private ISheetContentWidget SetInputCheckBoxWidget(ElementObjectManager eom, SheetContentInputCheckBoxContext context)
		{
			return null;
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x0000216A File Offset: 0x0000036A
		private ISheetContentWidget SetInputTextWidget(ElementObjectManager eom, SheetContentInputTextContext context)
		{
			return null;
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x0000216A File Offset: 0x0000036A
		private ISheetContentWidget SetInputTextConfirmWidget(ElementObjectManager eom, SheetContentInputTextConfirmContext context)
		{
			return null;
		}

		// Token: 0x040094FB RID: 38139
		private readonly Transform m_Owner;

		// Token: 0x040094FC RID: 38140
		private readonly Dictionary<SheetContentType, ElementObjectManager> m_WidgetTemplates;
	}
}
