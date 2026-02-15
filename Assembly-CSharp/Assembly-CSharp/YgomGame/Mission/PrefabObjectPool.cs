using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Mission
{
	// Token: 0x02000A41 RID: 2625
	public class PrefabObjectPool
	{
		// Token: 0x06004C7F RID: 19583 RVA: 0x0000216A File Offset: 0x0000036A
		private Stack<GameObject> GetReusableStack(GameObject pref)
		{
			return null;
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x00002739 File Offset: 0x00000939
		public PrefabObjectPool(Transform root)
		{
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject Create(GameObject pref, Transform owner)
		{
			return null;
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reserve(GameObject pref, int count = 1)
		{
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject Rent(GameObject pref, GameObject owner)
		{
			return null;
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x0000216D File Offset: 0x0000036D
		public void Return(GameObject pref, GameObject obj)
		{
		}

		// Token: 0x04008A52 RID: 35410
		private readonly Transform m_Root;

		// Token: 0x04008A53 RID: 35411
		private readonly Dictionary<GameObject, Stack<GameObject>> m_ReusableStackMap;

		// Token: 0x04008A54 RID: 35412
		public Dictionary<GameObject, Action<GameObject>> onCreatedCallbackMap;
	}
}
