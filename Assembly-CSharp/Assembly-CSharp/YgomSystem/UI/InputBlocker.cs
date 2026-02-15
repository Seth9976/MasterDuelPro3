using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x020005A0 RID: 1440
	public class InputBlocker : MonoBehaviour
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isBlocked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0000216D File Offset: 0x0000036D
		private void Clear()
		{
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reboot()
		{
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBlockEnabled(bool enabled)
		{
		}

		// Token: 0x04002B72 RID: 11122
		[SerializeField]
		private Graphic m_GameMaxRaycastBlocker;

		// Token: 0x04002B73 RID: 11123
		private int m_BlockCounter;
	}
}
