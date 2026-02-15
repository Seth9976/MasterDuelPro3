using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x020005AD RID: 1453
	public class PlatformPlayerNameGroup : MonoBehaviour
	{
		// Token: 0x06002DF9 RID: 11769 RVA: 0x0000216D File Offset: 0x0000036D
		public void Set(string ymdPlayerName, string platformPlayerName, long pcode, bool isRegistedPlatform)
		{
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetYmdPlayerName()
		{
			return null;
		}

		// Token: 0x04002BA9 RID: 11177
		private readonly string k_CurrentPlatformIconPath;

		// Token: 0x04002BAA RID: 11178
		[SerializeField]
		private TMP_Text m_YmdPlayerNameText;

		// Token: 0x04002BAB RID: 11179
		[SerializeField]
		private GameObject m_PlatformNameRoot;

		// Token: 0x04002BAC RID: 11180
		[SerializeField]
		private TMP_Text m_PlatformPlayerNameText;

		// Token: 0x04002BAD RID: 11181
		[SerializeField]
		private Image m_PlatformPlayerIcon;

		// Token: 0x04002BAE RID: 11182
		[SerializeField]
		private bool m_IsDispPcode;
	}
}
