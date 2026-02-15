using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005C0 RID: 1472
	public abstract class ScrollContentPlaceholder<Content> where Content : class
	{
		// Token: 0x06002E50 RID: 11856 RVA: 0x00002739 File Offset: 0x00000939
		protected ScrollContentPlaceholder(Transform parent, RectTransform viewport)
		{
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeContainViewport(bool isContain)
		{
		}

		// Token: 0x06002E52 RID: 11858
		protected abstract Content CreateContent();

		// Token: 0x06002E53 RID: 11859
		protected abstract void UpdateContent(Content content);

		// Token: 0x06002E54 RID: 11860 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnReleaseContent(Content content)
		{
		}

		// Token: 0x04002C02 RID: 11266
		protected RectTransform m_RectTransform;

		// Token: 0x04002C03 RID: 11267
		private Content m_Content;
	}
}
