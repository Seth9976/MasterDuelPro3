using System;
using UnityEngine.EventSystems;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x02000691 RID: 1681
	public class ElementWidgetUIBehaviourBase<T> : UIBehaviour where T : ElementWidgetUIBehaviourBase<T>
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x0000216A File Offset: 0x0000036A
		public ElementObjectManager eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000F2C40 File Offset: 0x000F0E40
		protected static T InnerCreate(ElementObjectManager eom)
		{
			return default(T);
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void CollectComponents()
		{
		}

		// Token: 0x04003023 RID: 12323
		private ElementObjectManager m_EomCache;
	}
}
