using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Utility
{
	// Token: 0x0200081F RID: 2079
	public class CommonObjectPool : MonoBehaviour
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x0000216A File Offset: 0x0000036A
		private static CommonObjectPool instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x0000216D File Offset: 0x0000036D
		private void Push(string group, string label, GameObject target, bool setInactivate)
		{
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject Pop(string group, string label, Transform parent, bool setAcvtive)
		{
			return null;
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearGroup(string group)
		{
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PushPool(string group, string label, GameObject target, bool setActive = true)
		{
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject PopPool(string group, string label, Transform parent, bool setInactive = true)
		{
			return null;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearPoolGroup(string group)
		{
		}

		// Token: 0x04003941 RID: 14657
		private static CommonObjectPool _instance;

		// Token: 0x04003942 RID: 14658
		private Dictionary<string, List<CommonObjectPool.PoolInfo>> pool;

		// Token: 0x02000820 RID: 2080
		private class PoolInfo
		{
			// Token: 0x0600402E RID: 16430 RVA: 0x00002739 File Offset: 0x00000939
			public PoolInfo(string label, GameObject gameObject)
			{
			}

			// Token: 0x04003943 RID: 14659
			public string label;

			// Token: 0x04003944 RID: 14660
			public GameObject gameObject;
		}
	}
}
