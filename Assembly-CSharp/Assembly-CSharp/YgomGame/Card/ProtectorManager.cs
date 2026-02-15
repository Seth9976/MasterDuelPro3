using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Card
{
	// Token: 0x02001128 RID: 4392
	public class ProtectorManager
	{
		// Token: 0x060082D4 RID: 33492 RVA: 0x0000216A File Offset: 0x0000036A
		public static ProtectorManager Create(bool isforui)
		{
			return null;
		}

		// Token: 0x060082D5 RID: 33493 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetProtectorMatAsync(int protectorId, UnityAction<Material, int> action)
		{
		}

		// Token: 0x060082D6 RID: 33494 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetTable()
		{
		}

		// Token: 0x060082D7 RID: 33495 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(bool isforui)
		{
		}

		// Token: 0x060082D8 RID: 33496 RVA: 0x000029CC File Offset: 0x00000BCC
		private int CheckProtectorId(int protectorId)
		{
			return 0;
		}

		// Token: 0x060082D9 RID: 33497 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetProtectorPath(int protectorId)
		{
			return null;
		}

		// Token: 0x0400BDF0 RID: 48624
		private const string PATH_PROTECORFOLD = "Protector/<_CARD_ILLUST_>";

		// Token: 0x0400BDF1 RID: 48625
		private Dictionary<string, Material> m_PidMatTable;

		// Token: 0x0400BDF2 RID: 48626
		private Dictionary<string, Queue<UnityAction<Material, int>>> m_PidTaskTable;

		// Token: 0x0400BDF3 RID: 48627
		private bool m_ForUI;
	}
}
