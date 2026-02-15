using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CCC RID: 3276
	public class CardIndividualSetting : ScriptableObject
	{
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06005D39 RID: 23865 RVA: 0x0000216A File Offset: 0x0000036A
		protected static CardIndividualSetting instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadSetting(Action<CardIndividualSetting> onFinished)
		{
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadSetting()
		{
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x000029CC File Offset: 0x00000BCC
		public static EffectTaskCardMove.LandingType GetMonsterPower(int cardid)
		{
			return EffectTaskCardMove.LandingType.NORMAL;
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMonsterCutin(int cardID)
		{
			return false;
		}

		// Token: 0x040098A4 RID: 39076
		private static CardIndividualSetting m_Instance;

		// Token: 0x040098A5 RID: 39077
		private const string assetPath = "Duel/ScriptableObject/CardIndividualData";

		// Token: 0x040098A6 RID: 39078
		public List<CardIndividualSetting.MrkPowerTable> mrkPowerTable;

		// Token: 0x040098A7 RID: 39079
		public List<CardIndividualSetting.MonsterCutinTable> monsterCutinTable;

		// Token: 0x02000CCD RID: 3277
		[Serializable]
		public class MrkPowerTable
		{
			// Token: 0x040098A8 RID: 39080
			public int mrk;

			// Token: 0x040098A9 RID: 39081
			public EffectTaskCardMove.LandingType power;
		}

		// Token: 0x02000CCE RID: 3278
		[Serializable]
		public class MonsterCutinTable
		{
			// Token: 0x040098AA RID: 39082
			public int mrk;
		}
	}
}
