using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x0200064A RID: 1610
	public class UISpriteMaskLocator : UIBehaviour
	{
		// Token: 0x06003250 RID: 12880 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnEnable()
		{
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnDisable()
		{
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshComponents()
		{
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshTransform()
		{
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x0000216D File Offset: 0x0000036D
		private void SettingSpriteMask(RectMask2D selfMask)
		{
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransformParentChanged()
		{
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnDestroy()
		{
		}

		// Token: 0x04002EEC RID: 12012
		[SerializeField]
		private SpriteMask m_SpriteMaskPref;

		// Token: 0x04002EED RID: 12013
		[SerializeField]
		private SpriteMask m_SpriteMask;

		// Token: 0x04002EEE RID: 12014
		private GameObject m_SpriteMaskRoot;

		// Token: 0x04002EEF RID: 12015
		private List<RectMask2D> m_Masks;

		// Token: 0x04002EF0 RID: 12016
		private List<ParticleSystemRenderer> m_Particles;

		// Token: 0x04002EF1 RID: 12017
		private Vector3 m_LastWorldPos;

		// Token: 0x04002EF2 RID: 12018
		private bool m_Dirty;
	}
}
