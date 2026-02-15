using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005AA RID: 1450
	public class ParticleAlphaGroup : MonoBehaviour
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x0000216D File Offset: 0x0000036D
		public float alpha
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0000216D File Offset: 0x0000036D
		public void AssignTarget(ParticleAlphaGroupTarget target)
		{
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveTarget(ParticleAlphaGroupTarget target)
		{
		}

		// Token: 0x04002BA0 RID: 11168
		[SerializeField]
		private float m_Alpha;

		// Token: 0x04002BA1 RID: 11169
		private List<ParticleAlphaGroupTarget> m_Targets;
	}
}
