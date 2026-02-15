using System;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004C3 RID: 1219
	public class SafeAreaScreen : MonoBehaviour
	{
		// Token: 0x06002736 RID: 10038 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSafeArea()
		{
		}

		// Token: 0x04002812 RID: 10258
		[SerializeField]
		private SafeAreaScreen.Mode verticalMode;

		// Token: 0x04002813 RID: 10259
		[SerializeField]
		private SafeAreaScreen.Mode horizontalMode;

		// Token: 0x04002814 RID: 10260
		[SerializeField]
		private bool fillRectTransformOnApplySafeArea;

		// Token: 0x020004C4 RID: 1220
		public enum Mode
		{
			// Token: 0x04002816 RID: 10262
			None,
			// Token: 0x04002817 RID: 10263
			SafeArea,
			// Token: 0x04002818 RID: 10264
			MinOnly,
			// Token: 0x04002819 RID: 10265
			MaxOnly,
			// Token: 0x0400281A RID: 10266
			MinOutside,
			// Token: 0x0400281B RID: 10267
			MaxOutside
		}
	}
}
