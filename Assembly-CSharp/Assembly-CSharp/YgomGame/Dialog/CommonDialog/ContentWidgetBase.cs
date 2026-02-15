using System;
using UnityEngine;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F75 RID: 3957
	public abstract class ContentWidgetBase<T, ENTRY> : ElementWidgetUIBehaviourBase<T>, IContentWidget where T : ContentWidgetBase<T, ENTRY> where ENTRY : class, IEntryData
	{
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06007463 RID: 29795 RVA: 0x0000216A File Offset: 0x0000036A
		public CommonDialogContentContainerWidget parentWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007464 RID: 29796 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(CommonDialogContentContainerWidget parentWidget)
		{
		}

		// Token: 0x06007465 RID: 29797 RVA: 0x0000216A File Offset: 0x0000036A
		public IContentWidget DuplicateInstantiate()
		{
			return null;
		}

		// Token: 0x06007466 RID: 29798 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(IEntryData entryData)
		{
		}

		// Token: 0x06007467 RID: 29799
		protected abstract void InnerBinding(ENTRY entryData);

		// Token: 0x06007469 RID: 29801 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject YgomGame_002EDialog_002ECommonDialog_002EIContentWidget_002Eget_gameObject()
		{
			return null;
		}

		// Token: 0x0600746A RID: 29802 RVA: 0x0000216A File Offset: 0x0000036A
		private Transform YgomGame_002EDialog_002ECommonDialog_002EIContentWidget_002Eget_transform()
		{
			return null;
		}

		// Token: 0x0600746B RID: 29803 RVA: 0x000F65A1 File Offset: 0x000F47A1
		GameObject IContentWidget.get_gameObject()
		{
			return base.gameObject;
		}

		// Token: 0x0600746C RID: 29804 RVA: 0x000F65A9 File Offset: 0x000F47A9
		Transform IContentWidget.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400AD77 RID: 44407
		private CommonDialogContentContainerWidget m_ParentWidget;
	}
}
