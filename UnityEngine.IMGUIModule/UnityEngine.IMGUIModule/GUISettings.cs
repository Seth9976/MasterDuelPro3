using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x04000076 RID: 118
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x04000078 RID: 120
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x04000079 RID: 121
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x0400007A RID: 122
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
