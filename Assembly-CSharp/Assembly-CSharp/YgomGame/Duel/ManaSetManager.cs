using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000ECC RID: 3788
	public class ManaSetManager : MonoBehaviour
	{
		// Token: 0x06006E6C RID: 28268 RVA: 0x0000216A File Offset: 0x0000036A
		public static ManaSetManager Create(Transform parent, Action<ManaSetManager> initializedCallback)
		{
			return null;
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(Action onFinished)
		{
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x0000216A File Offset: 0x0000036A
		private ManaSet CreateManaSet()
		{
			return null;
		}

		// Token: 0x06006E6F RID: 28271 RVA: 0x0000216A File Offset: 0x0000036A
		public ManaSet GetManaSet()
		{
			return null;
		}

		// Token: 0x06006E70 RID: 28272 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowAll()
		{
		}

		// Token: 0x06006E71 RID: 28273 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideAll()
		{
		}

		// Token: 0x0400A959 RID: 43353
		private ManaSet prefab;

		// Token: 0x0400A95A RID: 43354
		private const string prefabPath = "Prefabs/Duel/ManaSet";
	}
}
