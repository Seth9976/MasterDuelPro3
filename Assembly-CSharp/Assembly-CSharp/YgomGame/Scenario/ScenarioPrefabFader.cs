using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009DD RID: 2525
	public class ScenarioPrefabFader : MonoBehaviour
	{
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06004977 RID: 18807 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06004978 RID: 18808 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onCompleteEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x0000216D File Offset: 0x0000036D
		public void AssignChildren(GameObject root)
		{
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assign(GameObject target)
		{
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assign(MeshRenderer meshRenderer)
		{
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assign(SpriteRenderer spriteRenderer)
		{
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assign(ParticleSystemRenderer particleRenderer)
		{
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assign(Material material)
		{
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFadeIn()
		{
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFadeOut()
		{
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x04008757 RID: 34647
		private List<Material> m_TargetMaterials;

		// Token: 0x04008758 RID: 34648
		public float duration;

		// Token: 0x04008759 RID: 34649
		private float m_PastSec;

		// Token: 0x0400875A RID: 34650
		private float m_FromAlpha;

		// Token: 0x0400875B RID: 34651
		private float m_DstAlpha;

		// Token: 0x0400875C RID: 34652
		private float m_CurrentAlpha;
	}
}
