using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YgomSystem.UI
{
	// Token: 0x020005AB RID: 1451
	public class ParticleAlphaGroupTarget : UIBehaviour
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x0000216A File Offset: 0x0000036A
		private Renderer target
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransformParentChanged()
		{
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetGroupDirty()
		{
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnChangeGroupAlpha(float alpha)
		{
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnDestroy()
		{
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float CollectCanvasGroupAlpha()
		{
			return 0f;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTargetAlpha(float alpha)
		{
		}

		// Token: 0x04002BA2 RID: 11170
		[SerializeField]
		private string m_AlphaParamName;

		// Token: 0x04002BA3 RID: 11171
		private ParticleAlphaGroup m_Group;

		// Token: 0x04002BA4 RID: 11172
		private Renderer m_TargetCache;
	}
}
