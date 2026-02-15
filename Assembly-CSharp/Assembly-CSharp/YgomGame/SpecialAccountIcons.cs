using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame
{
	// Token: 0x020007DD RID: 2013
	public class SpecialAccountIcons : MonoBehaviour
	{
		// Token: 0x06003EA4 RID: 16036 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetPlayeridByTeam(SpecialAccountIcons.TEAMTYPE team)
		{
			return 0;
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSpIconByPlayerid(int playerid)
		{
		}

		// Token: 0x0400379C RID: 14236
		[SerializeField]
		private SpecialAccountIcons.TEAMTYPE teamtype;

		// Token: 0x0400379D RID: 14237
		private ElementObjectManager m_Eom;

		// Token: 0x020007DE RID: 2014
		public enum TEAMTYPE
		{
			// Token: 0x0400379F RID: 14239
			Team0,
			// Token: 0x040037A0 RID: 14240
			Team1
		}
	}
}
