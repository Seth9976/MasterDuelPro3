using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI
{
	// Token: 0x020005C3 RID: 1475
	public class ScrollRectPageSnapButtons : MonoBehaviour
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton backButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton nextButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0000216D File Offset: 0x0000036D
		private void Refresh()
		{
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPageChanged()
		{
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBack()
		{
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickNext()
		{
		}

		// Token: 0x04002C1D RID: 11293
		[SerializeField]
		public ElementObjectManager eom;

		// Token: 0x04002C1E RID: 11294
		[SerializeField]
		public string backButtonLabel;

		// Token: 0x04002C1F RID: 11295
		[SerializeField]
		public string nextButtonLabel;

		// Token: 0x04002C20 RID: 11296
		[SerializeField]
		public bool hideOnEmpty;

		// Token: 0x04002C21 RID: 11297
		private ScrollRectPageSnap m_PageSnap;
	}
}
