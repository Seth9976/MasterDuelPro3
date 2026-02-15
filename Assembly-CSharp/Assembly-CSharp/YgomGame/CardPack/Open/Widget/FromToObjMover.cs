using System;
using UnityEngine;

namespace YgomGame.CardPack.Open.Widget
{
	// Token: 0x020010C4 RID: 4292
	[ExecuteInEditMode]
	public class FromToObjMover : MonoBehaviour
	{
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06007F7E RID: 32638 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06007F7F RID: 32639 RVA: 0x0000216D File Offset: 0x0000036D
		public float normal
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007F81 RID: 32641 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateLocation()
		{
		}

		// Token: 0x0400B826 RID: 47142
		[SerializeField]
		private GameObject m_From;

		// Token: 0x0400B827 RID: 47143
		[SerializeField]
		private GameObject m_To;

		// Token: 0x0400B828 RID: 47144
		[SerializeField]
		private float m_Normal;

		// Token: 0x0400B829 RID: 47145
		private float m_LastNormal;
	}
}
