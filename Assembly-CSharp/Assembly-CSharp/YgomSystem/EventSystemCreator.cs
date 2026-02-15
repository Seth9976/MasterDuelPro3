using System;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x0200049D RID: 1181
	public class EventSystemCreator : MonoBehaviour
	{
		// Token: 0x06002639 RID: 9785 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x04002741 RID: 10049
		[SerializeField]
		protected GameObject m_InputSystemUIInputModule;

		// Token: 0x04002742 RID: 10050
		[SerializeField]
		protected GameObject m_StandaloneInputModule;
	}
}
