using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D5C RID: 3420
	public class DuelCountDown : MonoBehaviour
	{
		// Token: 0x06006378 RID: 25464 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent)
		{
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddCountDownCmd(int number)
		{
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowCountDownImpl(int number)
		{
		}

		// Token: 0x04009D93 RID: 40339
		private static DuelCountDown m_Instance;

		// Token: 0x04009D94 RID: 40340
		private static Queue<int> m_TaskQueue;

		// Token: 0x04009D95 RID: 40341
		[SerializeField]
		private int m_FontSize_Text;

		// Token: 0x04009D96 RID: 40342
		[SerializeField]
		private int m_FontSize_Number;

		// Token: 0x04009D97 RID: 40343
		[SerializeField]
		private Color m_FontColor;

		// Token: 0x04009D98 RID: 40344
		[SerializeField]
		private int m_CountDownStartLine;

		// Token: 0x04009D99 RID: 40345
		[SerializeField]
		private int[] m_CountDownShowUpTime;

		// Token: 0x04009D9A RID: 40346
		private ElementObjectManager m_Eom;

		// Token: 0x04009D9B RID: 40347
		private ExtendedTextMeshProUGUI m_CountDownText;

		// Token: 0x04009D9C RID: 40348
		private string m_Str_RestTime;

		// Token: 0x04009D9D RID: 40349
		private string m_Str_Seconds;
	}
}
