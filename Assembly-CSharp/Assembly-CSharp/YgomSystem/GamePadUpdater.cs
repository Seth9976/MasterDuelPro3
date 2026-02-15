using System;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem
{
	// Token: 0x020004A2 RID: 1186
	public class GamePadUpdater : MonoBehaviour
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ApplicationFocused
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterUpdateEvent(UnityAction onUpdate)
		{
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnregisterUpdateEvent(UnityAction onUpdate)
		{
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnApplicationFocus(bool focusStatus)
		{
		}

		// Token: 0x0400277C RID: 10108
		private UnityEvent m_updateEvent;

		// Token: 0x0400277D RID: 10109
		private static bool s_applicationFocused;
	}
}
