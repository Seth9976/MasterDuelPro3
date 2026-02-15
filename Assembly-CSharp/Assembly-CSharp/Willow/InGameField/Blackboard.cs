using System;
using System.Collections.Generic;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x0200155C RID: 5468
	public class Blackboard : ScriptableObject
	{
		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x06009EC1 RID: 40641 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<int, BaseFieldEvent> fieldEventMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06009EC2 RID: 40642 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEventFromRecipe(EventRecipe eventRecipe)
		{
		}

		// Token: 0x06009EC3 RID: 40643 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEvent(BaseFieldEvent fieldEvent, EventScope eventScope = EventScope.Global)
		{
		}

		// Token: 0x06009EC4 RID: 40644 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool HasChildEvent(BaseFieldEvent targetEvent)
		{
			return false;
		}

		// Token: 0x06009EC5 RID: 40645 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool HasChildEvent(string targetEventName)
		{
			return false;
		}

		// Token: 0x06009EC6 RID: 40646 RVA: 0x0019B600 File Offset: 0x00199800
		public T GetChildEvent<T>(BaseFieldEvent targetEvent) where T : BaseFieldEvent
		{
			return default(T);
		}

		// Token: 0x06009EC7 RID: 40647 RVA: 0x0000216A File Offset: 0x0000036A
		public BaseFieldEvent GetChildEvent(BaseFieldEvent targetEvent)
		{
			return null;
		}

		// Token: 0x06009EC8 RID: 40648 RVA: 0x0019B618 File Offset: 0x00199818
		public T GetChildEvent<T>(string targetEventName) where T : BaseFieldEvent
		{
			return default(T);
		}

		// Token: 0x06009EC9 RID: 40649 RVA: 0x0000216A File Offset: 0x0000036A
		public BaseFieldEvent GetChildEvent(string targetEventName)
		{
			return null;
		}

		// Token: 0x0400DE2C RID: 56876
		private Dictionary<int, BaseFieldEvent> m_fieldEventMap;
	}
}
