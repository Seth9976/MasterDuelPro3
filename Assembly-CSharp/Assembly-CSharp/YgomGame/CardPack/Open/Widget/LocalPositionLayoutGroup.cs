using System;
using UnityEngine;

namespace YgomGame.CardPack.Open.Widget
{
	// Token: 0x020010C5 RID: 4293
	[ExecuteInEditMode]
	public class LocalPositionLayoutGroup : MonoBehaviour
	{
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06007F83 RID: 32643 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06007F84 RID: 32644 RVA: 0x0000216D File Offset: 0x0000036D
		public float xSpace
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x06007F85 RID: 32645 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateLayout()
		{
		}

		// Token: 0x0400B82A RID: 47146
		[SerializeField]
		private float m_XSpace;

		// Token: 0x0400B82B RID: 47147
		private float m_LastXSpace;
	}
}
