using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A50 RID: 2640
	public class ContentBlurOverlay : MonoBehaviour
	{
		// Token: 0x06004D0C RID: 19724 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isAssigned(GameObject contentView)
		{
			return false;
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Assign(GameObject contentView)
		{
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Remove(GameObject contentView)
		{
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckContent()
		{
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayShow()
		{
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayHide()
		{
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickClose()
		{
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayShowBegin()
		{
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnPlayHideFinish()
		{
		}

		// Token: 0x04008ABF RID: 35519
		[SerializeField]
		private string m_TweenShow;

		// Token: 0x04008AC0 RID: 35520
		[SerializeField]
		private string m_TweenHide;

		// Token: 0x04008AC1 RID: 35521
		private static ContentBlurOverlay s_Instance;

		// Token: 0x04008AC2 RID: 35522
		private readonly List<GameObject> m_ContentViews;
	}
}
