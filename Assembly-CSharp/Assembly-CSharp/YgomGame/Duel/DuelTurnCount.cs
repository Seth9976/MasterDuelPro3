using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000DA3 RID: 3491
	public class DuelTurnCount : MonoBehaviour
	{
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060066A8 RID: 26280 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060066A9 RID: 26281 RVA: 0x0000216D File Offset: 0x0000036D
		public bool Finished
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x0000216D File Offset: 0x0000036D
		public void Set(int mrk, int num)
		{
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFinishCard()
		{
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFinishAnim()
		{
		}

		// Token: 0x0400A0BE RID: 41150
		private ElementObjectManager eom;

		// Token: 0x0400A0BF RID: 41151
		private ExtendedTextMeshProUGUI count;

		// Token: 0x0400A0C0 RID: 41152
		private RawImage cardImg;

		// Token: 0x0400A0C1 RID: 41153
		private GameObject clockObj;

		// Token: 0x0400A0C2 RID: 41154
		private int targetNum;

		// Token: 0x0400A0C3 RID: 41155
		private int cardId;
	}
}
