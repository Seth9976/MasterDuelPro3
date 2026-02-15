using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D0C RID: 3340
	public class CardSelectionListGroupLabel : MonoBehaviour
	{
		// Token: 0x06006032 RID: 24626 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabel(int player, CardSelectionList.CardLocateType locate)
		{
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdatePosition(Vector2 position)
		{
		}

		// Token: 0x04009B5F RID: 39775
		[SerializeField]
		private Color m_ColorMyself;

		// Token: 0x04009B60 RID: 39776
		[SerializeField]
		private Color m_ColorRIval;

		// Token: 0x04009B61 RID: 39777
		[SerializeField]
		private Color m_ColorDefault;
	}
}
