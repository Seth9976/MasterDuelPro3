using System;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.UI;

namespace YgomGame
{
	// Token: 0x020007C8 RID: 1992
	public class FullScreenUiBg : MonoBehaviour
	{
		// Token: 0x06003E35 RID: 15925 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, UnityAction<FullScreenUiBg> onFinished)
		{
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x04003726 RID: 14118
		private const string PREFAB_PATH = "Prefabs/VC/Utility/FullScreenUIBg";

		// Token: 0x04003727 RID: 14119
		private UiSwitchTweenAnimationController m_UiSwitchTweenAnimationController;

		// Token: 0x04003728 RID: 14120
		private bool m_IsShow_Current;

		// Token: 0x04003729 RID: 14121
		private bool m_IsShow_Next;
	}
}
