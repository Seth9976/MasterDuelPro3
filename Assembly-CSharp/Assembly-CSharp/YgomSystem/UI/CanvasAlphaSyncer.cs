using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YgomSystem.UI
{
	// Token: 0x02000579 RID: 1401
	public class CanvasAlphaSyncer : UIBehaviour
	{
		// Token: 0x06002C8A RID: 11402 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransformParentChanged()
		{
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDirtyAlpha()
		{
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDirtyTransformTree()
		{
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0000216D File Offset: 0x0000036D
		public void Apply()
		{
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x0000216D File Offset: 0x0000036D
		private void CollectCanvasGroups()
		{
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float CalcSourceAlpha()
		{
			return 0f;
		}

		// Token: 0x04002AC4 RID: 10948
		[SerializeField]
		private List<CanvasAlphaSyncer.TargetData> m_Targets;

		// Token: 0x04002AC5 RID: 10949
		private float m_LastAlpha;

		// Token: 0x04002AC6 RID: 10950
		private bool m_CanvasGroupDirty;

		// Token: 0x04002AC7 RID: 10951
		private List<CanvasGroup> m_CanvasGroups;

		// Token: 0x0200057A RID: 1402
		[Serializable]
		private class TargetData
		{
			// Token: 0x04002AC8 RID: 10952
			public Renderer renderer;

			// Token: 0x04002AC9 RID: 10953
			public string alphaParamName;
		}
	}
}
