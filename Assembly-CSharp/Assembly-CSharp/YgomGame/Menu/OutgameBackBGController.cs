using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AB4 RID: 2740
	public class OutgameBackBGController : MonoBehaviour
	{
		// Token: 0x06004FD6 RID: 20438 RVA: 0x0000216A File Offset: 0x0000036A
		private static OutgameBackBGController GetOrCreateInstance()
		{
			return null;
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Regist(ViewController vc)
		{
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unregist(ViewController vc)
		{
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Set(ViewController vc, int bgId)
		{
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0000216D File Offset: 0x0000036D
		private void ApplyBg(ViewController vc)
		{
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0000216D File Offset: 0x0000036D
		public void RecoveryBg()
		{
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnTransitionStart(ViewController.TransitionType type, ViewController vc, ViewController preVc)
		{
		}

		// Token: 0x04008DD1 RID: 36305
		private static OutgameBackBGController k_Instance;

		// Token: 0x04008DD2 RID: 36306
		private Dictionary<ViewController, int> m_Owners;

		// Token: 0x04008DD3 RID: 36307
		private string m_RecoveryBgPath;
	}
}
