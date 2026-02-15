using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200056B RID: 1387
	public class BindingMaterial : MonoBehaviour
	{
		// Token: 0x06002C2A RID: 11306 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadMaterial()
		{
		}

		// Token: 0x04002A9E RID: 10910
		[SerializeField]
		private string materialPath;

		// Token: 0x04002A9F RID: 10911
		[HideInInspector]
		public bool IsDone;

		// Token: 0x04002AA0 RID: 10912
		[SerializeField]
		private bool immediate;

		// Token: 0x04002AA1 RID: 10913
		[SerializeField]
		private bool StartOnAwake;
	}
}
