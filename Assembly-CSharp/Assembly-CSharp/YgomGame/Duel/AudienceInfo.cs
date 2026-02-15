using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000C86 RID: 3206
	public class AudienceInfo : MonoBehaviour
	{
		// Token: 0x06005BFA RID: 23546 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ElementObjectManager ui)
		{
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetWatcherNum(int num, bool show)
		{
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowIfExistWatcher()
		{
		}

		// Token: 0x0400971E RID: 38686
		private ElementObjectManager ui;

		// Token: 0x0400971F RID: 38687
		private ExtendedTextMeshProUGUI numText;

		// Token: 0x04009720 RID: 38688
		private int watcherNum;
	}
}
