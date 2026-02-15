using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C55 RID: 3157
	[Serializable]
	public class DuelLiveProductGroupTreeData<T> : DuelLiveProductGroupData where T : class, IDuelLiveProductGruopData
	{
		// Token: 0x06005A1C RID: 23068 RVA: 0x0000216A File Offset: 0x0000036A
		public T[] GetChildren()
		{
			return null;
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x000F4DA0 File Offset: 0x000F2FA0
		public T GetGroupData(int groupId)
		{
			return default(T);
		}

		// Token: 0x06005A1E RID: 23070 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddChildrenData(T data)
		{
		}

		// Token: 0x040095AA RID: 38314
		[SerializeField]
		private T[] m_Children;

		// Token: 0x040095AB RID: 38315
		private Dictionary<int, T> m_ChildrenMap;
	}
}
