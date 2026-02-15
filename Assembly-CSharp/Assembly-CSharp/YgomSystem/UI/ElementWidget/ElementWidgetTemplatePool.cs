using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x02000690 RID: 1680
	public class ElementWidgetTemplatePool<T> : UIObjectPool<T> where T : ElementWidgetBase
	{
		// Token: 0x060034C5 RID: 13509 RVA: 0x000F2C06 File Offset: 0x000F0E06
		public ElementWidgetTemplatePool(Transform parent, ElementObjectManager template, Func<ElementObjectManager, T> factory)
		{
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000F2C28 File Offset: 0x000F0E28
		protected override T Create()
		{
			return default(T);
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnAfterCreate(T obj)
		{
		}

		// Token: 0x04003020 RID: 12320
		private Func<ElementObjectManager, T> m_Factory;

		// Token: 0x04003021 RID: 12321
		private readonly Transform m_Parent;

		// Token: 0x04003022 RID: 12322
		private readonly ElementObjectManager m_Template;
	}
}
