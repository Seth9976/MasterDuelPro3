using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.TextIDs;
using YgomSystem.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000D1C RID: 3356
	public class CounterUtil
	{
		// Token: 0x0600611B RID: 24859 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(SpriteContainer counterIconContainer)
		{
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCounterName(Engine.CounterType type)
		{
			return null;
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x0000216A File Offset: 0x0000036A
		public static Sprite GetCounterIcon(Engine.CounterType type)
		{
			return null;
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Terminate()
		{
		}

		// Token: 0x04009C3C RID: 39996
		private static SpriteContainer counterIconContainer;

		// Token: 0x04009C3D RID: 39997
		private static Dictionary<Engine.CounterType, CounterUtil.CounterInfo> counterInfoList;

		// Token: 0x02000D1D RID: 3357
		private class CounterInfo
		{
			// Token: 0x06006120 RID: 24864 RVA: 0x00002739 File Offset: 0x00000939
			public CounterInfo(IDS_COUNTER nameTextID, string iconLabel)
			{
			}

			// Token: 0x04009C3E RID: 39998
			public IDS_COUNTER nameTextID;

			// Token: 0x04009C3F RID: 39999
			public string iconLabel;
		}
	}
}
