using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000545 RID: 1349
	public class SingleFrameBoolean : MonoBehaviour
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x0000216D File Offset: 0x0000036D
		public bool frameValue
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0000216A File Offset: 0x0000036A
		public static SingleFrameBoolean Create(GameObject owner, bool defaultVal = false)
		{
			return null;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x04002A10 RID: 10768
		private bool m_DefaultVal;

		// Token: 0x04002A11 RID: 10769
		private bool m_CurrentVal;
	}
}
