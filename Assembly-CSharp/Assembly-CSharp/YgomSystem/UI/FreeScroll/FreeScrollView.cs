using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI.FreeScroll
{
	// Token: 0x0200068A RID: 1674
	public class FreeScrollView : MonoBehaviour
	{
		// Token: 0x06003494 RID: 13460 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddSelections(IReadOnlyList<SelectionItem> selections)
		{
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetAnalogSelectTime()
		{
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x0000216D File Offset: 0x0000036D
		public void TrySelectDefault(bool initializeSelection)
		{
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x0000216D File Offset: 0x0000036D
		private void ScrollMovement(Vector2 dir, bool byAnalog = false, bool initializeSelection = false)
		{
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScroll(Vector2 vec)
		{
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalog(Vector2 vec, bool isMain)
		{
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputUp()
		{
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputDown()
		{
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputLeft()
		{
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputRight()
		{
		}

		// Token: 0x04002FFF RID: 12287
		private Canvas m_ScrollRectCanvas;

		// Token: 0x04003000 RID: 12288
		private SelectionItem m_ScrollRectSelectionItem;

		// Token: 0x04003001 RID: 12289
		private ScrollRect m_ScrollRect;

		// Token: 0x04003002 RID: 12290
		private List<SelectionItem> m_ContainSelections;

		// Token: 0x04003003 RID: 12291
		private List<SelectionItem> m_InnerViewPortSelections;

		// Token: 0x04003004 RID: 12292
		private List<SelectionItem> m_SortTargetSelections;

		// Token: 0x04003005 RID: 12293
		private Dictionary<SelectionItem, float> m_SortAmounts;

		// Token: 0x04003006 RID: 12294
		private bool m_DirtyViewPort;

		// Token: 0x04003007 RID: 12295
		private Vector2 m_LastInputAnalogMain;

		// Token: 0x04003008 RID: 12296
		private Vector2 m_LastInputAnalogSub;

		// Token: 0x04003009 RID: 12297
		private float m_LastSelectByAnalogTime;

		// Token: 0x0400300A RID: 12298
		private float m_RepeatSelectByAnalogCnt;
	}
}
