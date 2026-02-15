using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x0200068E RID: 1678
	public class ElementWidgetBehaviourBase<T> : MonoBehaviour where T : ElementWidgetBehaviourBase<T>
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x0000216A File Offset: 0x0000036A
		public ElementObjectManager eom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000F2BF0 File Offset: 0x000F0DF0
		protected static T InnerCreate(ElementObjectManager eom)
		{
			return default(T);
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void CollectComponents()
		{
		}

		// Token: 0x0400301E RID: 12318
		private ElementObjectManager m_EomCache;
	}
}
