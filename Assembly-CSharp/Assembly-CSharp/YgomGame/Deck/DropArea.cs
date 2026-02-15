using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FE6 RID: 4070
	public class DropArea : MonoBehaviour
	{
		// Token: 0x06007AB2 RID: 31410 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x0000216D File Offset: 0x0000036D
		private void Setup()
		{
		}

		// Token: 0x06007AB4 RID: 31412 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnDropAction(UnityAction callback)
		{
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDropAction()
		{
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveDropArea(bool b, bool canDrop)
		{
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsContainsPoint(Vector2 point)
		{
			return false;
		}

		// Token: 0x0400B16A RID: 45418
		private UnityAction m_OnDropAction;

		// Token: 0x0400B16B RID: 45419
		public string label;

		// Token: 0x0400B16C RID: 45420
		private RectSelectionItem item;

		// Token: 0x0400B16D RID: 45421
		private static readonly List<string> LABEL;

		// Token: 0x0400B16E RID: 45422
		private bool isDeckList;

		// Token: 0x0400B16F RID: 45423
		private bool setup;
	}
}
